using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using p2_40_Charge_Tester.Data;
using p2_40_Charge_Tester.LIB;

namespace p2_40_Charge_Tester.Communication
{
    public class SerialChannelPort
    {
        public int ChannelNo { get; }
        public SerialPort Port { get; private set; }
        public bool IsOpen { get { return Port != null && Port.IsOpen; } }

        public RichTextBox LogTarget { get; set; }
        // true: TX, false: RX
        public Action<RichTextBox, string, bool> LogCommToUI { get; set; }
        public Action<RichTextBox, string> LogTxToUI { get; set; }
        public Action<RichTextBox, string> LogRxToUI { get; set; }

        public bool TxRxOutToConsole => Settings.Instance.Use_TxRx_Console_Pba;
        

        private readonly List<byte> _receiveBuffer = new List<byte>();
        private TaskCompletionSource<byte[]> _packetTcs;
        private readonly object _bufferLock = new object();

        // Modbus ASCII framing
        private const byte PacketHeader = 0x3A; // ':'
        private const byte PacketFooter1 = 0x0D; // '\r'
        private const byte PacketFooter2 = 0x0A; // '\n'

        // 요청 간 인터벌
        private DateTime _lastTxUtc = DateTime.MinValue;
        public int MinIntervalMs => Settings.Instance.Pba_Min_Interval; //================이 수치 밑으로는 데이터 송수신 안된다 (최소 인터벌 보장)
        

        public SerialChannelPort(int ch)
        {
            ChannelNo = ch;
        }

        #region Connect
        public bool Connect(string portName, int baudRate = 115200)
        {
            try
            {
                //Console.WriteLine("[Serial] CH{0} 연결 시도: {1}, {2}-8-N-1", ChannelNo, portName, baudRate);

                if (Port != null)
                {
                    try { if (Port.IsOpen) Port.Close(); } catch { }
                    Port.DataReceived -= OnDataReceived;
                    Port.Dispose();
                    Port = null;
                }

                Port = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
                Port.Encoding = Encoding.ASCII;      // ASCII 라인
                Port.NewLine = "\r\n";               // 줄 끝
                Port.ReadTimeout = 1000;
                Port.WriteTimeout = 1000;

                Port.DataReceived += OnDataReceived;
                Port.Open();

                Port.DiscardInBuffer();
                Port.DiscardOutBuffer();

                //Console.WriteLine("[Serial] CH{0} 연결 성공", ChannelNo);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Serial] CH{0} 연결 실패: {1} - {2}", ChannelNo, ex.GetType().Name, ex.Message);
                return false;
            }
        }

        public async Task<bool> ConnectAsync(string portName, int baudRate = 115200, int timeoutMs = 3000, CancellationToken token = default)//Timeout 있는 시리얼 연결 메서드
        {
            // ★ 1. 현재 연결 상태 확인
            // 포트가 이미 열려 있고, 이름과 속도가 같다면 시도하지 않고 바로 true 리턴
            if (Port != null && Port.IsOpen && Port.PortName == portName && Port.BaudRate == baudRate)
            {
                Console.WriteLine($"[Serial] CH{ChannelNo} 이미 동일한 포트로 연결되어 있습니다. ({portName})");
                return true;
            }

            DateTime startTime = DateTime.Now;

            while ((DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
            {
                // STOP 버튼 체크
                if (token != default) token.ThrowIfCancellationRequested();

                try
                {
                    // 2. 새로운 연결을 위해 기존 포트 정리
                    if (Port != null)
                    {
                        try { if (Port.IsOpen) Port.Close(); } catch { }
                        Port.DataReceived -= OnDataReceived;
                        Port.Dispose();
                        Port = null;
                    }

                    // 3. 포트 설정 및 생성
                    Port = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
                    Port.Encoding = Encoding.ASCII;
                    Port.NewLine = "\r\n";
                    Port.ReadTimeout = 1000;
                    Port.WriteTimeout = 1000;
                    Port.DataReceived += OnDataReceived;

                    // 4. 물리적 연결 시도
                    Port.Open();

                    Port.DiscardInBuffer();
                    Port.DiscardOutBuffer();

                    Console.WriteLine($"[Serial] CH{ChannelNo} 새 연결 성공 ({portName})");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Serial] CH{ChannelNo} 연결 시도 중... ({ex.Message})");

                    // 재시도 대기 시간 중에도 STOP 체크 가능하도록 token 전달
                    await Task.Delay(500, token);
                }
            }

            Console.WriteLine($"[Serial] CH{ChannelNo} 연결 최종 실패 (타임아웃 {timeoutMs}ms)");
            return false;
        }

        public void Disconnect()
        {
            try
            {
                if (IsOpen)
                {
                    Port.DataReceived -= OnDataReceived;
                    Port.Close();
                    Port.Dispose();
                    Port = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Serial] CH{0} Disconnect 예외: {1}", ChannelNo, ex.Message);
            }
        }
        #endregion

        #region Tx/Rx (byte)
        public async Task<bool> SendAsync(byte[] data, int timeoutMs = 2000)
        {
            if (!IsOpen) return false;

            // 인터벌 보장
            int remain = MinIntervalMs - (int)(DateTime.UtcNow - _lastTxUtc).TotalMilliseconds;
            if (remain > 0) await Task.Delay(remain).ConfigureAwait(false);

            using (var cts = new CancellationTokenSource(timeoutMs))
            {
                try
                {
                    await Port.BaseStream.WriteAsync(data, 0, data.Length, cts.Token).ConfigureAwait(false);
                    _lastTxUtc = DateTime.UtcNow;
                    if (LogCommToUI != null) LogCommToUI(LogTarget, SafeAscii(data), true); 
                    if (LogTxToUI != null) LogTxToUI(LogTarget, SafeAscii(data));
                    if (TxRxOutToConsole) Console.WriteLine($"TX => {SafeAscii(data).Replace("\r", "").Replace("\n", "")} [Channel {ChannelNo}]");

                    if (Settings.Instance.Use_Write_Log)
                    {
                        string Saved_Data = SafeAscii(data);
                        if (!Saved_Data.Contains(":2A030000000DC6")) //90ms마다 데이터 불러오는 건 넣지 않겠다.(너무 많음)
                            CsvManager.CsvSave($"[TX] => {Saved_Data}");

                    }
                    return true;
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("[Serial] CH{0} SendAsync 타임아웃({1}ms)", ChannelNo, timeoutMs);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Serial] CH{0} SendAsync 예외: {1}", ChannelNo, ex.Message);
                    return false;
                }
            }
        }

