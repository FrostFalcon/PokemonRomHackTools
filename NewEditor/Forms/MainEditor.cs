using NewEditor.Data;
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
using System.Diagnostics;
using NewEditor.Data.NARCTypes;

namespace NewEditor.Forms
{
    public partial class MainEditor : Form
    {
        public string loadedRomPath = "";

        //Key data
        int FirstNARCPointerLocation;
        int FirstRelevantNARCPointerLocation => FirstNARCPointerLocation + NARCsToSkip * 8;
        int LastNarc;
        int NARCsToSkip;
        int fileSizeLimit;

        int textNarcID;
        int storyTextNarcID;
        int pokemonDataNarcID;
        int levelUpMovesNarcID;
        int evolutionNarcID;
        int moveDataNarcID;
        int zoneDataNarcID;
        int mapMatrixNarcID;
        int scriptNarcID;

        //Rom Data
        public static string romType = "";
        public static List<NARC> narcFiles;

        byte[] preNARCData;
        byte[] postNARCData;

        //Narcs
        public static TextNARC textNarc;
        public static TextNARC storyTextNarc;
        public static PokemonDataNARC pokemonDataNarc;
        public static LearnsetNARC learnsetNarc;
        public static EvolutionDataNARC evolutionsNarc;
        public static MoveDataNARC moveDataNarc;
        public static ZoneDataNARC zoneDataNarc;
        public static MapMatrixNARC mapMatrixNarc;
        public static ScriptNARC scriptNarc;

        //Forms
        public static TextViewer textViewer;
        public static PokemonEditor pokemonEditor;
        public static MoveEditor moveEditor;
        public static OverworldEditor overworldEditor;
        public static TypeSwapEditor typeSwapEditor;
        public static ScriptEditor scriptEditor;

        public MainEditor()
        {
            InitializeComponent();
        }

        private void GetVersionConstants()
        {
            if (romType == "pokemon b2" || romType == "pokemon w2")
            {
                FirstNARCPointerLocation = VersionConstants.BW2_FirstNarcPointerLocation;
                LastNarc = VersionConstants.BW2_LastNarc;
                NARCsToSkip = VersionConstants.BW2_NARCsToSkip;
                fileSizeLimit = VersionConstants.BW2_FileSizeLimit;

                textNarcID = VersionConstants.BW2_TextNARCID;
                storyTextNarcID = VersionConstants.BW2_StoryTextNARCID;
                pokemonDataNarcID = VersionConstants.BW2_PokemonDataNARCID;
                levelUpMovesNarcID = VersionConstants.BW2_LevelUpMovesNARCID;
                evolutionNarcID = VersionConstants.BW2_EvolutionsNARCID;
                moveDataNarcID = VersionConstants.BW2_MoveDataNARCID;
                zoneDataNarcID = VersionConstants.BW2_ZoneDataNARCID;
                mapMatrixNarcID = VersionConstants.BW2_MapMatriciesNARCID;
                scriptNarcID = VersionConstants.BW2_ScriptNARCID;
                return;
            }

            MessageBox.Show("Invalid Rom type.\nExpected pokemon black 2 or white 2");
            Close();
        }

