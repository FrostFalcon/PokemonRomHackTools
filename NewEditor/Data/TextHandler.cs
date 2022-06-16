using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace NewEditor.Data
{
    public class PPTxtHandler
    {
        static List<int> keys;
        static List<int> unknowns;

        //Decompression
        public static List<string> GetStrings(byte[] ds)
        {
            List<string> strings = new List<string>();
            keys = new List<int>();
            unknowns = new List<int>();
            {
                int pos = 0;
                int i = 0;

                int numSections, numEntries, tmpCharCount, tmpUnknown, tmpChar;
                int tmpOffset;
                int[] sizeSections = { 0, 0, 0 };
                int[] sectionOffset = { 0, 0, 0 };
                Dictionary<int, List<int>> tableOffsets = new Dictionary<int, List<int>>();
                Dictionary<int, List<int>> characterCount = new Dictionary<int, List<int>>();
                Dictionary<int, List<int>> unknown = new Dictionary<int, List<int>>();
                Dictionary<int, List<List<int>>> encText = new Dictionary<int, List<List<int>>>();
                Dictionary<int, List<List<string>>> decText = new Dictionary<int, List<List<string>>>();
                string str;
                int key;

                numSections = HelperFunctions.ReadShort(ds, 0);
                numEntries = HelperFunctions.ReadShort(ds, 2);
                sizeSections[0] = HelperFunctions.ReadInt(ds, 4);

                pos += 12;
                if (numSections > i)
                {
                    for (int z = 0; z < numSections; z++)
                    {
                        sectionOffset[z] = HelperFunctions.ReadInt(ds, pos);
                        pos += 4;
                    }
                    pos = sectionOffset[i];
                    sizeSections[i] = HelperFunctions.ReadInt(ds, pos);
                    pos += 4;

                    tableOffsets.Add(i, new List<int>());
                    characterCount.Add(i, new List<int>());
                    unknown.Add(i, new List<int>());
                    encText.Add(i, new List<List<int>>());
                    decText.Add(i, new List<List<string>>());

                    for (int j = 0; j < numEntries; j++)
                    {
                        tmpOffset = HelperFunctions.ReadInt(ds, pos);
                        pos += 4;
                        tmpCharCount = HelperFunctions.ReadShort(ds, pos);
                        pos += 2;
                        tmpUnknown = HelperFunctions.ReadShort(ds, pos);
                        pos += 2;
                        tableOffsets[i].Add(tmpOffset);
                        characterCount[i].Add(tmpCharCount);
                        unknown[i].Add(tmpUnknown);
                        unknowns.Add(tmpUnknown);
                    }
                    for (int j = 0; j < numEntries; j++)
                    {
                        List<int> tmpEncChars = new List<int>();
                        pos = sectionOffset[i] + tableOffsets[i][j];
                        for (int k = 0; k < characterCount[i][j]; k++)
                        {
                            tmpChar = HelperFunctions.ReadShort(ds, pos);
                            pos += 2;
                            tmpEncChars.Add(tmpChar);
                        }
                        encText[i].Add(tmpEncChars);
                        key = encText[i][j][characterCount[i][j] - 1] ^ 0xFFFF;
                        for (int k = characterCount[i][j] - 1; k >= 0; k--)
                        {
                            encText[i][j][k] = encText[i][j][k] ^ key;
                            if (k == 0)
                            {
                                keys.Add(key);
                            }
                            key = ((key >> 3) | (key << 13)) & 0xffff;
                        }
                        if (encText[i][j][0] == 0xF100)
                        {
                            encText[i][j] = Decompress(encText[i][j]);
                            characterCount[i][j] = encText[i][j].Count;
                        }

                        List<string> chars = new List<string>();
                        str = "";
                        for (int k = 0; k < characterCount[i][j]; k++)
                        {
                            if (encText[i][j][k] == 0xFFFF)
                            {
                                chars.Add("\\xFFFF");
                            }
                            else
                            {
                                if (encText[i][j][k] > 20 && encText[i][j][k] <= 0xFFF0)
                                {
                                    chars.Add("" + (char)encText[i][j][k]);
                                }
                                else
                                {
                                    string num = string.Format("%04X", encText[i][j][k]);
                                    chars.Add("\\x" + num);
                                }
                                str += chars[k];
                            }
                        }
                        strings.Add(str);
                        decText[i].Add(chars);
                    }
                }
            }
            return strings;
        }

        private static List<int> Decompress(List<int> chars)
        {
            List<int> uncomp = new List<int>();
            int j = 1;
            int shift1 = 0;
            int trans = 0;
            while (true)
            {
                int tmp = chars[j];
                tmp = tmp >> shift1;
                int tmp1 = tmp;
                if (shift1 >= 0x10)
                {
                    shift1 -= 0x10;
                    if (shift1 > 0)
                    {
                        tmp1 = (trans | ((chars[j] << (9 - shift1)) & 0x1FF));
                        if ((tmp1 & 0xFF) == 0xFF)
                        {
                            break;
                        }
                        if (tmp1 != 0x0 && tmp1 != 0x1)
                        {
                            uncomp.Add(tmp1);
                        }
                    }
                }
                else
                {
                    tmp1 = ((chars[j] >> shift1) & 0x1FF);
                    if ((tmp1 & 0xFF) == 0xFF)
                    {
                        break;
                    }
                    if (tmp1 != 0x0 && tmp1 != 0x1)
                    {
                        uncomp.Add(tmp1);
                    }
                    shift1 += 9;
                    if (shift1 < 0x10)
                    {
                        trans = (chars[j] >> shift1) & 0x1FF;
                        shift1 += 9;
                    }
                    j += 1;
                }
            }
            return uncomp;
        }

        //Compression
        public static byte[] SaveEntry(byte[] originalData, List<string> text)
        {

            // Parse strings against the reverse table
            //for (int sn = 0; sn < text.size(); sn++)
            //{
            //    text.set(sn, bulkReplace(text.get(sn), textToPokePattern, textToPoke));
            //}

            // Make sure we have the original unknowns etc
            GetStrings(originalData);

            // Start getting stuff
            int numSections, numEntries;
            int[] sizeSections = new int[] { 0, 0, 0 };
            int[] sectionOffset = new int[] { 0, 0, 0 };
            int[] newsizeSections = new int[] { 0, 0, 0 };
            int[] newsectionOffset = new int[] { 0, 0, 0 };

            // Data-Stream
            byte[] ds = originalData;
            int pos = 0;

            numSections = HelperFunctions.ReadShort(ds, 0);
            numEntries = HelperFunctions.ReadShort(ds, 2);
            sizeSections[0] = HelperFunctions.ReadInt(ds, 4);
            pos += 12;
            if (text.Count < numEntries)
            {
                System.Diagnostics.Debug.WriteLine("Can't do anything due to too few lines");
                return originalData;
            }
            else
            {
                byte[] newEntry = MakeSection(text, numEntries);
                for (int z = 0; z < numSections; z++)
                {
                    sectionOffset[z] = HelperFunctions.ReadInt(ds, pos);
                    pos += 4;
                }
                for (int z = 0; z < numSections; z++)
                {
                    pos = sectionOffset[z];
                    sizeSections[z] = HelperFunctions.ReadInt(ds, pos);
                    pos += 4;
                }
                newsizeSections[0] = newEntry.Length;

                byte[] newData = new byte[ds.Length - sizeSections[0] + newsizeSections[0]];
                Array.Copy(ds, 0, newData, 0, Math.Min(ds.Length, newData.Length));
                HelperFunctions.WriteInt(newData, 4, newsizeSections[0]);
                if (numSections == 2)
                {
                    newsectionOffset[1] = newsizeSections[0] + sectionOffset[0];
                    HelperFunctions.WriteInt(newData, 0x10, newsectionOffset[1]);
                }
                Array.Copy(newEntry, 0, newData, sectionOffset[0], newEntry.Length);
                if (numSections == 2)
                {
                    Array.Copy(ds, sectionOffset[1], newData, newsectionOffset[1], sizeSections[1]);
                }
                return newData;
            }
        }

        private static byte[] MakeSection(List<string> strings, int numEntries)
        {
            List<List<int>> data = new List<List<int>>();
            int size = 0;
            int offset = 4 + 8 * numEntries;
            int charCount;
            for (int i = 0; i < numEntries; i++)
            {
                data.Add(ParseString(strings[i], i));
                size += data[i].Count * 2;
            }
            if (size % 4 == 2)
            {
                size += 2;
                int tmpKey = keys[numEntries - 1];
                for (int i = 0; i < data[numEntries - 1].Count; i++)
                {
                    tmpKey = ((tmpKey << 3) | (tmpKey >> 13)) & 0xFFFF;
                }
                data[numEntries - 1].Add(0xFFFF ^ tmpKey);
            }
            size += offset;
            byte[] section = new byte[size];
            int pos = 0;
            HelperFunctions.WriteInt(section, pos, size);
            pos += 4;
            for (int i = 0; i < numEntries; i++)
            {
                charCount = data[i].Count;
                HelperFunctions.WriteInt(section, pos, offset);
                pos += 4;
                HelperFunctions.WriteShort(section, pos, charCount);
                pos += 2;
                HelperFunctions.WriteShort(section, pos, unknowns[i]);
                pos += 2;
                offset += (charCount * 2);
            }
            for (int i = 0; i < numEntries; i++)
            {
                foreach (int word in data[i])
                {
                    HelperFunctions.WriteShort(section, pos, word);
                    pos += 2;
                }
            }
            return section;
        }

        private static List<int> ParseString(string str, int entry_id)
        {
            List<int> chars = new List<int>();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != '\\')
                {
                    chars.Add(str[i]);
                }
                else
                {
                    if (((i + 2) < str.Length) && str[i + 2] == '{')
                    {
                        chars.Add(str[i]);
                    }
                    else
                    {
                        string tmp = "";
                        for (int j = 0; j < 4; j++)
                        {
                            tmp += str[i + j + 2];
                        }
                        i += 5;
                        foreach (char c in tmp) chars.Add(c);
                    }
                }
            }
            chars.Add(0xFFFF);
            int key = keys[entry_id];
            for (int i = 0; i < chars.Count; i++)
            {
                chars[i] = (chars[i] ^ key) & 0xFFFF;
                key = ((key << 3) | (key >> 13)) & 0xFFFF;
            }
            return chars;
        }
    }
}
