using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p2_40_Charge_Tester.Communication
{
    /// <summary>
    /// 전압/전류/전력 측정 모듈용 시리얼 채널.
    /// 연결 시에는 포트만 연다. 값이 필요할 때 GetValue 호출 시 초기화 패킷을 보내고 한 프레임만 수신하여 반환한다.
    /// </summary>
    public class PowerMeterChannelPort
    {
        public int ChannelNo { get; }
        public SerialPort Port { get; private set; }
        public bool IsOpen => Port != null && Port.IsOpen;

        public RichTextBox LogTarget { get; set; }
        public Action<RichTextBox, string, bool> LogCommToUI { get; set; }

        private readonly List<byte> _receiveBuffer = new List<byte>();
        private TaskCompletionSource<byte[]> _packetTcs;
        private readonly object _bufferLock = new object();

        private const byte Header1 = 0xA5;
        private const byte Header2Len = 0x24; // 패킷 길이 36
        private const int PacketLength = 36;

        public PowerMeterChannelPort(int ch)
        {
            ChannelNo = ch;
        }

        #region Connect / Disconnect

        public bool Connect(string portName, int baudRate = 115200)
        {
            try
            {
                if (Port != null)
                {
                    try { if (Port.IsOpen) Port.Close(); } catch { }
                    Port.DataReceived -= OnDataReceived;
                    Port.Dispose();
                    Port = null;
                }

                Port = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
                Port.ReadTimeout = 2000;
                Port.WriteTimeout = 2000;
                Port.DataReceived += OnDataReceived;
                Port.Open();

                Port.DiscardInBuffer();
                Port.DiscardOutBuffer();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PowerMeter] CH{0} 연결 실패: {1}", ChannelNo, ex.Message);
                return false;
            }
        }

        public async Task<bool> ConnectAsync(string portName, int baudRate = 115200, int timeoutMs = 3000, CancellationToken token = default)
        {
            if (Port != null && Port.IsOpen && Port.PortName == portName && Port.BaudRate == baudRate)
            {
                Console.WriteLine("[PowerMeter] CH{0} 이미 연결됨: {1}", ChannelNo, portName);
                return true;
            }

            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
            {
                if (token != default) token.ThrowIfCancellationRequested();

                try
                {
                    if (Port != null)
                    {
                        try { if (Port.IsOpen) Port.Close(); } catch { }
                        Port.DataReceived -= OnDataReceived;
                        Port.Dispose();
                        Port = null;
                    }

                    Port = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
                    Port.ReadTimeout = 2000;
                    Port.WriteTimeout = 2000;
                    Port.DataReceived += OnDataReceived;
                    Port.Open();

                    Port.DiscardInBuffer();
                    Port.DiscardOutBuffer();

                    Console.WriteLine("[PowerMeter] CH{0} 연결 성공: {1}", ChannelNo, portName);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[PowerMeter] CH{0} 연결 시도 중: {1}", ChannelNo, ex.Message);
                    await Task.Delay(500, token).ConfigureAwait(false);
                }
            }

            Console.WriteLine("[PowerMeter] CH{0} 연결 실패 (타임아웃 {1}ms)", ChannelNo, timeoutMs);
            return false;
        }

        private void SendInitCommands()
        {
            if (Port == null || !Port.IsOpen) return;

            byte[] cmdConnect = new byte[] {
                0xA5, 0x04, 0x00, 0x00, 0x00, 0x01, 0x07, 0x01, 0x00, 0x07, 0x5A
            };
            Port.Write(cmdConnect, 0, cmdConnect.Length);
            if (LogCommToUI != null) LogCommToUI(LogTarget, ToHex(cmdConnect), true);

            Thread.Sleep(100);

            byte[] cmdStartReport = new byte[] {
                0xA5, 0x08, 0x00, 0x00, 0x00, 0x01, 0x09, 0x0E, 0x00, 0x64, 0x00, 0x00, 0x00, 0x62, 0x5A
            };
            Port.Write(cmdStartReport, 0, cmdStartReport.Length);
            if (LogCommToUI != null) LogCommToUI(LogTarget, ToHex(cmdStartReport), true);
        }

        /// <summary>
        /// 측정값 전송 중지 명령을 전송한다. 계속 측정값을 보내는 상태를 멈추기 위해 호출한다.
        /// </summary>
        public void StopMeasurement()
        {
            if (Port == null || !Port.IsOpen) return;

            byte[] cmdStop = new byte[] {
                0xA5, 0x04, 0x00, 0x00, 0x00, 0x01, 0x07, 0x10, 0x00, 0x16, 0x5A
            };
            try
            {
                Port.Write(cmdStop, 0, cmdStop.Length);
                if (LogCommToUI != null) LogCommToUI(LogTarget, ToHex(cmdStop), true);
                Console.WriteLine("[PowerMeter] CH{0} 종료 명령 전송", ChannelNo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PowerMeter] CH{0} 종료 명령 전송 예외: {1}", ChannelNo, ex.Message);
            }
        }

        public void Disconnect()
        {
            try
            {
                if (Port != null)
                {
                    if (Port.IsOpen)
                    {
                        StopMeasurement();
                        Port.DataReceived -= OnDataReceived;
                        Port.Close();
                    }
                    Port.Dispose();
                    Port = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PowerMeter] CH{0} Disconnect 예외: {1}", ChannelNo, ex.Message);
            }
        }

        #endregion

        #region GetValue / GetValueRaw

        /// <summary>
        /// 그 순간의 측정 데이터 한 프레임을 수신하여 전압/전류/전력 등으로 파싱해 반환한다.
        /// 타임아웃 시 null.
        /// </summary>
        public async Task<PowerMeterValue> GetValueAsync(int timeoutMs = 2000, CancellationToken token = default)
        {
            byte[] raw = await GetValueRawAsync(timeoutMs, token).ConfigureAwait(false);
            if (raw == null || raw.Length < PacketLength) return null;
            return PowerMeterValue.FromPacket(raw);
        }

        /// <summary>
        /// 그 순간의 측정 데이터 한 프레임(36바이트)을 수신하여 바이트 배열로 반환한다.
        /// 타임아웃 시 null.
        /// </summary>
        public async Task<byte[]> GetValueRawAsync(int timeoutMs = 2000, CancellationToken token = default)
        {
            if (!IsOpen) return null;

            lock (_bufferLock)
            {
                _receiveBuffer.Clear();
                if (Port != null)
                {
                    try { Port.DiscardInBuffer(); } catch { }
                }
                _packetTcs = new TaskCompletionSource<byte[]>();
            }

            SendInitCommands();

            Task timeoutTask = Task.Delay(timeoutMs, token);
            Task completed = await Task.WhenAny(_packetTcs.Task, timeoutTask).ConfigureAwait(false);

            if (completed == timeoutTask)
            {
                _packetTcs.TrySetCanceled();
                Console.WriteLine("[PowerMeter] CH{0} GetValue 타임아웃({1}ms)", ChannelNo, timeoutMs);
                StopMeasurement();
                return null;
            }

            if (_packetTcs.Task.IsCompleted && !_packetTcs.Task.IsFaulted)
            {
                byte[] result = await _packetTcs.Task.ConfigureAwait(false);
                StopMeasurement();
                return result;
            }
            StopMeasurement();
            return null;
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (_bufferLock)
                {
                    if (Port == null || !Port.IsOpen) return;
                    int bytesToRead = Port.BytesToRead;
                    if (bytesToRead <= 0) return;

                    byte[] temp = new byte[bytesToRead];
                    int read = Port.Read(temp, 0, bytesToRead);
                    if (read > 0)
                    {
                        for (int i = 0; i < read; i++)
                            _receiveBuffer.Add(temp[i]);
                    }
                    TryExtractOnePacket();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PowerMeter] CH{0} DataReceived 예외: {1}", ChannelNo, ex.Message);
            }
        }

        private void TryExtractOnePacket()
        {
            if (_packetTcs == null || _packetTcs.Task.IsCompleted) return;

            while (_receiveBuffer.Count >= PacketLength)
            {
                if (_receiveBuffer[0] == Header1 && _receiveBuffer[1] == Header2Len)
                {
                    byte[] packet = _receiveBuffer.Take(PacketLength).ToArray();
                    _receiveBuffer.RemoveRange(0, PacketLength);

                    if (LogCommToUI != null) LogCommToUI(LogTarget, ToHex(packet), false);
                    _packetTcs?.TrySetResult(packet);
                    return;
                }
                _receiveBuffer.RemoveAt(0);
            }
        }

        public void DiscardBuffers()
        {
            if (Port == null) return;
            try { Port.DiscardInBuffer(); } catch { }
            try { Port.DiscardOutBuffer(); } catch { }
            lock (_bufferLock) { _receiveBuffer.Clear(); }
        }

        #endregion

        #region Helpers

        public bool IsConnected() => IsOpen;

        private static string ToHex(byte[] buf)
        {
            if (buf == null) return string.Empty;
            return BitConverter.ToString(buf, 0, buf.Length).Replace("-", " ");
        }

        #endregion
    }

    /// <summary>
    /// 전압/전류/전력 측정 모듈 한 프레임 파싱 결과.
    /// 패킷 오프셋: Voltage 9, Current 13, Power 17, D+ 21, D- 25 (float, 리틀 엔디안).
    /// </summary>
    public class PowerMeterValue
    {
        public float Voltage { get; set; }
        public float Current { get; set; }
        public float Power { get; set; }
        public float DPlus { get; set; }
        public float DMinus { get; set; }

        public static PowerMeterValue FromPacket(byte[] packet)
        {
            if (packet == null || packet.Length < 29) return null;
            return new PowerMeterValue
            {
                Voltage = BitConverter.ToSingle(packet, 9),
                Current = BitConverter.ToSingle(packet, 13),
                Power = BitConverter.ToSingle(packet, 17),
                DPlus = packet.Length >= 25 ? BitConverter.ToSingle(packet, 21) : 0f,
                DMinus = packet.Length >= 29 ? BitConverter.ToSingle(packet, 25) : 0f
            };
        }

        public override string ToString()
        {
            return $"V={Voltage:F4} V, I={Current:F6} A, P={Power:F6} W";
        }
    }
}