        private void OpenRom(object sender, EventArgs e)
        {
            OpenFileDialog prompt = new OpenFileDialog();
            prompt.Filter = "Nds Roms|*.nds";

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                if (textViewer != null && !textViewer.IsDisposed) textViewer.Close();
                if (pokemonEditor != null && !pokemonEditor.IsDisposed) pokemonEditor.Close();

                narcFiles = new List<NARC>();

                loadedRomPath = prompt.FileName;
                FileStream fileStream = File.OpenRead(loadedRomPath);

                //Set UI text
                romNameText.Text = "Rom: " + loadedRomPath.Substring(loadedRomPath.LastIndexOf('\\') + 1, loadedRomPath.Length - (loadedRomPath.LastIndexOf('\\') + 1));
                byte[] romTypeBytes = new byte[16];
                fileStream.Read(romTypeBytes, 0, 16);
                romType = Encoding.ASCII.GetString(romTypeBytes).ToLower();
                romType = romType.Remove(romType.IndexOf((char)0));
                romTypeText.Text = "Rom Type: " + romType;

                GetVersionConstants();
                if (IsDisposed) return;

                fileStream.Position = FirstRelevantNARCPointerLocation;

                //Setup NARCs from pointers
                for (int i = 0; i <= LastNarc; i++)
                {
                    int pLoc = (int)fileStream.Position;
                    int loc = fileStream.ReadInt();

                    NARC n;
                    if (i == textNarcID)
                    {
                        n = new TextNARC() { ID = (short)i, pointerLocation = pLoc, location = loc, size = fileStream.ReadInt() - loc };
                        textNarc = (TextNARC)n;
                    }
                    else if (i == storyTextNarcID)
                    {
                        n = new TextNARC() { ID = (short)i, pointerLocation = pLoc, location = loc, size = fileStream.ReadInt() - loc };
                        storyTextNarc = (TextNARC)n;
                    }
                    else if (i == mapMatrixNarcID)
                    {
                        n = new MapMatrixNARC() { ID = (short)i, pointerLocation = pLoc, location = loc, size = fileStream.ReadInt() - loc };
                        mapMatrixNarc = (MapMatrixNARC)n;
                    }
                    else if (i == zoneDataNarcID)
                    {
                        n = new ZoneDataNARC() { ID = (short)i, pointerLocation = pLoc, location = loc, size = fileStream.ReadInt() - loc };
                        zoneDataNarc = (ZoneDataNARC)n;
                    }
                    else if (i == pokemonDataNarcID)
                    {
                        n = new PokemonDataNARC() { ID = (short)i, pointerLocation = pLoc, location = loc, size = fileStream.ReadInt() - loc };
                        pokemonDataNarc = (PokemonDataNARC)n;
                    }
                    else if (i == levelUpMovesNarcID)
                    {
                        n = new LearnsetNARC() { ID = (short)i, pointerLocation = pLoc, location = loc, size = fileStream.ReadInt() - loc };
                        learnsetNarc = (LearnsetNARC)n;
                    }
                    else if (i == evolutionNarcID)
                    {
                        n = new EvolutionDataNARC() { ID = (short)i, pointerLocation = pLoc, location = loc, size = fileStream.ReadInt() - loc };
                        evolutionsNarc = (EvolutionDataNARC)n;
                    }
                    else if (i == moveDataNarcID)
                    {
                        n = new MoveDataNARC() { ID = (short)i, pointerLocation = pLoc, location = loc, size = fileStream.ReadInt() - loc };
                        moveDataNarc = (MoveDataNARC)n;
                    }
                    else if (i == scriptNarcID)
                    {
                        n = new ScriptNARC() { ID = (short)i, pointerLocation = pLoc, location = loc, size = fileStream.ReadInt() - loc };
                        scriptNarc = (ScriptNARC)n;
                    }
                    else
                    {
                        n = new NARC() { ID = (short)i, pointerLocation = pLoc, location = loc, size = fileStream.ReadInt() - loc };
                    }
                    narcFiles.Add(n);
                }


                //Store all data before the first NARC
                fileStream.Position = 0;
                preNARCData = new byte[narcFiles[0].location];
                fileStream.Read(preNARCData, 0, narcFiles[0].location);

                //Read bytes into NARCs
                foreach (NARC narc in narcFiles) narc.ReadData(fileStream);

                //All data after NARCs
                postNARCData = new byte[fileSizeLimit - fileStream.Position];
                fileStream.Read(postNARCData, 0, postNARCData.Length);

                fileStream.Close();

                //Setup Editor
                saveRomButton.Enabled = true;
            }
        }

        private void SaveRom(object sender, EventArgs e)
        {
            SaveFileDialog prompt = new SaveFileDialog();
            prompt.Filter = "Nds Roms|*.nds";

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                //Compile data into target file
                loadedRomPath = prompt.FileName;
                FileStream fileStream = File.OpenWrite(loadedRomPath);
                fileStream.SetLength(0);

                fileStream.Write(preNARCData, 0, preNARCData.Length);

                foreach (NARC narc in narcFiles) narc.WriteData(fileStream);
                fileStream.Write(postNARCData, 0, postNARCData.Length);

                fileStream.Position = 0;
                //fileStream.Write(preNARCData, 0, preNARCData.Length);

                fileStream.Close();
            }
        }

        public static void OpenTextViewer(object sender, EventArgs e)
        {
            if (textNarc == null || storyTextNarc == null)
            {
                MessageBox.Show("Text files have not been loaded");
                return;
            }

            if (textViewer == null || textViewer.IsDisposed) textViewer = new TextViewer();
            textViewer.Show();
            textViewer.BringToFront();
        }

        public static void OpenPokemonEditor(object sender, EventArgs e)
        {
            if (pokemonDataNarc == null)
            {
                MessageBox.Show("Pokemon data files have not been loaded");
                return;
            }

            if (pokemonEditor == null || pokemonEditor.IsDisposed) pokemonEditor = new PokemonEditor();
            pokemonEditor.Show();
            pokemonEditor.BringToFront();
        }

        public static void OpenOverworldEditor(object sender, EventArgs e)
        {
            if (zoneDataNarc == null)
            {
                MessageBox.Show("Zone data files have not been loaded");
                return;
            }

            if (overworldEditor == null || overworldEditor.IsDisposed) overworldEditor = new OverworldEditor();
            overworldEditor.Show();
            overworldEditor.BringToFront();
        }

        private void OpenTypeSwapEditor(object sender, EventArgs e)
        {
            if (pokemonDataNarc == null || moveDataNarc == null)
            {
                MessageBox.Show("Necessary data files have not been loaded");
                return;
            }

            if (typeSwapEditor == null || typeSwapEditor.IsDisposed) typeSwapEditor = new TypeSwapEditor();
            typeSwapEditor.Show();
            typeSwapEditor.BringToFront();
        }

        private void OpenMoveEditor(object sender, EventArgs e)
        {
            if (moveDataNarc == null)
            {
                MessageBox.Show("Move data files have not been loaded");
                return;
            }

            if (moveEditor == null || moveEditor.IsDisposed) moveEditor = new MoveEditor();
            moveEditor.Show();
            moveEditor.BringToFront();
        }

        public static void OpenScriptEditor(object sender, EventArgs e)
        {
            if (scriptNarc == null)
            {
                MessageBox.Show("Script data files have not been loaded");
                return;
            }

            if (scriptEditor == null || scriptEditor.IsDisposed) scriptEditor = new ScriptEditor();
            scriptEditor.Show();
            scriptEditor.BringToFront();
        }
    }
}
