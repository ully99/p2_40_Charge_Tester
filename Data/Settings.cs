using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;


namespace p2_40_Charge_Tester.Data
{
    public class Settings : INotifyPropertyChanged
    {
        private readonly object _lock = new object();


        public string Password { get; set; } = "ITM";

        #region MCU LOT MODEL 
        public string Mcu_No { get; set; } = "1";
        public string Lot_No { get; set; } = "1";
        public string Model_No { get; set; } = "P2 4.0";
        public List<string> ModelList { get; set; } = new List<string>() {"P2 4.0", "Model A", "Model B"};
        #endregion

        public bool Use_Write_Log { get; set; }

        public string SavedCurrentDate { get; set; }
        public int LogFileStack { get; set; } = 1;

        public int Board_Connect_Timeout { get; set; } = 3000;
        public int Pba_Connect_Timeout { get; set; } = 3000;
        public int Board_Read_Timeout { get; set; } = 4500;
        public int Pba_Read_Timeout { get; set; } = 4500;

        public int Pba_Min_Interval { get; set; } = 30;
        public int Board_Min_Interval { get; set; } = 30;

        public int Pba_On_Delay { get; set; } = 3000;

        public bool Use_Maurer { get; set; } = false;
        public string Maurer_Port { get; set; } = "";

        #region CH1
        private bool _use_ch1;
        public bool Use_CH1
        {
            get => _use_ch1;
            set
            {
                if (_use_ch1 == value) return; // 값이 같으면 무시 (무한 루프 방지)
                _use_ch1 = value;
                OnPropertyChanged("Use_CH1"); // UI에 변경 사실을 알림
            }
        }
        public string Board_IP_CH1 { get; set; } = "192.168.0.101";
        public int Board_Port_CH1 { get; set; } = 5000;
        public string Device_Port_CH1 { get; set; } = "None";
        public int Device_BaudRate_CH1 { get; set; } = 115200;
        public string Qr_Port_CH1 { get; set; } = "None";
        public int Qr_BaudRate_CH1 { get; set; } = 9600;
        public string Module_Port_CH1 { get; set; } = "None";
        public int Module_BaudRate_CH1 { get; set; } = 115200;



        #endregion

        #region CH2

        private bool _use_ch2;
        public bool Use_CH2
        {
            get => _use_ch2;
            set
            {
                if (_use_ch2 == value) return; // 값이 같으면 무시 (무한 루프 방지)
                _use_ch2 = value;
                OnPropertyChanged("Use_CH2"); // UI에 변경 사실을 알림
            }
        }
        public string Board_IP_CH2 { get; set; } = "192.168.0.102";
        public int Board_Port_CH2 { get; set; } = 5001;
        public string Device_Port_CH2 { get; set; } = "None";
        public int Device_BaudRate_CH2 { get; set; } = 115200;
        public string Qr_Port_CH2 { get; set; } = "None";
        public int Qr_BaudRate_CH2 { get; set; } = 9600;
        public string Module_Port_CH2 { get; set; } = "None";
        public int Module_BaudRate_CH2 { get; set; } = 115200;


        #endregion

        #region CH3

        private bool _use_ch3;
        public bool Use_CH3
        {
            get => _use_ch3;
            set
            {
                if (_use_ch3 == value) return; // 값이 같으면 무시 (무한 루프 방지)
                _use_ch3 = value;
                OnPropertyChanged("Use_CH3"); // UI에 변경 사실을 알림
            }
        }
        public string Board_IP_CH3 { get; set; } = "192.168.0.102";
        public int Board_Port_CH3 { get; set; } = 5002;
        public string Device_Port_CH3 { get; set; } = "None";
        public int Device_BaudRate_CH3 { get; set; } = 115200;
        public string Qr_Port_CH3 { get; set; } = "None";
        public int Qr_BaudRate_CH3 { get; set; } = 9600;
        public string Module_Port_CH3 { get; set; } = "None";
        public int Module_BaudRate_CH3 { get; set; } = 115200;


        #endregion

        #region CH4
        private bool _use_ch4;
        public bool Use_CH4
        {
            get => _use_ch4;
            set
            {
                if (_use_ch4 == value) return; // 값이 같으면 무시 (무한 루프 방지)
                _use_ch4 = value;
                OnPropertyChanged("Use_CH4"); // UI에 변경 사실을 알림
            }
        }
        public string Board_IP_CH4 { get; set; } = "192.168.0.103";
        public int Board_Port_CH4 { get; set; } = 5003;
        public string Device_Port_CH4 { get; set; } = "None";
        public int Device_BaudRate_CH4 { get; set; } = 115200;
        public string Qr_Port_CH4 { get; set; } = "None";
        public int Qr_BaudRate_CH4 { get; set; } = 9600;
        public string Module_Port_CH4 { get; set; } = "None";
        public int Module_BaudRate_CH4 { get; set; } = 115200;

        #endregion

