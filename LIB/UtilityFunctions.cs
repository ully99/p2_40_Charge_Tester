using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using p2_40_Charge_Tester.Data;
using p2_40_Charge_Tester.Communication;
using p2_40_Charge_Tester.UI;


namespace p2_40_Charge_Tester.LIB
{
    internal static class UtilityFunctions
    {
        

        public static bool CheckEchoAck(byte[] tx, byte[] rx) //write 시 응답검사
        {
            if (rx == null || rx.Length == 0) { return false; }

            // 1) 문자열 비교(간단 경로)
            string sTx = Encoding.ASCII.GetString(tx).Trim().ToUpperInvariant();
            string sRx = Encoding.ASCII.GetString(rx).Trim().ToUpperInvariant();

            if (sTx == sRx) return true;
            else return false;
        }

        public static bool CheckWriteMultiAck(byte[] tx, byte[] rx)
        {
            if (tx == null || rx == null) return false;

            byte[] txFrame;
            byte[] rxFrame;

            // 1) tx ASCII → PacketBytes 변환
            if (!TryParseAsciiBytesToPacketBytes(tx, out txFrame))
                txFrame = tx;

            // 2) rx ASCII → PacketBytes 변환
            if (!TryParseAsciiBytesToPacketBytes(rx, out rxFrame))
                rxFrame = rx;

            // 최소 7바이트: Slave Func AddrHi AddrLo QtyHi QtyLo
            if (txFrame == null || txFrame.Length < 7) return false;
            if (rxFrame == null || rxFrame.Length < 7) return false;

            // 3) txFrame에서 Slave / Func / Addr / Qty 추출
            byte slave = txFrame[0];
            byte func = txFrame[1];
            if (func != 0x10) return false;   // Multi Write 전용

            ushort txAddr = (ushort)((txFrame[2] << 8) | txFrame[3]);
            ushort txQty = (ushort)((txFrame[4] << 8) | txFrame[5]);

            // 4) rxFrame에서 동일한 패턴 찾기
            for (int i = 0; i <= rxFrame.Length - 6; i++)
            {
                if (rxFrame[i] == slave && rxFrame[i + 1] == 0x10)
                {
                    ushort rxAddr = (ushort)((rxFrame[i + 2] << 8) | rxFrame[i + 3]);
                    ushort rxQty = (ushort)((rxFrame[i + 4] << 8) | rxFrame[i + 5]);

                    if (rxAddr == txAddr && rxQty == txQty)
                        return true;
                }
            }

            return false;
        }

        public static bool CheckTcpRxData(byte[] tx, byte[] rx)
        {
            if (rx == null) return false;
            if (rx.Length < 7) return false;
            if (tx[5] != rx[5]) return false;

            return true;
        }

        public static bool TryParseAsciiBytesToPacketBytes(byte[] asciiFrame, out byte[] bytes)
        {
            bytes = null;
            if (asciiFrame == null || asciiFrame.Length < 3) return false;

            // 1) 문자열 변환
            string s = System.Text.Encoding.ASCII.GetString(asciiFrame);

            // 2) 시작 콜론(:) 위치
            int colon = s.IndexOf(':');
            if (colon >= 0) s = s.Substring(colon + 1);

            // 3) 줄바꿈 제거
            while (s.Length > 0 && (s[s.Length - 1] == '\r' || s[s.Length - 1] == '\n'))
                s = s.Substring(0, s.Length - 1);

            // 4) 짝수 길이 체크
            if ((s.Length % 2) != 0) return false;

            int len = s.Length / 2;
            byte[] buf = new byte[len];
            for (int i = 0; i < len; i++)
            {
                int hi = HexVal(s[2 * i]);
                int lo = HexVal(s[2 * i + 1]);
                if (hi < 0 || lo < 0) return false;
                buf[i] = (byte)((hi << 4) | lo);
            }
            bytes = buf;
            return true;
        }

        public static int HexVal(char c) //ex "3" -> 3 // "A" -> 10
        {
            if (c >= '0' && c <= '9') return c - '0';
            if (c >= 'A' && c <= 'F') return c - 'A' + 10;
            if (c >= 'a' && c <= 'f') return c - 'a' + 10;
            return -1;
        }

        public static string JoinUShorts(System.Collections.Generic.IEnumerable<ushort> arr)
        {
            return string.Join(" ", arr.Select(x => x.ToString()));
        }

        public static string BuildCompareLine(string title, string kind, ushort[] expected, ushort[] actual)
        {
            // kind = "Temp" 또는 "Time"
            return string.Format(
                "{0} {1} : {2} [{3}]",
                title,
                kind,
                JoinUShorts(expected),
                JoinUShorts(actual)
            );
        }


        public static ushort ParseU16HexLoose(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new FormatException("값이 비어있음");

            s = s.Trim();

            // 사용자가 1, 17, A, 0017 이런 식으로 넣는 걸 전부 허용
            // 그냥 HEX로 해석
            return Convert.ToUInt16(s, 16);
        }