        private void TryExtractFixedPacket()
        {
            if (_packetTcs == null || _packetTcs.Task.IsCompleted)
                return;

            while (true)
            {
                // 헤더(':') 탐색
                int headerIdx = _receiveBuffer.IndexOf(PacketHeader);
                if (headerIdx < 0)
                {
                    _receiveBuffer.Clear();
                    return;
                }
                if (headerIdx > 0) _receiveBuffer.RemoveRange(0, headerIdx);

                // CRLF 찾기
                int crIdx = -1;
                for (int i = 1; i < _receiveBuffer.Count; i++)
                {
                    if (_receiveBuffer[i] == PacketFooter1)
                    {
                        crIdx = i;
                        break;
                    }
                }
                if (crIdx < 0 || crIdx + 1 >= _receiveBuffer.Count) return; // 아직 라인 미완성

                if (_receiveBuffer[crIdx + 1] != PacketFooter2)
                {
                    // 깨진 라인: ':' 하나 버리고 재시도
                    _receiveBuffer.RemoveAt(0);
                    continue;
                }

                // 프레임(':~CRLF') 확보
                int frameLen = crIdx + 2;
                byte[] frame = new byte[frameLen];
                for (int i = 0; i < frameLen; i++) frame[i] = _receiveBuffer[i];
                _receiveBuffer.RemoveRange(0, frameLen);

                if (frame.Length < 7) continue; // 최소 길이 보호

                // ":" 제외, CRLF 제외 → HEX 본문
                string hex = Encoding.ASCII.GetString(frame, 1, frame.Length - 3);
                if ((hex.Length % 2) != 0) continue;

                byte[] raw = HexToBytes(hex); // [Addr][Func][Data...][LRC]
                if (raw == null || raw.Length < 3) continue;

                // LRC 검증 (마지막 1바이트는 LRC)
                byte rxLrc = raw[raw.Length - 1];
                byte calc = ComputeLRC(raw, 0, raw.Length - 1);
                if (rxLrc != calc)
                {
                    Console.WriteLine("[Serial] CH{0} LRC 불일치: calc={1:X2}, rx={2:X2}", ChannelNo, calc, rxLrc);
                    // 라인 하나는 이미 소비했으니 계속 루프
                    continue;
                }

                if (LogCommToUI != null) LogCommToUI(LogTarget, SafeAscii(frame), false);
                if (LogRxToUI != null) LogRxToUI(LogTarget, SafeAscii(frame));
                if (TxRxOutToConsole) Console.WriteLine($"RX => {SafeAscii(frame).Replace("\r", "").Replace("\n", "")} [Channel {ChannelNo}]");
                if (Settings.Instance.Use_Write_Log)
                {
                    string Saved_Data = SafeAscii(frame);
                    if (!Saved_Data.Contains(":2A031A")) //90ms마다 데이터 불러오는 건 넣지 않겠다.(너무 많음)
                        CsvManager.CsvSave($"[RX] => {Saved_Data}");
                }

                _packetTcs.TrySetResult(frame); // ':'~CRLF 포함 그대로 반환
                return;
            }
        }