        public string Jig_Port { get; set; } = "None";
        public int Jig_BaudRate { get; set; } = 9600;
        public string Recipe_Qr_Port { get; set; } = "None";
        public int Recipe_Qr_BaudRate { get; set; } = 9600;
        public bool Use_1CH_Only { get; set; } = false;

        public bool Use_TxRx_Console_Board { get; set; } = true;
        public bool Use_TxRx_Console_Pba { get; set; } = true;

        public bool Use_Debug_Mode { get; set; } = false;

        public bool Use_Board_Retry { get; set; } = true;
        public bool Use_Pba_Retry { get; set; } = true;

        public int Board_Retry_Count { get; set; } = 3;
        public int Pba_Retry_Count { get; set; } = 3;


        #region Production
        // --- 핵심 데이터 (이 값들이 바뀌면 나머지는 자동 계산됨) ---
        private int _okCount_Ch1; public int OkCount_Ch1 { get => _okCount_Ch1; set { lock (_lock) { _okCount_Ch1 = value; } NotifyUpdates(); } }
        private int _ngCount_Ch1; public int NgCount_Ch1 { get => _ngCount_Ch1; set { lock (_lock) { _ngCount_Ch1 = value; } NotifyUpdates(); } }

        private int _okCount_Ch2; public int OkCount_Ch2 { get => _okCount_Ch2; set { lock (_lock) { _okCount_Ch2 = value; } NotifyUpdates(); } }
        private int _ngCount_Ch2; public int NgCount_Ch2 { get => _ngCount_Ch2; set { lock (_lock) { _ngCount_Ch2 = value; } NotifyUpdates(); } }

        private int _okCount_Ch3; public int OkCount_Ch3 { get => _okCount_Ch3; set { lock (_lock) { _okCount_Ch3 = value; } NotifyUpdates(); } }
        private int _ngCount_Ch3; public int NgCount_Ch3 { get => _ngCount_Ch3; set { lock (_lock) { _ngCount_Ch3 = value; } NotifyUpdates(); } }

        private int _okCount_Ch4; public int OkCount_Ch4 { get => _okCount_Ch4; set { lock (_lock) { _okCount_Ch4 = value; } NotifyUpdates(); } }
        private int _ngCount_Ch4; public int NgCount_Ch4 { get => _ngCount_Ch4; set { lock (_lock) { _ngCount_Ch4 = value; } NotifyUpdates(); } }


        // --- [자동 계산 영역] 채널별 Total ---
        public int TotalCount_Ch1 => OkCount_Ch1 + NgCount_Ch1;
        public int TotalCount_Ch2 => OkCount_Ch2 + NgCount_Ch2;
        public int TotalCount_Ch3 => OkCount_Ch3 + NgCount_Ch3;
        public int TotalCount_Ch4 => OkCount_Ch4 + NgCount_Ch4;


        // --- [자동 계산 영역] 전체 생산 합계 ---
        public int OkCount => OkCount_Ch1 + OkCount_Ch2 + OkCount_Ch3 + OkCount_Ch4;
        public int NgCount => NgCount_Ch1 + NgCount_Ch2 + NgCount_Ch3 + NgCount_Ch4;
        public int TotalCount => OkCount + NgCount;


        // --- [자동 계산 영역] 불량률 및 양품률 ---
        public double OkRate => TotalCount == 0 ? 0 : (double)OkCount / TotalCount;
        public double NgRate => TotalCount == 0 ? 0 : (double)NgCount / TotalCount;
        public void ResetAllCounts()
        {
            _okCount_Ch1 = 0; _ngCount_Ch1 = 0;
            _okCount_Ch2 = 0; _ngCount_Ch2 = 0;
            _okCount_Ch3 = 0; _ngCount_Ch3 = 0;
            _okCount_Ch4 = 0; _ngCount_Ch4 = 0;

            NotifyUpdates(); // 모든 UI를 0으로 갱신
        }

        #endregion


        // --- 핵심 로직: 값 변경 알림 ---
        private void NotifyUpdates()
        {
            // 인자값으로 string.Empty를 보내면 이 클래스에 바인딩된 모든 UI가 갱신됩니다.
            // 개별적으로 OnPropertyChanged를 수십번 호출할 필요가 없어 매우 효율적입니다.
            OnPropertyChanged(string.Empty);
        }





        #region 판정 값

        public List<string> RunTaskList { get; set; } = new List<string>(); // 실행할 테스트 목록
        public string CurrentRecipeFile { get; set; } = ""; // 현재 불러온 레시피 파일명

        #region INTERLOCK 
        public bool INTERLOCK_Enable { get; set; } = true;

        #endregion

        #region QR READ (0)
        public bool QR_READ_Enable { get; set; } = true;
        public int QR_READ_Step_Delay { get; set; } = 30;
        public int QR_READ_Len { get; set; } = 16;
        #endregion
        
