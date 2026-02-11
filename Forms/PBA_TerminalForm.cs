using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using p2_40_Charge_Tester.Data;
using p2_40_Charge_Tester.Communication;
using p2_40_Charge_Tester.UI;
using p2_40_Charge_Tester.LIB;

namespace p2_40_Charge_Tester.Forms
{
    public partial class PBA_TerminalForm : Form
    {
        #region Field
        public ManualForm manualform;
        private SerialChannelPort Pba;
        RichTextLogger DeviceTx;
        RichTextLogger DeviceRx;
        private bool StatusPortOpen = false;
        #endregion

        #region Init
        public PBA_TerminalForm(ManualForm parentform)
        {
            this.manualform = parentform;
            InitializeComponent();
            RescanSerialPort();
            DeviceTx = new RichTextLogger(tboxTx);
            DeviceRx = new RichTextLogger(tboxRx);
        }

        private void RescanSerialPort()
        {
            try
            {
                // 현재 PC에 연결된 모든 COM 포트 가져오기
                string[] portNames = SerialPort.GetPortNames();

                cboxPbaCom.Items.Clear();
                cboxPbaCom.Items.Add("None");
                cboxPbaCom.Items.AddRange(portNames);
                cboxPbaCom.SelectedIndex = 0;

                // 새로운 COM 포트 목록 추가
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while searching for port " + ex.Message);
            }
        }

        private void InitControlData()
        {
            lblMcuId.Text = "-";
            lblSn.Text = "-";
            lblFwVer.Text = "-";
        }

        #endregion

        #region Event
        private void btnRescan_Click(object sender, EventArgs e)
        {
            RescanSerialPort();
        }
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            string portName = cboxPbaCom.SelectedItem?.ToString();
            btnConnect.Enabled = false;
            try
            {
                if (StatusPortOpen)
                {
                    ToggleLogComm_Device(Pba, true, false); //tx log 해제
                    ToggleLogComm_Device(Pba, false, false); //rx log 해제
                    Pba.Disconnect();
                    InitControlData();
                    btnConnect.BackColor = Color.White;
                    btnConnect.Text = "연결";
                    StatusPortOpen = false;
                    
                    Console.WriteLine($"{portName} 해제 [Channel {Pba.ChannelNo}]");
                    return;
                }

                if (Pba == null)
                    Pba = new SerialChannelPort(ch: 101); // 채널 번호 적절히

                if (Pba.IsConnected())
                    Pba.Disconnect();

                bool ok = Pba.Connect(portName);
                if (!ok)
                {
                    StatusPortOpen = false;
                    btnConnect.BackColor = Color.White;
                    btnConnect.Text = "연결";
                    return;
                }

                StatusPortOpen = true;
                btnConnect.BackColor = Color.Green;
                btnConnect.Text = "해제";
                Console.WriteLine($"{portName} 연결 완료 [Channel {Pba.ChannelNo}]");

                ToggleLogComm_Device(Pba, true, true); //tx log 연결
                ToggleLogComm_Device(Pba, false, true); //rx log 연결

                //연결 되었을 때 쓸 함수를 여기다 기술
                await Task.Delay(100);

                await GetMcuId();
                await GetSn();
                await GetFwVer();
            }
            catch (Exception ex)
            {
                Console.WriteLine("PBA 연결 중 예외 발생!" + ex.Message);
            }
            finally
            {
                btnConnect.Enabled = true;
            }
        }

