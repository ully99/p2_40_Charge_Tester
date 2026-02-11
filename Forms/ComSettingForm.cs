using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using p2_40_Charge_Tester.Data;
using p2_40_Charge_Tester.Communication;

namespace p2_40_Charge_Tester.Forms
{
    public partial class ComSettingForm : Form
    {
        #region Field
        public MainForm mainform;

        #endregion

        #region Init

        public ComSettingForm(MainForm parentform)
        {
            this.mainform = parentform;
            InitializeComponent();
            RescanSerialPort();
            LoadSetting();
            ShowConnectStatus();
        }

        private void RescanSerialPort()
        {
            try
            {
                string[] portNames = SerialPort.GetPortNames();

                ComboBox[] boxes =
                {
                    cboxQrPortCh1, cboxQrPortCh2, cboxQrPortCh3, cboxQrPortCh4,
                    cboxModulePortCh1, cboxModulePortCh2, cboxModulePortCh3, cboxModulePortCh4,
                    cboxJigPort, cboxRecipeQrPort,
                };

                foreach (var box in boxes)
                {
                    box.Items.Clear();
                    box.Items.Add("None");
                    box.Items.AddRange(portNames);
                    box.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("포트 스캔 중 에러 발생! " + ex.Message);
            }
        }

        private void LoadSetting()
        {
            //ch1
            SwitchUseCh1.Checked = Settings.Instance.Use_CH1;
            tboxTcpCh1Ip.Text = Settings.Instance.Board_IP_CH1;
            tboxTcpCh1Port.Text = Settings.Instance.Board_Port_CH1.ToString();

            tboxPbaPortCh1.Text = Settings.Instance.Device_Port_CH1;
            cboxPbaBaudRateCh1.Text = Settings.Instance.Device_BaudRate_CH1.ToString();

            cboxQrPortCh1.Text = Settings.Instance.Qr_Port_CH1;
            cboxQrBaudRateCh1.Text = Settings.Instance.Qr_BaudRate_CH1.ToString();

            cboxModulePortCh1.Text = Settings.Instance.Module_Port_CH1;
            cboxModuleBaudRateCh1.Text = Settings.Instance.Module_BaudRate_CH1.ToString();

            //ch2
            SwitchUseCh2.Checked = Settings.Instance.Use_CH2;
            tboxTcpCh2Ip.Text = Settings.Instance.Board_IP_CH2;
            tboxTcpCh2Port.Text = Settings.Instance.Board_Port_CH2.ToString();

            tboxPbaPortCh2.Text = Settings.Instance.Device_Port_CH2;
            cboxPbaBaudRateCh2.Text = Settings.Instance.Device_BaudRate_CH2.ToString();

            cboxQrPortCh2.Text = Settings.Instance.Qr_Port_CH2;
            cboxQrBaudRateCh2.Text= Settings.Instance.Qr_BaudRate_CH2.ToString();

            cboxModulePortCh2.Text = Settings.Instance.Module_Port_CH2;
            cboxModuleBaudRateCh2.Text = Settings.Instance.Module_BaudRate_CH2.ToString();

            //ch3
            SwitchUseCh3.Checked = Settings.Instance.Use_CH3;
            tboxTcpCh3Ip.Text = Settings.Instance.Board_IP_CH3;
            tboxTcpCh3Port.Text = Settings.Instance.Board_Port_CH3.ToString();

            tboxPbaPortCh3.Text = Settings.Instance.Device_Port_CH3;
            cboxPbaBaudRateCh3.Text= Settings.Instance.Device_BaudRate_CH3.ToString();

            cboxQrPortCh3.Text= Settings.Instance.Qr_Port_CH3;
            cboxQrBaudRateCh3.Text = Settings.Instance.Qr_BaudRate_CH3.ToString();

            cboxModulePortCh3.Text = Settings.Instance.Module_Port_CH3;
            cboxModuleBaudRateCh3.Text = Settings.Instance.Module_BaudRate_CH3.ToString();

            //ch4
            SwitchUseCh4.Checked = Settings.Instance.Use_CH4;
            tboxTcpCh4Ip.Text = Settings.Instance.Board_IP_CH4;
            tboxTcpCh4Port.Text = Settings.Instance.Board_Port_CH4.ToString();

            tboxPbaPortCh4.Text = Settings.Instance.Device_Port_CH4;
            cboxPbaBaudRateCh4.Text= Settings.Instance.Device_BaudRate_CH4.ToString();

            cboxQrPortCh4.Text = Settings.Instance.Qr_Port_CH4;
            cboxQrBaudRateCh4.Text = Settings.Instance.Qr_BaudRate_CH4.ToString();

            cboxModulePortCh4.Text = Settings.Instance.Module_Port_CH4;
            cboxModuleBaudRateCh4.Text = Settings.Instance.Module_BaudRate_CH4.ToString();

            //Jig
            cboxJigPort.Text = Settings.Instance.Jig_Port;
            cboxJigBaudRate.Text = Settings.Instance.Jig_BaudRate.ToString();

            //Recipe Qr
            cboxRecipeQrPort.Text = Settings.Instance.Recipe_Qr_Port;
            cboxRecipeQrBaudRate.Text = Settings.Instance.Recipe_Qr_BaudRate.ToString();

            //function setting
            checkbox1ChOnly.Checked = Settings.Instance.Use_1CH_Only;
            checkboxTxRxConsole_board.Checked = Settings.Instance.Use_TxRx_Console_Board;
            checkboxTxRxConsole_pba.Checked = Settings.Instance.Use_TxRx_Console_Pba;
            checkboxDebugMode.Checked = Settings.Instance.Use_Debug_Mode;

            //value setting
            tboxBoardConnectTimeOut.Text = Settings.Instance.Board_Connect_Timeout.ToString();
            tboxPbaReadTimeOut.Text = Settings.Instance.Pba_Read_Timeout.ToString();
            tboxBoardReadTimeOut.Text = Settings.Instance.Board_Read_Timeout.ToString();
            checkboxBoardRetry.Checked = Settings.Instance.Use_Board_Retry;
            checkboxPbaRetry.Checked = Settings.Instance.Use_Pba_Retry;
            tboxBoardRetryCount.Text = Settings.Instance.Board_Retry_Count.ToString();
            tboxPbaRetryCount.Text = Settings.Instance.Pba_Retry_Count.ToString();
            tboxBoardMinInterval.Text = Settings.Instance.Board_Min_Interval.ToString();
            tboxPbaMinInterval.Text = Settings.Instance.Pba_Min_Interval.ToString();
            tboxPbaOnDelay.Text = Settings.Instance.Pba_On_Delay.ToString();
        }



        #endregion

        #region Event
        private void btnRescan_Click(object sender, EventArgs e)
        {
            RescanSerialPort();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            SettingValueSave();

            var loadingForm = new LoadingForm("Connecting...") { Owner = this, StartPosition = FormStartPosition.CenterScreen };
            loadingForm.Show();
            Application.DoEvents();
            try
            {
                await CommManager.ConnectAllComponent(Settings.Instance.Board_Connect_Timeout);
                LoadSetting();
                ShowConnectStatus();
            }
            finally
            {
                loadingForm.Close();
                loadingForm.Dispose();
            }
        }
        private void btnOpenMesSetting_Click(object sender, EventArgs e)
        {
            var form = new DBSettingForm(this)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.ShowDialog(this);
        }

        private void btnOpenFtpSetting_Click(object sender, EventArgs e)
        {
            var form = new FtpSettingForm(this)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.ShowDialog(this);
        }
        private async void btnClose_Click(object sender, EventArgs e)
        {
            SettingValueSave();

            var loadingForm = new LoadingForm("Connecting...") { Owner = this, StartPosition = FormStartPosition.CenterScreen };
            loadingForm.Show();
            Application.DoEvents();
            try
            {
                await CommManager.ConnectAllComponent(Settings.Instance.Board_Connect_Timeout);
                mainform.UpdateLayoutByChannelConfig();
                mainform.ResetRecipeInfo();
            }
            finally
            {
                loadingForm.Close();
                loadingForm.Dispose();
            }
            this.Close();
        }

        private void ComSettingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainform.CheckMesStatus();
            mainform.CheckFtpStatus();
        }

