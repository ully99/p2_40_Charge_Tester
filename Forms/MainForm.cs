using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using p2_40_Charge_Tester.Forms;
using p2_40_Charge_Tester.Communication;
using p2_40_Charge_Tester.Data;
using p2_40_Charge_Tester.UserControls;
using p2_40_Charge_Tester.UI;
using p2_40_Charge_Tester.LIB;
using System.Data.OleDb;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.IO;
using System.IO.Ports;


namespace p2_40_Charge_Tester
{
    public partial class MainForm : Form
    {
        #region Field
        private bool _isTestRunning = false;
        private CancellationTokenSource _cts; // 중간에 STOP 누르면 취소하기 위해 사용

        #endregion


        #region Init
        public MainForm()
        {
            InitializeComponent();
            ConnectEvent();
        }



        private void StartLoadingAnimation()
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
        }

        private void StopLoadingAnimation()
        {
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Value = 100;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            Init_ChControls();
            Init_McuLotModel_Value();
            UpdateLayoutByChannelConfig();

            var loadingForm = new LoadingForm("Connecting...") { Owner = this, StartPosition = FormStartPosition.CenterScreen };
            loadingForm.Show();
            Application.DoEvents();
            try
            {
                await CommManager.ConnectAllComponent(Settings.Instance.Board_Connect_Timeout);
                SettingMesImage();
                SettingFtpImage();
            }
            finally
            {
                loadingForm.Close();
                loadingForm.Dispose();
            }
        }

        private void Init_ChControls()
        {
            chControl_ch1.Init(1);
            chControl_ch2.Init(2);
            chControl_ch3.Init(3);
            chControl_ch4.Init(4);
        }

        private void Init_McuLotModel_Value()
        {
            lblValueMcuNo.Text = Settings.Instance.Mcu_No;
            lblValueLotNo.Text = Settings.Instance.Lot_No;
            lblValueModel.Text = Settings.Instance.Model_No;
        }

        public void UpdateLayoutByChannelConfig()
        {
            bool is1ChOnly = Settings.Instance.Use_1CH_Only;

            

            tableLayoutPanel_Channels.SuspendLayout(); // 깜빡임 방지
            if (is1ChOnly)
            {
                Console.WriteLine("1CH 레이아웃 설정.");
                // 1채널만 크게 보여주고 나머지는 숨기는 로직
                chControl_ch2.Visible = false;
                chControl_ch3.Visible = false;
                chControl_ch4.Visible = false;

                int totalColums = tableLayoutPanel_Channels.ColumnCount;
                tableLayoutPanel_Channels.SetColumnSpan(chControl_ch1, totalColums);

                tableLayoutPanel_Channels.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                
            }
            else
            {
                Console.WriteLine("다채널 레이아웃 설정.");

                // 다시 4채널 모드로 복구 (기존 배치가 TableLayoutPanel이라면 컨트롤만 다시 보이게)

                tableLayoutPanel_Channels.SetColumnSpan(chControl_ch1, 1);
                chControl_ch2.Visible = true;
                chControl_ch3.Visible = true;
                chControl_ch4.Visible = true;

                tableLayoutPanel_Channels.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            }

            tableLayoutPanel_Channels.ResumeLayout();
        }

        private void ToggleLogComm(TcpChannelClient board, bool enable, RichTextLogger logger)
        {
            if (board == null) return;

            if (enable)
            {
                
                board.LogCommToUI = (tbox, msg, isTx) =>
                {
                    if (isTx) logger.Tx(msg);
                    else logger.Rx(msg);
                };
            }
            else
            {
                board.LogCommToUI = null;
                board.LogTarget = null;
            }
        }

        private void ToggleLogComm(SerialChannelPort device, bool enable, RichTextLogger logger)
        {
            if (device == null) return;

            if (enable)
            {
                // device.LogTarget = ... (SerialPort도 필요하다면 설정)
                device.LogCommToUI = (tbox, msg, isTx) =>
                {
                    if (isTx) logger.Tx(msg);
                    else logger.Rx(msg);
                };
            }
            else
            {
                device.LogCommToUI = null;
            }
        }

        private void ToggleLogComm(QrChannelPort Qr, bool enable, RichTextLogger logger)
        {
            if (Qr == null) return;

            if (enable)
            {
                Qr.LogCommToUI = (tbox, msg, isTx) =>
                {
                    if (isTx) logger.Tx(msg);
                    else logger.Rx(msg);
                };
            }
            else
            {
                Qr.LogCommToUI = null;
            }
        }


        #endregion

