using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using p2_40_Charge_Tester.Data;

namespace p2_40_Charge_Tester.Communication
{
    public class TcpChannelClient
    {
        public int ChannelNo { get; }
        public TcpClient Client { get; private set; }
        //public bool IsConnected => Client?.Connected ?? false;

        public bool WasConnected { get; set; } = true;

        public RichTextBox LogTarget { get; set; }
        public bool TxRxOutToConsole => Settings.Instance.Use_TxRx_Console_Board; //콘솔창에 rx tx 표시할거냐
        // true: TX, false: RX
        public Action<RichTextBox, string, bool> LogCommToUI { get; set; }

        // 요청 간 인터벌(Serial과 동일 개념)
        private DateTime _lastTxUtc = DateTime.MinValue;
        public int MinIntervalMs => Settings.Instance.Board_Min_Interval; // TCP는 필요 없을 수도 있지만 동일 인터페이스 유지

        // 수신 버퍼/동기화
        private readonly List<byte> _receiveBuffer = new List<byte>();
        private readonly object _bufferLock = new object();

        // 패킷 대기용
        private TaskCompletionSource<byte[]> _packetTcs;

        // 수신 루프
        private CancellationTokenSource _rxCts;
        private Task _rxTask;

        // 고정 프로토콜 헤더 "ITM"
        private const byte H1 = 0x49; // I
        private const byte H2 = 0x54; // T
        private const byte H3 = 0x4D; // M

        public TcpChannelClient(int ch)
        {
            ChannelNo = ch;
        }

        #region Connect/Disconnect
        public async Task<bool> ConnectAsync(string ip, int port, int timeoutMillis, int retryCount = 2)
        {
            Disconnect(); // 기존 연결 정리

            for (int attempt = 1; attempt <= retryCount; attempt++)
            {
                Console.WriteLine($"연결 시도 : {attempt} 최대 시도 : {retryCount} [{ip}][{port}]");
                try
                {
                    Client = new TcpClient();

                    var connectTask = Client.ConnectAsync(ip, port);
                    var timeoutTask = Task.Delay(timeoutMillis);

                    var completed = await Task.WhenAny(connectTask, timeoutTask).ConfigureAwait(false);
                    if (completed == connectTask && Client.Connected)
                    {
                        StartReceiveLoop(); // 여기서부터 Serial의 DataReceived 역할 시작
                        return true;
                    }

                    // 실패 정리
                    SafeCloseClient();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TCP] CH{ChannelNo} Connect 예외(시도 {attempt}): {ex.Message}");
                    SafeCloseClient();
                }

                await Task.Delay(1000).ConfigureAwait(false);
            }

            Console.WriteLine($"[TCP] CH{ChannelNo} 최종 연결 실패: [{ip}][{port}]");
            return false;
        }