        public static ushort ParseU16Hex(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw new FormatException("HEX 값이 비어있음");
            s = s.Trim();
            if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)) s = s.Substring(2);
            return Convert.ToUInt16(s, 16);
        }

        public static ushort ParseU16Dec(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw new FormatException("DEC 값이 비어있음");
            return Convert.ToUInt16(s.Trim(), 10);
        }

        public static string ToHexNoDash(byte[] buf)
        {
            if (buf == null) return "null";
            return BitConverter.ToString(buf).Replace("-", "");
        }

        // Read 응답 Data 파싱: 레지스터 2바이트씩 Big-Endian이라고 가정
        public static ushort[] ParseRegistersBigEndian(byte[] data)
        {
            if (data == null || data.Length < 2) return Array.Empty<ushort>();
            int n = data.Length / 2;
            ushort[] regs = new ushort[n];
            for (int i = 0; i < n; i++)
            {
                int hi = data[i * 2];
                int lo = data[i * 2 + 1];
                regs[i] = (ushort)((hi << 8) | lo);
            }
            return regs;
        }

        public static ushort[] ParseMultiWriteRegs(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return Array.Empty<ushort>();

            // 구분자들: 공백, 탭, 콤마, 세미콜론, 슬래시, 파이프 등
            char[] seps = new[] { ' ', '\t', ',', ';', '/', '|', '\r', '\n' };
            var tokens = s.Trim().Split(seps, StringSplitOptions.RemoveEmptyEntries);

            // 만약 사용자가 "000100020003"처럼 붙여썼다면 (공백이 아예 없음)
            // 길이가 4의 배수이고 토큰이 1개면 4자리씩 끊어서 처리
            if (tokens.Length == 1 && tokens[0].Length >= 4 && (tokens[0].Length % 4) == 0)
            {
                string one = tokens[0];
                var list = new List<ushort>(one.Length / 4);
                for (int i = 0; i < one.Length; i += 4)
                {
                    string part = one.Substring(i, 4);
                    list.Add(Convert.ToUInt16(part, 16));
                }
                return list.ToArray();
            }

            // 일반 케이스: 토큰 단위로 변환
            var regs = new List<ushort>(tokens.Length);
            foreach (var t in tokens)
            {
                string x = t.Trim();
                if (x.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    x = x.Substring(2);

                regs.Add(Convert.ToUInt16(x, 16)); // "1" -> 0x0001
            }
            return regs.ToArray();
        }

        public static byte[] BuildMultiWritePayload(ushort addr, ushort[] regs)
        {
            ushort qty = (ushort)regs.Length;
            byte byteCount = (byte)(qty * 2);

            byte[] payload = new byte[2 + 2 + 1 + byteCount];

            // Addr
            payload[0] = (byte)(addr >> 8);
            payload[1] = (byte)(addr & 0xFF);

            // Qty
            payload[2] = (byte)(qty >> 8);
            payload[3] = (byte)(qty & 0xFF);

            // ByteCount
            payload[4] = byteCount;

            // Data (Big-Endian)
            int p = 5;
            for (int i = 0; i < regs.Length; i++)
            {
                payload[p++] = (byte)(regs[i] >> 8);
                payload[p++] = (byte)(regs[i] & 0xFF);
            }

            return payload;
        }

        public static ushort ParseU16DecLoose(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw new FormatException("Value(INT) 비어있음");
            s = s.Trim();

            // 10진만 허용 (원하면 0x 들어오면 hex로 처리하도록 확장 가능)
            int v = int.Parse(s, System.Globalization.NumberStyles.Integer);
            if (v < 0 || v > 65535) throw new OverflowException("Value(INT) 범위: 0~65535");
            return (ushort)v;
        }

        public static byte ParseU8HexLoose(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw new FormatException("HEX 비어있음");
            s = s.Trim();

            if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                s = s.Substring(2);

            // "1" -> 01, "A" -> 0A, "00" -> 00, "FF" -> FF
            int v = int.Parse(s, System.Globalization.NumberStyles.HexNumber);
            if (v < 0 || v > 255) throw new OverflowException("HEX 범위: 00~FF");
            return (byte)v;
        }

        public static float ParseFloatLoose(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw new FormatException("Float 값이 비어있음");
            s = s.Trim();

            // 한국 로캘이면 0,1708 같이 콤마 쓰는 경우가 있어서 둘 다 허용
            // 우선 Invariant(점)로 시도 -> 실패하면 CurrentCulture로 시도
            if (float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out float f1))
                return f1;

            if (float.TryParse(s, NumberStyles.Float, CultureInfo.CurrentCulture, out float f2))
                return f2;

            throw new FormatException("Float 파싱 실패");
        }


    }
}
