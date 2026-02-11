using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using p2_40_Charge_Tester.Communication;
using p2_40_Charge_Tester.LIB;
using p2_40_Charge_Tester.Forms;
using p2_40_Charge_Tester.UserControls;
using System.Reflection;


namespace p2_40_Charge_Tester.Data
{
    internal static class TaskFunctions
    {
        public static async Task<bool> RunTestItem(string taskName, int channelIndex, ChControl control, CancellationToken token, bool totalResult)
        {
            bool result = false;

            control.Logger.Section(taskName);
            

            switch (taskName)
            {
                case "QR READ":
                    result = await Test_QrRead(channelIndex, control, token);
                    break;
                case "MCU INFO":
                    result = await Test_McuInfo(channelIndex, control, token);
                    break;
                case "SDP":
                    result = await Test_SDP(channelIndex, control, token);
                    break;
                case "DCP":
                    result = await Test_DCP(channelIndex, control, token);
                    break;
                case "HVDCP":
                    result = await Test_HVDCP(channelIndex, control, token);
                    break;
                case "PPS":
                    result = await Test_PPS(channelIndex, control, token);
                    break;
                case "CHARGE COUNT RESET":
                    result = await Test_ChargeCountReset(channelIndex, control, token, totalResult);
                    break;


                default:
                    control.Logger.Info($"이 작업은 구현되지 않았습니다. : {taskName}");
                    result = false;
                    break;
            }

            
            control.Logger.ResultSection(taskName, result);

            return result;
        }

      

        private static async Task<bool> Test_QrRead(int ch, ChControl control, CancellationToken token)
        {
            var Qr = CommManager.QrPorts[ch];
            if (!Qr.IsOpen)
            {
                Console.WriteLine($"QR PORT가 연결되어있지 않습니다. [CH{ch + 1}]");
                control.Logger.Fail("QR PORT is not connected!");
                return false;
            }
            try
            {
                await Task.Delay(Settings.Instance.QR_READ_Step_Delay);

                string rx = null;
                const int retryMax = 3;
                for (int retry = 1; retry <= retryMax; retry++)
                {
                    token.ThrowIfCancellationRequested();

                    Console.WriteLine($"CH{ch + 1} 스캐너 시작 명령 전송 [QR READ] (시도 {retry})");

                    rx = await Qr.SendAndReceiveAsync(Variable.QR_START, 5000, token);
                    if (rx != null)
                    {
                        Console.WriteLine($"CH{ch + 1} QR 수신 완료: {rx}");
                        break;
                    }

                    await Qr.SendAsync(Variable.QR_END);
                    await Task.Delay(200);
                }

                if (rx == null)
                {
                    control.Logger.Fail("QR Read Fail: null");
                    await Qr.SendAsync(Variable.QR_END);
                    return false;
                }

                bool isQrReadOk = rx.Length == Settings.Instance.QR_READ_Len;
                if (isQrReadOk) control.Logger.Pass($"QR READ : {rx} ===> Len : {rx.Length} [{Settings.Instance.QR_READ_Len}]");
                else control.Logger.Fail($"QR READ : {rx} ===> Len : {rx.Length} [{Settings.Instance.QR_READ_Len}]");

                await Qr.SendAsync(Variable.QR_END);

                return isQrReadOk;
            }
            catch (OperationCanceledException)
            {
                control.UpdateNowStatus(ChControl.NowStatus.STOP);

                throw;
            }
            catch (Exception ex)
            {
                string errorMsg = $"[{ch + 1}CH] QR READ 예외: {ex.Message}";
                Console.WriteLine(errorMsg);
                control.Logger.Fail(errorMsg);
                return false;
            }
            

        }