        #endregion

        #region Connect Status

        private void ShowConnectStatus()
        {
            var ConnectedStatus = CommManager.GetAllBoardsConnected();

            SetStatusLabel(lblCh1Status, ConnectedStatus[0]);
            SetStatusLabel(lblCh2Status, ConnectedStatus[1]);
            SetStatusLabel(lblCh3Status, ConnectedStatus[2]);
            SetStatusLabel(lblCh4Status, ConnectedStatus[3]);

            // QR Ch1 ~ Ch3 
            SetStatusLabel(lblQrStatusCh1, CommManager.QrPorts[0].IsOpen);
            SetStatusLabel(lblQrStatusCh2, CommManager.QrPorts[1].IsOpen);
            SetStatusLabel(lblQrStatusCh3, CommManager.QrPorts[2].IsOpen);
            SetStatusLabel(lblQrStatusCh4, CommManager.QrPorts[3].IsOpen);

            // JIG
            //UpdateStatusLabel(lblJigStatus, CommManager.Jig.IsOpened);

            //Recipe Qr
            SetStatusLabel(lblRecipeQrStatus, CommManager.RecipeQr.IsOpen);

            // Module (PowerMeter) Ch1~4
            SetStatusLabel(lblModuleStatusCh1, CommManager.PowerMeterPorts[0].IsOpen);
            SetStatusLabel(lblModuleStatusCh2, CommManager.PowerMeterPorts[1].IsOpen);
            SetStatusLabel(lblModuleStatusCh3, CommManager.PowerMeterPorts[2].IsOpen);
            SetStatusLabel(lblModuleStatusCh4, CommManager.PowerMeterPorts[3].IsOpen);
        }