        #region Event
        private void ConnectEvent()
        {
            Application.ApplicationExit += OnApplicationExit;
            this.btnComSettingsOpen.Click += btnComSettingsOpen_Click;
            this.btnRecipeSettingsOpen.Click += btnRecipeSettingsOpen_Click;
            this.btnCalibrationOpen.Click += btnCalibrationOpen_Click;
            this.btnManualOpen.Click += btnManualOpen_Click;
            this.lblValueMcuNo.DoubleClick += lblValueMcuNo_DoubleClick;
            this.lblValueLotNo.DoubleClick += lblValueLotNo_DoubleClick;
            this.lblValueModel.DoubleClick += lblValueModel_DoubleClick;
            this.btnRecipeOpen.Click += btnRecipeOpen_Click;
            this.btnStartStop.Click += btnStartStop_Click;
        }
 

        private void OnApplicationExit(object sender, EventArgs e)
        {
            Settings.Instance.Save();
        }

        private void btnManualOpen_Click(object sender, EventArgs e)
        {
            var form = new ManualForm(this)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.ShowDialog(this);
        }

        private void btnCalibrationOpen_Click(object sender, EventArgs e)
        {
            var form = new CalibrationForm(this)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.ShowDialog(this);
        }

        private void btnRecipeSettingsOpen_Click(object sender, EventArgs e)
        {
            if (!RequireLogin(this)) return;

            var form = new RecipeSettingForm(this)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.ShowDialog(this);
        }

        private void btnComSettingsOpen_Click(object sender, EventArgs e)
        {
            var form = new ComSettingForm(this)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.ShowDialog(this);
        }

        private bool RequireLogin(IWin32Window owner)
        {
            using (var dlg = new LoginForm())
            {
                dlg.StartPosition = FormStartPosition.CenterScreen;
                return dlg.ShowDialog(owner) == DialogResult.OK;
            }
        }

        private void lblValueModel_DoubleClick(object sender, EventArgs e)
        {
            var form = new McuLotModelSettingForm(this)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.ShowDialog(this);
        }

        private void lblValueLotNo_DoubleClick(object sender, EventArgs e)
        {
            var form = new McuLotModelSettingForm(this)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.ShowDialog(this);
        }

        private void lblValueMcuNo_DoubleClick(object sender, EventArgs e)
        {
            var form = new McuLotModelSettingForm(this)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.ShowDialog(this);
        }


        


        #endregion

        #region MES

        private enum MesConnState { Off, Ok, Fail }
        private void SettingMesImage()
        {
            // 상태바 기본 설정
            statusStripMes.ImageScalingSize = new Size(16, 16); // ← System.Drawing 필요
            statusStripMes.ShowItemToolTips = true;

            CheckMesStatus();
        }

