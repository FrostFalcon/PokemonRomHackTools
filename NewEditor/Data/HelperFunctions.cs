using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEditor.Data
{
    public static class HelperFunctions
    {
        public static int ReadInt(this FileStream fs)
        {
            byte[] toInt = new byte[4];
            fs.Read(toInt, 0, 4);
            return BitConverter.ToInt32(toInt, 0);
        }
        public static int ReadShort(byte[] data, int offset) => ((data[offset] & 0xFF) + ((data[offset + 1] & 0xFF) << 8));
        public static int ReadInt(byte[] data, int offset) => (data[offset] & 0xFF) + ((data[offset + 1] & 0xFF) << 8) + ((data[offset + 2] & 0xFF) << 16)
                + ((data[offset + 3] & 0xFF) << 24);

        public static void WriteShort(byte[] data, int offset, int value)
        {
            data[offset] = (byte)(value & 0xFF);
            data[offset + 1] = (byte)((value >> 8) & 0xFF);
        }

        public static void WriteInt(byte[] data, int offset, int value)
        {
            data[offset] = (byte)(value & 0xFF);
            data[offset + 1] = (byte)((value >> 8) & 0xFF);
            data[offset + 2] = (byte)((value >> 16) & 0xFF);
            data[offset + 3] = (byte)((value >> 24) & 0xFF);
        }
    }
}