        private static async Task<bool> Test_McuInfo(int ch, ChControl control, CancellationToken token)
        {
            bool isPass = true;

            var Board = CommManager.Boards[ch];
            var Pba = CommManager.Pbas[ch];
            if (!Board.IsConnected())
            {
                Console.WriteLine($"TCP가 연결되어있지 않습니다. [CH{ch + 1}]");
                control.Logger.Fail("TCP is not connected!");
                return false;
            }
            
            try
            {
                await Task.Delay(Settings.Instance.MCU_INFO_Step_Delay);

                byte[] start_cmd_tx = new TcpProtocol(0xC1, 0x01).GetPacket();
                int start_cmd_timeout = Settings.Instance.Board_Read_Timeout + Settings.Instance.MCU_INFO_Tcp_01_Delay;
                Console.WriteLine($"MCU INFO START CMD RX 수신 대기 [Delay : {start_cmd_timeout}ms] [CH{ch + 1}]");
                byte[] start_cmd_rx = await Board.SendAndReceivePacketAsync(start_cmd_tx, start_cmd_timeout, token);

                if (!UtilityFunctions.CheckTcpRxData(start_cmd_tx, start_cmd_rx))
                {
                    control.Logger.Fail($"START CMD RX 에러");
                    return false;
                }
                control.Logger.Pass($"START CMD 적용 완료");

                //await Task.Delay(Settings.Instance.Pba_On_Delay);

                Console.WriteLine($"pba 연결 딜레이 : {Settings.Instance.Pba_Connect_Timeout}");

                bool connectOk = await Pba.ConnectAsync(Return_Pba_Port_Name(ch), Return_Pba_Port_Baudrate(ch), Settings.Instance.Pba_Connect_Timeout, token);
                if(!connectOk)
                {
                    control.Logger.Fail($"PBA connect fail [{Return_Pba_Port_Name(ch)}]");
                    return false;
                }
                //control.Logger.Pass($"PBA connect success [{Return_Pba_Port_Name(ch)}]");

                byte[] MCU_ID_READ_CMD_tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_MCU_ID).GetPacket();
                int MCU_ID_READ_CMD_timeout = Settings.Instance.Pba_Read_Timeout + Settings.Instance.MCU_INFO_Pba_Delay;
                Console.WriteLine($"MCU ID READ CMD RX 수신 대기 [Delay : {MCU_ID_READ_CMD_timeout}ms] [CH{ch + 1}]");

                byte[] MCU_ID_READ_CMD_rx = await Pba.SendAndReceivePacketAsync_OnlyData(MCU_ID_READ_CMD_tx, MCU_ID_READ_CMD_timeout, token);
                if (MCU_ID_READ_CMD_rx == null) { control.Logger.Fail("MCU ID: Rx is NULL"); return false; }

                string McuId = $"{MCU_ID_READ_CMD_rx[7]:X2}{MCU_ID_READ_CMD_rx[6]:X2}" +
                    $"{MCU_ID_READ_CMD_rx[5]:X2}{MCU_ID_READ_CMD_rx[4]:X2}" +
                    $"{MCU_ID_READ_CMD_rx[3]:X2}{MCU_ID_READ_CMD_rx[2]:X2}" +
                    $"{MCU_ID_READ_CMD_rx[1]:X2}{MCU_ID_READ_CMD_rx[0]:X2}"; 
                bool isPass_McuId = McuId.Length == Settings.Instance.MCU_INFO_Mcu_Id_Len;
                if(!isPass_McuId)
                {
                    control.Logger.Fail($"MCU ID : {McuId} ===> Len : {McuId.Length} [{Settings.Instance.MCU_INFO_Mcu_Id_Len}]");
                    isPass = false;
                }
                control.Logger.Pass($"MCU ID : {McuId} ===> Len : {McuId.Length} [{Settings.Instance.MCU_INFO_Mcu_Id_Len}]");

                byte[] soc_cmd_tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_SOC).GetPacket();
                int soc_cmd_timeout = Settings.Instance.Pba_Read_Timeout + Settings.Instance.MCU_INFO_Pba_Delay;
                Console.WriteLine($"SOC READ CMD RX 수신 대기 [Delay : {soc_cmd_timeout}ms] [CH{ch + 1}]");

                byte[] soc_cmd_rx = await Pba.SendAndReceivePacketAsync_OnlyData(soc_cmd_tx, soc_cmd_timeout, token);
                if (soc_cmd_rx != null && soc_cmd_rx.Length > 0)
                {
                    short socPercent = (short)((soc_cmd_rx[0] << 8) | soc_cmd_rx[1]);
                    control.Logger.Info($"SOC : {socPercent}%");
                }

                byte[] end_cmd_tx = new TcpProtocol(0xC1, 0x02).GetPacket();
                int end_cmd_timeout = Settings.Instance.Board_Read_Timeout + Settings.Instance.MCU_INFO_Tcp_02_Delay;
                Console.WriteLine($"MCU INFO END CMD RX 수신 대기 [Delay : {end_cmd_timeout}ms] [CH{ch + 1}]");
                byte[] end_cmd_rx = await Board.SendAndReceivePacketAsync(end_cmd_tx, end_cmd_timeout, token);

                if (!UtilityFunctions.CheckTcpRxData(end_cmd_tx, end_cmd_rx))
                {
                    control.Logger.Fail($"END CMD RX 이상");
                    return false;
                }