        public void Disconnect()
        {
            try
            {
                StopReceiveLoop();
                SafeCloseClient();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TCP] CH{ChannelNo} Disconnect 예외: {ex.Message}");
            }
        }

        private void SafeCloseClient()
        {
            try { Client?.Close(); } catch { }
            try { Client?.Dispose(); } catch { }
            Client = null;
        }

        public bool IsConnected()
        {
            return IsSocketConnected();
        }
        #endregion

        #region Tx/Rx
        public async Task<bool> SendAsync(byte[] data, int timeoutMs = 2000)
        {
            if (!IsSocketConnected() || data == null || data.Length == 0) return false;

            int remain = MinIntervalMs - (int)(DateTime.UtcNow - _lastTxUtc).TotalMilliseconds;
            if (remain > 0) await Task.Delay(remain).ConfigureAwait(false);

            try
            {
                using (var cts = new CancellationTokenSource(timeoutMs))
                {
                    NetworkStream stream = Client.GetStream();
                    await stream.WriteAsync(data, 0, data.Length, cts.Token).ConfigureAwait(false);
                    await stream.FlushAsync(cts.Token).ConfigureAwait(false);

                    _lastTxUtc = DateTime.UtcNow;

                    if (LogCommToUI != null) LogCommToUI(LogTarget, ToHex(data), true);
                    if (TxRxOutToConsole) Console.WriteLine($"TX => {ToHex(data).Replace("\r", "").Replace("\n", "")} [Channel {ChannelNo}]");
                    return true;
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"[TCP] CH{ChannelNo} SendAsync 타임아웃({timeoutMs}ms)");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TCP] CH{ChannelNo} SendAsync 예외: {ex.Message}");
                return false;
            }
        }

        public void DiscardBuffers()
        {
            lock (_bufferLock)
            {
                _receiveBuffer.Clear();
            }
        }

        public async Task<byte[]> SendAndReceivePacketAsync(byte[] txPacket, int timeoutMs, CancellationToken token = default)
        {
            // 1. 재시도 횟수 설정 가져오기 (설정 꺼져있으면 1회)
            int maxAttempts = (Settings.Instance.Use_Board_Retry && Settings.Instance.Board_Retry_Count > 0)
                              ? Settings.Instance.Board_Retry_Count
                              : 1;

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                // 1. 토큰이 유효한(기본값이 아닌) 경우에만 취소 체크
                if (token != default) token.ThrowIfCancellationRequested();

                // 1. 이전 버퍼 비우기 (중요: 이전 시도의 찌꺼기 데이터 제거)
                DiscardBuffers();

                // 2. 응답 대기용 신호기(TCS) 생성
                _packetTcs = new TaskCompletionSource<byte[]>();

                // 3. 데이터 전송
                bool sendOk = await SendAsync(txPacket, timeoutMs).ConfigureAwait(false);
                if (!sendOk)
                {
                    // 전송 자체가 실패(소켓 끊김 등)했으면 TCS 정리하고 다음 시도 혹은 종료
                    _packetTcs.TrySetResult(null);

                    // 소켓이 아예 끊겼으면 재시도 의미가 없으니 바로 종료
                    if (!IsSocketConnected()) return null;
                }
                else
                {
                    // 4. 응답 대기
                    Console.WriteLine($"TimeOut Value : {timeoutMs}");
                    Task timeoutTask = Task.Delay(timeoutMs);
                    Task completed = await Task.WhenAny(_packetTcs.Task, timeoutTask).ConfigureAwait(false);

                    if (completed != timeoutTask) // 타임아웃 전에 완료됨
                    {
                        // 성공적으로 응답 받음!
                        if (_packetTcs.Task.IsCompleted && !_packetTcs.Task.IsFaulted)
                        {
                            // (옵션) 로그에 재시도 성공했다는 걸 남기면 좋음
                            if (attempt > 1)
                                Console.WriteLine($"[TCP Success] CH{ChannelNo} Succeeded at attempt {attempt}");

                            return await _packetTcs.Task.ConfigureAwait(false);
                        }
                    }
                    else // 타임아웃 발생
                    {
                        _packetTcs.TrySetCanceled();
                        Console.WriteLine($"[TCP Timeout] CH{ChannelNo} Request Timeout ({attempt}/{maxAttempts})");
                    }
                }

                // --- 재시도 준비 ---
                if (attempt < maxAttempts)
                {
                    Console.WriteLine($"[TCP Retry] CH{ChannelNo} Retrying TX... ({attempt}/{maxAttempts})");
                    // 너무 급하게 보내지 않도록 잠깐 숨 고르기 (장비 부하 방지)
                    await Task.Delay(200, token).ConfigureAwait(false);
                }
            }

            // 모든 시도 실패
            Console.WriteLine($"[TCP Fail] CH{ChannelNo} Failed after {maxAttempts} attempts.");
            return null;
        }


        public async Task<byte[]> ReceiveFixedProtocolAsync(int timeoutMs)
        {
            _packetTcs = new TaskCompletionSource<byte[]>();

            Task timeoutTask = Task.Delay(timeoutMs);
            Task completed = await Task.WhenAny(_packetTcs.Task, timeoutTask).ConfigureAwait(false);

            if (completed == timeoutTask)
            {
                _packetTcs.TrySetCanceled();
                return null;
            }

            return await _packetTcs.Task.ConfigureAwait(false);
        }
        #endregion

        #region ReceiveLoop + Packet Parse
        private void StartReceiveLoop()
        {
            StopReceiveLoop();

            _rxCts = new CancellationTokenSource();

            _rxTask = ReceiveLoopAsync(_rxCts.Token);
        }

        private void StopReceiveLoop()
        {
            try
            {
                var cts = _rxCts;
                var task = _rxTask;

                if (cts == null) return;

                _rxCts = null;
                _rxTask = null;

                try { cts.Cancel(); } catch { }

                // ★ 중요: 종료를 '확실히' 기다린다 (타임아웃은 두되, 300ms는 너무 짧음)
                if (task != null)
                {
                    try
                    {
                        // 동기 메서드이므로 GetAwaiter().GetResult() 사용
                        Task.WhenAny(task, Task.Delay(2000)).GetAwaiter().GetResult();
                    }
                    catch { }
                }

                try { cts.Dispose(); } catch { }
            }
            catch { }
        }

        private async Task ReceiveLoopAsync(CancellationToken token)
        {
            byte[] temp = new byte[2048];

            while (!token.IsCancellationRequested)
            {
                try
                {
                    var client = Client; // ★ 로컬 캡처 (중간에 null로 바뀌는 레이스 완화)
                    if (client == null || !IsSocketConnected())
                    {
                        await Task.Delay(50, token).ConfigureAwait(false);
                        continue;
                    }

                    NetworkStream stream;
                    try
                    {
                        stream = client.GetStream();
                    }
                    catch (ObjectDisposedException)
                    {
                        // 끊는 중 정상 케이스
                        break;
                    }

                    int bytesRead = await stream.ReadAsync(temp, 0, temp.Length, token).ConfigureAwait(false);

                    if (bytesRead <= 0)
                    {
                        await Task.Delay(20, token).ConfigureAwait(false);
                        continue;
                    }

                    lock (_bufferLock)
                    {
                        for (int i = 0; i < bytesRead; i++)
                            _receiveBuffer.Add(temp[i]);

                        TryExtractFixedPacket_NoLock();
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ObjectDisposedException)
                {
                    // ★ 네가 보던 "삭제된 개체(NetworkStream)" 계열: 끊는 중이면 정상 종료
                    break;
                }
                catch (IOException)
                {
                    await Task.Delay(50, token).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TCP] CH{ChannelNo} ReceiveLoop 예외: {ex.Message}");
                    await Task.Delay(50).ConfigureAwait(false);
                }
            }
        }

        private void TryExtractFixedPacket_NoLock()
        {
            if (_packetTcs == null || _packetTcs.Task.IsCompleted)
                return;

            while (true)
            {
                // 헤더 찾기
                int headerIdx = FindHeader(_receiveBuffer);
                if (headerIdx < 0)
                {
                    _receiveBuffer.Clear();
                    return;
                }
                if (headerIdx > 0)
                    _receiveBuffer.RemoveRange(0, headerIdx);

                // 최소 길이 체크: Header3 + Len2 + (payload 최소 1?) + CRC1 => 최소 7,
                // 네 기존 코드는 while(buffer.Count>=8)이었는데 안전하게 7부터 체크
                if (_receiveBuffer.Count < 7) return;

                int len = (_receiveBuffer[3] << 8) | _receiveBuffer[4];
                int fullLen = len + 6; // 3 + 2 + len + 1

                if (fullLen <= 0)
                {
                    // 이상치면 헤더 하나 버리고 재탐색
                    _receiveBuffer.RemoveAt(0);
                    continue;
                }

                if (_receiveBuffer.Count < fullLen) return; // 아직 덜 옴

                byte[] packet = _receiveBuffer.Take(fullLen).ToArray();
                _receiveBuffer.RemoveRange(0, fullLen);

                if (LogCommToUI != null) LogCommToUI(LogTarget, ToHex(packet), false);
                if (TxRxOutToConsole) Console.WriteLine($"RX => {ToHex(packet).Replace("\r", "").Replace("\n", "")} [Channel {ChannelNo}]");

                _packetTcs.TrySetResult(packet);
                return;
            }
        }

        private int FindHeader(List<byte> buffer)
        {
            for (int i = 0; i < buffer.Count - 2; i++)
            {
                if (buffer[i] == H1 && buffer[i + 1] == H2 && buffer[i + 2] == H3)
                    return i;
            }
            return -1;
        }
        #endregion

        #region Socket Status / Helpers
        public bool IsSocketConnected()
        {
            try
            {
                if (Client == null || Client.Client == null) return false;
                if (!Client.Client.Connected) return false;

                bool part1 = Client.Client.Poll(0, SelectMode.SelectRead);
                bool part2 = (Client.Client.Available == 0);

                // 읽기 가능 + Available 0이면 끊긴 것
                return !(part1 && part2);
            }
            catch
            {
                return false;
            }
        }

        private static string ToHex(byte[] buf, int maxBytes = 0)
        {
            if (buf == null) return string.Empty;

            int len = buf.Length;
            if (maxBytes > 0 && len > maxBytes) len = maxBytes;

            // "49 54 4D 00 10 ..."
            return BitConverter.ToString(buf, 0, len).Replace("-", " ");
        }
        #endregion

    }
}
