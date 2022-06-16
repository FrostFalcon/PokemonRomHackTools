using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEditor.Data
{
    public class NARC
    {
        protected int pointerStartAddress = 28;
        public byte[] byteData;
        public short ID;
        public int pointerLocation;
        public int location;
        public int size;

        protected int numFileEntries => (byteData != null && byteData.Length > 32) ? HelperFunctions.ReadInt(byteData, 24) : 0;

        public virtual void ReadData(FileStream fs)
        {
            byteData = new byte[size];
            fs.Position = location;
            fs.Read(byteData, 0, size);
        }

        public virtual void WriteData(FileStream fs)
        {
            location = (int)fs.Length;
            size = byteData.Length;

            fs.Position = pointerLocation;
            fs.Write(BitConverter.GetBytes(location), 0, 4);
            fs.Write(BitConverter.GetBytes(location + size), 0, 4);

            fs.Position = fs.Length;
            fs.Write(byteData, 0, byteData.Length);

            while (fs.Length % 256 != 0) fs.WriteByte(0xFF);
        }
    }
}
