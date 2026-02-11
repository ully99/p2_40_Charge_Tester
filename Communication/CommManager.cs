using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.IO.Ports;
using p2_40_Charge_Tester.Data;

namespace p2_40_Charge_Tester.Communication
{
    public static class CommManager
    {
        private static readonly object _sync = new object();
        public static SerialChannelPort[] Pbas { get; }
        public static TcpChannelClient[] Boards { get; }

        public static QrChannelPort[] QrPorts { get; } //(0~3) : 채널, 4 : 레시피
        public static RecipeQrPort RecipeQr { get; }
        public static PowerMeterChannelPort[] PowerMeterPorts { get; }
        //public static QrPort QrReader { get; }
        //public static JigPort Jig { get; }

        static CommManager()
        {
            Pbas = new SerialChannelPort[4];
            Boards = new TcpChannelClient[4];
            QrPorts = new QrChannelPort[4]; // 총 5개 생성
            for (int i = 0; i < 4; i++)
            {
                QrPorts[i] = new QrChannelPort(i);
            }
            RecipeQr = new RecipeQrPort();
            PowerMeterPorts = new PowerMeterChannelPort[4];
            for (int i = 0; i < 4; i++)
            {
                PowerMeterPorts[i] = new PowerMeterChannelPort(i + 1);
            }
        }

        public static async Task ConnectAllComponent(int TcpConnectTimeoutMs)
        {
            await ConnectAllChannel(TcpConnectTimeoutMs); //보드 Tcp 연결
            ConnectAllQrPorts(); //Qr 연결
            ConnectAllPowerMeterPorts(); //전력계 모듈 연결
        }


        public static async Task ConnectAllChannel(int timeoutMs) //모든 보드 TCP 연결
        {
            for (int i = 0; i < Boards.Length; i++) //연결되있는 IP/PORT 를 전부 연결 해제한다.
            {
                if (Boards[i]?.IsSocketConnected() == true)
                {
                    Boards[i].Disconnect();
                    Boards[i] = null;
                }
            } //이걸 안넣어주면 USE가 FALSE여도 해당 객체의 TCP연결은 그대로 연결되있다.
            // 이미 연결되있는 IP/PORT 는 연결할 수 없음.

            var tasks = new List<Task>();

            tasks.Add(TryConnectAsync(0, Settings.Instance.Use_CH1, Settings.Instance.Board_IP_CH1, Settings.Instance.Board_Port_CH1, timeoutMs));
            tasks.Add(TryConnectAsync(1, Settings.Instance.Use_CH2, Settings.Instance.Board_IP_CH2, Settings.Instance.Board_Port_CH2, timeoutMs));
            tasks.Add(TryConnectAsync(2, Settings.Instance.Use_CH3, Settings.Instance.Board_IP_CH3, Settings.Instance.Board_Port_CH3, timeoutMs));
            tasks.Add(TryConnectAsync(3, Settings.Instance.Use_CH4, Settings.Instance.Board_IP_CH4, Settings.Instance.Board_Port_CH4, timeoutMs));

            await Task.WhenAll(tasks);
        }



        private static async Task TryConnectAsync(int index, bool use, string ip, int port, int timeoutMs) //보드 TCP연결
        {

            if (Boards[index] != null) //기존에 연결이 되있으면 닫고 시작한다.
            {
                if (Boards[index].IsSocketConnected())
                    Boards[index].Disconnect();

                Boards[index] = null;
            }

            if (!use) return;
            
            Console.WriteLine($"CH{index + 1} 연결 시도 중...[{ip}][{port}]");

            var client = new TcpChannelClient(index + 1);
            bool success = await client.ConnectAsync(ip, port, timeoutMs);

            Boards[index] = client;

            if (success)
                Console.WriteLine($"CH{index + 1} 연결 성공![{ip}][{port}]");
            else
                Console.WriteLine($"CH{index + 1} 연결 실패 (타임아웃 또는 오류)[{ip}][{port}]");
        }

        public static void ConnectAllDevice()
        {
            TryConnectDevice(0, Settings.Instance.Use_CH1, Settings.Instance.Device_Port_CH1);
            TryConnectDevice(1, Settings.Instance.Use_CH2, Settings.Instance.Device_Port_CH2);
            TryConnectDevice(2, Settings.Instance.Use_CH3, Settings.Instance.Device_Port_CH3);
            TryConnectDevice(3, Settings.Instance.Use_CH4, Settings.Instance.Device_Port_CH4);
        } //모든 채널 디바이스 연결


        public static void TryConnectDevice(int index, bool use, string DevicePort)
        {

            if (index < 0 || index >= Pbas.Length)
            {
                Console.WriteLine($"[Comm] Invalid channel index: {index}");
                return;
            }

            

            lock (_sync)
            {
                if (!use) //사용안하면 연결끊기
                {
                    Console.WriteLine($"CH{index + 1} 사용 안함 (pass)");
                    Pbas[index]?.Disconnect();
                    return;
                }


                Console.WriteLine($"CH{index + 1} 연결 시도 중... Device : [{DevicePort}]");

                if (Pbas[index] != null && Pbas[index].IsConnected())
                    Pbas[index].Disconnect();

                

                
                if (Pbas[index] == null) Pbas[index] = new SerialChannelPort(index + 1);

                bool DeviceConnected = false; 
               
                if (DevicePort != "None")
                {
                    DeviceConnected = Pbas[index].Connect(DevicePort);
                }




                bool success = DeviceConnected;

                if (success)
                    Console.WriteLine($"CH{index + 1} 연결 성공! Device : [{DevicePort}]");
                else
                    Console.WriteLine($"CH{index + 1} 연결 실패 Device : [{(DeviceConnected ? DevicePort : "Fail")}]");
            }

        } //디바이스 연결