        private void SetStatusLabel(Label lbl, bool isConnected)
        {
            lbl.Text = isConnected ? "Connected" : "Not Connected";
            lbl.BackColor = isConnected ? Color.LightGreen : Color.Gainsboro;
            lbl.ForeColor = Color.Black;
        }

        #endregion

        #region Save
        private void SettingValueSave()
        {
            try
            {
                //ch1
                Settings.Instance.Use_CH1 = SwitchUseCh1.Checked;
                Settings.Instance.Board_IP_CH1 = tboxTcpCh1Ip.Text?.Trim();
                int.TryParse(tboxTcpCh1Port.Text, out int tcpPort_ch1);
                Settings.Instance.Board_Port_CH1 = tcpPort_ch1;

                Settings.Instance.Device_Port_CH1 = tboxPbaPortCh1.Text?.Trim();
                int.TryParse(cboxPbaBaudRateCh1.Text, out int pbaBaudRate_ch1);
                Settings.Instance.Device_BaudRate_CH1 = pbaBaudRate_ch1;


                Settings.Instance.Qr_Port_CH1 = cboxQrPortCh1.SelectedItem?.ToString().Trim();
                int.TryParse(cboxQrBaudRateCh1.Text, out int qrBaudRate_ch1);
                Settings.Instance.Qr_BaudRate_CH1 = qrBaudRate_ch1;

                Settings.Instance.Module_Port_CH1 = cboxModulePortCh1.SelectedItem?.ToString().Trim();
                int.TryParse(cboxModuleBaudRateCh1.Text, out int moduleBaud_ch1);
                Settings.Instance.Module_BaudRate_CH1 = moduleBaud_ch1 > 0 ? moduleBaud_ch1 : 115200;

                //ch2
                Settings.Instance.Use_CH2 = SwitchUseCh2.Checked;
                Settings.Instance.Board_IP_CH2 = tboxTcpCh2Ip.Text?.Trim();
                int.TryParse(tboxTcpCh2Port.Text, out int tcpPort_ch2);
                Settings.Instance.Board_Port_CH2 = tcpPort_ch2;

                Settings.Instance.Device_Port_CH2 = tboxPbaPortCh2.Text?.Trim();
                int.TryParse(cboxPbaBaudRateCh2.Text, out int pbaBaudRate_ch2);
                Settings.Instance.Device_BaudRate_CH2 = pbaBaudRate_ch2;


                Settings.Instance.Qr_Port_CH2 = cboxQrPortCh2.SelectedItem?.ToString().Trim();
                int.TryParse(cboxQrBaudRateCh2.Text, out int qrBaudRate_ch2);
                Settings.Instance.Qr_BaudRate_CH2 = qrBaudRate_ch2;

                Settings.Instance.Module_Port_CH2 = cboxModulePortCh2.SelectedItem?.ToString().Trim();
                int.TryParse(cboxModuleBaudRateCh2.Text, out int moduleBaud_ch2);
                Settings.Instance.Module_BaudRate_CH2 = moduleBaud_ch2 > 0 ? moduleBaud_ch2 : 115200;

                //ch3
                Settings.Instance.Use_CH3 = SwitchUseCh3.Checked;
                Settings.Instance.Board_IP_CH3 = tboxTcpCh3Ip.Text?.Trim();
                int.TryParse(tboxTcpCh3Port.Text, out int tcpPort_ch3);
                Settings.Instance.Board_Port_CH3 = tcpPort_ch3;

                Settings.Instance.Device_Port_CH3 = tboxPbaPortCh3.Text?.Trim();
                int.TryParse(cboxPbaBaudRateCh3.Text, out int pbaBaudRate_ch3);
                Settings.Instance.Device_BaudRate_CH3 = pbaBaudRate_ch3;


                Settings.Instance.Qr_Port_CH3 = cboxQrPortCh3.SelectedItem?.ToString().Trim();
                int.TryParse(cboxQrBaudRateCh3.Text, out int qrBaudRate_ch3);
                Settings.Instance.Qr_BaudRate_CH3 = qrBaudRate_ch3;

                Settings.Instance.Module_Port_CH3 = cboxModulePortCh3.SelectedItem?.ToString().Trim();
                int.TryParse(cboxModuleBaudRateCh3.Text, out int moduleBaud_ch3);
                Settings.Instance.Module_BaudRate_CH3 = moduleBaud_ch3 > 0 ? moduleBaud_ch3 : 115200;

                //ch4
                Settings.Instance.Use_CH4 = SwitchUseCh4.Checked;
                Settings.Instance.Board_IP_CH4 = tboxTcpCh4Ip.Text?.Trim();
                int.TryParse(tboxTcpCh4Port.Text, out int tcpPort_ch4);
                Settings.Instance.Board_Port_CH4 = tcpPort_ch4;

                Settings.Instance.Device_Port_CH4 = tboxPbaPortCh4.Text?.Trim();
                int.TryParse(cboxPbaBaudRateCh4.Text, out int pbaBaudRate_ch4);
                Settings.Instance.Device_BaudRate_CH4 = pbaBaudRate_ch4;


                Settings.Instance.Qr_Port_CH4 = cboxQrPortCh4.SelectedItem?.ToString().Trim();
                int.TryParse(cboxQrBaudRateCh4.Text, out int qrBaudRate_ch4);
                Settings.Instance.Qr_BaudRate_CH4 = qrBaudRate_ch4;

                Settings.Instance.Module_Port_CH4 = cboxModulePortCh4.SelectedItem?.ToString().Trim();
                int.TryParse(cboxModuleBaudRateCh4.Text, out int moduleBaud_ch4);
                Settings.Instance.Module_BaudRate_CH4 = moduleBaud_ch4 > 0 ? moduleBaud_ch4 : 115200;

                //Jig
                Settings.Instance.Jig_Port = cboxJigPort.SelectedItem?.ToString().Trim();
                int.TryParse(cboxJigBaudRate.Text, out int jigBaudRate);
                Settings.Instance.Jig_BaudRate = jigBaudRate;

                //Recipe Qr
                Settings.Instance.Recipe_Qr_Port = cboxRecipeQrPort.SelectedItem?.ToString().Trim();
                int.TryParse(cboxRecipeQrBaudRate.Text, out int RecipeQrBaudRate);
                Settings.Instance.Recipe_Qr_BaudRate = RecipeQrBaudRate;

                //function setting
                Settings.Instance.Use_1CH_Only = checkbox1ChOnly.Checked;
                Settings.Instance.Use_TxRx_Console_Board = checkboxTxRxConsole_board.Checked;
                Settings.Instance.Use_TxRx_Console_Pba = checkboxTxRxConsole_pba.Checked;
                Settings.Instance.Use_Debug_Mode = checkboxDebugMode.Checked;

                //value setting
                Settings.Instance.Board_Connect_Timeout = int.Parse(tboxBoardConnectTimeOut.Text);
                Settings.Instance.Pba_Read_Timeout = int.Parse(tboxPbaReadTimeOut.Text);
                Settings.Instance.Board_Read_Timeout = int.Parse(tboxBoardReadTimeOut.Text);
                Settings.Instance.Use_Board_Retry = checkboxBoardRetry.Checked;
                Settings.Instance.Use_Pba_Retry = checkboxPbaRetry.Checked;
                Settings.Instance.Board_Retry_Count = int.Parse(tboxBoardRetryCount.Text);
                Settings.Instance.Pba_Retry_Count = int.Parse(tboxPbaRetryCount.Text);
                Settings.Instance.Board_Min_Interval = int.Parse(tboxBoardMinInterval.Text);
                Settings.Instance.Pba_Min_Interval = int.Parse(tboxPbaMinInterval.Text);
                Settings.Instance.Pba_On_Delay = int.Parse(tboxPbaOnDelay.Text);

                if (checkbox1ChOnly.Checked)
                {
                    if (Settings.Instance.Use_CH2) Settings.Instance.Use_CH2 = false;
                    if (Settings.Instance.Use_CH3) Settings.Instance.Use_CH3 = false;
                    if (Settings.Instance.Use_CH4) Settings.Instance.Use_CH4 = false;
                }

                Settings.Instance.Save();
                Console.WriteLine("설정 저장 완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"설정 저장 실패! {ex.Message}");
            }
        }






        #endregion

        
    }
}