        public async void CheckMesStatus()
        {
            if (!Settings.Instance.USE_MES)
            {
                SetMesStatus(MesConnState.Off, "MES 비활성화");
                return;
            }

            SetMesStatus(MesConnState.Fail);

            bool ok = await Task.Run(() =>
            {
                try
                {
                    // 세팅값을 리소스 이용해 연결문자열 구성
                    string cs =
                        $@"Provider=SQLOLEDB;Data Source={Settings.Instance.DB_IP},{Settings.Instance.DB_PORT};" +
                        $@"Initial Catalog={Settings.Instance.DB_NAME};User ID={Settings.Instance.DB_USER};" +
                        $@"Password={Settings.Instance.DB_PW};Connect Timeout=5;";

                    using (var conn = new OleDbConnection(cs))
                    {
                        conn.Open(); // 붙기만 확인
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            });

            //SetMesStatus(ok ? MesConnState.Ok : MesConnState.Fail);
            if (ok)
            {
                SetMesStatus(MesConnState.Ok, $"연결 성공: {Settings.Instance.DB_IP}");
            }
            else
            {
                SetMesStatus(MesConnState.Fail, "MES 서버에 연결할 수 없습니다. 설정을 확인하세요.");
            }

        }

        private void SetMesStatus(MesConnState state, string tip = null)
        {
            switch (state)
            {
                case MesConnState.Off:
                    lblMesStatus.Image = Properties.Resources.LED_OFF_SM;
                    lblMesStatus.Text = "MES OFF";
                    break;

                case MesConnState.Ok:
                    lblMesStatus.Image = Properties.Resources.LED_GREEN_SM;
                    lblMesStatus.Text = "MES ON";
                    break;

                case MesConnState.Fail:
                    lblMesStatus.Image = Properties.Resources.LED_RED_SM;
                    lblMesStatus.Text = "MES Connect Fail";
                    break;
            }
            if (!string.IsNullOrEmpty(tip))
                lblMesStatus.ToolTipText = tip;
        }
        #endregion

        #region FTP
        private enum FtpConnState { Off, Ok, Fail }
        private void SettingFtpImage()
        {
            statusStripMes.ImageScalingSize = new Size(16, 16);
            statusStripMes.ShowItemToolTips = true;

            CheckFtpStatus();
        }

        private void SetFtpStatus(FtpConnState state, string tip = null)
        {
            switch (state)
            {
                case FtpConnState.Off:
                    lblFtpStatus.Image = Properties.Resources.LED_OFF_SM;
                    lblFtpStatus.Text = "FTP OFF";
                    break;

                case FtpConnState.Ok:
                    lblFtpStatus.Image = Properties.Resources.LED_GREEN_SM;
                    lblFtpStatus.Text = "FTP ON";
                    break;

                case FtpConnState.Fail:
                    lblFtpStatus.Image = Properties.Resources.LED_RED_SM;
                    lblFtpStatus.Text = "FTP Connect Fail";
                    break;
            }
            if (!string.IsNullOrEmpty(tip))
                lblFtpStatus.ToolTipText = tip;
        }

        public async void CheckFtpStatus()
        {
            // 1. FTP 사용 여부 확인
            if (!Settings.Instance.USE_FTP)
            {
                SetFtpStatus(FtpConnState.Off, "FTP 비활성화 상태");
                return;
            }

            // 포트 번호가 문자열이라 숫자로 변환이 필요 (기본값 21)
            if (!int.TryParse(Settings.Instance.FTP_PORT, out int port))
            {
                port = 21; //변환 실패하면 그냥 기본값 21사용
            }

            // 2. 연결 시도 중 상태 표시 (선택 사항, 필요하면 Fail 등으로 초기화)
            SetFtpStatus(FtpConnState.Fail, "연결 확인 중...");

            // 3. FtpProfiles.TestAsync를 사용하여 연결 테스트 진행
            // SSL은  false로 고정.
            bool isOk = await FtpProfiles.TestAsync(
                Settings.Instance.FTP_HOST,
                port,
                false, // FTP_USE_SSL 대신 false 고정
                Settings.Instance.FTP_BASE_DIR,
                Settings.Instance.FTP_USER,
                Settings.Instance.FTP_PW
            );

            
            // 4. 결과에 따른 UI 업데이트
            if (isOk)
            {
                SetFtpStatus(FtpConnState.Ok, $"연결 성공: {Settings.Instance.FTP_HOST}");
            }
            else
            {
                SetFtpStatus(FtpConnState.Fail, "FTP 서버에 연결할 수 없습니다. 설정을 확인하세요.");
            }


        }


        #endregion

        #region Recipe
        private void btnRecipeOpen_Click(object sender, EventArgs e)
        {
            // [추가] FTP 사용 중이면 로컬 레시피 열기 차단
            if (Settings.Instance.USE_FTP)
            {
                MessageBox.Show("FTP 사용 모드에서는 로컬 레시피 파일을 열 수 없습니다.\nFTP 설정을 해제해주세요.",
                                "열기 제한", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            openFileDialog.Title = "레시피 파일 열기";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(filePath);

                SettingNowTask(filePath, fileName);
            }
        }


        //JSON에 저장된 키 이름과 Settings의 프로퍼티이름이 같아야함!
        private void SettingNowTask(string filePath, string fileName)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                JObject recipe = JObject.Parse(json);

                // 1. 파일 형식 검사
                if (recipe["Type"]?.ToString() != "Recipe")
                {
                    MessageBox.Show("유효하지 않은 레시피 파일입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                JObject settingsJson = recipe["Settings"] as JObject;
                JArray taskOrderJson = recipe["TaskOrder"] as JArray;

                if (settingsJson == null || taskOrderJson == null)
                {
                    MessageBox.Show("설정값 또는 순서 정보가 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Settings.Instance에 값 자동 반영 (Reflection 사용)
                // JSON의 키 이름(예: "QR_READ_Len")과 Settings 속성 이름이 같으면 자동으로 값이 들어갑니다.
                var instance = Settings.Instance;
                Type type = typeof(Settings);

                foreach (JProperty prop in settingsJson.Properties())
                {
                    PropertyInfo pi = type.GetProperty(prop.Name);
                    if (pi != null && pi.CanWrite)
                    {
                        try
                        {
                            // JSON 값을 해당 속성의 타입(int, float, bool 등)으로 변환하여 설정
                            object value = prop.Value.ToObject(pi.PropertyType);
                            pi.SetValue(instance, value);
                        }
                        catch
                        {
                            Console.WriteLine($"값 적용 실패: {prop.Name}");
                        }
                    }
                }

                // 3. 실행할 Task 목록 생성 (Enable 체크 된 것만 순서대로 담기)
                List<string> newTaskList = new List<string>();

                foreach (var taskToken in taskOrderJson)
                {
                    string taskName = taskToken.ToString(); // 예: "QR READ"

                    // Task 이름으로 Enable 속성 찾기 (예: "QR READ" -> "QR_READ_Enable")
                    // 공백을 언더바(_)로 바꾸고 뒤에 _Enable을 붙이는 규칙 사용
                    string enablePropName = taskName.Replace(" ", "_") + "_Enable";

                    PropertyInfo piEnable = type.GetProperty(enablePropName);

                    if (piEnable != null)
                    {
                        bool isEnabled = (bool)piEnable.GetValue(instance);
                        if (isEnabled)
                        {
                            newTaskList.Add(taskName);
                        }
                    }
                }

                // 4. 결과 저장 및 UI 반영

                instance.RunTaskList = newTaskList;
                instance.CurrentRecipeFile = Path.GetFileName(filePath);
                instance.Save(); // config.json에도 저장

                if (tboxRecipeFile != null) tboxRecipeFile.Text = instance.CurrentRecipeFile;
                if (tboxRecipeFilePath != null) tboxRecipeFilePath.Text = filePath;

                string msg = $"레시피 적용 완료!\n파일명: {instance.CurrentRecipeFile}\n총 공정 수: {instance.RunTaskList.Count}개";
                MessageBox.Show(msg, "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (instance.RunTaskList != null && instance.RunTaskList.Count > 0)
                {
                    var chControls = new ChControl[] { chControl_ch1, chControl_ch2, chControl_ch3, chControl_ch4 };
                    bool[] Ch_Enables = new bool[] { instance.Use_CH1, instance.Use_CH2, instance.Use_CH3, instance.Use_CH4 };
                    for (int i = 0; i < chControls.Length; i++)
                    {
                        if (Ch_Enables[i])
                        {
                            chControls[i].SetRecipeList(instance.RunTaskList);
                            chControls[i].UpdateNowStatus(ChControl.NowStatus.READY);
                        }
                        else chControls[i].SetRecipeList(null); //사용상태가 아닌 채널은 리스트를 비워줌.
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"레시피 적용 중 오류 발생: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ResetRecipeInfo()
        {
            // 1. UI 텍스트박스 비우기
            if (tboxRecipeFile != null) tboxRecipeFile.Text = string.Empty;
            if (tboxRecipeFilePath != null) tboxRecipeFilePath.Text = string.Empty;

            // 2. 내부 설정값(Settings) 초기화
            Settings.Instance.CurrentRecipeFile = "";
            Settings.Instance.RunTaskList = null; // 리스트 날리기
            Settings.Instance.Save();

            // 3. 각 채널 그리드(리스트) 비우기
            var chControls = new ChControl[] { chControl_ch1, chControl_ch2, chControl_ch3, chControl_ch4 };
            foreach (var ch in chControls)
            {
                ch.ClearAllData();
            }
        }

        #endregion

        #region Main Task
        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (_isTestRunning)
            {
                // 실행 중이면 -> 정지 요청
                StopTestRoutine();
            }
            else
            {
                // 정지 상태면 -> 시작 요청
                StartTestRoutine();
            }
        }

        private async void StartTestRoutine()
        {
            if (string.IsNullOrEmpty(tboxRecipeFile.Text))
            {
                MessageBox.Show("Recipe를 선택해주세요");
                return;
            }
            // 이미 돌고 있으면 무시
            if (_isTestRunning) return;

            // 1) 상태 플래그 설정
            _isTestRunning = true;
            _cts = new CancellationTokenSource(); // 취소 토큰 생성

            // 2) UI 변경 (버튼을 STOP으로, 로딩바 마퀴 시작)
            btnStartStop.Text = "STOP";
            btnStartStop.BackColor = Color.IndianRed;
            btnStartStop.ForeColor = Color.Black;
            StartLoadingAnimation();

            // 3) 설정된 채널들만 테스트 시작
            var chControls = new ChControl[] { chControl_ch1, chControl_ch2, chControl_ch3, chControl_ch4 };
            bool[] chEnables = new bool[] { Settings.Instance.Use_CH1, Settings.Instance.Use_CH2, Settings.Instance.Use_CH3, Settings.Instance.Use_CH4 };

            List<Task> runningTasks = new List<Task>();

            for (int i = 0; i < chControls.Length; i++)
            {
                if (chEnables[i])
                {
                    // 각 채널별로 비동기 검사 시작
                    runningTasks.Add(RunSequenceAsync(chControls[i], i, _cts.Token));
                }
            }

            // 4) 모든 채널이 끝날 때까지 대기
            try
            {
                await Task.WhenAll(runningTasks);
            }
            catch (OperationCanceledException)
            {
                // Stop 버튼 눌러서 취소된 경우
                Console.WriteLine("Test Stopped by User/JIG");
            }
            finally
            {
                // 5) 모든 작업이 끝나면(혹은 취소되면) 마무리
                StopTestRoutine(isFinished: true);
            }
        }

        private void StopTestRoutine(bool isFinished = false)
        {
            // 사용자 강제 중단인 경우 토큰 취소 요청
            if (!isFinished && _cts != null)
            {
                _cts.Cancel();
            }

            _isTestRunning = false;

            // 로딩바 정지
            StopLoadingAnimation();

            // UI 원상복구 (버튼을 START로)
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => StopTestRoutine(true)));
                return;
            }

            btnStartStop.Text = "START";
            btnStartStop.BackColor = Color.DarkSeaGreen; // 원래 색상 (디자이너 설정값 확인 필요)
            btnStartStop.ForeColor = Color.Black;
        }

        // ★★★ 4. 핵심 시퀀스 로직 (각 채널별로 돌아가는 함수) ★★★
        private async Task RunSequenceAsync(ChControl ch, int chIdx, CancellationToken token)
        {
            var board = CommManager.Boards[chIdx];   // TCP
            var pba = CommManager.Pbas[chIdx];       // Serial
            var qr = CommManager.QrPorts[chIdx];     // QR


            try
            {
                //로그 연결
                ToggleLogComm(board, true, ch.CommLogger);
                ToggleLogComm(pba, true, ch.CommLogger);
                ToggleLogComm(qr, true, ch.CommLogger);

                // 1) 시작 전 초기화
                ch.StartInspection();

                List<string> taskList = Settings.Instance.RunTaskList;
                

                if (taskList == null || taskList.Count == 0)
                {
                    ch.StopInspection();
                    return;
                }

                bool totalResult = true; // 전체 결과 (하나라도 실패하면 false)

                // 2) 공정 리스트 순회
                for (int step = 0; step < taskList.Count; step++)
                {
                    await Task.Delay(100); //공정 사이 딜레이

                    // 스탑 버튼 눌렸는지 체크
                    if (token.IsCancellationRequested)
                    {
                        ch.StopInspection();
                        return;
                    }

                    string taskName = taskList[step];

                    // 2-1) 현재 단계 노란색(RUNNING) 표시
                    ch.UpdateItemStatus(step, ChControl.TaskStatus.RUNNING);

                    // 2-2) 실제 검사 함수 호출 (Data/TaskFunctions.cs)
                    bool isPass = await TaskFunctions.RunTestItem(taskName, chIdx, ch, token, totalResult);

                    // 2-3) 결과 처리
                    if (isPass)
                    {
                        ch.UpdateItemStatus(step, ChControl.TaskStatus.PASS); // 초록색
                    }
                    else
                    {
                        ch.UpdateItemStatus(step, ChControl.TaskStatus.FAIL); // 빨간색
                        totalResult = false; // 전체 실패

                        // ★ 디버그 모드가 아니면 여기서 즉시 중단
                        if (!Settings.Instance.Use_Debug_Mode)
                        {
                            // 남은 공정들 일괄 FAIL 처리 (빨간색 칠하기)
                            for (int remainStep = step + 1; remainStep < taskList.Count; remainStep++)
                            {
                                // FAIL로 찍거나, 구분하고 싶으면 SKIP 등의 상태를 만들어서 찍어도 됨
                                ch.UpdateItemStatus(remainStep, ChControl.TaskStatus.FAIL);
                            }

                            break;
                        }
                        // 디버그 모드(true)면 다음 루프로 계속 진행
                    }

                }

                ch.EndInspection(totalResult);
            }
            finally
            {
                ToggleLogComm(board, false, null);
                ToggleLogComm(pba, false, null);
                ToggleLogComm(qr, false, null);
            }
        }










        #endregion
    }
}