        public async Task<byte[]> SendAndReceivePacketAsync(byte[] data, int timeoutMs, CancellationToken token = default)
        {
            int maxAttempts = (Settings.Instance.Use_Pba_Retry && Settings.Instance.Pba_Retry_Count > 0)
                      ? Settings.Instance.Pba_Retry_Count
                      : 1;

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                if (token != default) token.ThrowIfCancellationRequested();
                // === 기존 로직 시작 ===
                DiscardBuffers();
                _packetTcs = new TaskCompletionSource<byte[]>();

                bool sendOk = await SendAsync(data).ConfigureAwait(false);
                if (!sendOk)
                {
                    // 송신 자체를 실패했으면 재시도 없이 바로 실패 처리할지, 이것도 재시도할지 결정해야 함.
                    // 보통 송신 실패는 포트가 닫힌 거라 재시도 의미가 없음.
                    _packetTcs.TrySetResult(null);
                    return null;
                }

                Task timeoutTask = Task.Delay(timeoutMs);
                Task completed = await Task.WhenAny(_packetTcs.Task, timeoutTask).ConfigureAwait(false);

                if (completed != timeoutTask)
                {
                    // 성공! (타임아웃 전에 응답 옴)
                    if (_packetTcs.Task.IsCompleted && !_packetTcs.Task.IsFaulted)
                        return await _packetTcs.Task.ConfigureAwait(false);
                }
                else
                {
                    // 타임아웃 발생 (실패)
                    _packetTcs.TrySetCanceled();
                }
                // === 기존 로직 끝 ===

                // 실패해서 여기까지 왔는데, 아직 기회가 남았다면?
                if (attempt < maxAttempts)
                {
                    Console.WriteLine($"[Serial Retry] CH{ChannelNo} Timeout! Retrying... ({attempt}/{maxAttempts})");
                    // 너무 빠르게 재시도하면 장비가 체할 수 있으니 살짝 쉰다 (선택 사항)
                    await Task.Delay(200, token).ConfigureAwait(false);
                }
            }

            // 모든 시도 다 실패함
            Console.WriteLine($"[Serial Fail] CH{ChannelNo} Failed after {maxAttempts} attempts.");
            return null;
        }

        public async Task<byte[]> SendAndReceivePacketAsync_OnlyData(byte[] txPacket, int timeoutMs = 2000, CancellationToken token = default)
        {
            // 1) 기존 함수 호출해서 전체 패킷(: ~ \r\n)을 그대로 받는다
            byte[] frame = await SendAndReceivePacketAsync(txPacket, timeoutMs, token);
            if (frame == null || frame.Length == 0)
                return null;

            // 2) byte[] → ASCII 문자열
            string ascii = Encoding.ASCII.GetString(frame).Trim();

            // 3) ":" 제거, CRLF 제거
            if (ascii.StartsWith(":"))
                ascii = ascii.Substring(1);

            if (ascii.EndsWith("\r\n"))
                ascii = ascii.Substring(0, ascii.Length - 2);

            // 4) hex 문자열 → raw byte[]
            byte[] raw = HexToBytes(ascii);
            if (raw == null || raw.Length < 4)
                return null;

            // raw 구조:
            // raw[0] = Slave
            // raw[1] = Function
            // raw[2] = ByteCount
            // raw[3..] = Data (count 만큼)
            // raw[last] = LRC

            byte count = raw[2];

            if (raw.Length < (3 + count + 1))
                return null;

            // 5) Data 부분만 추출
            return raw.Skip(3).Take(count).ToArray();
        }
        #endregion

        #region Tx/Rx (Ascii)
        public async Task<bool> SendAsciiAsync(string ascii, int timeoutMs = 1000)
        {
            if (string.IsNullOrEmpty(ascii)) return false;
            byte[] data = Encoding.ASCII.GetBytes(ascii);
            return await SendAsync(data, timeoutMs);
        }

