using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using p2_40_Charge_Tester.Data;
using p2_40_Charge_Tester.Communication;
using p2_40_Charge_Tester.UI;
using p2_40_Charge_Tester.LIB;

namespace p2_40_Charge_Tester.Forms
{
    public partial class ManualForm : Form
    {
        #region Field
        MainForm mainform;
        TcpChannelClient board;


        #endregion


        #region Init
        public ManualForm(MainForm parentform)
        {
            this.mainform = parentform;
            InitializeComponent();
            ConnectEvent();
        }
        private void ManualForm_Load(object sender, EventArgs e)
        {
            cboxSelectChannel.SelectedIndex = 0;
            LoadInputBaseTable();
        }

        

        private void LoadInputBaseTable()
        {
            dgvInputBase.Rows.Clear();

            string[] items = {"PS1 Volt", "PS1 Curr", "PS1 VBUS", "PS1 SYS",
                "PS1 SYS 3V","PS1 MCU 33V","PS1 VBOOST", "PS1 HT BOOST",
                "PS1 SUB HT BOOST OUT", "PS1 KATO BOOST OUT",
                "PS1 Sleep Curr","PS1 Ship Curr",
                "PS1 VDD 3V", "PS1 LDC 3V"};

            foreach (var name in items)
            {
                int row = dgvInputBase.Rows.Add();
                dgvInputBase.Rows[row].Cells["colBaseName"].Value = name;
                dgvInputBase.Rows[row].Cells["colValue"].Value = "0";
            }
        }


        #endregion

        #region Utility
        private bool CheckChannel()
        {
            int ch = cboxSelectChannel.SelectedIndex;
            if (ch < 0 || CommManager.Boards[ch] == null || !CommManager.Boards[ch].IsConnected())
            {
                MessageBox.Show("this Channel is not connected!");
                return false;
            }

            return true;
        }

        private void AllClearSwitch()
        {
            for (int i = 0; i < 26; i++)
            {
                var cbox = this.Controls.Find($"cboxSw{i + 1}", true).FirstOrDefault() as CheckBox;
                if (cbox != null)
                {
                    if (cbox.Checked)
                    {
                        cbox.Checked = false;
                    }
                }
                else
                {
                    Console.WriteLine($"cboxSw{i + 1} 컨트롤을 찾지 못함");
                    return;
                }
            }
        }
        #endregion

        #region Event
        private void ConnectEvent()
        {
            cboxSelectChannel.SelectedIndexChanged += cboxSelectChannel_SelectedIndexChanged;
            btnTcpConnectionCheck.Click += btnTcpConnectionCheck_Click;
            btnOpenPbaTerminal.Click += btnOpenPbaTerminal_Click;
            btnOutputBaseWrite.Click += btnOutputBaseWrite_Click;
            btnSwitchWriteAll.Click += btnSwitchWriteAll_Click;
            btnSwitchReadAll.Click += btnSwitchReadAll_Click;
            btnSwClear.Click += btnSwClear_Click;
            btnClose.Click += btnClose_Click;
            dgvInputBase.CellContentClick += dgvInputBase_CellContentClick;
            btnTesterInit.Click += btnTesterInit_Click;
            btnTesterReset.Click += btnTesterReset_Click;
        }
        private async void btnTesterInit_Click(object sender, EventArgs e)
        {
            await InitTester();
        }
        private async void btnTesterReset_Click(object sender, EventArgs e)
        {
            await ResetTester();
        }

        private async void btnOutputBaseWrite_Click(object sender, EventArgs e)
        {
            await WriteOutputPs1();
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboxSelectChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboxSelectChannel.SelectedIndex)
            {
                case 0:
                    {
                        board = CommManager.Boards[0];
                        break;
                    }
                case 1:
                    {
                        board = CommManager.Boards[1];
                        break;
                    }
                case 2:
                    {
                        board = CommManager.Boards[2];
                        break;
                    }
                case 3:
                    {
                        board = CommManager.Boards[3];
                        break;
                    }
            }

            lblTcpConnectionStatus.Text = "";
            lblTcpConnectionStatus.BackColor = Color.Transparent;
        }

        private void btnTcpConnectionCheck_Click(object sender, EventArgs e)
        {
            if (board != null && board.IsConnected())
            {
                lblTcpConnectionStatus.Text = "Open!";
                lblTcpConnectionStatus.BackColor = Color.LimeGreen;
            }
            else
            {
                lblTcpConnectionStatus.Text = "Not Opened!";
                lblTcpConnectionStatus.BackColor = Color.LightCoral;
            }
        }

