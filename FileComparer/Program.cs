using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileComparer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog prompt = new OpenFileDialog();
            string loadedFilePath = "";
            Stream file1;
            Stream file2 = null;

            byte[] file1Bytes = new byte[0];
            byte[] file2Bytes = new byte[0];


            Console.WriteLine("Enter file 1");
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                loadedFilePath = prompt.FileName;
                file1 = File.OpenRead(loadedFilePath);
                file1Bytes = new byte[file1.Length];
                file1.Read(file1Bytes, 0, (int)file1.Length);
            }

            Console.WriteLine("Enter file 2");
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                loadedFilePath = prompt.FileName;
                file2 = File.OpenRead(loadedFilePath);
                file2Bytes = new byte[file2.Length];
                file2.Read(file2Bytes, 0, (int)file2.Length);
            }

            long numDifferences = 0;
            for (long i = 0; i < Math.Min(file1Bytes.Length, file2Bytes.Length); i++)
            {
                if (file1Bytes[i] != file2Bytes[i] && i < 100000)
                {
                    if (numDifferences == 0) Console.Write($"Difference at bytes: {i}");
                    else Console.Write($", {i}");

                    //file2Bytes[i] = file1Bytes[i];

                    numDifferences++;
                    if (numDifferences > 600) break;
                }
            }
            if (numDifferences == 0) Console.WriteLine("No difference was found");

            //file2.Close();
            //file2 = File.OpenWrite(loadedFilePath);
            //file2.Write(file2Bytes, 0, file2Bytes.Length);

            Console.WriteLine("\nDone\nPress enter to exit");
            Console.ReadLine();
        }
    }
}
