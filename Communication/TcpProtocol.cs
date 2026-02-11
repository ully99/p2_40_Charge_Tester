using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2_40_Charge_Tester
{
    class TcpProtocol
    {
        private readonly byte HEADER1 = 0x49; // 'I'
        private readonly byte HEADER2 = 0x54; // 'T'
        private readonly byte HEADER3 = 0x4D; // 'M'

        public byte CMD { get; set; }
        public byte ITEM { get; set; }
        public byte[] DATA { get; set; }

        public TcpProtocol(byte cmd, byte item, byte[] data = null)
        {
            CMD = cmd;
            ITEM = item;
            DATA = data ?? Array.Empty<byte>();
        }

        public byte[] GetPacket()
        {
            // LEN = CMD + ITEM + DATA.Length (CRC 제외)
            ushort len = (ushort)(2 + DATA.Length); // WORD (2바이트)
            byte[] lenBytes = { (byte)(len >> 8), (byte)(len & 0xFF) };

            List<byte> packet = new List<byte>
            {
            HEADER1, HEADER2, HEADER3
            };

            packet.AddRange(lenBytes); // LEN (2 bytes)
            packet.Add(CMD);
            packet.Add(ITEM);
            packet.AddRange(DATA);

            byte crc = CalculateCRC(CMD, ITEM, DATA);
            packet.Add(crc);

            return packet.ToArray();
        }

        public string GetPacketAsHexString()
        {
            return BitConverter.ToString(GetPacket()).Replace("-", " ");
        }

        private byte CalculateCRC(byte cmd, byte item, byte[] data)
        {
            int sum = cmd + item + data.Sum(d => d);
            byte crc = (byte)(sum ^ 0x55); // XOR 0x55
            return crc;
        }
    }
}