                return isPass;

            }
            catch (OperationCanceledException)
            {
                control.UpdateNowStatus(ChControl.NowStatus.STOP);

                throw;
            }
            catch (Exception ex)
            {
                string errorMsg = $"[{ch + 1}CH] MCU INFO 예외: {ex.Message}";
                Console.WriteLine(errorMsg);
                control.Logger.Fail(errorMsg);
                return false;
            }
        }

        private static async Task<bool> Test_SDP(int ch, ChControl control, CancellationToken token)
        {
            bool isPass = true;

            var Board = CommManager.Boards[ch];
            var Pba = CommManager.Pbas[ch];
            if (!Board.IsConnected())
            {
                Console.WriteLine($"TCP가 연결되어있지 않습니다. [CH{ch + 1}]");
                control.Logger.Fail("TCP is not connected!");
                return false;
            }

            try
            {
                await Task.Delay(Settings.Instance.SDP_Step_Delay);

                byte[] sdp_cmd_tx = new TcpProtocol(0xC2, 0x01).GetPacket();
                int sdp_cmd_timeout = Settings.Instance.Board_Read_Timeout + Settings.Instance.SDP_Tcp_01_Delay;
                Console.WriteLine($"SDP CMD RX Delay : {sdp_cmd_timeout}ms] [CH{ch + 1}]");
                byte[] sdp_cmd_rx = await Board.SendAndReceivePacketAsync(sdp_cmd_tx, sdp_cmd_timeout, token);
                if (!UtilityFunctions.CheckTcpRxData(sdp_cmd_tx, sdp_cmd_rx))
                {
                    control.Logger.Fail("SDP CMD RX 이상");
                    return false;
                }
                control.Logger.Pass("SDP CMD 적용 완료");

                await Task.Delay(Settings.Instance.SDP_TA_Delay);

                bool connectOk = await Pba.ConnectAsync(Return_Pba_Port_Name(ch), Return_Pba_Port_Baudrate(ch), Settings.Instance.Pba_Connect_Timeout, token);
                if (!connectOk)
                {
                    control.Logger.Fail($"PBA connect fail [{Return_Pba_Port_Name(ch)}]");
                    return false;
                }

               
                byte[] ta_cmd_tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_TA_CHECK).GetPacket();
                int ta_cmd_timeout = Settings.Instance.Pba_Read_Timeout + Settings.Instance.SDP_Pba_Delay;
                byte[] ta_cmd_rx = await Pba.SendAndReceivePacketAsync_OnlyData(ta_cmd_tx, ta_cmd_timeout, token);
                if (ta_cmd_rx == null || ta_cmd_rx.Length < 1)
                {
                    control.Logger.Fail("SDP TA 이상 : Rx NULL ");
                    return false;
                }
                short taResponse = (short)((ta_cmd_rx[0] << 8) | ta_cmd_rx[1]);
                if (taResponse != (short)Settings.Instance.SDP_TA_Type)
                {
                    control.Logger.Fail($"SDP TA : {taResponse} [{Settings.Instance.SDP_TA_Type}]");
                    isPass = false;
                }
                control.Logger.Pass($"SDP TA : {taResponse} [{Settings.Instance.SDP_TA_Type}]");

                
                byte[] cur_cmd_tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_BAT_CURRENT).GetPacket();
                int cur_cmd_timeout = Settings.Instance.Pba_Read_Timeout + Settings.Instance.SDP_Pba_Delay;
                byte[] cur_cmd_rx = await Pba.SendAndReceivePacketAsync_OnlyData(cur_cmd_tx, cur_cmd_timeout, token);
                if (cur_cmd_rx == null || cur_cmd_rx.Length < 2)
                {
                    control.Logger.Fail("SDP CHG_SDP Current : Rx NULL ");
                    return false;
                }
                short chgCurrentMa = (short)((cur_cmd_rx[0] << 8) | cur_cmd_rx[1]);
                short chgMin = Settings.Instance.SDP_CHG_Current_Min;
                short chgMax = Settings.Instance.SDP_CHG_Current_Max;
                bool chgOk = chgCurrentMa >= chgMin && chgCurrentMa <= chgMax;
                if (!chgOk)
                {
                    control.Logger.Fail($"CHG_SDP Current: {chgCurrentMa} mA [{chgMin}~{chgMax}]");
                    isPass = false;
                }
                control.Logger.Pass($"CHG_SDP Current: {chgCurrentMa} mA [{chgMin}~{chgMax}]");

                byte[] usb_sdp_tx = new TcpProtocol(0xC2, 0x02).GetPacket();
                int usb_sdp_timeout = Settings.Instance.Board_Read_Timeout + Settings.Instance.SDP_Tcp_02_Delay;
                Console.WriteLine($"USB SDP CMD RX 연결 딜레이: [Delay : {usb_sdp_timeout}ms] [CH{ch + 1}]");
                byte[] usb_sdp_rx = await Board.SendAndReceivePacketAsync(usb_sdp_tx, usb_sdp_timeout, token);
                if (!UtilityFunctions.CheckTcpRxData(usb_sdp_tx, usb_sdp_rx))
                {
                    control.Logger.Fail("USB SDP CMD RX 이상");
                    return false;
                }

                float usbCurrent = BitConverter.ToSingle(usb_sdp_rx, 7);
                float usbMin = Settings.Instance.SDP_USB_Current_Min;
                float usbMax = Settings.Instance.SDP_USB_Current_Max;
                bool usbOk = usbCurrent >= usbMin && usbCurrent <= usbMax;
                if (!usbOk)
                {
                    control.Logger.Fail($"USB_SDP Current: {usbCurrent} mA [{usbMin}~{usbMax}]");
                    isPass = false;
                }
                control.Logger.Pass($"USB_SDP Current: {usbCurrent} mA [{usbMin}~{usbMax}]");


                control.Logger.Pass("SDP END CMD 적용 완료");
                return isPass;
            }
            catch (OperationCanceledException)
            {
                control.UpdateNowStatus(ChControl.NowStatus.STOP);
                throw;
            }
            catch (Exception ex)
            {
                string errorMsg = $"[{ch + 1}CH] SDP 예외: {ex.Message}";
                Console.WriteLine(errorMsg);
                control.Logger.Fail(errorMsg);
                return false;
            }
        }

        private static async Task<bool> Test_DCP(int ch, ChControl control, CancellationToken token)
        {
            bool isPass = true;

            var Board = CommManager.Boards[ch];
            var Pba = CommManager.Pbas[ch];
            if (!Board.IsConnected())
            {
                Console.WriteLine($"TCP가 연결되어있지 않습니다. [CH{ch + 1}]");
                control.Logger.Fail("TCP is not connected!");
                return false;
            }

            try
            {
                await Task.Delay(Settings.Instance.DCP_Step_Delay);

                byte[] dcp_cmd_tx = new TcpProtocol(0xC3, 0x01).GetPacket();
                int dcp_cmd_timeout = Settings.Instance.Board_Read_Timeout + Settings.Instance.DCP_Tcp_01_Delay;
                Console.WriteLine($"DCP CMD RX Delay : {dcp_cmd_timeout}ms] [CH{ch + 1}]");
                byte[] dcp_cmd_rx = await Board.SendAndReceivePacketAsync(dcp_cmd_tx, dcp_cmd_timeout, token);
                if (!UtilityFunctions.CheckTcpRxData(dcp_cmd_tx, dcp_cmd_rx))
                {
                    control.Logger.Fail("DCP CMD RX 이상");
                    return false;
                }
                control.Logger.Pass("DCP CMD 적용 완료");

                await Task.Delay(Settings.Instance.DCP_TA_Delay);

                bool connectOk = await Pba.ConnectAsync(Return_Pba_Port_Name(ch), Return_Pba_Port_Baudrate(ch), Settings.Instance.Pba_Connect_Timeout, token);
                if (!connectOk)
                {
                    control.Logger.Fail($"PBA connect fail [{Return_Pba_Port_Name(ch)}]");
                    return false;
                }

                byte[] ta_cmd_tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_TA_CHECK).GetPacket();
                int ta_cmd_timeout = Settings.Instance.Pba_Read_Timeout + Settings.Instance.DCP_Pba_Delay;
                byte[] ta_cmd_rx = await Pba.SendAndReceivePacketAsync_OnlyData(ta_cmd_tx, ta_cmd_timeout, token);
                if (ta_cmd_rx == null || ta_cmd_rx.Length < 1)
                {
                    control.Logger.Fail("DCP TA 이상 : Rx NULL ");
                    return false;
                }
                short taResponse = (short)((ta_cmd_rx[0] << 8) | ta_cmd_rx[1]);
                if (taResponse != (short)Settings.Instance.DCP_TA_Type)
                {
                    control.Logger.Fail($"TA TYPE : {taResponse} [{Settings.Instance.DCP_TA_Type}]");
                    isPass = false;
                }
                control.Logger.Pass($"TA TYPE : {taResponse} [{Settings.Instance.DCP_TA_Type}]");

                byte[] cur_cmd_tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_BAT_CURRENT).GetPacket();
                int cur_cmd_timeout = Settings.Instance.Pba_Read_Timeout + Settings.Instance.DCP_Pba_Delay;
                byte[] cur_cmd_rx = await Pba.SendAndReceivePacketAsync_OnlyData(cur_cmd_tx, cur_cmd_timeout, token);
                if (cur_cmd_rx == null || cur_cmd_rx.Length < 2)
                {
                    control.Logger.Fail("DCP CHG_DCP Current : Rx NULL ");
                    return false;
                }
                short chgCurrentMa = (short)((cur_cmd_rx[0] << 8) | cur_cmd_rx[1]);
                short chgMin = Settings.Instance.DCP_CHG_Current_Min;
                short chgMax = Settings.Instance.DCP_CHG_Current_Max;
                bool chgOk = chgCurrentMa >= chgMin && chgCurrentMa <= chgMax;
                if (!chgOk)
                {
                    control.Logger.Fail($"CHG_DCP Current : {chgCurrentMa} mA [{chgMin}~{chgMax}]");
                    isPass = false;
                }
                control.Logger.Pass($"CHG_DCP Current : {chgCurrentMa} mA [{chgMin}~{chgMax}]");

                byte[] usb_dcp_tx = new TcpProtocol(0xC3, 0x02).GetPacket();
                int usb_dcp_timeout = Settings.Instance.Board_Read_Timeout + Settings.Instance.DCP_Tcp_02_Delay;
                Console.WriteLine($"USB DCP CMD RX 연결 딜레이: [Delay : {usb_dcp_timeout}ms] [CH{ch + 1}]");
                byte[] usb_dcp_rx = await Board.SendAndReceivePacketAsync(usb_dcp_tx, usb_dcp_timeout, token);
                if (!UtilityFunctions.CheckTcpRxData(usb_dcp_tx, usb_dcp_rx))
                {
                    control.Logger.Fail("USB DCP CMD RX 이상");
                    return false;
                }

                float usbCurrent = BitConverter.ToSingle(usb_dcp_rx, 7);
                float usbMin = Settings.Instance.DCP_USB_Current_Min;
                float usbMax = Settings.Instance.DCP_USB_Current_Max;
                bool usbOk = usbCurrent >= usbMin && usbCurrent <= usbMax;
                if (!usbOk)
                {
                    control.Logger.Fail($"USB_DCP Current: {usbCurrent} mA [{usbMin}~{usbMax}]");
                    isPass = false;
                }
                control.Logger.Pass($"USB_DCP Current: {usbCurrent} mA [{usbMin}~{usbMax}]");



                return isPass;
            }
            catch (OperationCanceledException)
            {
                control.UpdateNowStatus(ChControl.NowStatus.STOP);
                throw;
            }
            catch (Exception ex)
            {
                string errorMsg = $"[{ch + 1}CH] DCP 예외: {ex.Message}";
                Console.WriteLine(errorMsg);
                control.Logger.Fail(errorMsg);
                return false;
            }
        }

        private static async Task<bool> Test_HVDCP(int ch, ChControl control, CancellationToken token)
        {
            bool isPass = true;
            var Board = CommManager.Boards[ch];
            var Pba = CommManager.Pbas[ch];

            if (!Board.IsConnected())
            {
                Console.WriteLine($"TCP가 연결되어있지 않습니다. [CH{ch + 1}]");
                control.Logger.Fail("TCP is not connected!");
                return false;
            }

            try
            {
                // STEP Delay: 검사 시작 전 대기
                await Task.Delay(Settings.Instance.HVDCP_Step_Delay);

                // --- Step 1~4: HVDCP 시작 설정 (시트 1~4번) ---
                // 1. HVDCP CMD (0xC4, 0x01) 송신
                byte[] hvdcp_cmd_tx = new TcpProtocol(0xC4, 0x01).GetPacket();
                int hvdcp_cmd_timeout = Settings.Instance.Board_Read_Timeout + Settings.Instance.HVDCP_Tcp_01_Delay;

                byte[] hvdcp_cmd_rx = await Board.SendAndReceivePacketAsync(hvdcp_cmd_tx, hvdcp_cmd_timeout, token);
                if (!UtilityFunctions.CheckTcpRxData(hvdcp_cmd_tx, hvdcp_cmd_rx))
                {
                    control.Logger.Fail("HVDCP CMD (C4 01) 응답 이상");
                    return false;
                }

                await Task.Delay(Settings.Instance.HVDCP_TA_Delay);
                bool connectOk = await Pba.ConnectAsync(Return_Pba_Port_Name(ch), Return_Pba_Port_Baudrate(ch), Settings.Instance.Pba_Connect_Timeout, token);
                if (!connectOk)
                {
                    control.Logger.Fail($"PBA connect fail [{Return_Pba_Port_Name(ch)}]");
                    return false;
                }

                // --- Step 7~9: TA 모드 확인 CMD (7F 01) ---
                byte[] ta_check_tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_TA_CHECK).GetPacket();
                int pba_delay = Settings.Instance.Pba_Read_Timeout + Settings.Instance.HVDCP_Pba_Delay;

                byte[] ta_check_rx = await Pba.SendAndReceivePacketAsync_OnlyData(ta_check_tx, pba_delay, token);
                if (ta_check_rx == null || ta_check_rx.Length < 2)
                {
                    control.Logger.Fail("HVDCP TA 확인 응답 NULL");
                    return false;
                }

                short taType = (short)((ta_check_rx[0] << 8) | ta_check_rx[1]);
                if (taType != (short)Settings.Instance.HVDCP_TA_Type) // 설정값 2
                {
                    control.Logger.Fail($"TA TYPE : {taType} [{Settings.Instance.HVDCP_TA_Type}]");
                    isPass = false;
                }
                control.Logger.Pass($"TA TYPE : {taType} [{Settings.Instance.HVDCP_TA_Type}]");

                // --- Step 10~12: 배터리 전류 확인 (CHG_HVDCP Current) ---
                byte[] chg_cur_tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_BAT_CURRENT).GetPacket();
                byte[] chg_cur_rx = await Pba.SendAndReceivePacketAsync_OnlyData(chg_cur_tx, pba_delay, token);

                if (chg_cur_rx == null || chg_cur_rx.Length < 2)
                {
                    control.Logger.Fail("CHG CUR RX receive fail");
                    return false;
                }

                short chgCurrent = (short)((chg_cur_rx[0] << 8) | chg_cur_rx[1]);
                if (chgCurrent < Settings.Instance.HVDCP_CHG_Current_Min || chgCurrent > Settings.Instance.HVDCP_CHG_Current_Max)
                {
                    control.Logger.Fail($"CHG_HVDCP Current : {chgCurrent} mA [{Settings.Instance.HVDCP_CHG_Current_Min} ~" +
                        $" {Settings.Instance.HVDCP_CHG_Current_Max}]");
                    isPass = false;
                }
                control.Logger.Pass($"CHG_HVDCP Current: {chgCurrent} mA [{Settings.Instance.HVDCP_CHG_Current_Min} ~" +
                        $" {Settings.Instance.HVDCP_CHG_Current_Max}]");

                // --- Step 13~14: USB HVDCP 전류 확인 (0xC4, 0x02) ---
                byte[] usb_hvdcp_tx = new TcpProtocol(0xC4, 0x02).GetPacket();
                hvdcp_cmd_timeout = Settings.Instance.Board_Read_Timeout + Settings.Instance.HVDCP_Tcp_02_Delay;
                byte[] usb_hvdcp_rx = await Board.SendAndReceivePacketAsync(usb_hvdcp_tx, hvdcp_cmd_timeout, token);

                if (!UtilityFunctions.CheckTcpRxData(usb_hvdcp_tx, usb_hvdcp_rx))
                {
                    control.Logger.Fail($"HVDCP USB Current rx receive Fail");
                    return false;
                }

                float usbCurrent = BitConverter.ToSingle(usb_hvdcp_rx, 7); // 보드 데이터 파싱
                if (usbCurrent < Settings.Instance.HVDCP_USB_Current_Min || usbCurrent > Settings.Instance.HVDCP_USB_Current_Max)
                {
                    control.Logger.Fail($"USB_HVDCP Current : {usbCurrent} mA [{Settings.Instance.HVDCP_USB_Current_Min}" +
                        $" ~ {Settings.Instance.HVDCP_USB_Current_Max}]");
                    isPass = false;
                }
                control.Logger.Pass($"USB_HVDCP Current: {usbCurrent} mA [{Settings.Instance.HVDCP_USB_Current_Min}" +
                        $" ~ {Settings.Instance.HVDCP_USB_Current_Max}]");

                return isPass;
            }
            catch (Exception ex)
            {
                string errorMsg = $"[{ch + 1}CH] HVDCP 예외: {ex.Message}";
                Console.WriteLine(errorMsg);
                control.Logger.Fail(errorMsg);
                return false;
            }
        }

        private static async Task<bool> Test_PPS(int ch, ChControl control, CancellationToken token)
        {
            bool isPass = true;

            var Board = CommManager.Boards[ch];
            var Pba = CommManager.Pbas[ch];
            var Module = CommManager.PowerMeterPorts[ch];
            int module_timeout = 1000;
            if (!Board.IsConnected())
            {
                Console.WriteLine($"TCP가 연결되어있지 않습니다. [CH{ch + 1}]");
                control.Logger.Fail("TCP is not connected!");
                return false;
            }
            if (!Module.IsConnected())
            {
                Console.WriteLine($"POWER-Z MODULE이 연결되어있지 않습니다. [CH{ch + 1}]");
                control.Logger.Fail("POWER-Z MODULE is not connected!");
                return false;
            }
            try
            {
                // Step 0: 검사 시작 전 Delay
                await Task.Delay(Settings.Instance.PPS_Step_Delay, token);

                // Step 1~5: PPS 시작 커맨드 (0xC5 0x01)
                byte[] pps_start_tx = new TcpProtocol(0xC5, 0x01).GetPacket();
                int tcp01_timeout = Settings.Instance.Board_Read_Timeout + Settings.Instance.PPS_Tcp_01_Delay;
                byte[] pps_start_rx = await Board.SendAndReceivePacketAsync(pps_start_tx, tcp01_timeout, token);

                if (!UtilityFunctions.CheckTcpRxData(pps_start_tx, pps_start_rx))
                {
                    control.Logger.Fail("PPS CMD (0xC5 0x01) 응답 이상");
                    return false;
                }

                // Step 6: TA 모드 진입 Delay (강제 7000ms)
                await Task.Delay(Settings.Instance.PPS_TA_Delay, token);
                bool connectOk = await Pba.ConnectAsync(Return_Pba_Port_Name(ch), Return_Pba_Port_Baudrate(ch), Settings.Instance.Pba_Connect_Timeout, token);
                if (!connectOk)
                {
                    control.Logger.Fail($"PBA connect fail [{Return_Pba_Port_Name(ch)}]");
                    return false;
                }

                // Step 7~9: TA 모드 확인 CMD (7F 01)
                byte[] ta_check_tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_TA_CHECK).GetPacket();
                int pba_timeout = Settings.Instance.Pba_Read_Timeout + Settings.Instance.PPS_Pba_Delay;
                byte[] ta_check_rx = await Pba.SendAndReceivePacketAsync_OnlyData(ta_check_tx, pba_timeout, token);

                short taType = (short)((ta_check_rx[0] << 8) | ta_check_rx[1]);
                if (taType != Settings.Instance.PPS_TA_Type) // Target: 6
                {
                    control.Logger.Fail($"TA TYPE : {taType} [{Settings.Instance.PPS_TA_Type}]");
                    isPass = false;
                }
                control.Logger.Pass($"TA TYPE : {taType} [{Settings.Instance.PPS_TA_Type}]");

                // Step 10~12: 배터리 전류 확인 (CHG_PPS Current)
                byte[] chg_cur_tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_BAT_CURRENT).GetPacket();
                byte[] chg_cur_rx = await Pba.SendAndReceivePacketAsync_OnlyData(chg_cur_tx, pba_timeout, token);

                if (chg_cur_rx == null || chg_cur_rx.Length < 2)
                {
                    control.Logger.Fail("CHG CUR RX receive fail");
                    return false;
                }

                short chgCurrent = (short)((chg_cur_rx[0] << 8) | chg_cur_rx[1]);
                if (chgCurrent < Settings.Instance.PPS_CHG_Current_Min || chgCurrent > Settings.Instance.PPS_CHG_Current_Max)
                {
                    control.Logger.Fail($"CHG_PPS Current : {chgCurrent} mA [{Settings.Instance.PPS_CHG_Current_Min} ~" +
                        $" {Settings.Instance.PPS_CHG_Current_Max}]");
                    isPass = false;
                }
                control.Logger.Pass($"CHG_PPS Current: {chgCurrent} mA [{Settings.Instance.PPS_CHG_Current_Min} ~" +
                        $" {Settings.Instance.PPS_CHG_Current_Max}]");

                var Getted_value = new PowerMeterValue();
                Getted_value = await Module.GetValueAsync(module_timeout, token);
                float Module_Current = Getted_value.Current;
                if (Module_Current < Settings.Instance.PPS_USB_Current_Min || Module_Current > Settings.Instance.PPS_USB_Current_Max)
                {
                    control.Logger.Fail($"USB_PPS Current (getted by power-z) : {Module_Current} " +
                        $"[{Settings.Instance.PPS_USB_Current_Min} ~ {Settings.Instance.PPS_USB_Current_Max}]");
                    isPass = false;
                }
                control.Logger.Pass($"USB_PPS Current (getted by power-z) : {Module_Current} " +
                        $"[{Settings.Instance.PPS_USB_Current_Min} ~ {Settings.Instance.PPS_USB_Current_Max}]");

                // Step 14~16: PPS 종료 커맨드 (0xC5 0x02)
                byte[] pps_end_tx = new TcpProtocol(0xC5, 0x02).GetPacket();
                int tcp02_timeout = Settings.Instance.Board_Read_Timeout + Settings.Instance.PPS_Tcp_02_Delay;
                byte[] pps_end_rx = await Board.SendAndReceivePacketAsync(pps_end_tx, tcp02_timeout, token);

                if (!UtilityFunctions.CheckTcpRxData(pps_end_tx, pps_end_rx))
                {
                    control.Logger.Fail("PPS END CMD (0xC5 0x02) 응답 이상");
                    return false;
                }

                return isPass;
            }
            catch (Exception ex)
            {
                control.Logger.Fail($"PPS 예외 발생: {ex.Message}");
                return false;
            }
        }

        private static async Task<bool> Test_ChargeCountReset(int ch, ChControl control, CancellationToken token, bool totalResult)
        {
            bool isPass = true;

            var Board = CommManager.Boards[ch];
            var Pba = CommManager.Pbas[ch];

            if (!Board.IsConnected())
            {
                Console.WriteLine($"TCP가 연결되어있지 않습니다. [CH{ch + 1}]");
                control.Logger.Fail("TCP is not connected!");
                return false;
            }

            try
            {
                // Step 0: 검사 시작 전 Delay
                await Task.Delay(Settings.Instance.CHARGE_COUNT_RESET_Step_Delay, token);

                // Step 1~5: CHARGING COUNT RESET CMD 전송 (0xC6 0x01)
                byte[] reset_start_tx = new TcpProtocol(0xC6, 0x01).GetPacket();
                int tcp01_timeout = Settings.Instance.Board_Read_Timeout + Settings.Instance.CHARGE_COUNT_RESET_Tcp_01_Delay;
                byte[] reset_start_rx = await Board.SendAndReceivePacketAsync(reset_start_tx, tcp01_timeout, token);

                if (!UtilityFunctions.CheckTcpRxData(reset_start_tx, reset_start_rx))
                {
                    control.Logger.Fail("CHARGE COUNT RESET CMD (0xC6 0x01) 응답 이상");
                    return false;
                }

                
                await Task.Delay(Settings.Instance.Pba_On_Delay, token); //TA 딜레이 필요없으므로 On delay 사용

                // PBA 연결 확인
                bool connectOk = await Pba.ConnectAsync(Return_Pba_Port_Name(ch), Return_Pba_Port_Baudrate(ch), Settings.Instance.Pba_Connect_Timeout, token);
                if (!connectOk)
                {
                    control.Logger.Fail($"PBA connect fail [{Return_Pba_Port_Name(ch)}]");
                    return false;
                }

                int pba_timeout = Settings.Instance.Pba_Read_Timeout + Settings.Instance.CHARGE_COUNT_RESET_Pba_Delay;

                // Step 6~7: FIFG_CHARGE_COUNT Write 전송 (0x21C~0x21F Write 0)
                byte[] count_write_tx = new CDCProtocol(Variable.SLAVE, Variable.MULTI_WRITE, Variable.MULTI_WRITE_FIFG_CHARGE_COUNT).GetPacket();
                byte[] count_write_rx = await Pba.SendAndReceivePacketAsync(count_write_tx, pba_timeout, token);
                if (!UtilityFunctions.CheckWriteMultiAck(count_write_tx, count_write_rx))
                {
                    control.Logger.Fail($"FIFG CHARGE COUNT write 이상");
                    return false;
                }

                // Step 8~10: FIFG_CHARGE_COUNT Read 전송 및 데이터 확인 (00.00)
                byte[] count_read_tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_FIFG_CHARGE_COUNT).GetPacket();
                byte[] count_read_rx = await Pba.SendAndReceivePacketAsync_OnlyData(count_read_tx, pba_timeout, token);

                if (count_read_rx == null || count_read_rx.Length < 4)
                {
                    control.Logger.Fail("CHARGE COUNT Read fail");
                    return false;
                }
                short charge_count_data_1 = (short)((count_read_rx[0] << 8) | count_read_rx[1]);
                short charge_count_data_2 = (short)((count_read_rx[2] << 8) | count_read_rx[3]);
                short charge_count_data_3 = (short)((count_read_rx[4] << 8) | count_read_rx[5]);
                short charge_count_data_4 = (short)((count_read_rx[6] << 8) | count_read_rx[7]);

                string charge_count_result = $"{charge_count_data_1}{charge_count_data_2}.{charge_count_data_3}{charge_count_data_4}";

                //if (charge_count_data_1 != 0 || charge_count_data_2 != 0 || charge_count_data_3 != 0 || charge_count_data_4 != 0)
                //{
                //    Console.WriteLine($"CHARGE COUNT : {charge_count_result}. [CH{ch + 1}]");
                //}

                Console.WriteLine($"CHARGE COUNT : {charge_count_result}. [CH{ch + 1}]"); //몰래넣는거니까 콘솔창에만 표시


                // Step 11~13: Flag Write (PASS: 0BBC 0007) 및 Read 확인
                byte[] Write_Flag = totalResult ? Variable.WRITE_CHARGE_FLAG_PASS : Variable.WRITE_CHARGE_FLAG_FAIL;
                short Expected_Flag = (short)((Write_Flag[2] << 8) | Write_Flag[3]);

                byte[] flag_write_tx = new CDCProtocol(Variable.SLAVE, Variable.WRITE, Write_Flag).GetPacket();
                byte[] flag_write_rx = await Pba.SendAndReceivePacketAsync(flag_write_tx, pba_timeout, token);
                if (UtilityFunctions.CheckEchoAck(flag_write_tx, flag_write_rx))
                {
                    control.Logger.Fail("FLAG WRITE FAIL");
                    return false;
                }

                byte[] flag_read_tx = new CDCProtocol(Variable.SLAVE, Variable.READ, Variable.READ_CHARGE_FLAG).GetPacket();
                byte[] flag_read_rx = await Pba.SendAndReceivePacketAsync_OnlyData(flag_read_tx, pba_timeout, token);

                if (flag_read_rx == null || flag_read_rx.Length < 2)
                {
                    control.Logger.Fail($"FLAG Read fail");
                    return false;
                }

                short flag_result = (short)((flag_read_rx[0] << 8) | flag_read_rx[1]);
                if (flag_result != Expected_Flag)
                {
                    control.Logger.Fail($"FLAG : {flag_result} [{Expected_Flag}]");
                    isPass = false;
                }
                control.Logger.Pass($"FLAG : {flag_result} [{Expected_Flag}]");

                // Step 14~15: 검사완료 cmd 전송 (초기화 Tester initialize cmd - 0xC6 0x02)
                byte[] reset_end_tx = new TcpProtocol(0xC6, 0x02).GetPacket();
                int tcp02_timeout = Settings.Instance.Board_Read_Timeout + Settings.Instance.CHARGE_COUNT_RESET_Tcp_02_Delay;
                byte[] reset_end_rx = await Board.SendAndReceivePacketAsync(reset_end_tx, tcp02_timeout, token);

                if (!UtilityFunctions.CheckTcpRxData(reset_end_tx, reset_end_rx))
                {
                    control.Logger.Fail("CHARGE COUNT RESET END CMD (0xC6 0x02) 응답 이상");
                    return false;
                }

                return isPass;
            }
            catch (Exception ex)
            {
                control.Logger.Fail($"CHARGE COUNT RESET 예외 발생: {ex.Message}");
                return false;
            }
        }
        #region Utility
        private static string Return_Pba_Port_Name (int ch)
        {
            string portName = "";
            switch (ch)
            {
                case 0:
                    portName = Settings.Instance.Device_Port_CH1;
                    break;
                case 1:
                    portName = Settings.Instance.Device_Port_CH2;
                    break;
                case 2:
                    portName = Settings.Instance.Device_Port_CH3;
                    break;
                case 3:
                    portName = Settings.Instance.Device_Port_CH4;
                    break;

            }

            return portName;
        }

        private static int Return_Pba_Port_Baudrate(int ch)
        {
            int Baudrate = 115200;
            switch (ch)
            {
                case 0:
                    Baudrate = Settings.Instance.Device_BaudRate_CH1;
                    break;
                case 1:
                    Baudrate = Settings.Instance.Device_BaudRate_CH2;
                    break;
                case 2:
                    Baudrate = Settings.Instance.Device_BaudRate_CH3;
                    break;
                case 3:
                    Baudrate = Settings.Instance.Device_BaudRate_CH4;
                    break;

            }

            return Baudrate;
        }

        #endregion
    }
}
