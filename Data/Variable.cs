using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace p2_40_Charge_Tester.Data
{
    internal static class Variable
    {
        public const byte SLAVE = 0x3A; //=====> p2 4.0 은 3A
        public const string SLAVE_STR = "3A";

        public const byte READ = 0x03;
        public const byte WRITE = 0x06;
        public const byte MULTI_WRITE = 0x10;

        public static readonly byte[] QR_START = { 0x16, 0x54, 0x0D }; //Qr 찍기 명령
        public static readonly byte[] QR_END = { 0x16, 0x55, 0x0D }; //Qr 찍기 종료 명령

        public static readonly byte[] CHARGE_OFF = { 0x00, 0x0B, 0x00, 0x00 };

        public static readonly byte[] READ_MCU_ID = { 0x00, 0x41, 0x00, 0x04 };
        public static readonly string READ_MCU_ID_ADDR = "0041";
        public static readonly string READ_MCU_ID_BYTECOUNT = "0004";

        /// <summary> SOC 확인 CMD (000C), 1바이트 읽기 </summary>
        public static readonly byte[] READ_SOC = { 0x00, 0x0C, 0x00, 0x01 };

        /// <summary> SDP TA 모드 확인 CMD (7F 01), 1바이트 읽기 </summary>
        public static readonly byte[] READ_TA_CHECK = { 0x00, 0x7F, 0x00, 0x01 };
        /// <summary> SDP 전류 확인 CMD (0A), 2바이트 읽기 (mA) </summary>
        public static readonly byte[] READ_BAT_CURRENT = { 0x00, 0x0A, 0x00, 0x01 };

        public static readonly byte[] MULTI_WRITE_FIFG_CHARGE_COUNT = { 0x02, 0x1C, 0x00, 0x04, 0x08,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] READ_FIFG_CHARGE_COUNT = { 0x02, 0x1C, 0x00, 0x04 };

        public static readonly byte[] WRITE_CHARGE_FLAG_PASS = { 0xB, 0xBC, 0x00, 0x07 };
        public static readonly byte[] WRITE_CHARGE_FLAG_FAIL = { 0xB, 0xBC, 0x00, 0x03 };
        public static readonly byte[] READ_CHARGE_FLAG = { 0xB, 0xBC, 0x00, 0x01 };




        public static readonly byte[] READ_IMAGE_FW_VER = { 0x00, 0x2A, 0x00, 0x02 };

        public static readonly byte[] WRITE_VSYS_EN_PIN_ON = { 0x00, 0x05, 0x00, 0x08 };
        public static readonly byte[] WRITE_VDD_3V3_ON = { 0x00, 0x05, 0x00, 0x02 };
        public static readonly byte[] WRITE_LCD_3V0_ON = { 0x00, 0x05, 0x00, 0x04 };
        public static readonly byte[] WRITE_DC_BOOST_ON = { 0x00, 0x05, 0x00, 0x01 };

        public static readonly byte[] WRITE_LDO_OFF = { 0x00, 0x05, 0x00, 0x00 };

        public static readonly byte[] WRITE_SLEEP_CMD = { 0x00, 0x06 }; //데이터가 없는건가
        public static readonly byte[] WRITE_SHIP_CMD = { 0x00, 0x01, 0x00, 0x01 };

        public static readonly byte[] WRITE_VIB_TEST_START = { 0x00, 0x09, 0x00, 0x01 };

        public static readonly byte[] READ_FLOOD_STATE = { 0x00, 0x9C, 0x00, 0x01 };

        public static readonly byte[] WRITE_CARTRIDGE_BOOST_ON = { 0x00, 0x07, 0x00, 0x01 };
        public static readonly byte[] WRITE_CARTRIDGE_BOOST_OFF = { 0x00, 0x07, 0x00, 0x00 };

        public static readonly byte[] WRITE_SUB_HEATER_BOOST_ON = { 0x00, 0x07, 0x00, 0x02 };
        public static readonly byte[] WRITE_SUB_HEATER_BOOST_OFF = { 0x00, 0x07, 0x00, 0x00 };

        public static readonly byte[] WRITE_ACCEL_IC_TEST_START = { 0x00, 0x52, 0x00, 0x01 };
        public static readonly byte[] READ_ACCEL_IC_TEST_RESULT = { 0x00, 0x25, 0x00, 0x01 };

        public static readonly byte[] WRITE_MCU_FLASH_INTEGRITY_CHECK_START = { 0x00, 0x0C, 0x00, 0x00 };
        public static readonly byte[] READ_FLASH_INTEGRITY_CHECK_RESULT = { 0x00, 0x87, 0x00, 0x01 };
        public static readonly byte[] WRITE_EXT_FLASH_INTEGRITY_CHECK_START = { 0x00, 0x0C, 0x00, 0x00 };
        public static readonly byte[] READ_EXT_FLASH_INTEGRITY_CHECK_RESULT = { 0x00, 0x89, 0x00, 0x01 };

        public static readonly byte[] WRITE_GPAK_TEST_START = { 0x00, 0x50, 0x00, 0x01 };
        public static readonly byte[] READ_GPAK_TEST_RESULT = { 0x00, 0x26, 0x00, 0x01 };
        public static readonly byte[] WRITE_GPAK_TEST_END = { 0x00, 0x50, 0x00, 0x00 };

        public static readonly byte[] READ_SERIAL_NO = { 0x08, 0x08, 0x00, 0x10 };
        public static readonly byte[] READ_FW_VER = { 0x00, 0x30, 0x00, 0x05 };
        public static readonly byte[] READ_EXT_MEMORY_VER = { 0x00, 0x2A, 0x00, 0x02 };
        public static readonly byte[] READ_PID = { 0x0B, 0xBD, 0x00, 0x0C };

        public static readonly byte[] GRADIENT = { 0x0B, 0xAD, 0x00, 0x0C };

        public static readonly byte[] READ_INIT_MAIN_HEATER_TEMP = { 0x0B, 0xDD, 0x00, 0x04 };
        public static readonly byte[] READ_INIT_MAIN_HEATER_RESIST = { 0x0B, 0xD9, 0x00, 0x04 };
        public static readonly byte[] READ_INIT_SUB_HEATER_TEMP = { 0x0B, 0xED, 0x00, 0x04 };
        public static readonly byte[] READ_INIT_SUB_HEATER_RESIST = { 0x0B, 0xE9, 0x00, 0x04 };

        public static readonly byte[] WRITE_INIT_MAIN_HEATER_RESIST = { 0x00, 0x10, 0x00, 0x00 };
        public static readonly byte[] WRITE_INIT_SUB_HEATER_RESIST = { 0x00, 0x12, 0x00, 0x00 };

        public static readonly byte[] READ_PROFILE_TEMP = { 0x08, 0x19, 0x00, 0x10 };
        public static readonly byte[] READ_PROFILE_TIME = { 0x08, 0x99, 0x00, 0x08 };

        public static readonly byte[] WRITE_FLASH_UPDATE = { 0x00, 0x03, 0x00, 0x00 };
        public static readonly byte[] WRITE_CHARGER_OFF = { 0x00, 0x0B, 0x00, 0x00 };
        public static readonly byte[] WRITE_CHARGER_ON = { 0x00, 0x0B, 0x00, 0x01 };


        public static readonly byte[] WRITE_CART_ON = { 0x00, 0x14, 0x00, 0x00 };
        public static readonly byte[] WRITE_CART_OFF = { 0x00, 0x14, 0x00, 0x01 };

        public static readonly byte[] READ_HEATING_INFO = { 0x00, 0x00, 0x00, 0x11 }; //길이 확인 필요 //내부적으로 공간이 더 있나?

        public static readonly byte[] HEATING_START = { 0x00, 0x00, 0x00, 0x02 };
        public static readonly byte[] HEATING_STOP = { 0x00, 0x00, 0x00, 0x01 };

        public static readonly byte[] READ_PROCESS_FLAG = { 0x0B, 0xB9, 0x00, 0x04 };
        public static readonly byte[] INIT_PROCESS_FLAG = { 0x0B, 0xB9, 0x00, 0x00 };

        //HEATING PROFILE
        public static readonly byte[] READ_NORMAL_HEATER_TEMP_STANDARD = { 0x08, 0x19, 0x00, 0x10 };
        public static readonly byte[] READ_NORMAL_HEATER_TEMP_CASUAL = { 0x08, 0x29, 0x00, 0x10 };
        public static readonly byte[] READ_NORMAL_HEATER_TEMP_CLASSIC = { 0x08, 0x39, 0x00, 0x10 };
        public static readonly byte[] READ_NORMAL_HEATER_TEMP_USER = { 0x08, 0x49, 0x00, 0x10 };

        public static readonly byte[] READ_OV_HEATER_TEMP_STANDARD = { 0x08, 0x59, 0x00, 0x10 };
        public static readonly byte[] READ_OV_HEATER_TEMP_CASUAL = { 0x08, 0x69, 0x00, 0x10 };
        public static readonly byte[] READ_OV_HEATER_TEMP_CLASSIC = { 0x08, 0x79, 0x00, 0x10 };
        public static readonly byte[] READ_OV_HEATER_TEMP_USER = { 0x08, 0x89, 0x00, 0x10 };

        public static readonly byte[] READ_NORMAL_HEATER_TIME_STANDARD = { 0x08, 0x99, 0x00, 0x08 };
        public static readonly byte[] READ_NORMAL_HEATER_TIME_CASUAL = { 0x08, 0xA1, 0x00, 0x08 };
        public static readonly byte[] READ_NORMAL_HEATER_TIME_CLASSIC = { 0x08, 0xA9, 0x00, 0x08 };
        public static readonly byte[] READ_NORMAL_HEATER_TIME_USER = { 0x08, 0xB1, 0x00, 0x08 };

        public static readonly byte[] READ_OV_HEATER_TIME_STANDARD = { 0x08, 0xB9, 0x00, 0x08 };
        public static readonly byte[] READ_OV_HEATER_TIME_CASUAL = { 0x08, 0xC1, 0x00, 0x08 };
        public static readonly byte[] READ_OV_HEATER_TIME_CLASSIC = { 0x08, 0xC9, 0x00, 0x08 };
        public static readonly byte[] READ_OV_HEATER_TIME_USER = { 0x08, 0xD1, 0x00, 0x08 };

        public static readonly byte[] READ_BATTERY = { 0x00, 0x07, 0x00, 0x06 };
        public static readonly byte[] READ_BATTERY_CYCLE = { 0x00, 0x24, 0x00, 0x01 };

        public static readonly byte[] READ_IDLE_CURRENT = { 0x00, 0x0A, 0x00, 0x01 };

        public static readonly byte[] WRITE_PRESSURE_ON = { 0x00, 0x04, 0x00, 0x02 };
        public static readonly byte[] WRITE_PRESSURE_OFF = { 0x00, 0x04, 0x00, 0x00 };
        public static readonly byte[] READ_PRESSURE_DATA = { 0x00, 0x0E, 0x00, 0x02 };

        public static readonly byte[] WRITE_MOTOR_CURRENT = { 0x00, 0x09, 0x00, 0x01 };
        public static readonly byte[] READ_MOTOR_CURRENT = { 0x00, 0x2F, 0x00, 0x01 };

        public static readonly byte[] WRITE_MCU_CHECKSUM = { 0x00, 0x0C, 0x00, 0x87 };
        public static readonly byte[] READ_MCU_CHECKSUM = { 0x00, 0x87, 0x00, 0x01 };

        public static readonly byte[] WRITE_EXT_MEMORY_CHECKSUM = { 0x00, 0x0D, 0x00, 0x89 };
        public static readonly byte[] READ_EXT_MEMORY_CHECKSUM = { 0x00, 0x89, 0x00, 0x01 };

        public static readonly byte[] READ_MCU_NTC = { 0x00, 0x08, 0x00, 0x01 };

        public static readonly byte[] READ_RTC_CHECK = { 0x02, 0x06, 0x00, 0x06 };

        public static readonly byte[] WRITE_ERROR_CLEAR = { 0x00, 0x42, 0x00, 0x01 };

        public static readonly byte[] WRITE_SHIP_MODE = { 0x00, 0x01, 0x00, 0x01 };

        //public static readonly byte[] WRITE_FINAL_FLAG = { 0x09, 0x53, 0x00, 0xFF }
    }
}
