using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using NewEditor.Forms;

namespace NewEditor.Data.NARCTypes
{
    public class TextNARC : NARC
    {
        public List<TextFile> textFiles;

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

            //Register text files
            textFiles = new List<TextFile>();

            pos = pointerStartAddress;
            for (int i = 0; i < numFileEntries; i++)
            {
                int start = HelperFunctions.ReadInt(byteData, pos);
                int end = HelperFunctions.ReadInt(byteData, pos + 4);
                byte[] bytes = new byte[end - start];

                for (int j = 0; j < end - start; j++) bytes[j] = byteData[initialPosition + start + j];

                textFiles.Add(new TextFile(bytes));
                pos += 8;
            }
        }

        //public override void WriteData(FileStream fs)
        //{
        //    List<byte> newByteData = new List<byte>();
        //    List<byte> oldByteData = new List<byte>(byteData);

        //    newByteData.AddRange(oldByteData.GetRange(0, pointerStartAddress));

        //    //Find start of file instances
        //    int pos = 0;
        //    while (pos < byteData.Length)
        //    {
        //        pos++;
        //        if (pos >= byteData.Length) return;
        //        if (byteData[pos] == 'B' && byteData[pos + 1] == 'T' && byteData[pos + 2] == 'N' && byteData[pos + 3] == 'F') break;
        //    }

        //    newByteData.AddRange(oldByteData.GetRange(pos, 24));

        //    //Write Files
        //    int totalSize = 0;
        //    int pPos = pointerStartAddress;
        //    foreach (TextFile t in textFiles)
        //    {
        //        t.CompressData();
        //        newByteData.AddRange(t.bytes);
        //        newByteData.InsertRange(pPos, BitConverter.GetBytes(totalSize));
        //        pPos += 4;
        //        totalSize += t.bytes.Length;
        //        newByteData.InsertRange(pPos, BitConverter.GetBytes(totalSize));
        //        pPos += 4;
        //    }

        //    byteData = newByteData.ToArray();

        //    base.WriteData(fs);
        //}

        public void ApplyTextList(RichTextBox textbox, int fileIndex)
        {
            TextFile t = textFiles[fileIndex];

            t.text = new List<string>();
            //for (int i = 0; i < textbox.Text.Length; i++)
            //{
            //    string str = "";
            //    while (textbox.Text[i] != '\n' && i < textbox.Text.Length)
            //    {
            //        str += textbox.Text[i];
            //        i++;
            //    }
            //    i++;
            //    t.text.Add(str);
            //}
            t.CompressData();
        }

        public string GetLine(int file, int line) => textFiles[file].text[line];
    }

    public class TextFile
    {
        public byte[] bytes;
        public List<string> text;

        public TextFile(byte[] bytes)
        {
            this.bytes = bytes;

            this.text = PPTxtHandler.GetStrings(bytes);
        }

        public void CompressData()
        {
            PPTxtHandler.SaveEntry(bytes, text);
        }
    }
}