        private void PBA_TerminalForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Pba != null && Pba.IsConnected())
            {
                ToggleLogComm_Device(Pba, true, false); //tx log 해제
                ToggleLogComm_Device(Pba, false, false); //rx log 해제
                Pba.Disconnect();
                Console.WriteLine($"PBA Port 해제");
            }
        }
        #endregion

        #region Utility
        private void ToggleLogComm_Device(SerialChannelPort device, bool Istx, bool enable)
        {
            if (device == null) return;

            if (enable)
            {
                if (Istx)
                {
                    device.LogTxToUI = (tbox, msg) => DeviceTx.Tx(msg);
                }
                else
                {
                    device.LogRxToUI = (tbox, msg) => DeviceRx.Rx(msg);
                }
                
            }
            else
            {
                if (Istx)
                {
                    device.LogTxToUI = null;
                }
                else
                {
                    device.LogRxToUI = null;
                }
            }
        }

        
        #endregion

        #region Task


        private async Task GetMcuId()
        {
            try
            {
                byte[] tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_MCU_ID).GetPacket();
                byte[] rx = await Pba.SendAndReceivePacketAsync_OnlyData(tx, Settings.Instance.Pba_Read_Timeout);

                if (rx == null) { DeviceRx.Fail("ID Check: Rx is NULL"); return; }

                string DeviceId = $"{rx[6]:X2}{rx[7]:X2}{rx[4]:X2}{rx[5]:X2}{rx[2]:X2}{rx[3]:X2}{rx[0]:X2}{rx[1]:X2}";
                lblMcuId.Text = DeviceId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("MCU ID 못가져옴" + ex.Message);
            }

        }

        private async Task GetSn()
        {
            try
            {
                byte[] tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_SERIAL_NO).GetPacket();
                byte[] rx = await Pba.SendAndReceivePacketAsync_OnlyData(tx, Settings.Instance.Pba_Read_Timeout);

                if (rx == null) { DeviceRx.Fail("SN Check: Rx is NULL"); return; }

                var sb = new StringBuilder();
                for (int i = 1; i < rx.Length; i += 2) sb.Append((char)rx[i]);
                string SerialNo = sb.ToString();
                lblSn.Text = SerialNo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SN 못가져옴" + ex.Message);
            }
        }

        private async Task GetFwVer()
        {
            try
            {
                byte[] tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_FW_VER).GetPacket();
                byte[] rx = await Pba.SendAndReceivePacketAsync_OnlyData(tx, Settings.Instance.Pba_Read_Timeout);
                if (rx == null) { DeviceRx.Fail("FW Check: Rx is NULL"); return; }

                string major = $"{(char)rx[0]}{(char)rx[1]}";
                string minor = $"{(char)rx[2]}{(char)rx[3]}";
                string FwVer = $"{major}.{minor}";
                lblFwVer.Text = FwVer;
            }
            catch (Exception ex)
            {
                Console.WriteLine("FW Ver 못가져옴" + ex.Message);
            }
        }

        private void lblClearTxRx_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tboxTx.Clear();
            tboxRx.Clear();
        }


        #endregion

        private async void btnReadSend_Click(object sender, EventArgs e)
        {
            if (Pba == null || !Pba.IsConnected())
            {
                DeviceRx.Fail("포트 연결 안됨");
                return;
            }

            btnReadSend.Enabled = false;

            try
            {
                // 전부 HEX로 해석
                ushort addr = UtilityFunctions.ParseU16HexLoose(tboxReadAddr.Text);   // ex: "1"  -> 0x0001
                ushort qty = UtilityFunctions.ParseU16HexLoose(tboxReadQuantity.Text);    // ex: "17" -> 0x0017

                // payload = [AddrHi AddrLo QtyHi QtyLo]
                byte[] payload = new byte[4];
                payload[0] = (byte)(addr >> 8);
                payload[1] = (byte)(addr & 0xFF);
                payload[2] = (byte)(qty >> 8);
                payload[3] = (byte)(qty & 0xFF);

                byte[] tx = new CDCProtocol(Variable.SLAVE, Variable.READ, payload).GetPacket();
                byte[] rx = await Pba.SendAndReceivePacketAsync_OnlyData(tx, Settings.Instance.Pba_Read_Timeout);

                if (rx == null)
                {
                    DeviceRx.Fail("RECV = null");
                    return;
                }

                var regs = UtilityFunctions.ParseRegistersBigEndian(rx);
                for (int i = 0; i < regs.Length; i++)
                {
                    DeviceRx.Info(
                        $"RECV[{i}] ( DEC = {regs[i]}, HEX = {regs[i]:X4} )"
                    );
                }

            }
            catch (Exception ex)
            {
                DeviceRx.Fail("Read 실패: " + ex.Message);
            }
            finally
            {
                btnReadSend.Enabled = true;
            }
        }

        private async void btnWriteSend_Click(object sender, EventArgs e)
        {
            if (Pba == null || !Pba.IsConnected())
            {
                DeviceRx.Fail("포트 연결 안됨");
                return;
            }

            btnWriteSend.Enabled = false;

            try
            {
                ushort addr = UtilityFunctions.ParseU16HexLoose(tboxWriteAddr.Text); // 예: "1" -> 0x0001
                ushort data = UtilityFunctions.ParseU16HexLoose(tboxWriteData.Text); // 예: "A" -> 0x000A

                // payload = [AddrHi AddrLo DataHi DataLo]
                byte[] payload = new byte[4];
                payload[0] = (byte)(addr >> 8);
                payload[1] = (byte)(addr & 0xFF);
                payload[2] = (byte)(data >> 8);
                payload[3] = (byte)(data & 0xFF);

                byte[] tx = new CDCProtocol(Variable.SLAVE, Variable.WRITE, payload).GetPacket();
                byte[] rx = await Pba.SendAndReceivePacketAsync(tx, Settings.Instance.Pba_Read_Timeout);

                if (!UtilityFunctions.CheckEchoAck(tx,rx))
                {
                    if (rx == null) DeviceRx.Fail("RECV = null");
                    else DeviceRx.Fail("RX is not unexpected value");
                    return;
                }

            }
            catch (Exception ex)
            {
                DeviceRx.Fail("Write 실패: " + ex.Message);
            }
            finally
            {
                btnWriteSend.Enabled = true;
            }
        }

        private async void btnMultiWriteSend_Click(object sender, EventArgs e)
        {
            if (Pba == null || !Pba.IsConnected())
            {
                DeviceRx.Fail("포트 연결 안됨");
                return;
            }

            btnMultiWriteSend.Enabled = false;

            try
            {
                ushort addr = UtilityFunctions.ParseU16HexLoose(tboxMultiWriteAddr.Text);

                // Datas(HEX) -> ushort[] 레지스터 값들로 파싱
                ushort[] regs = UtilityFunctions.ParseMultiWriteRegs(tboxMultiWriteDatas.Text);
                if (regs == null || regs.Length == 0)
                    throw new FormatException("Datas(HEX)가 비어있음");

                // payload = [Addr(2)][Qty(2)][ByteCount(1)][Data(2*Qty)]
                byte[] payload = UtilityFunctions.BuildMultiWritePayload(addr, regs);

                byte[] tx = new CDCProtocol(Variable.SLAVE, Variable.MULTI_WRITE, payload).GetPacket();

                // MultiWrite는 응답이 "요약 ACK"라 프레임 그대로 받아야 안전
                byte[] rx = await Pba.SendAndReceivePacketAsync(tx, Settings.Instance.Pba_Read_Timeout);

                //if (!UtilityFunctions.CheckWriteMultiAck(tx,rx))
                //{
                //    if (rx == null) DeviceRx.Fail("RECV = null");
                //    DeviceRx.Fail("RECV = null");
                //    return;
                //}

                bool ok = UtilityFunctions.CheckWriteMultiAck(tx, rx);
                if (ok)
                {
                    DeviceRx.Info($"(ADDR=0x{addr:X4}, QTY={regs.Length})");
                }
                else
                {
                    DeviceRx.Fail($"MULTI WRITE ACK FAIL (ADDR=0x{addr:X4}, QTY={regs.Length})");
                    DeviceRx.Info("TX=" + Encoding.ASCII.GetString(tx).Trim());
                    DeviceRx.Info("RX=" + Encoding.ASCII.GetString(rx).Trim());
                }
            }
            catch (Exception ex)
            {
                DeviceRx.Fail("MultiWrite 실패: " + ex.Message);
            }
            finally
            {
                btnMultiWriteSend.Enabled = true;
            }
        }

        

        #region Parsing
        private void btnIntToHex_Click(object sender, EventArgs e)
        {
            try
            {
                ushort val = UtilityFunctions.ParseU16DecLoose(tboxInt.Text); // 0~65535

                byte hi = (byte)(val >> 8);
                byte lo = (byte)(val & 0xFF);

                tboxHexHi.Text = hi.ToString("X2"); // 항상 2자리
                tboxHexLo.Text = lo.ToString("X2");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Int → Hex 변환 실패: " + ex.Message);
            }
        }

        private void btnHexToInt_Click(object sender, EventArgs e)
        {
            try
            {
                byte hi = UtilityFunctions.ParseU8HexLoose(tboxHexHi.Text); // 00~FF
                byte lo = UtilityFunctions.ParseU8HexLoose(tboxHexLo.Text);

                ushort val = (ushort)((hi << 8) | lo);
                tboxInt.Text = val.ToString(); // 10진수로 표시
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hex → Int 변환 실패: " + ex.Message);
            }
        }

        private void btnFloatToHex_Click(object sender, EventArgs e)
        {
            try
            {
                float f = UtilityFunctions.ParseFloatLoose(tboxFloat.Text);

                // float -> bytes (BitConverter는 시스템 엔디안(대부분 Little))
                byte[] bytes = BitConverter.GetBytes(f); // [b0,b1,b2,b3] = Little-endian

                // UI: hex1=LSB, hex4=MSB (32 E6 2E 3E 같은 형태)
                tboxHex1.Text = bytes[0].ToString("X2");
                tboxHex2.Text = bytes[1].ToString("X2");
                tboxHex3.Text = bytes[2].ToString("X2");
                tboxHex4.Text = bytes[3].ToString("X2");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Float → Hex 변환 실패: " + ex.Message);
            }
        }

        private void btnHexToFloat_Click(object sender, EventArgs e)
        {
            try
            {
                byte b0 = UtilityFunctions.ParseU8HexLoose(tboxHex1.Text);
                byte b1 = UtilityFunctions.ParseU8HexLoose(tboxHex2.Text);
                byte b2 = UtilityFunctions.ParseU8HexLoose(tboxHex3.Text);
                byte b3 = UtilityFunctions.ParseU8HexLoose(tboxHex4.Text);

                byte[] bytes = new byte[] { b0, b1, b2, b3 }; // UI가 LSB->MSB 순서라고 가정

                float f = BitConverter.ToSingle(bytes, 0);

                // 표시 포맷은 취향. 정확히 쓰고 싶으면 "R" 추천
                tboxFloat.Text = f.ToString("0.####", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hex → Float 변환 실패: " + ex.Message);
            }
        }






        #endregion

        
    }
}
