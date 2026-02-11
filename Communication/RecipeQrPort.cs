using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace p2_40_Charge_Tester.Communication
{
    public class RecipeQrPort : IDisposable
    {
        //public int ChannelIndex { get; private set; } // 0~3: Ch1~4, 4: Recipe
        public SerialPort Port { get; private set; }
        public bool IsOpen => Port != null && Port.IsOpen;

        private readonly List<byte> _buffer = new List<byte>();
        private readonly object _bufferLock = new object();

        // ReadLineAsync가 기다릴 때 사용하는 신호기
        private TaskCompletionSource<string> _readLineTcs;

        public RecipeQrPort(int channelIndex = -1)
        {
            //ChannelIndex = channelIndex;
        }

        #region Connect / Disconnect
        public bool Open(string portName, int baudRate)
        {
            try
            {
                Close(); // 기존 연결 정리

                if (string.IsNullOrEmpty(portName) || portName.ToUpper() == "NONE")
                    return false;

                Port = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
                Port.Encoding = Encoding.ASCII;
                Port.NewLine = "\r"; // QR은 보통 CR로 끝남

                // 이벤트 핸들러 등록
                Port.DataReceived += OnDataReceived;

                Port.Open();
                DiscardInBuffer();

                Console.WriteLine($"[Recipe QR] Port Opened: {portName}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Recipe QR] Port Open Fail: {ex.Message}");
                return false;
            }
        }

        public void Close()
        {
            if (Port != null)
            {
                if (Port.IsOpen)
                {
                    Port.DataReceived -= OnDataReceived; // 이벤트 해제 중요!
                    try { Port.Close(); } catch { }
                }
                Port.Dispose();
                Port = null;
            }
            lock (_bufferLock) { _buffer.Clear(); }
        }





        #endregion
        #region Send / Read
        public async Task SendAsync(byte[] data)
        {
            if (!IsOpen) return;
            try
            {
                await Port.BaseStream.WriteAsync(data, 0, data.Length);
                // 필요하다면 여기서 TX 로그 출력
                string msg = Encoding.ASCII.GetString(data);
                Console.WriteLine($"[Recipe QR] TX : {msg} [Recipe Qr]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Recipe QR] Send Error: {ex.Message}");
            }
        }

        // 읽기 함수 (이제 Event와 연동됨)
        public async Task<string> ReadLineAsync(int timeoutMs = 2000)
        {
            if (!IsOpen) return null;

            // 1. 버퍼 비우기 (새로운 응답을 기다림)
            DiscardInBuffer();

            // 2. 신호기(TCS) 생성
            _readLineTcs = new TaskCompletionSource<string>();

            // 3. 타임아웃 설정
            Task timeoutTask = Task.Delay(timeoutMs);

            // 4. 데이터가 들어오거나 타임아웃이 될 때까지 대기
            Task completedTask = await Task.WhenAny(_readLineTcs.Task, timeoutTask);

            if (completedTask == timeoutTask)
            {
                _readLineTcs.TrySetCanceled(); // 타임아웃 시 취소 처리
                _readLineTcs = null;
                return null;
            }

            // 5. 결과 반환
            string result = await _readLineTcs.Task;
            _readLineTcs = null; // 사용 후 초기화
            return result;
        }

        public void DiscardInBuffer()
        {
            try { Port?.DiscardInBuffer(); } catch { }
            lock (_bufferLock) { _buffer.Clear(); } // 내부 버퍼도 비워야 함
        }
        public void DiscardOutBuffer() => Port?.DiscardOutBuffer();
        #endregion

        #region Event Handler (Monitoring Logic)
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int bytesToRead = Port.BytesToRead;
                if (bytesToRead <= 0) return;

                byte[] chunk = new byte[bytesToRead];
                Port.Read(chunk, 0, bytesToRead);

                lock (_bufferLock)
                {
                    _buffer.AddRange(chunk);

                    // --- 데이터 파싱 (CR 또는 LF를 만나면 한 줄로 인식) ---
                    while (true)
                    {
                        // 0x0D(\r) 또는 0x0A(\n) 찾기
                        int eolIndex = _buffer.FindIndex(b => b == 0x0D || b == 0x0A);

                        if (eolIndex < 0) break; // 아직 줄바꿈 문자가 안 들어옴 -> 대기

                        // 한 줄 추출
                        string line = Encoding.ASCII.GetString(_buffer.Take(eolIndex).ToArray()).Trim();

                        // 사용된 데이터 + 줄바꿈 문자 제거 (줄바꿈이 연속될 경우 처리)
                        int removeCount = eolIndex + 1;
                        if (removeCount < _buffer.Count && (_buffer[removeCount] == 0x0D || _buffer[removeCount] == 0x0A))
                        {
                            removeCount++; // \r\n 인 경우 하나 더 제거
                        }
                        _buffer.RemoveRange(0, removeCount);

                        if (string.IsNullOrEmpty(line)) continue; // 빈 줄 무시

                        //  1. 모니터링 기능 (무조건 콘솔에 찍음)
                        Console.WriteLine($"[Recipe QR] RX => {line}");

                        //  2. ReadLineAsync가 기다리고 있었다면 값 전달!
                        if (_readLineTcs != null && !_readLineTcs.Task.IsCompleted)
                        {
                            _readLineTcs.TrySetResult(line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Recipe QR] DataReceived Error: {ex.Message}");
            }
        }
        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