        // QR 연결 메서드 추가
        public static void ConnectAllQrPorts()
        {
            var set = Settings.Instance;

            for (int i = 0; i <= 3; i++)
            {
                QrPorts[i].Close();
            }
            RecipeQr.Close();
            // Ch 1~4
            QrPorts[0].Open(set.Qr_Port_CH1, set.Qr_BaudRate_CH1);
            QrPorts[1].Open(set.Qr_Port_CH2, set.Qr_BaudRate_CH2);
            QrPorts[2].Open(set.Qr_Port_CH3, set.Qr_BaudRate_CH3);
            QrPorts[3].Open(set.Qr_Port_CH4, set.Qr_BaudRate_CH4);

            // Recipe QR (인덱스 4번 사용)
            RecipeQr.Open(set.Recipe_Qr_Port, set.Recipe_Qr_BaudRate);
        }

        /// <summary>
        /// 전압/전류/전력 측정 모듈(PowerMeter) Ch1~4 연결.
        /// </summary>
        public static void ConnectAllPowerMeterPorts()
        {
            var set = Settings.Instance;

            for (int i = 0; i < 4; i++)
            {
                PowerMeterPorts[i].Disconnect();
            }

            if (set.Module_Port_CH1 != null && set.Module_Port_CH1 != "None")
            {
                bool ok = PowerMeterPorts[0].Connect(set.Module_Port_CH1, set.Module_BaudRate_CH1);
                Console.WriteLine(ok ? $"[PowerMeter] CH1 연결 성공: {set.Module_Port_CH1}" : $"[PowerMeter] CH1 연결 실패: {set.Module_Port_CH1}");
            }
            if (set.Module_Port_CH2 != null && set.Module_Port_CH2 != "None")
            {
                bool ok = PowerMeterPorts[1].Connect(set.Module_Port_CH2, set.Module_BaudRate_CH2);
                Console.WriteLine(ok ? $"[PowerMeter] CH2 연결 성공: {set.Module_Port_CH2}" : $"[PowerMeter] CH2 연결 실패: {set.Module_Port_CH2}");
            }
            if (set.Module_Port_CH3 != null && set.Module_Port_CH3 != "None")
            {
                bool ok = PowerMeterPorts[2].Connect(set.Module_Port_CH3, set.Module_BaudRate_CH3);
                Console.WriteLine(ok ? $"[PowerMeter] CH3 연결 성공: {set.Module_Port_CH3}" : $"[PowerMeter] CH3 연결 실패: {set.Module_Port_CH3}");
            }
            if (set.Module_Port_CH4 != null && set.Module_Port_CH4 != "None")
            {
                bool ok = PowerMeterPorts[3].Connect(set.Module_Port_CH4, set.Module_BaudRate_CH4);
                Console.WriteLine(ok ? $"[PowerMeter] CH4 연결 성공: {set.Module_Port_CH4}" : $"[PowerMeter] CH4 연결 실패: {set.Module_Port_CH4}");
            }
        }

        public static bool TryConnectChannel_bool(int index, bool use, string DevicePort)
        {

            if (index < 0 || index >= Pbas.Length)
            {
                Console.WriteLine($"[Comm] Invalid channel index: {index}");
                return false;
            }



            lock (_sync)
            {
                if (!use) //사용안하면 연결끊기
                {
                    Console.WriteLine($"CH{index + 1} 사용 안함 (pass)");
                    Pbas[index]?.Disconnect();
                    return false;
                }


                Console.WriteLine($"CH{index + 1} 연결 시도 중... Device : [{DevicePort}]");

                if (Pbas[index] != null && Pbas[index].IsConnected())
                    Pbas[index].Disconnect();

                


                if (Pbas[index] == null) Pbas[index] = new SerialChannelPort(index + 1);

                bool DeviceConnected = false;
                if (DevicePort != "None")
                {
                    DeviceConnected = Pbas[index].Connect(DevicePort);
                }




                bool success = DeviceConnected;

                if (success)
                {
                    Console.WriteLine($"CH{index + 1} 연결 성공! Device : [{DevicePort}]");
                    return success;
                }
                    
                else
                {
                    Console.WriteLine($"CH{index + 1} 연결 실패 Device : [{(DeviceConnected ? DevicePort : "Fail")}]");
                    return success;

                }
                    
            }

        }

        public static bool IsBoardConnected(int index)
        {
            if (index < 0 || index >= Boards.Length) return false;

            lock (_sync)
            {
                var board = Boards[index];
                bool boardOk = board?.IsConnected() ?? false;
                return boardOk;
            }
        }

        public static bool[] GetAllBoardsConnected()
        {
            return new[]
            {
                IsBoardConnected(0),
                IsBoardConnected(1),
                IsBoardConnected(2),
                IsBoardConnected(3)
            };
        }





        public static bool IsDeviceConnected(int index) //디바이스 연결상태 확인
        {
            if (index < 0 || index >= Pbas.Length) return false;

            lock (_sync)
            {
                var dev = Pbas[index];
                //var mau = Maurer;

                bool devOk = dev?.IsConnected() ?? false;
                //bool mauOk = Settings.Instance.Use_Maurer? (mau?.IsOpen ?? false) : true;

                return devOk;
            }
        }

        public static bool[] GetAllDevicesConnected()
        {
            return new[]
            {
                IsDeviceConnected(0),
                IsDeviceConnected(1),
                IsDeviceConnected(2),
                IsDeviceConnected(3)
            };
        }


    }
}
