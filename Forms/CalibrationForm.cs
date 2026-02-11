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
using p2_40_Charge_Tester.LIB;

namespace p2_40_Charge_Tester.Forms
{

    public partial class CalibrationForm : Form
    {

        #region Filed
        public MainForm mainform;
        TcpChannelClient board;


        #endregion

        #region Init
        public CalibrationForm(MainForm parentform)
        {
            this.mainform = parentform;
            InitializeComponent();
            InitializeNumericControls();
            ConnectEvent();
        }

        private void CalibrationForm_Load(object sender, EventArgs e)
        {
            cboxCalChannel.SelectedIndex = 0;
        }

        private void InitializeNumericControls()
        {
            ConfigureAllNumericUpDowns(this); //폼안에 모든 numericControl 세팅 초기화
        }

        private void ConfigureAllNumericUpDowns(Control parent, int decimalPlaces = 7, decimal increment = 0.0000001M, decimal min = 0, decimal max = 100)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is NumericUpDown num)
                {
                    num.DecimalPlaces = decimalPlaces;
                    num.Increment = increment;
                    num.Minimum = min;
                    num.Maximum = max;
                }

                if (c.HasChildren)
                    ConfigureAllNumericUpDowns(c, decimalPlaces, increment, min, max);
            }
        }
        #endregion

        #region Event
        private void ConnectEvent()
        {
            cboxCalChannel.SelectedIndexChanged += cboxCalChannel_SelectedIndexChanged;

            btnGainRead.Click += btnGainRead_Click;
            btnOffsetSave.Click += btnOffsetSave_Click;

            btnDa1Min.Click += btnDa1Min_Click;
            btnDa1Max.Click += btnDa1Max_Click;
            btnDa1Test.Click += btnDa1Test_Click;
            btnCalDa1.Click += btnCalDa1_Click;

            btnDa2Min.Click += btnDa2Min_Click;
            btnDa2Max.Click += btnDa2Max_Click;
            btnDa2Test.Click += btnDa2Test_Click;
            btnCalDa2.Click += btnCalDa2_Click;

            btnMux1Min.Click += btnMux1Min_Click;
            btnMux1Max.Click += btnMux1Max_Click;
            btnMux1Test.Click += btnMux1Test_Click;
            btnCalMux1.Click += btnCalMux1_Click;

            btnMux2Min.Click += btnMux2Min_Click;
            btnMux2Max.Click += btnMux2Max_Click;
            btnMux2Test.Click += btnMux2Test_Click;
            btnCalMux2.Click += btnCalMux2_Click;

            btnMux3Min.Click += btnMux3Min_Click;
            btnMux3Max.Click += btnMux3Max_Click;
            btnMux3Test.Click += btnMux3Test_Click;
            btnCalMux3.Click += btnCalMux3_Click;

            btnMux4Min.Click += btnMux4Min_Click;
            btnMux4Max.Click += btnMux4Max_Click;
            btnMux4Test.Click += btnMux4Test_Click;
            btnCalMux4.Click += btnCalMux4_Click;

            btnMux5Min.Click += btnMux5Min_Click;
            btnMux5Max.Click += btnMux5Max_Click;
            btnMux5Test.Click += btnMux5Test_Click;
            btnCalMux5.Click += btnCalMux5_Click;

            btnMux7Min.Click += btnMux7Min_Click;
            btnMux7Max.Click += btnMux7Max_Click;
            btnMux7Test.Click += btnMux7Test_Click;
            btnCalMux7.Click += btnCalMux7_Click;

            btnMux8Min.Click += btnMux8Min_Click;
            btnMux8Max.Click += btnMux8Max_Click;
            btnMux8Test.Click += btnMux8Test_Click;
            btnCalMux8.Click += btnCalMux8_Click;
        }

        

        private void cboxCalChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboxCalChannel.SelectedIndex)
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

            InitializeNumericControls();
        }

        #region entire cal Data
        private async void btnGainRead_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboxCalChannel.SelectedIndex < 0 || board == null || !board.IsConnected())
                {
                    MessageBox.Show("This Channel is not connected.");
                    return;
                }

                byte[] tx = new TcpProtocol(0x03, 0x00).GetPacket();
                byte[] rx = await board.SendAndReceivePacketAsync(tx, Settings.Instance.Board_Read_Timeout);

                if (!UtilityFunctions.CheckTcpRxData(tx, rx))
                {
                    Console.WriteLine($"RX 이상 => rx : {rx}");
                    return;
                }

                TextBox[] gainBoxes = new TextBox[]
                {
                    tboxGainDa1,  // 0x01
                    tboxGainDa2,  // 0x02
                    tboxGainDa4,
                    tboxGainMux1, // 0x04
                    tboxGainMux2,
                    tboxGainMux3,
                    tboxGainMux4,
                    tboxGainMux5,

                    tboxGainMux7,
                    tboxGainMux8,
                };

                TextBox[] offsetBoxes = new TextBox[]
                {
                    tboxOffsetDa1,  // 0x01
                    tboxOffsetDa2,  // 0x02
                    tboxOffsetDa4,
                    tboxOffsetMux1, // 0x04
                    tboxOffsetMux2,
                    tboxOffsetMux3,
                    tboxOffsetMux4,
                    tboxOffsetMux5,
                    
                    tboxOffsetMux7,
                    tboxOffsetMux8,
                };

                for (int i = 0; i < gainBoxes.Length; i++)
                {
                    if (gainBoxes[i] == null || offsetBoxes[i] == null) continue;

                    int baseIndex = 7 + i * 8; // Gain/Offset 각 4byte씩
                    byte[] gainBytes = rx.Skip(baseIndex).Take(4).ToArray();
                    byte[] offsetBytes = rx.Skip(baseIndex + 4).Take(4).ToArray();

                    float gain = BitConverter.ToSingle(gainBytes, 0);
                    float offset = BitConverter.ToSingle(offsetBytes, 0);

                    gainBoxes[i].Text = gain.ToString("F7");
                    offsetBoxes[i].Text = offset.ToString("F7");
                }

                Console.WriteLine("Calibration Read 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"예외 발생: {ex.Message}");
            }
        }

        private void btnOffsetSave_Click(object sender, EventArgs e)
        {
            AllWriteGainOffset();
        }

        #endregion

        #region Da1
        private async void btnDa1Min_Click(object sender, EventArgs e)
        {
            float setValue = (float)numDa1SetOutputMin.Value;
            await SendCalSetOutputAsync(0x01, setValue, false);
        }

        private async void btnDa1Max_Click(object sender, EventArgs e)
        {
            float setValue = (float)numDa1SetOutputMax.Value;
            await SendCalSetOutputAsync(0x01, setValue, false);
        }

        private async void btnDa1Test_Click(object sender, EventArgs e)
        {
            float setValue = (float)numDa1SetTestoutput.Value;
            await SendCalSetOutputAsync(0x01, setValue, true);
        }
        private void btnCalDa1_Click(object sender, EventArgs e)
        {
            CalculateGainOffset(
                numDa1MeassuredValueMin,
                numDa1MeassuredValueMax,
                numDa1SetOutputMin,
                numDa1SetOutputMax,
                lblDa1Gain,
                lblDa1Offset,
                tboxGainDa1,
                tboxOffsetDa1);
        }

        #endregion

        #region Da2
        private async void btnDa2Min_Click(object sender, EventArgs e)
        {
            float setValue = (float)numDa2SetOutputMin.Value;
            await SendCalSetOutputAsync(0x02, setValue, false);
        }

        private async void btnDa2Max_Click(object sender, EventArgs e)
        {
            float setValue = (float)numDa2SetOutputMax.Value;
            await SendCalSetOutputAsync(0x02, setValue, false);
        }

        private async void btnDa2Test_Click(object sender, EventArgs e)
        {
            float setValue = (float)numDa2SetTestoutput.Value;
            await SendCalSetOutputAsync(0x02, setValue, true);
        }

        private void btnCalDa2_Click(object sender, EventArgs e)
        {
            CalculateGainOffset(
                numDa2MeassuredValueMin,
                numDa2MeassuredValueMax,
                numDa2SetOutputMin,
                numDa2SetOutputMax,
                lblDa2Gain,
                lblDa2Offset,
                tboxGainDa2,
                tboxOffsetDa2);
        }
        #endregion

        #region Mux1
        private async void btnMux1Min_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux1SetOutputMin.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x03, setValue, false); // 항상 CAL 적용 = true
            if (!float.IsNaN(measured))
                numMux1MeassuredValueMin.Value = (decimal)measured;
        }

        private async void btnMux1Max_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux1SetOutputMax.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x03, setValue, false);
            if (!float.IsNaN(measured))
                numMux1MeassuredValueMax.Value = (decimal)measured;
        }

        private async void btnMux1Test_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux1SetTestoutput.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x03, setValue, true);
            if (!float.IsNaN(measured))
                numMux1MeassuredValueTest.Value = (decimal)measured;
        }

        private void btnCalMux1_Click(object sender, EventArgs e)
        {
            CalculateGainOffset(
                numMux1MeassuredValueMin,
                numMux1MeassuredValueMax,
                numMux1SetOutputMin,
                numMux1SetOutputMax,
                lblMux1Gain,
                lblMux1Offset,
                tboxGainMux1,
                tboxOffsetMux1);
        }

        #endregion

        #region Mux2
        private async void btnMux2Min_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux2SetOutputMin.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x04, setValue, false);
            if (!float.IsNaN(measured))
                numMux2MeassuredValueMin.Value = (decimal)measured;
        }

        private async void btnMux2Max_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux2SetOutputMax.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x04, setValue, false);
            if (!float.IsNaN(measured))
                numMux2MeassuredValueMax.Value = (decimal)measured;
        }

        private async void btnMux2Test_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux2SetTestoutput.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x04, setValue, true);
            if (!float.IsNaN(measured))
                numMux2MeassuredValueTest.Value = (decimal)measured;
        }

        private void btnCalMux2_Click(object sender, EventArgs e)
        {
            CalculateGainOffset(
                numMux2MeassuredValueMin,
                numMux2MeassuredValueMax,
                numMux2SetOutputMin,
                numMux2SetOutputMax,
                lblMux2Gain,
                lblMux2Offset,
                tboxGainMux2,
                tboxOffsetMux2);
        }

        #endregion

        #region Mux3
        private async void btnMux3Min_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux3SetOutputMin.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x05, setValue, false);
            if (!float.IsNaN(measured))
                numMux3MeassuredValueMin.Value = (decimal)measured;
        }

        private async void btnMux3Max_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux3SetOutputMax.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x05, setValue, false);
            if (!float.IsNaN(measured))
                numMux3MeassuredValueMax.Value = (decimal)measured;
        }

        private async void btnMux3Test_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux3SetTestoutput.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x05, setValue, true);
            if (!float.IsNaN(measured))
                numMux3MeassuredValueTest.Value = (decimal)measured;
        }

        private void btnCalMux3_Click(object sender, EventArgs e)
        {
            CalculateGainOffset(
                numMux3MeassuredValueMin,
                numMux3MeassuredValueMax,
                numMux3SetOutputMin,
                numMux3SetOutputMax,
                lblMux3Gain,
                lblMux3Offset,
                tboxGainMux3,
                tboxOffsetMux3);
        }

        #endregion

        #region Mux4
        private async void btnMux4Min_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux4SetOutputMin.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x06, setValue, false);
            if (!float.IsNaN(measured))
                numMux4MeassuredValueMin.Value = (decimal)measured;
        }

        private async void btnMux4Max_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux4SetOutputMax.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x06, setValue, false);
            if (!float.IsNaN(measured))
                numMux4MeassuredValueMax.Value = (decimal)measured;
        }

        private async void btnMux4Test_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux4SetTestoutput.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x06, setValue, true);
            if (!float.IsNaN(measured))
                numMux4MeassuredValueTest.Value = (decimal)measured;
        }

        private void btnCalMux4_Click(object sender, EventArgs e)
        {
            CalculateGainOffset(
                numMux4MeassuredValueMin,
                numMux4MeassuredValueMax,
                numMux4SetOutputMin,
                numMux4SetOutputMax,
                lblMux4Gain,
                lblMux4Offset,
                tboxGainMux4,
                tboxOffsetMux4);
        }

        #endregion

        #region Mux5
        private async void btnMux5Min_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux5SetOutputMin.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x07, setValue, false);
            if (!float.IsNaN(measured))
                numMux5MeassuredValueMin.Value = (decimal)measured;
        }

        private async void btnMux5Max_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux5SetOutputMax.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x07, setValue, false);
            if (!float.IsNaN(measured))
                numMux5MeassuredValueMax.Value = (decimal)measured;
        }

        private async void btnMux5Test_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux5SetTestoutput.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x07, setValue, true);
            if (!float.IsNaN(measured))
                numMux5MeassuredValueTest.Value = (decimal)measured;
        }

        private void btnCalMux5_Click(object sender, EventArgs e)
        {
            CalculateGainOffset(
                numMux5MeassuredValueMin,
                numMux5MeassuredValueMax,
                numMux5SetOutputMin,
                numMux5SetOutputMax,
                lblMux5Gain,
                lblMux5Offset,
                tboxGainMux5,
                tboxOffsetMux5);
        }

        #endregion


        #region Mux7
        private async void btnMux7Min_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux5SetOutputMin.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x08, setValue, false);
            if (!float.IsNaN(measured))
                numMux5MeassuredValueMin.Value = (decimal)measured;
        }
        private async void btnMux7Max_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux5SetOutputMax.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x08, setValue, false);
            if (!float.IsNaN(measured))
                numMux5MeassuredValueMax.Value = (decimal)measured;
        }
        private async void btnMux7Test_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux5SetTestoutput.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x08, setValue, true);
            if (!float.IsNaN(measured))
                numMux5MeassuredValueTest.Value = (decimal)measured;
        }

        private void btnCalMux7_Click(object sender, EventArgs e)
        {
            CalculateGainOffset(
                numMux7MeassuredValueMin,
                numMux7MeassuredValueMax,
                numMux7SetOutputMin,
                numMux7SetOutputMax,
                lblMux7Gain,
                lblMux7Offset,
                tboxGainMux7,
                tboxOffsetMux7);
        }


        #endregion

        #region Mux8
        private async void btnMux8Min_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux5SetOutputMin.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x09, setValue, false);
            if (!float.IsNaN(measured))
                numMux5MeassuredValueMin.Value = (decimal)measured;
        }
        private async void btnMux8Max_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux5SetOutputMax.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x09, setValue, false);
            if (!float.IsNaN(measured))
                numMux5MeassuredValueMax.Value = (decimal)measured;
        }
        private async void btnMux8Test_Click(object sender, EventArgs e)
        {
            float setValue = (float)numMux5SetTestoutput.Value;
            float measured = await SendCalSetOutputAsync_ADC(0x09, setValue, true);
            if (!float.IsNaN(measured))
                numMux5MeassuredValueTest.Value = (decimal)measured;
        }
        private void btnCalMux8_Click(object sender, EventArgs e)
        {
            CalculateGainOffset(
                numMux8MeassuredValueMin,
                numMux8MeassuredValueMax,
                numMux8SetOutputMin,
                numMux8SetOutputMax,
                lblMux8Gain,
                lblMux8Offset,
                tboxGainMux8,
                tboxOffsetMux8);
        }





        #endregion

        #endregion



        #region Task
        private async Task SendCalSetOutputAsync(byte itemCode, float setValue, bool calApplied)
        {
            try
            {
                if (cboxCalChannel.SelectedIndex < 0 || board == null || !board.IsConnected())
                {
                    MessageBox.Show("This Channel is not connected.");
                    return;
                }

                List<byte> payload = new List<byte>();

                if (calApplied) payload.Add(0x01);
                else payload.Add(0x00);

                byte[] valueBytes = BitConverter.GetBytes(setValue);
                //if (BitConverter.IsLittleEndian) Array.Reverse(valueBytes);
                payload.AddRange(valueBytes);

                byte[] tx = new TcpProtocol(0x02, itemCode, payload.ToArray()).GetPacket();
                byte[] rx = await board.SendAndReceivePacketAsync(tx, Settings.Instance.Board_Read_Timeout);

                if (!UtilityFunctions.CheckTcpRxData(tx, rx))
                {
                    Console.WriteLine($"RX 이상 => rx : {rx}");
                    return;
                }

                Console.WriteLine($"Set output => itemCode : {itemCode}  setValue : {setValue}  calApplied : {calApplied}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"예외 발생: {ex.Message}");
            }
        }

        private async Task<float> SendCalSetOutputAsync_ADC(byte itemCode, float setValue, bool calApplied)
        {
            try
            {
                if (cboxCalChannel.SelectedIndex < 0 || board == null || !board.IsConnected())
                {
                    MessageBox.Show("This Channel is not connected.");
                    return float.NaN;
                }

                List<byte> payload = new List<byte>();

                if (calApplied) payload.Add(0x01);
                else payload.Add(0x00);

                byte[] valueBytes = BitConverter.GetBytes(setValue);
                //if (BitConverter.IsLittleEndian) Array.Reverse(valueBytes);
                payload.AddRange(valueBytes);

                byte[] tx = new TcpProtocol(0x02, itemCode, payload.ToArray()).GetPacket();
                byte[] rx = await board.SendAndReceivePacketAsync(tx, Settings.Instance.Board_Read_Timeout);
                if (!UtilityFunctions.CheckTcpRxData(tx, rx))
                {
                    Console.WriteLine($"RX 이상 => rx : {rx}");
                    return float.NaN;
                }

                byte[] measuredBytes = rx.Skip(7).Take(4).Reverse().ToArray();
                float measuredValue = BitConverter.ToSingle(measuredBytes, 0);

                return measuredValue;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"예외 발생: {ex.Message}");
                return float.NaN;
            }
        }


        #endregion

        #region function
        private void CalculateGainOffset(
           NumericUpDown numMeasuredMin,
           NumericUpDown numMeasuredMax,
           NumericUpDown numSetMin,
           NumericUpDown numSetMax,
           Label lblGain,
           Label lblOffset,
           TextBox tboxGain,
           TextBox tboxOffset)
        {
            try
            {
                float x1 = (float)numMeasuredMin.Value;
                float y1 = (float)numSetMin.Value;
                float x2 = (float)numMeasuredMax.Value;
                float y2 = (float)numSetMax.Value;

                if (x1 == x2)
                {
                    MessageBox.Show("No equalize Set Output Min and Max.");
                    return;
                }

                float gain = (y2 - y1) / (x2 - x1);
                float offset = y1 - gain * x1;

                lblGain.Text = gain.ToString("F7");
                lblOffset.Text = offset.ToString("F7");
                tboxGain.Text = gain.ToString("F7");
                tboxOffset.Text = offset.ToString("F7");

                Console.WriteLine($"[Cal] → Gain: {gain}, Offset: {offset}");

                //AllWriteGainOffset(); //원래는 계산하고 Gain/Offset 값 자동으로 쓰려고한건데 일단 빼자
            }
            catch (Exception ex)
            {
                MessageBox.Show($"계산 오류: {ex.Message}");
            }
        }

        private async void AllWriteGainOffset()
        {
            try
            {
                if (cboxCalChannel.SelectedIndex < 0 || board == null || !board.IsConnected())
                {
                    MessageBox.Show("This Channel is not connected.");
                    return;
                }

                List<float> gains = new List<float>();
                List<float> offsets = new List<float>();

                TextBox[] gainBoxes =
                {
                    tboxGainDa1, tboxGainDa2, tboxGainDa4,
                    tboxGainMux1, tboxGainMux2, tboxGainMux3, tboxGainMux4,
                    tboxGainMux5, tboxGainMux7, tboxGainMux8
                };
                TextBox[] offsetBoxes =
                {
                    tboxOffsetDa1, tboxOffsetDa2, tboxOffsetDa4,
                    tboxOffsetMux1, tboxOffsetMux2, tboxOffsetMux3, tboxOffsetMux4,
                    tboxOffsetMux5, tboxOffsetMux7, tboxOffsetMux8
                };

                foreach (var tb in gainBoxes)
                {
                    if (float.TryParse(tb.Text, out float f)) gains.Add(f);
                    else gains.Add(0f); // 파싱 실패 시 0으로 처리
                }

                foreach (var tb in offsetBoxes)
                {
                    if (float.TryParse(tb.Text, out float f)) offsets.Add(f);
                    else offsets.Add(0f);
                }

                List<byte> payload = new List<byte>();

                for (int i = 0; i < gains.Count; i++)
                {
                    byte[] gainBytes = BitConverter.GetBytes(gains[i]);
                    byte[] offsetBytes = BitConverter.GetBytes(offsets[i]);
                    //if (BitConverter.IsLittleEndian)
                    //{
                    //    Array.Reverse(gainBytes);
                    //    Array.Reverse(offsetBytes);
                    //}
                    payload.AddRange(gainBytes);
                    payload.AddRange(offsetBytes);
                }

                byte[] tx = new TcpProtocol(0x03, 0x01, payload.ToArray()).GetPacket();
                byte[] rx = await board.SendAndReceivePacketAsync(tx, Settings.Instance.Board_Read_Timeout);
                if (!UtilityFunctions.CheckTcpRxData(tx, rx))
                {
                    Console.WriteLine($"RX 이상 => rx : {rx}");
                    return;
                }

                Console.WriteLine($"Write All Calibration Success!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"예외 발생: {ex.Message}");
            }
        }




        #endregion

        
    }




}