        #region MCU INFO (1)
        public bool MCU_INFO_Enable { get; set; } = true;
        public int MCU_INFO_Step_Delay { get; set; } = 200;
        public int MCU_INFO_Pba_Delay { get; set; } = 200;
        public int MCU_INFO_Tcp_01_Delay { get; set; } = 100;
        public int MCU_INFO_Tcp_02_Delay { get; set; } = 100;
        public int MCU_INFO_Mcu_Id_Len { get; set; } = 16;

        #endregion

        #region SDP
        public bool SDP_Enable { get; set; } = true;
        public int SDP_Step_Delay { get; set; } = 10;
        public int SDP_Pba_Delay { get; set; } = 10;
        public int SDP_TA_Delay { get; set; } = 2000;
        public int SDP_Tcp_01_Delay { get; set; } = 1000;
        public int SDP_Tcp_02_Delay { get; set; } = 1000;
        public short SDP_TA_Type { get; set; } = 3;
        public float SDP_USB_Current_Min { get; set; } = 350;
        public float SDP_USB_Current_Max { get; set; } = 530;
        public short SDP_CHG_Current_Min { get; set; } = 1800;
        public short SDP_CHG_Current_Max { get; set; } = 2200;
        #endregion

        #region DCP
        public bool DCP_Enable { get; set; } = true;
        public int DCP_Step_Delay { get; set; } = 10;
        public int DCP_Pba_Delay { get; set; } = 10;
        public int DCP_TA_Delay { get; set; } = 2000;
        public int DCP_Tcp_01_Delay { get; set; } = 1000;
        public int DCP_Tcp_02_Delay { get; set; } = 1000;
        public short DCP_TA_Type { get; set; } = 1;
        public float DCP_USB_Current_Min { get; set; } = 1440;
        public float DCP_USB_Current_Max { get; set; } = 2130;
        public short DCP_CHG_Current_Min { get; set; } = 1800;
        public short DCP_CHG_Current_Max { get; set; } = 2200;
        #endregion

        #region HVDCP
        public bool HVDCP_Enable { get; set; } = true;
        public int HVDCP_Step_Delay { get; set; } = 10;
        public int HVDCP_Pba_Delay { get; set; } = 10;
        public int HVDCP_TA_Delay { get; set; } = 2000;
        public int HVDCP_Tcp_01_Delay { get; set; } = 1000;
        public int HVDCP_Tcp_02_Delay { get; set; } = 1000;
        public short HVDCP_TA_Type { get; set; } = 2;
        public float HVDCP_USB_Current_Min { get; set; } = 2070;
        public float HVDCP_USB_Current_Max { get; set; } = 2530;
        public short HVDCP_CHG_Current_Min { get; set; } = 4140;
        public short HVDCP_CHG_Current_Max { get; set; } = 5060;

        #endregion
        #region MES
        public bool MES_Enable { get; set; } = true;
        #endregion

        //-------------------------------------

        #endregion

        #region MES
        //DB
        public bool USE_MES { get; set; } = false;
        public string DB_IP { get; set; }
        public string DB_PORT { get; set; } = "1433";
        public string DB_NAME { get; set; }
        public string DB_TABLE { get; set; }
        public string DB_USER { get; set; }
        public string DB_PW { get; set; }
        public bool USE_INTERLOCK { get; set; }
        public string INTERLOCK_PROCEDURE_1 { get; set; }

        #endregion

        #region FTP
        //FTP
        public bool USE_FTP { get; set; } = false;
        public string FTP_HOST { get; set; }
        public string FTP_PORT { get; set; }
        public string FTP_USER { get; set; }
        public string FTP_PW { get; set; }
        public string FTP_BASE_DIR { get; set; }
        public bool FTP_USE_SSL { get; set; } = false;

        #endregion

        private string _nowTime = DateTime.Now.ToString("yyyy-MM-dd : HH:mm:ss");
        public string NowTime
        {
            get => _nowTime;
            set
            {
                if (_nowTime != value)
                {
                    _nowTime = value;
                    OnPropertyChanged(nameof(NowTime));
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler == null) return;

            // 현재 열려있는 폼 중 Invoke가 가능한 아무 폼이나 찾음
            Form target = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.IsHandleCreated);

            if (target != null && target.InvokeRequired)
            {
                target.BeginInvoke(new Action(() => handler.Invoke(this, new PropertyChangedEventArgs(propertyName))));
            }
            else
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //전역 단일 인스턴스
        private static Settings _instance;
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Load();
                return _instance;
            }
        }

        private static readonly string filePath = Path.Combine(
            System.Windows.Forms.Application.StartupPath, "config.json");

        


        public void Save()
        {
            try
            {
                string dir = Path.GetDirectoryName(filePath);
                if (dir != null)
                    Directory.CreateDirectory(dir);

                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    $"설정 저장 오류: {ex.Message}",
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public static Settings Load()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var serializerSettings = new JsonSerializerSettings
                    {
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    };
                    return JsonConvert.DeserializeObject<Settings>(json, serializerSettings) ?? new Settings();
                }
                else
                {
                    return new Settings();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    $"설정 불러오기 오류: {ex.Message}",
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return new Settings();
            }
        }
    }
}
