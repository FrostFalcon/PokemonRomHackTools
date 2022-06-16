using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewEditor.Forms;

namespace NewEditor.Data.NARCTypes
{
    public class MoveDataNARC : NARC
    {
        public List<MoveDataEntry> moves;

        public override void ReadData(FileStream fs)
        {
            base.ReadData(fs);

            //Find first file instance
            int pos = numFileEntries * 8;
            while (pos < byteData.Length)
            {
                pos++;
                if (pos >= byteData.Length) return;
                if (byteData[pos] == 'B' && byteData[pos + 1] == 'T' && byteData[pos + 2] == 'N' && byteData[pos + 3] == 'F') break;
            }
            int initialPosition = pos + 24;

            //Register data files
            moves = new List<MoveDataEntry>();

            pos = pointerStartAddress;

            //Populate data types
            for (int i = 0; i < numFileEntries; i++)
            {
                int start = HelperFunctions.ReadInt(byteData, pos);
                int end = HelperFunctions.ReadInt(byteData, pos + 4);
                byte[] bytes = new byte[end - start];

                for (int j = 0; j < end - start; j++) bytes[j] = byteData[initialPosition + start + j];

                MoveDataEntry m = new MoveDataEntry(bytes) { nameID = i };
                moves.Add(m);

                pos += 8;
            }
        }

        public override void WriteData(FileStream fs)
        {
            List<byte> newByteData = new List<byte>();
            List<byte> oldByteData = new List<byte>(byteData);

            newByteData.AddRange(oldByteData.GetRange(0, pointerStartAddress));

            //Find start of file instances
            int pos = 0;
            while (pos < byteData.Length)
            {
                pos++;
                if (pos >= byteData.Length) return;
                if (byteData[pos] == 'B' && byteData[pos + 1] == 'T' && byteData[pos + 2] == 'N' && byteData[pos + 3] == 'F') break;
            }

            newByteData.AddRange(oldByteData.GetRange(pos, 24));

            //Write Files
            int totalSize = 0;
            int pPos = pointerStartAddress;
            foreach (MoveDataEntry m in moves)
            {
                newByteData.AddRange(m.bytes);
                newByteData.InsertRange(pPos, BitConverter.GetBytes(totalSize));
                pPos += 4;
                totalSize += m.bytes.Length;
                newByteData.InsertRange(pPos, BitConverter.GetBytes(totalSize));
                pPos += 4;
            }

            byteData = newByteData.ToArray();

            HelperFunctions.WriteInt(byteData, 8, byteData.Length);
            HelperFunctions.WriteInt(byteData, 24, moves.Count);

            base.WriteData(fs);
        }
    }

    public class MoveDataEntry
    {
        public byte[] bytes;

        public int nameID;
        public byte element;

        public MoveDataEntry(byte[] bytes)
        {
            this.bytes = bytes;

            ReadData();
        }

        internal void ReadData()
        {
            element = bytes[0];
        }

        internal void ApplyData()
        {
            bytes[0] = element;
        }

        public override string ToString()
        {
            return nameID < MainEditor.textNarc.textFiles[VersionConstants.BW2_MoveNameTextFileID].text.Count ? MainEditor.textNarc.textFiles[VersionConstants.BW2_MoveNameTextFileID].text[nameID] + " - " + nameID : "Name not found";
        }
    }
}