        private void btnOpenPbaTerminal_Click(object sender, EventArgs e)
        {
            var form = new PBA_TerminalForm(this)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.Show(this);
        }

        

        

        private async void btnSwitchWriteAll_Click(object sender, EventArgs e)
        {
            await WriteAllSwitch();
        }

        private async void btnSwitchReadAll_Click(object sender, EventArgs e)
        {
            await ReadAllSwitch();
        }

        private void btnSwClear_Click(object sender, EventArgs e)
        {
            AllClearSwitch();
        }

        private async void dgvInputBase_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvInputBase.Columns[e.ColumnIndex].Name != "colRead") return;

            await ReadInputBase(e.RowIndex);
        }



        #endregion

        #region Task

        private async Task WriteOutputPs1()
        {
            try
            {
                if (!CheckChannel()) return;

                byte[] DataByteArray = null;
                var text1 = tboxPs1VoltWrite.Text;
                var text2 = tboxPs1CurrWrite.Text;
                if (float.TryParse(text1, out float volt))
                {
                    DataByteArray = BitConverter.GetBytes(volt); //기본 리틀엔디안
                    //Array.Reverse(DataByteArray); //빅엔디안으로 바꾸기
                    Console.WriteLine($"변환된 PS1 Volt 바이트 배열: {BitConverter.ToString(DataByteArray)}");
                }
                else
                {
                    Console.WriteLine($"유효한 float 숫자가 아닙니다. [PS1 Volt]");
                    return;
                }

                if (float.TryParse(text2, out float curr))
                {
                    byte[] TmpByteArray = BitConverter.GetBytes(curr); //기본 리틀엔디안
                    //Array.Reverse(TmpByteArray); //빅엔디안으로 바꾸기
                    Console.WriteLine($"변환된 PS1 Curr 바이트 배열: {BitConverter.ToString(TmpByteArray)}");

                    DataByteArray = DataByteArray.Concat(TmpByteArray).ToArray();
                }
                else
                {
                    Console.WriteLine($"유효한 float 숫자가 아닙니다. [PS1 Curr]");
                    return;
                }

                byte[] tx = new TcpProtocol(0x05, 0x01, DataByteArray).GetPacket();
                byte[] rx = await board.SendAndReceivePacketAsync(tx, Settings.Instance.Board_Read_Timeout);

                if (!UtilityFunctions.CheckTcpRxData(tx, rx))
                {
                    Console.WriteLine($"RX 이상 => rx : {rx}");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ps1 Write 중 예외 발생! " + ex.Message);
            }
        }
        

        
        private async Task ReadAllSwitch()
        {
            try
            {
                if (!CheckChannel()) return;

                byte[] tx = new TcpProtocol(0x04, 0x00).GetPacket();
                byte[] rx = await board.SendAndReceivePacketAsync(tx, Settings.Instance.Board_Read_Timeout);
                if (!UtilityFunctions.CheckTcpRxData(tx, rx))
                {
                    Console.WriteLine($"RX 이상 => rx : {rx}");
                    return;
                }

                byte[] switchBytes = rx.Skip(7).Take(4).Reverse().ToArray();

                for (int i = 0; i < 26; i++)
                {
                    int byteIndex = i / 8;
                    int bitIndex = i % 8;
                    bool isOn = (switchBytes[byteIndex] & (1 << bitIndex)) != 0; 

                    var cbox = this.Controls.Find($"cboxSw{i + 1}", true).FirstOrDefault() as CheckBox;

                    if (cbox != null)  cbox.Checked = isOn;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"예외 발생: {ex.Message}");
            }

        }

        private async Task WriteAllSwitch()
        {
            try
            {
                if (!CheckChannel()) return;

                byte[] switchBytes = new byte[4];

                for (int i = 0; i < 26; i++)
                {
                    var cbox = this.Controls.Find($"cboxSw{i + 1}", true).FirstOrDefault() as CheckBox;
                    if (cbox != null)
                    {
                        if (cbox.Checked)
                        {
                            int byteIndex = i / 8;
                            int bitIndex = i % 8;
                            switchBytes[byteIndex] |= (byte)(1 << bitIndex);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"cboxSw{i + 1} 컨트롤을 찾지 못함");
                        return;
                    }
                }

                byte[] sendBytes = switchBytes.Reverse().ToArray();
                byte[] tx = new TcpProtocol(0x04, 0x01, sendBytes).GetPacket();
                byte[] rx = await board.SendAndReceivePacketAsync(tx, Settings.Instance.Board_Read_Timeout);
                if (!UtilityFunctions.CheckTcpRxData(tx, rx))
                {
                    Console.WriteLine($"RX 이상 => rx : {rx}");
                    return;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"예외 발생: {ex.Message}");
            }
        }

        private async Task ReadInputBase(int rowIndex)
        {
            try
            {
                if (!CheckChannel()) return;

                var row = dgvInputBase.Rows[rowIndex];
                string name = row.Cells["colBaseName"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Input Base Name 없음");
                    return;
                }

                byte item;

                //"PS1 Volt", "PS1 Curr", "PS1 VBUS", "PS1 SYS",
                //"PS1 SYS 3V","PS1 MCJ 33V","PS1 VBOOST", "PS1 HT BOOST",
                //"PS1 SUB HT BOOST OUT", "PS1 KATO BOOST OUT",
                //"PS1 Sleep Curr","PS1 Ship Curr",
                //"PS1 VDD 3V", "PS1 LDC 3V"

                if (string.Equals(name,"PS1 Volt")) item = 0x01;
                else if (string.Equals(name, "PS1 Curr")) item = 0x02;
                else if (string.Equals(name, "PS1 VBUS")) item = 0x03;
                else if (string.Equals(name, "PS1 SYS")) item = 0x04;
                else if (string.Equals(name, "PS1 SYS 3V")) item = 0x05;
                else if (string.Equals(name, "PS1 MCU 33V")) item = 0x06;
                else if (string.Equals(name, "PS1 VBOOST")) item = 0x07;
                else if (string.Equals(name, "PS1 HT BOOST")) item = 0x08;
                else if (string.Equals(name, "PS1 SUB HT BOOST OUT")) item = 0x09;
                else if (string.Equals(name, "PS1 KATO BOOST OUT")) item = 0x0A;
                else if (string.Equals(name, "PS1 Sleep Curr")) item = 0x0B;
                else if (string.Equals(name, "PS1 Ship Curr")) item = 0x0C;
                else if (string.Equals(name, "PS1 VDD 3V")) item = 0x0D;
                else if (string.Equals(name, "PS1 LDC 3V")) item = 0x0E;

                else
                {
                    Console.WriteLine("이 기능은 구현되지 않았습니다.");
                    return;
                }

                byte[] tx = new TcpProtocol(0x06, item).GetPacket();
                byte[] rx = await board.SendAndReceivePacketAsync(tx, Settings.Instance.Board_Read_Timeout);

                if (!UtilityFunctions.CheckTcpRxData(tx, rx))
                {
                    Console.WriteLine($"RX 이상: {name}");
                    return;
                }

                // rx[7]부터 FLOAT 4바이트 (Big Endian)
                //byte[] floatBytes = rx.Skip(7).Take(4).Reverse().ToArray();
                byte[] floatBytes = rx.Skip(7).Take(4).ToArray();

                float value = BitConverter.ToSingle(floatBytes, 0);

                row.Cells["colValue"].Value = value.ToString("F7");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"InputBase Read 실패: {ex.Message}");
            }
        }

        private async Task InitTester()
        {
            try
            {
                if (!CheckChannel()) return;

                byte[] tx = new TcpProtocol(0x01, 0x00).GetPacket();
                byte[] rx = await board.SendAndReceivePacketAsync(tx, Settings.Instance.Board_Connect_Timeout);

                if (!UtilityFunctions.CheckTcpRxData(tx, rx))
                {
                    Console.WriteLine($"RX 이상: {rx}");
                    return;
                }

                Console.WriteLine("Tester 초기화 완료!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"InputBase Read 실패: {ex.Message}");
            }
        }

        private async Task ResetTester()
        {
            try
            {
                if (!CheckChannel()) return;

                Console.WriteLine("소프트웨어 리셋을 시작합니다. (통신 상태에 따라 ACK가 수신되지 않을 수도 있습니다.)");

                byte[] tx = new TcpProtocol(0x01, 0xFF).GetPacket();
                byte[] rx = await board.SendAndReceivePacketAsync(tx, Settings.Instance.Board_Connect_Timeout);

                if (!UtilityFunctions.CheckTcpRxData(tx, rx))
                {
                    Console.WriteLine($"RX : {rx}");
                    Console.WriteLine($"RX 값이 이상하거나 ACK가 수신되지 않았습니다.");

                    return;
                }

                Console.WriteLine("Tester 리셋 완료!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"InputBase Read 실패: {ex.Message}");
            }
        }

        
        #endregion


    }
}
