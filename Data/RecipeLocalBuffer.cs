using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace p2_40_Charge_Tester.Data
{
    public sealed class RecipeLocalBuffer
    {
        //여기에 적힌 값들이 레시피 디폴트 값
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
    }
}
