using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Editor;
using Editor.Data;

namespace MapRandomizer
{
    public partial class MapRandomizer : Form
    {
        public string loadedRomPath = "";
        public List<byte> romData;

        List<OverworldData> overworldData = new List<OverworldData>();

        public MapRandomizer()
        {
            InitializeComponent();
        }

        private void openRomButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog prompt = new OpenFileDialog();
            prompt.Filter = "Nds Roms|*.nds";

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                //Read all file data to the byte array
                loadedRomPath = prompt.FileName;
                FileStream fileStream = File.OpenRead(loadedRomPath);
                byte[] data = new byte[fileStream.Length];
                fileStream.Read(data, 0, (int)fileStream.Length);
                romData = new List<byte>(data);
                fileStream.Close();

                RegisterData();

                //Set UI text
                romNameText.Text = "Rom: " + loadedRomPath.Substring(loadedRomPath.LastIndexOf('\\') + 1, loadedRomPath.Length - (loadedRomPath.LastIndexOf('\\') + 1));
            }
        }

        private void SaveRom()
        {
            SaveFileDialog prompt = new SaveFileDialog();
            prompt.Filter = "Nds Roms|*.nds";

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                //Fix file size
                while (romData.Count < 0x12000000)
                {
                    romData.Add(0xFF);
                }
                int fileEnd = 0x12000000;
                if (fileEnd < romData.Count) while (romData[fileEnd] != 0xFF) fileEnd++;
                //Write current rom data to a file
                loadedRomPath = prompt.FileName;
                try
                {
                    FileStream fileStream = File.OpenWrite(loadedRomPath);
                    fileStream.Write(romData.ToArray(), 0, fileEnd);
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void randomizeRomButton_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            //Assign pairs of warps
            RandomizeWarpListPair(topRouteGates, bottomRouteGates, random);
            RandomizeWarpListPair(rightRouteGates, leftRouteGates, random);
            
            ApplyOverworldData();

            SaveRom();
        }

        void RandomizeWarpListPair(List<(int, int)> list1, List<(int, int)> list2, Random random)
        {
            List<(int, int, int)[]> pairs = new List<(int, int, int)[]>();
            List<(int, int)> unusedInList2 = new List<(int, int)>(list2);
            foreach ((int, int) l1 in list1)
            {
                if (list2.Count <= 0) break;
                (int, int, int)[] arr = new (int, int, int)[2];

                OverworldWarp w = (OverworldWarp)overworldData[list2[list1.IndexOf(l1)].Item1].warps[list2[list1.IndexOf(l1)].Item2];
                arr[0] = (l1.Item1, l1.Item2, w.targetMap);

                int i = random.Next(unusedInList2.Count);
                w = (OverworldWarp)overworldData[list1[list2.IndexOf(unusedInList2[i])].Item1].warps[list1[list2.IndexOf(unusedInList2[i])].Item2];
                arr[1] = (unusedInList2[i].Item1, unusedInList2[i].Item2, w.targetMap);
                unusedInList2.RemoveAt(i);

                pairs.Add(arr);
            }

            //Set warp destinations
            foreach ((int, int, int)[] pair in pairs)
            {
                ((OverworldWarp)overworldData[pair[0].Item1].warps[pair[0].Item2]).targetMap = pair[1].Item3;
                ((OverworldWarp)overworldData[pair[0].Item1].warps[pair[0].Item2]).targetWarp = pair[1].Item2;

                ((OverworldWarp)overworldData[pair[1].Item1].warps[pair[1].Item2]).targetMap = pair[0].Item3;
                ((OverworldWarp)overworldData[pair[1].Item1].warps[pair[1].Item2]).targetWarp = pair[0].Item2;
            }
        }

        void RegisterData()
        {
            HexOffsets.RegisterNARCLocations(romData);

            RegisterOverworlds();

            randomizeRomButton.Enabled = true;
        }

        void RegisterOverworlds()
        {
            //Register Overworld Data
            int i = 0;
            int pointerLoc = HexOffsets.overworldsLocation + 36;
            while (i < HexOffsets.overworldsTotalBytes - HexOffsets.overworldsFirstEntry)
            {
                int byteLoc = HexOffsets.overworldsLocation + HexOffsets.overworldsFirstEntry + i;

                OverworldData o = new OverworldData();
                o.byteLocation = byteLoc;
                //Setup lists
                o.furniture = new List<OverworldFurniture>();
                o.NPCs = new List<OverworldNPC>();
                o.warps = new List<Editor.OverworldWarp>();
                o.triggers = new List<OverworldTrigger>();
                if (romData[byteLoc] != 0 || romData[byteLoc + 1] != 0)
                {
                    for (int j = 0; j < romData[byteLoc + 4]; j++)
                    {
                        o.furniture.Add(new OverworldFurniture());
                    }
                    for (int j = 0; j < romData[byteLoc + 5]; j++)
                    {
                        o.NPCs.Add(new OverworldNPC());
                    }
                    for (int j = 0; j < romData[byteLoc + 6]; j++)
                    {
                        o.warps.Add(new OverworldWarp());
                    }
                    for (int j = 0; j < romData[byteLoc + 7]; j++)
                    {
                        o.triggers.Add(new OverworldTrigger());
                    }

                    byteLoc += 8;
                    //Read furniture
                    foreach (OverworldFurniture f in o.furniture)
                    {
                        byteLoc += 20;
                    }
                    //Read NPCs
                    foreach (OverworldNPC n in o.NPCs)
                    {
                        byteLoc += 36;
                    }
                    //Read Warps
                    foreach (OverworldWarp w in o.warps)
                    {
                        w.targetMap = romData[byteLoc] + romData[byteLoc + 1] * 256;
                        w.targetWarp = romData[byteLoc + 2] + romData[byteLoc + 3] * 256;
                        byteLoc += 20;
                    }
                }
                i = BitConverter.ToInt32(romData.GetRange(pointerLoc, 4).ToArray(), 0);
                pointerLoc += 8;
                overworldData.Add(o);
            }

            foreach (OverworldData o in overworldData) comboBox1.Items.Add(overworldData.IndexOf(o));
        }

        void ApplyOverworldData()
        {
            foreach (OverworldData o in overworldData)
            {
                int byteLoc = o.byteLocation;

                if (romData[byteLoc] != 0 || romData[byteLoc + 1] != 0)
                {
                    byteLoc += 8;
                    //Read furniture
                    foreach (OverworldFurniture f in o.furniture)
                    {
                        byteLoc += 20;
                    }
                    //Read NPCs
                    foreach (OverworldNPC n in o.NPCs)
                    {
                        byteLoc += 36;
                    }
                    //Read Warps
                    foreach (OverworldWarp w in o.warps)
                    {
                        byte[] bytes = BitConverter.GetBytes(w.targetMap);
                        romData[byteLoc] = bytes[0];
                        romData[byteLoc + 1] = bytes[1];
                        bytes = BitConverter.GetBytes(w.targetWarp);
                        romData[byteLoc + 2] = bytes[0];
                        romData[byteLoc + 3] = bytes[1];
                        byteLoc += 20;
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            OverworldData ow = overworldData[comboBox1.SelectedIndex];
            foreach (OverworldWarp warp in ow.warps)
            {
                listBox1.Items.Add("ToMap: " + warp.targetMap + " ToWarp: " + warp.targetWarp);
            }
        }

        List<(int, int)> topRouteGates = new List<(int, int)>
        {
            (559, 1),   //Top of Asperita route gate
            (54, 1),    //Top of Castelia route gate
            (208, 1),   //Top left of route 4 route gate
            (197, 2),   //Top right of route 4, join avenue
            (541, 0),   //Top of Undella route gate
            (591, 1),   //Top of Undella To Marine Tube
        };
        List<(int, int)> bottomRouteGates = new List<(int, int)>
        {
            (558, 0),   //Bottom of route 19
            (482, 5),   //Bottom of route 4
            (207, 0),   //Bottom of Desert Resort
            (66, 0),    //Bottom of Nimbasa
            (539, 1),   //Bottom of route 13
            (411, 0),   //Bottom of Marine Tube
        };

        List<(int, int)> rightRouteGates = new List<(int, int)>
        {
            (561, 1),   //Right of route 20 route gate
        };
        List<(int, int)> leftRouteGates = new List<(int, int)>
        {
            (174, 0),   //Left of Virbank
        };
    }

    class OverworldWarp : Editor.OverworldWarp
    {
        public int targetMap;
        public int targetWarp;
    }
}