        public async Task<string> SendAndReceiveAsciiAsync(string asciiFrame, int timeoutMs = 2000)
        {
            if (string.IsNullOrEmpty(asciiFrame)) return null;

            DiscardBuffers();

            _packetTcs = new TaskCompletionSource<byte[]>();

            bool ok = await SendAsciiAsync(asciiFrame, timeoutMs);
            if (!ok)
            {
                _packetTcs.TrySetResult(null);
                return null;
            }

            //응답 기다리기
            Task<byte[]> wait = _packetTcs.Task;
            Task timeout = Task.Delay(timeoutMs);

            Task done = await Task.WhenAny(wait, timeout);
            if (done == timeout)
            {
                _packetTcs.TrySetCanceled();
                return null;
            }

            byte[] responseFrame = await wait; //이미 완료된 Task의 결과를 읽는다.
            if (responseFrame == null) return null;

            return Encoding.ASCII.GetString(responseFrame);
        }

        public async Task<byte[]> SendAndReceiveAsciiAsync_OnlyData(string asciiFrame, int timeoutMs = 2000)
        {
            string frame = await SendAndReceiveAsciiAsync(asciiFrame, timeoutMs);

            if (string.IsNullOrEmpty(frame)) return null;

            // 1) ":" 제거 + CRLF 제거
            string hex = frame.Trim();
            if (hex.StartsWith(":")) hex = hex.Substring(1);
            if (hex.EndsWith("\r\n")) hex = hex.Substring(0, hex.Length - 2);//

            // 2) hex → byte[] 변환
            byte[] raw = HexToBytes(hex);
            if (raw == null || raw.Length < 4)
                return null;

            // 구조:
            // raw[0] = Slave
            // raw[1] = Function
            // raw[2] = ByteCount
            // raw[3..3+ByteCount-1] = Data
            // raw[last] = LRC

            byte slave = raw[0];
            byte function = raw[1];
            byte count = raw[2];

            if (raw.Length < (3 + count + 1)) return null;

            // 3) Data 부분만 추출
            byte[] data = raw.Skip(3).Take(count).ToArray();

            return data;
        }
        #endregion
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (_bufferLock)
                {
                    int bytesToRead = Port.BytesToRead;
                    if (bytesToRead <= 0) return;

                    byte[] temp = new byte[bytesToRead];
                    int read = Port.Read(temp, 0, bytesToRead);
                    if (read > 0)
                    {
                        for (int i = 0; i < read; i++) _receiveBuffer.Add(temp[i]);
                    }

                    // 고정 길이 파서 -> ASCII 라인 파서
                    TryExtractFixedPacket();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Serial] CH{0} DataReceived 예외: {1}", ChannelNo, ex.Message);
            }
        }

        
        /// <summary>
        /// 기존 메서드명 유지: 내부는 콜론~CRLF 프레임 추출 + HEX→바이너리 + LRC 검증
        /// </summary>


        public void DiscardBuffers()
        {
            if (Port == null) return;
            try { Port.DiscardInBuffer(); } catch { }
            try { Port.DiscardOutBuffer(); } catch { }
            lock (_bufferLock) { _receiveBuffer.Clear(); }
        }

        /// <summary>
        /// CDCProtocol.GetPacket()으로 만든 ASCII 프레임을 전송하고,
        /// 응답 ASCII 프레임(':'~CRLF 포함)을 반환.
        /// </summary>
        
   


        // === 기존 보조 메서드 유지 ===
        public bool IsConnected() { return IsOpen; }
        public void DiscardInBuffer() { if (Port != null) Port.DiscardInBuffer(); }
        public void DiscardOutBuffer() { if (Port != null) Port.DiscardOutBuffer(); }

        // ---------- Helpers ----------
        private static string SafeAscii(byte[] buf)
        {
            try { return Encoding.ASCII.GetString(buf); }
            catch { return BitConverter.ToString(buf); }
        }

        private static byte[] HexToBytes(string hex)
        {
            if (string.IsNullOrEmpty(hex) || (hex.Length % 2) != 0) return null;

            int len = hex.Length / 2;
            byte[] dst = new byte[len];
            for (int i = 0; i < len; i++)
            {
                int hi = HexToNib(hex[2 * i]);
                int lo = HexToNib(hex[2 * i + 1]);
                if (hi < 0 || lo < 0) return null;
                dst[i] = (byte)((hi << 4) | lo);
            }
            return dst;
        }

        private static int HexToNib(char c)
        {
            if (c >= '0' && c <= '9') return c - '0';
            char u = (char)(c & ~0x20); // 대문자화
            if (u >= 'A' && u <= 'F') return u - 'A' + 10;
            return -1;
        }

        private static byte ComputeLRC(byte[] buf, int offset, int len)
        {
            int sum = 0;
            for (int i = 0; i < len; i++) sum += buf[offset + i];
            return (byte)((-sum) & 0xFF);
        }
    }
}
