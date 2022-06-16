using Editor.Data;
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

namespace Editor
{
    public partial class MainEditor : Form
    {
        public string loadedRomPath = "";

        public List<byte> romData;
        public List<TrainerData> trainerData = new List<TrainerData>();
        public List<PokemonData> pokemonData = new List<PokemonData>();
        public List<int> weirdTrPokeHeaderIndexes = new List<int>();
        public List<EncounterData> routeEncounters = new List<EncounterData>();
        List<OverworldData> overworldData = new List<OverworldData>();

        public List<EncounterSlot> encounterGroupClipboard = new List<EncounterSlot>();
        public List<(int, int)> learnsetClipboard = new List<(int, int)>();

        public List<TabPage> trPokeTabs = new List<TabPage>();

        public MainEditor()
        {
            InitializeComponent();

            SetupTrPokeTabs();
            SetupPokemonEditor();
            SetupEncounterTable();
        }

        #region General
        public void LoadRom(object sender, EventArgs e)
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

                //Set UI text
                romNameText.Text = "Rom: " + loadedRomPath.Substring(loadedRomPath.LastIndexOf('\\') + 1, loadedRomPath.Length - (loadedRomPath.LastIndexOf('\\') + 1));
                romTypeText.Text = "Rom Type: " + Encoding.ASCII.GetString(romData.GetRange(0, 16).ToArray()).ToLower();

                HexOffsets.RegisterNARCLocations(romData);
                ReadAllData();

                //Register trPokeData indicies with weird header values
                for (int i = 1; i < trainerData.Count; i++)
                {
                    int num1 = BitConverter.ToInt32(romData.GetRange(HexOffsets.trPokeLocation + 24 + (i * 8), 4).ToArray(), 0);
                    int num2 = BitConverter.ToInt32(romData.GetRange(HexOffsets.trPokeLocation + 28 + (i * 8), 4).ToArray(), 0);

                    if (num1 != num2)
                    {
                        weirdTrPokeHeaderIndexes.Add(i);
                    }
                }
            }

            trDataDropdown.Enabled = true;
            encounterRouteNameDropdown.Enabled = true;
            pokemonEditorNameDropdown.Enabled = true;
            evolutionPokemonNameDropdown.Enabled = true;
            overworldEditorIndexDropdown.Enabled = true;
            saveRomButton.Enabled = true;
        }

        private void ReadAllData()
        {
            ReadTrainerData();
            ReadEncounterData();
            ReadPokemonData();
            ReadOverworldData();
        }

        private void SaveRom(object sender, EventArgs e)
        {
            SaveFileDialog prompt = new SaveFileDialog();
            prompt.Filter = "Nds Roms|*.nds";

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                //Fix NARC headers
                HexOffsets.RegisterNARCLocations(romData);
                FixHexLocationHeaders(HexOffsets.levelUpMovesHeaderAddress, HexOffsets.levelUpMovesLocation + HexOffsets.levelUpMovesTotalBytes);
                FixHexLocationHeaders(HexOffsets.trPokeHeaderAddress, HexOffsets.trPokeLocation + HexOffsets.trPokeTotalBytes);
                FixPokeLearnsetNARCHeader();
                FixTrPokeNARCHeader();
                //Fix file size
                while (romData.Count < 0x12000000)
                {
                    romData.Add(0xFF);
                }
                int fileEnd = 0x12000000;
                if (fileEnd < romData.Count) while (fileEnd < romData.Count && romData[fileEnd] != 0xFF) fileEnd++;
                //Write current rom data to a file
                loadedRomPath = prompt.FileName;
                FileStream fileStream = File.OpenWrite(loadedRomPath);
                fileStream.Write(romData.ToArray(), 0, fileEnd);
                fileStream.Close();

                if (changeLogCheckBox.Checked) WriteChangeLog();
            }
        }

        void SetupTrPokeTabs()
        {
            trPokeTabs.Clear();
            for (int i = 0; i < 6; i++)
            {
                TabPage newtab = new TabPage("Pokemon " + (i + 1));

                //Pokemon name
                newtab.Controls.Add(new Label() { Location = new Point(20, 30), Text = "Pokemon:", AutoSize = true });
                newtab.Controls.Add(new ComboBox() { Location = new Point(100, 26), AutoCompleteSource = AutoCompleteSource.ListItems, AutoCompleteMode = AutoCompleteMode.Suggest });
                ((ComboBox)newtab.Controls[1]).Items.AddRange(Pokemon.nameList.ToArray());

                //Level
                newtab.Controls.Add(new Label() { Location = new Point(40, 100), Text = "Level:", AutoSize = true });
                newtab.Controls.Add(new NumericUpDown() { Location = new Point(100, 96), Maximum = 100, Minimum = 1, Size = new Size(45, 22) });

                //Moves
                for (int j = 0; j < 4; j++)
                {
                    newtab.Controls.Add(new Label() { Location = new Point(260, 60 + (40 * j)), Text = "Move " + (j + 1) + ":", AutoSize = true });
                    newtab.Controls.Add(new ComboBox() { Location = new Point(320, 56 + (40 * j)), AutoCompleteSource = AutoCompleteSource.ListItems, AutoCompleteMode = AutoCompleteMode.Suggest });
                    ((ComboBox)newtab.Controls[5 + (j * 2)]).Items.AddRange(Pokemon.moveList.ToArray());
                }

                //Item
                newtab.Controls.Add(new Label() { Location = new Point(40, 180), Text = "Item:", AutoSize = true });
                newtab.Controls.Add(new ComboBox() { Location = new Point(100, 176), AutoCompleteSource = AutoCompleteSource.ListItems, AutoCompleteMode = AutoCompleteMode.Suggest });
                ((ComboBox)newtab.Controls[13]).Items.AddRange(Items.nameList.ToArray());

                //Ability
                newtab.Controls.Add(new Label() { Location = new Point(40, 140), Text = "Ability:", AutoSize = true });
                newtab.Controls.Add(new ComboBox() { Location = new Point(100, 136), AutoCompleteSource = AutoCompleteSource.ListItems, AutoCompleteMode = AutoCompleteMode.Suggest });
                ((ComboBox)newtab.Controls[15]).Items.AddRange(Items.nameList.ToArray());

                //Gender
                newtab.Controls.Add(new Label() { Location = new Point(40, 60), Text = "Gender:", AutoSize = true });
                newtab.Controls.Add(new ComboBox() { Location = new Point(100, 56), AutoCompleteSource = AutoCompleteSource.ListItems, AutoCompleteMode = AutoCompleteMode.Suggest });
                ((ComboBox)newtab.Controls[17]).Width = 80;
                ((ComboBox)newtab.Controls[17]).Items.Add("Any");
                ((ComboBox)newtab.Controls[17]).Items.Add("Male");
                ((ComboBox)newtab.Controls[17]).Items.Add("Female");
                ((ComboBox)newtab.Controls[17]).Items.Add("???");

                //Form
                newtab.Controls.Add(new Label() { Location = new Point(230, 30), Text = "Form:", AutoSize = true });
                newtab.Controls.Add(new NumericUpDown() { Location = new Point(280, 26), Maximum = 30, Minimum = 0, Size = new Size(40, 22) });

                trPokeTabs.Add(newtab);
                trainerPokemonTabs.TabPages.Add(newtab);
                ((Control)newtab).Enabled = false;
            }
        }

        void SetupEncounterTable()
        {
            encounterPokemonNameDropDown.Items.Clear();
            encounterPokemonNameDropDown.Items.AddRange(Pokemon.nameListWith0Index.ToArray());
        }

        void SetupPokemonEditor()
        {
            pokemonEditorNameDropdown.Items.Clear();
            pokemonEditorNameDropdown.Items.AddRange(Pokemon.nameListWithFormsAtEnd.ToArray());
            pokemonTypeDropdown1.Items.Clear();
            pokemonTypeDropdown1.Items.AddRange(Pokemon.typeList.ToArray());
            pokemonTypeDropdown2.Items.Clear();
            pokemonTypeDropdown2.Items.AddRange(Pokemon.typeList.ToArray());
            pokeAbilityDropdown1.Items.Clear();
            pokeAbilityDropdown1.Items.AddRange(Pokemon.abilityList.ToArray());
            pokeAbilityDropdown2.Items.Clear();
            pokeAbilityDropdown2.Items.AddRange(Pokemon.abilityList.ToArray());
            pokeAbilityDropdown3.Items.Clear();
            pokeAbilityDropdown3.Items.AddRange(Pokemon.abilityList.ToArray());

            learnsetMoveDropdown.Items.Clear();
            learnsetMoveDropdown.Items.AddRange(Pokemon.moveList.ToArray());
        }
        #endregion

        #region Pokemon
        private void ReadPokemonData()
        {
            //Register Pokemon Data
            pokemonData.Clear();
            for (int s = 0; s < Pokemon.nameListWithFormsToBeReordered.Count; s++) Pokemon.nameListWithFormsToBeReordered[s] = Pokemon.nameListWithFormsAtEnd[s];
            int lsByteLoc = HexOffsets.levelUpMovesLocation + HexOffsets.levelUpMovesFirstEntry;
            int evoByteLoc = HexOffsets.evolutionsLocation + HexOffsets.evolutionsFirstEntry;
            for (int i = 0; i < Pokemon.nameListWithFormsAtEnd.Count; i++)
            {
                int byteLoc = HexOffsets.pokeDataLocation + HexOffsets.pokeDataFirstEntry + i * HexOffsets.pokeDataEntrySize;
                if (i > Pokemon.nameListWithFormsAtEnd.IndexOf("Genesect")) byteLoc += 0xA64;

                PokemonData p = new PokemonData();
                p.byteLocation = byteLoc;
                p.pokeNum = i + 1;
                p.baseHp = romData[byteLoc];
                p.baseAtt = romData[byteLoc + 1];
                p.baseDef = romData[byteLoc + 2];
                p.baseSpe = romData[byteLoc + 3];
                p.baseSpA = romData[byteLoc + 4];
                p.baseSpD = romData[byteLoc + 5];
                p.type1 = romData[byteLoc + 6];
                p.type2 = romData[byteLoc + 7];
                p.levelRate = romData[byteLoc + 21];
                p.ability1 = romData[byteLoc + 24];
                p.ability2 = romData[byteLoc + 25];
                p.ability3 = romData[byteLoc + 26];
                pokemonData.Add(p);

                //Register learnset data location
                p.learnset = new List<(int, int)>();
                p.learnsetByteLocation = lsByteLoc;
                while (!(romData[lsByteLoc] == 0xFF && romData[lsByteLoc + 1] == 0xFF && romData[lsByteLoc + 2] == 0xFF && romData[lsByteLoc + 3] == 0xFF))
                {
                    p.learnset.Add((BitConverter.ToInt16(romData.GetRange(lsByteLoc, 2).ToArray(), 0), romData[lsByteLoc + 2]));
                    lsByteLoc += 4;
                }
                lsByteLoc += 4;

                //Register evolutions
                p.evolutions = new List<(int, int, int)>();
                p.evolutionByteLocation = evoByteLoc;
                for (int e = 0; e < 7; e++)
                {
                    p.evolutions.Add((BitConverter.ToInt16(romData.GetRange(evoByteLoc, 2).ToArray(), 0), BitConverter.ToInt16(romData.GetRange(evoByteLoc + 2, 2).ToArray(), 0), BitConverter.ToInt16(romData.GetRange(evoByteLoc + 4, 2).ToArray(), 0)));
                    evoByteLoc += 6;
                }
                evoByteLoc += 2;

                //Skip pokestar studio pokemon
                if (i == Pokemon.nameListWithFormsAtEnd.IndexOf("Genesect")) lsByteLoc += 316;

                //Relocate forms with their base form
                if (i > Pokemon.nameListWithFormsAtEnd.IndexOf("Genesect"))
                {
                    string name = Pokemon.nameListWithFormsAtEnd[i];
                    string name2 = name;
                    name2 = name2.Remove(name.IndexOf(' '));

                    int index = Pokemon.nameListWithFormsToBeReordered.IndexOf(name2) + 1;
                    while (Pokemon.nameListWithFormsToBeReordered[index].Contains(name2)) index++;
                    Pokemon.nameListWithFormsToBeReordered.RemoveAt(i);
                    Pokemon.nameListWithFormsToBeReordered.Insert(index, name);
                    pokemonData.RemoveAt(i);
                    pokemonData.Insert(index, p);
                }
            }
            int num = 0;
            pokemonEditorNameDropdown.Items.Clear();
            pokemonEditorNameDropdown.Items.AddRange(Pokemon.nameListWithFormsToBeReordered.ToArray());
            pokemonEditorNameDropdown.SelectedIndex = num;

            evolutionPokemonNameDropdown.Items.Clear();
            foreach (string str in pokemonEditorNameDropdown.Items) evolutionPokemonNameDropdown.Items.Add(str);
            evolutionMethodDropdown.Items.Clear();
            evolutionIntoDropdown.Items.Clear();
            evolutionMethodDropdown.Items.AddRange(Pokemon.evolutionMethodList.ToArray());
            evolutionIntoDropdown.Items.AddRange(Pokemon.nameListWith0Index.ToArray());
        }

        private void WritePokemonData()
        {
            PokemonData pk = pokemonData[pokemonEditorNameDropdown.SelectedIndex];
            int byteLoc = pk.byteLocation;


            romData[byteLoc] = (byte)pk.baseHp;
            romData[byteLoc + 1] = (byte)pk.baseAtt;
            romData[byteLoc + 2] = (byte)pk.baseDef;
            romData[byteLoc + 3] = (byte)pk.baseSpe;
            romData[byteLoc + 4] = (byte)pk.baseSpA;
            romData[byteLoc + 5] = (byte)pk.baseSpD;
            romData[byteLoc + 6] = (byte)pk.type1;
            romData[byteLoc + 7] = (byte)pk.type2;
            romData[byteLoc + 21] = (byte)pk.levelRate;
            romData[byteLoc + 24] = (byte)pk.ability1;
            romData[byteLoc + 25] = (byte)pk.ability2;
            romData[byteLoc + 26] = (byte)pk.ability3;

            byteLoc = pk.learnsetByteLocation;

            int oldLearnsetSize = 0;
            while (!(romData[byteLoc + oldLearnsetSize] == 0xFF && romData[byteLoc + oldLearnsetSize + 1] == 0xFF)) oldLearnsetSize += 4;
            if (oldLearnsetSize > pk.learnset.Count * 4) romData.RemoveRange(byteLoc + pk.learnset.Count * 4, oldLearnsetSize - (pk.learnset.Count * 4));

            foreach ((int, int) move in pk.learnset)
            {
                if (pk.learnset.IndexOf(move) * 4 < oldLearnsetSize)
                {
                    byte[] moveID = BitConverter.GetBytes(move.Item1);
                    romData[byteLoc] = moveID[0];
                    romData[byteLoc + 1] = moveID[1];
                    romData[byteLoc + 2] = (byte)move.Item2;
                    romData[byteLoc + 3] = 0;
                    byteLoc += 4;
                }
                else
                {
                    byte[] moveID = BitConverter.GetBytes(move.Item1);
                    romData.Insert(byteLoc, moveID[0]);
                    romData.Insert(byteLoc + 1, moveID[1]);
                    romData.Insert(byteLoc + 2, (byte)move.Item2);
                    romData.Insert(byteLoc + 3, 0);
                    byteLoc += 4;
                }
            }

            HexOffsets.RegisterNARCLocations(romData, 16, HexOffsets.pokeDataLocation);
        }

        private void pokemonEditorNameDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            pokemonTypeDropdown1.Enabled = true;
            pokemonTypeDropdown2.Enabled = true;
            pkLevelRateDropdown.Enabled = true;
            pokeAbilityDropdown1.Enabled = true;
            pokeAbilityDropdown2.Enabled = true;
            pokeAbilityDropdown3.Enabled = true;

            pokemonBaseHpNumberBox.Enabled = true;
            pokemonBaseAttackNumberBox.Enabled = true;
            pokemonBaseDefenseNumberBox.Enabled = true;
            pokemonBaseSpAttNumberBox.Enabled = true;
            pokemonBaseSpDefNumberBox.Enabled = true;
            pokemonBaseSpeedNumberBox.Enabled = true;

            PokemonData pk = pokemonData[pokemonEditorNameDropdown.SelectedIndex];
            pokemonTypeDropdown1.SelectedIndex = pk.type1;
            pokemonTypeDropdown2.SelectedIndex = pk.type2;
            pkLevelRateDropdown.SelectedIndex = pk.levelRate;
            pokeAbilityDropdown1.SelectedIndex = pk.ability1;
            pokeAbilityDropdown2.SelectedIndex = pk.ability2;
            pokeAbilityDropdown3.SelectedIndex = pk.ability3;

            pokemonBaseHpNumberBox.Value = pk.baseHp;
            pokemonBaseAttackNumberBox.Value = pk.baseAtt;
            pokemonBaseDefenseNumberBox.Value = pk.baseDef;
            pokemonBaseSpAttNumberBox.Value = pk.baseSpA;
            pokemonBaseSpDefNumberBox.Value = pk.baseSpD;
            pokemonBaseSpeedNumberBox.Value = pk.baseSpe;

            learnsetListBox.Enabled = true;
            addLearnsetMoveButton.Enabled = true;
            removeLearnsetMoveButton.Enabled = true;
            copyLearnsetButton.Enabled = true;
            learnsetMoveDropdown.Enabled = true;
            learnsetLevelNumberBox.Enabled = true;
            learnsetApplyMoveButton.Enabled = true;
            applyPokemonButton.Enabled = true;

            SetupPokemonLearnsetList();

            //Sync with evolution editor
            if (evolutionPokemonNameDropdown.Items.Count > 0) evolutionPokemonNameDropdown.SelectedIndex = pokemonEditorNameDropdown.SelectedIndex;
        }

        private void SetupPokemonLearnsetList()
        {
            learnsetListBox.Items.Clear();
            foreach ((int, int) move in pokemonData[pokemonEditorNameDropdown.SelectedIndex].learnset)
            {
                learnsetListBox.Items.Add(Pokemon.moveList[move.Item1] + " at lv " + move.Item2);
            }
            learnsetListBox.SelectedIndex = 0;
        }

        private void learnsetListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PokemonData pk = pokemonData[pokemonEditorNameDropdown.SelectedIndex];
            learnsetMoveDropdown.SelectedIndex = pk.learnset[learnsetListBox.SelectedIndex].Item1;
            learnsetLevelNumberBox.Value = pk.learnset[learnsetListBox.SelectedIndex].Item2;
        }

        private void learnsetApplyMoveButton_Click(object sender, EventArgs e)
        {
            PokemonData pk = pokemonData[pokemonEditorNameDropdown.SelectedIndex];
            pk.learnset[learnsetListBox.SelectedIndex] = (learnsetMoveDropdown.SelectedIndex, (int)learnsetLevelNumberBox.Value);

            int storeIndex = learnsetListBox.SelectedIndex;
            SetupPokemonLearnsetList();
            learnsetListBox.SelectedIndex = Math.Min(storeIndex, learnsetListBox.Items.Count - 1);
        }

        private void addLearnsetMoveButton_Click(object sender, EventArgs e)
        {
            PokemonData pk = pokemonData[pokemonEditorNameDropdown.SelectedIndex];
            pk.learnset.Add((1, 1));
            SetupPokemonLearnsetList();
            learnsetListBox.SelectedIndex = learnsetListBox.Items.Count - 1;
        }

        private void removeLearnsetMoveButton_Click(object sender, EventArgs e)
        {
            PokemonData pk = pokemonData[pokemonEditorNameDropdown.SelectedIndex];
            if (learnsetListBox.Items.Count > 1) pk.learnset.RemoveAt(learnsetListBox.SelectedIndex);

            int storeIndex = learnsetListBox.SelectedIndex;
            SetupPokemonLearnsetList();
            learnsetListBox.SelectedIndex = Math.Min(storeIndex, learnsetListBox.Items.Count - 1);
        }

        private void copyLearnsetButton_Click(object sender, EventArgs e)
        {
            PokemonData pk = pokemonData[pokemonEditorNameDropdown.SelectedIndex];
            learnsetClipboard = new List<(int, int)>();
            foreach ((int, int) move in pk.learnset) learnsetClipboard.Add((move.Item1, move.Item2));
            pasteLearnsetButton.Enabled = true;
        }

        private void pasteLearnsetButton_Click(object sender, EventArgs e)
        {
            PokemonData pk = pokemonData[pokemonEditorNameDropdown.SelectedIndex];
            pk.learnset = new List<(int, int)>();
            foreach ((int, int) move in learnsetClipboard) pk.learnset.Add((move.Item1, move.Item2));
            SetupPokemonLearnsetList();
        }

        private void applyPokemonButton_Click(object sender, EventArgs e)
        {
            PokemonData pk = pokemonData[pokemonEditorNameDropdown.SelectedIndex];
            pk.type1 = pokemonTypeDropdown1.SelectedIndex;
            pk.type2 = pokemonTypeDropdown2.SelectedIndex;
            pk.levelRate = pkLevelRateDropdown.SelectedIndex;
            pk.ability1 = pokeAbilityDropdown1.SelectedIndex;
            pk.ability2 = pokeAbilityDropdown2.SelectedIndex;
            pk.ability3 = pokeAbilityDropdown3.SelectedIndex;

            pk.baseHp = (int)pokemonBaseHpNumberBox.Value;
            pk.baseAtt = (int)pokemonBaseAttackNumberBox.Value;
            pk.baseDef = (int)pokemonBaseDefenseNumberBox.Value;
            pk.baseSpA = (int)pokemonBaseSpAttNumberBox.Value;
            pk.baseSpD = (int)pokemonBaseSpDefNumberBox.Value;
            pk.baseSpe = (int)pokemonBaseSpeedNumberBox.Value;

            DateTime time2 = DateTime.Now;
            pk.learnset.Sort(CompareLearnsetMoves);
            int storeLoc = pokemonEditorNameDropdown.SelectedIndex;
            SetupPokemonLearnsetList();

            WritePokemonData();

            ReadAllData();

            pokemonEditorNameDropdown.SelectedIndex = storeLoc;
        }

        private void PokemonBaseStatValueChanged(object sender, EventArgs e)
        {
            pokemonBSTText.Text = "Total: " + (pokemonBaseHpNumberBox.Value + pokemonBaseAttackNumberBox.Value + pokemonBaseDefenseNumberBox.Value + pokemonBaseSpAttNumberBox.Value + pokemonBaseSpDefNumberBox.Value + pokemonBaseSpeedNumberBox.Value);
        }

        private static int CompareLearnsetMoves((int, int) m1, (int, int) m2) => m1.Item2 == m2.Item2 ? Pokemon.moveList[m1.Item1].CompareTo(Pokemon.moveList[m2.Item1]) : Math.Sign(m1.Item2 - m2.Item2);
        #endregion

        #region Evolutions

        private void evolutionPokemonNameDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            evolutionsListBox.Items.Clear();
            foreach ((int, int, int) ev in pokemonData[evolutionPokemonNameDropdown.SelectedIndex].evolutions)
            {
                evolutionsListBox.Items.Add(Pokemon.nameListWith0Index[ev.Item3] + (ev.Item3 != 0 ? " by " + Pokemon.evolutionMethodList[ev.Item1] : ""));
            }
            evolutionsListBox.SelectedIndex = 0;
            evolutionApplyButton.Enabled = true;

            //Sync with pokemon editor
            pokemonEditorNameDropdown.SelectedIndex = evolutionPokemonNameDropdown.SelectedIndex;
        }

        private void evolutionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PokemonData p = pokemonData[evolutionPokemonNameDropdown.SelectedIndex];
            (int, int, int) evo = p.evolutions[evolutionsListBox.SelectedIndex];
            evolutionMethodDropdown.SelectedIndex = evo.Item1;
            evolutionIntoDropdown.SelectedIndex = evo.Item3;

            int conditionType = 0;
            evolutionConditionDropdown.Items.Clear();
            switch (evolutionMethodDropdown.SelectedIndex)
            {
                //Level
                case 4: conditionType = 1; break;
                case 9: conditionType = 1; break;
                case 10: conditionType = 1; break;
                case 11: conditionType = 1; break;
                case 12: conditionType = 1; break;
                case 13: conditionType = 1; break;
                case 14: conditionType = 1; break;
                case 15: conditionType = 1; break;
                case 23: conditionType = 1; break;
                case 24: conditionType = 1; break;

                //Item
                case 6: conditionType = 2; break;
                case 8: conditionType = 2; break;
                case 17: conditionType = 2; break;
                case 18: conditionType = 2; break;
                case 19: conditionType = 2; break;
                case 20: conditionType = 2; break;

                //Beauty
                case 16: conditionType = 3; break;

                //Move
                case 21: conditionType = 4; break;

                //With Pokemon
                case 22: conditionType = 5; break;

                //No Condition
                default: conditionType = 0; break;
            }

            if (conditionType == 0) evolutionConditionDropdown.Items.Add("---");
            if (conditionType == 1) for (int i = 0; i <= 100; i++) evolutionConditionDropdown.Items.Add(i);
            if (conditionType == 2) evolutionConditionDropdown.Items.AddRange(Items.nameList.ToArray());
            if (conditionType == 3) for (int i = 0; i <= 255; i++) evolutionConditionDropdown.Items.Add(i);
            if (conditionType == 4) evolutionConditionDropdown.Items.AddRange(Pokemon.moveList.ToArray());
            if (conditionType == 5) evolutionConditionDropdown.Items.AddRange(Pokemon.nameListWith0Index.ToArray());

            evolutionConditionDropdown.SelectedIndex = evo.Item2;

            evolutionMethodDropdown.Enabled = true;
            evolutionConditionDropdown.Enabled = true;
            evolutionIntoDropdown.Enabled = true;
        }

        private void evolutionMethodDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            PokemonData p = pokemonData[evolutionPokemonNameDropdown.SelectedIndex];
            (int, int, int) evo = p.evolutions[evolutionsListBox.SelectedIndex];

            int conditionType = 0;
            evolutionConditionDropdown.Items.Clear();
            switch (evolutionMethodDropdown.SelectedIndex)
            {
                //Level
                case 4: conditionType = 1; break;
                case 9: conditionType = 1; break;
                case 10: conditionType = 1; break;
                case 11: conditionType = 1; break;
                case 12: conditionType = 1; break;
                case 13: conditionType = 1; break;
                case 14: conditionType = 1; break;
                case 15: conditionType = 1; break;
                case 23: conditionType = 1; break;
                case 24: conditionType = 1; break;

                //Item
                case 6: conditionType = 2; break;
                case 8: conditionType = 2; break;
                case 17: conditionType = 2; break;
                case 18: conditionType = 2; break;
                case 19: conditionType = 2; break;
                case 20: conditionType = 2; break;

                //Beauty
                case 16: conditionType = 3; break;

                //Move
                case 21: conditionType = 4; break;

                //With Pokemon
                case 22: conditionType = 5; break;

                //No Condition
                default: conditionType = 0; break;
            }

            if (conditionType == 0) evolutionConditionDropdown.Items.Add("---");
            if (conditionType == 1) for (int i = 0; i <= 100; i++) evolutionConditionDropdown.Items.Add(i);
            if (conditionType == 2) evolutionConditionDropdown.Items.AddRange(Items.nameList.ToArray());
            if (conditionType == 3) for (int i = 0; i <= 255; i++) evolutionConditionDropdown.Items.Add(i);
            if (conditionType == 4) evolutionConditionDropdown.Items.AddRange(Pokemon.moveList.ToArray());
            if (conditionType == 5) evolutionConditionDropdown.Items.AddRange(Pokemon.nameListWith0Index.ToArray());

            if (evo.Item2 < evolutionConditionDropdown.Items.Count) evolutionConditionDropdown.SelectedIndex = evo.Item2;
            else evolutionConditionDropdown.SelectedIndex = 0;
        }

        private void evolutionApplyButton_Click(object sender, EventArgs e)
        {
            PokemonData poke = pokemonData[evolutionPokemonNameDropdown.SelectedIndex];
            poke.evolutions[evolutionsListBox.SelectedIndex] = (evolutionMethodDropdown.SelectedIndex, evolutionConditionDropdown.SelectedIndex, evolutionIntoDropdown.SelectedIndex);
            (int, int, int) evo = poke.evolutions[evolutionsListBox.SelectedIndex];

            byte[] bytes = BitConverter.GetBytes(evo.Item1);
            romData[poke.evolutionByteLocation] = bytes[0];
            romData[poke.evolutionByteLocation + 1] = bytes[1];
            bytes = BitConverter.GetBytes(evo.Item2);
            romData[poke.evolutionByteLocation + 2] = bytes[0];
            romData[poke.evolutionByteLocation + 3] = bytes[1];
            bytes = BitConverter.GetBytes(evo.Item3);
            romData[poke.evolutionByteLocation + 4] = bytes[0];
            romData[poke.evolutionByteLocation + 5] = bytes[1];

            evolutionsListBox.SelectedIndex = evolutionsListBox.SelectedIndex;
        }

        #endregion

        #region Trainers
        private void ChangeTrainerInDropdown(object sender, EventArgs e)
        {
            if (trDataDropdown.SelectedIndex != 0)
            {
                trNumPokemonBox.Value = trainerData[trDataDropdown.SelectedIndex].numPokemon;
                trBattleTypeDropDown.SelectedIndex = trainerData[trDataDropdown.SelectedIndex].battleType;
                trNumPokemonBox.Enabled = true;
                trMovesetCheckBox.Enabled = true;
                trItemCheckBox.Enabled = true;
                trBattleTypeDropDown.Enabled = true;
                trDataApplyButton.Enabled = true;
                trainerPokemonTabs.Enabled = true;
                for (int i = 0; i < 6; i++) ((Control)trPokeTabs[i]).Enabled = false;

                int entrySize = 0;
                if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat == 0) entrySize = 8;
                if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat == 1) entrySize = 16;
                if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat == 2) entrySize = 10;
                if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat == 3) entrySize = 18;
                
                for (int i = 0; i < trainerData[trDataDropdown.SelectedIndex].numPokemon; i++)
                {
                    ((Control)trPokeTabs[i]).Enabled = true;
                    int byteLoc = trainerData[trDataDropdown.SelectedIndex].pokeByteLocation + i * entrySize;
                    ((ComboBox)trainerPokemonTabs.TabPages[i].Controls[1]).SelectedIndex = BitConverter.ToInt16(romData.GetRange(byteLoc + 4, 2).ToArray(), 0) - 1;
                    ((NumericUpDown)trainerPokemonTabs.TabPages[i].Controls[19]).Value = romData[byteLoc + 6];
                    ((NumericUpDown)trainerPokemonTabs.TabPages[i].Controls[3]).Value = BitConverter.ToInt16(romData.GetRange(byteLoc + 2, 2).ToArray(), 0);

                    //Ability
                    ((ComboBox)trPokeTabs[i].Controls[15]).SelectedItem = null;
                    ((ComboBox)trPokeTabs[i].Controls[15]).Items.Clear();
                    ((ComboBox)trPokeTabs[i].Controls[15]).Items.Add("1 or 2");
                    ((ComboBox)trPokeTabs[i].Controls[15]).Items.Add(Pokemon.abilityList[PokemonDataFromID(BitConverter.ToInt16(romData.GetRange(byteLoc + 4, 2).ToArray(), 0)).ability1]);
                    ((ComboBox)trPokeTabs[i].Controls[15]).Items.Add(Pokemon.abilityList[PokemonDataFromID(BitConverter.ToInt16(romData.GetRange(byteLoc + 4, 2).ToArray(), 0)).ability2]);
                    ((ComboBox)trPokeTabs[i].Controls[15]).Items.Add(Pokemon.abilityList[PokemonDataFromID(BitConverter.ToInt16(romData.GetRange(byteLoc + 4, 2).ToArray(), 0)).ability3]);
                    ((ComboBox)trPokeTabs[i].Controls[15]).SelectedIndex = romData[byteLoc + 1] >> 4;
                    
                    //Gender
                    ((ComboBox)trPokeTabs[i].Controls[17]).SelectedItem = null;
                    ((ComboBox)trPokeTabs[i].Controls[17]).SelectedIndex = romData[byteLoc + 1] & 3;

                    if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat % 2 == 1)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            int startPos = byteLoc + 8;
                            if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat >= 2) startPos += 2;
                            ((ComboBox)trPokeTabs[i].Controls[5 + (j * 2)]).SelectedItem = null;
                            ((ComboBox)trPokeTabs[i].Controls[5 + (j * 2)]).SelectedIndex = BitConverter.ToInt16(romData.GetRange(startPos + (j * 2), 2).ToArray(), 0);
                            ((ComboBox)trPokeTabs[i].Controls[5 + (j * 2)]).Enabled = true;
                        }
                        trMovesetCheckBox.Checked = true;
                    }
                    else
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            ((ComboBox)trPokeTabs[i].Controls[5 + (j * 2)]).SelectedItem = null;
                            ((ComboBox)trPokeTabs[i].Controls[5 + (j * 2)]).SelectedIndex = 0;
                            ((ComboBox)trPokeTabs[i].Controls[5 + (j * 2)]).Enabled = false;
                        }
                        trMovesetCheckBox.Checked = false;
                    }
                    if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat >= 2)
                    {
                        ((ComboBox)trPokeTabs[i].Controls[13]).SelectedItem = null;
                        ((ComboBox)trPokeTabs[i].Controls[13]).SelectedIndex = BitConverter.ToInt16(romData.GetRange(byteLoc + 8, 2).ToArray(), 0);
                        ((ComboBox)trPokeTabs[i].Controls[13]).Enabled = true;
                    }
                    else
                    {
                        ((ComboBox)trPokeTabs[i].Controls[13]).SelectedItem = null;
                        ((ComboBox)trPokeTabs[i].Controls[13]).SelectedIndex = 0;
                        ((ComboBox)trPokeTabs[i].Controls[13]).Enabled = false;
                    }
                }
                if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat >= 2) trItemCheckBox.Checked = true;
                else trItemCheckBox.Checked = false;
            }
            else
            {
                trNumPokemonBox.Enabled = false;
                trMovesetCheckBox.Enabled = false;
                trItemCheckBox.Enabled = false;
                trBattleTypeDropDown.Enabled = false;
                trDataApplyButton.Enabled = false;
                trainerPokemonTabs.Enabled = false;
            }
        }

        private void ReadTrainerData()
        {
            //Register Trainer Data
            trainerData.Clear();
            bool newDropdownList = trDataDropdown.Items.Count == 0;
            for (int i = 0; i < (HexOffsets.trDataTotalBytes - HexOffsets.trDataFirstEntry) / HexOffsets.trDataEntrySize; i++)
            {
                int byteLoc = HexOffsets.trDataLocation + HexOffsets.trDataFirstEntry + i * HexOffsets.trDataEntrySize;
                if (newDropdownList)
                {
                    trDataDropdown.Items.Add(Trainers.nameList[i] + " - " + i);
                }

                trainerData.Add(new TrainerData() { byteLocation = byteLoc, numPokemon = romData[byteLoc + 3], pokeDataFormat = romData[byteLoc], battleType = romData[byteLoc + 2] });
            }

            //Register Trainer Pokemon Data Locations
            int pokeByteLoc = HexOffsets.trPokeLocation + HexOffsets.trPokeFirstEntry;
            for (int i = 0; i < trainerData.Count; i++)
            {
                int entrySize = 0;
                if (trainerData[i].pokeDataFormat == 0) entrySize = 8;
                if (trainerData[i].pokeDataFormat == 1) entrySize = 16;
                if (trainerData[i].pokeDataFormat == 2) entrySize = 10;
                if (trainerData[i].pokeDataFormat == 3) entrySize = 18;

                trainerData[i].pokeByteLocation = pokeByteLoc;
                pokeByteLoc += entrySize * trainerData[i].numPokemon;
                while (romData[pokeByteLoc] == 0xFF)
                {
                    pokeByteLoc++;
                }

                //Distinguish grunts
                if (Trainers.nameList[i] == "Grunt") trDataDropdown.Items[i] = Trainers.nameList[i] + " - " + i + " (level: " + romData[trainerData[i].pokeByteLocation + 2] + " " + Pokemon.nameList[BitConverter.ToInt16(romData.GetRange(trainerData[i].pokeByteLocation + 4, 2).ToArray(), 0) - 1] + ")";
            }
        }

        private void ReadTrainerPokeData()
        {
            int entrySize = 0;
            if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat == 0) entrySize = 8;
            if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat == 1) entrySize = 16;
            if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat == 2) entrySize = 10;
            if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat == 3) entrySize = 18;
            trainerPokemonTabs.Enabled = true;
            for (int i = 0; i < 6; i++) ((Control)trPokeTabs[i]).Enabled = false;
            for (int i = 0; i < trainerData[trDataDropdown.SelectedIndex].numPokemon; i++)
            {
                ((Control)trPokeTabs[i]).Enabled = true;
                int byteLoc = trainerData[trDataDropdown.SelectedIndex].pokeByteLocation + i * entrySize;
                ((ComboBox)trainerPokemonTabs.TabPages[i].Controls[1]).SelectedIndex = BitConverter.ToInt16(romData.GetRange(byteLoc + 4, 2).ToArray(), 0) - 1;
                ((NumericUpDown)trainerPokemonTabs.TabPages[i].Controls[19]).Value = romData[byteLoc + 6];
                ((NumericUpDown)trainerPokemonTabs.TabPages[i].Controls[3]).Value = BitConverter.ToInt16(romData.GetRange(byteLoc + 2, 2).ToArray(), 0);

                //Ability
                ((ComboBox)trPokeTabs[i].Controls[15]).SelectedItem = null;
                ((ComboBox)trPokeTabs[i].Controls[15]).Items.Clear();
                ((ComboBox)trPokeTabs[i].Controls[15]).Items.Add("1 or 2");
                ((ComboBox)trPokeTabs[i].Controls[15]).Items.Add(Pokemon.abilityList[PokemonDataFromID(BitConverter.ToInt16(romData.GetRange(byteLoc + 4, 2).ToArray(), 0)).ability1]);
                ((ComboBox)trPokeTabs[i].Controls[15]).Items.Add(Pokemon.abilityList[PokemonDataFromID(BitConverter.ToInt16(romData.GetRange(byteLoc + 4, 2).ToArray(), 0)).ability2]);
                ((ComboBox)trPokeTabs[i].Controls[15]).Items.Add(Pokemon.abilityList[PokemonDataFromID(BitConverter.ToInt16(romData.GetRange(byteLoc + 4, 2).ToArray(), 0)).ability3]);
                ((ComboBox)trPokeTabs[i].Controls[15]).SelectedIndex = romData[byteLoc + 1] >> 4;

                if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat % 2 == 1)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        int startPos = byteLoc + 8;
                        if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat >= 2) startPos += 2;
                        ((ComboBox)trPokeTabs[i].Controls[5 + (j * 2)]).SelectedItem = null;
                        ((ComboBox)trPokeTabs[i].Controls[5 + (j * 2)]).SelectedIndex = BitConverter.ToInt16(romData.GetRange(startPos + (j * 2), 2).ToArray(), 0);
                        ((ComboBox)trPokeTabs[i].Controls[5 + (j * 2)]).Enabled = true;
                    }
                }
                else
                {
                    for (int j = 0; j < 4; j++)
                    {
                        ((ComboBox)trPokeTabs[i].Controls[5 + (j * 2)]).SelectedItem = null;
                        ((ComboBox)trPokeTabs[i].Controls[5 + (j * 2)]).SelectedIndex = 0;
                        ((ComboBox)trPokeTabs[i].Controls[5 + (j * 2)]).Enabled = false;
                    }
                }

                if (trainerData[trDataDropdown.SelectedIndex].pokeDataFormat >= 2)
                {
                    trItemCheckBox.Checked = true;
                    trPokeTabs[i].Controls[13].Enabled = true;
                }
                else
                {
                    trItemCheckBox.Checked = false;
                    trPokeTabs[i].Controls[13].Enabled = false;
                }
            }
        }

        private void ApplyTrainerData(object sender, EventArgs e)
        {
            TrainerData trainer = trainerData[trDataDropdown.SelectedIndex];
            int byteLoc = HexOffsets.trDataLocation + HexOffsets.trDataFirstEntry + trDataDropdown.SelectedIndex * HexOffsets.trDataEntrySize;
            int oldEntrySize = 0;
            if (romData[byteLoc] == 0) oldEntrySize = 8;
            if (romData[byteLoc] == 1) oldEntrySize = 16;
            if (romData[byteLoc] == 2) oldEntrySize = 10;
            if (romData[byteLoc] == 3) oldEntrySize = 18;

            //Trainer Data
            romData[byteLoc] = 0;
            if (trItemCheckBox.Checked) romData[byteLoc] += 2;
            if (trMovesetCheckBox.Checked) romData[byteLoc] += 1;

            romData[byteLoc + 2] = Convert.ToByte(trBattleTypeDropDown.SelectedIndex);
            romData[byteLoc + 3] = Convert.ToByte(trNumPokemonBox.Value);

            //Trainer Pokemon Data
            int newEntrySize = 0;
            if (romData[byteLoc] == 0) newEntrySize = 8;
            if (romData[byteLoc] == 1) newEntrySize = 16;
            if (romData[byteLoc] == 2) newEntrySize = 10;
            if (romData[byteLoc] == 3) newEntrySize = 18;

            for (int i = 0; i < trainer.numPokemon; i++)
            {
                int byteLoc2 = trainer.pokeByteLocation + i * newEntrySize;
                //Fix the extry size checks to read 8 - 18, not 0 - 3
                if (newEntrySize % 8 == 2 && oldEntrySize % 8 == 0)
                {
                    //Add 2 bytes for an item
                    romData.InsertRange(byteLoc2 + 8, new byte[] { 0, 0 });
                }
                if (newEntrySize % 8 == 0 && oldEntrySize % 8 == 2)
                {
                    //Remove 2 bytes for an item
                    romData.RemoveRange(byteLoc2 + 8, 2);
                }
                if (newEntrySize > 10 && oldEntrySize <= 10)
                {
                    //Add 8 bytes for moves
                    romData.InsertRange(byteLoc2 + (newEntrySize % 8 == 2 ? 10 : 8), new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
                }
                if (newEntrySize <= 10 && oldEntrySize > 10)
                {
                    //Remove 8 bytes for moves
                    romData.RemoveRange(byteLoc2 + (newEntrySize % 8 == 2 ? 10 : 8), 8);
                }
            }
            if (trNumPokemonBox.Value > trainer.numPokemon)
            {
                //Add pokemon
                for (int i = trainer.numPokemon; i < trNumPokemonBox.Value; i++)
                {
                    int byteLoc3 = trainer.pokeByteLocation + i * newEntrySize;
                    byte[] bytes = new byte[newEntrySize];
                    bytes[2] = 1;
                    bytes[4] = 1;
                    romData.InsertRange(byteLoc3, bytes);
                }
            }
            if (trNumPokemonBox.Value < trainer.numPokemon)
            {
                //Remove pokemon
                for (int i = trainer.numPokemon; i > trNumPokemonBox.Value; i--)
                {
                    int byteLoc3 = trainerData[trDataDropdown.SelectedIndex].pokeByteLocation + i * newEntrySize;
                    romData.RemoveRange(byteLoc3, newEntrySize);
                }
            }

            ApplyTrPokeData(trainer, newEntrySize);
            HexOffsets.RegisterNARCLocations(romData, 92, HexOffsets.trPokeLocation);
            ReadAllData();
        }

        private void ApplyTrPokeData(TrainerData trainer, int entrySize)
        {
            for (int i = 0; i < trainer.numPokemon; i++)
            {
                int byteLoc = trainer.pokeByteLocation + i * entrySize;
                byte[] toBytes = BitConverter.GetBytes(((ComboBox)trainerPokemonTabs.TabPages[i].Controls[1]).SelectedIndex + 1);
                romData[byteLoc + 4] = toBytes[0];
                romData[byteLoc + 5] = toBytes[1];
                romData[byteLoc + 6] = (byte)((NumericUpDown)trainerPokemonTabs.TabPages[i].Controls[19]).Value;
                toBytes = BitConverter.GetBytes((int)((NumericUpDown)trainerPokemonTabs.TabPages[i].Controls[3]).Value);
                romData[byteLoc + 2] = toBytes[0];

                //Gender and ability
                byte[] GnA = BitConverter.GetBytes((((ComboBox)trainerPokemonTabs.TabPages[i].Controls[15]).SelectedIndex << 4) + ((ComboBox)trainerPokemonTabs.TabPages[i].Controls[17]).SelectedIndex);
                romData[byteLoc + 1] = GnA[0];

                if (entrySize > 10)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        int startPos = byteLoc + 8;
                        if (entrySize % 8 == 2) startPos += 2;
                        toBytes = BitConverter.GetBytes(((ComboBox)trPokeTabs[i].Controls[5 + (j * 2)]).SelectedIndex);
                        romData[startPos + (j * 2)] = toBytes[0];
                        romData[startPos + (j * 2) + 1] = toBytes[1];
                    }
                }
                if (entrySize % 8 == 2)
                {
                    toBytes = BitConverter.GetBytes(((ComboBox)trPokeTabs[i].Controls[13]).SelectedIndex);
                    romData[byteLoc + 8] = toBytes[0];
                    romData[byteLoc + 9] = toBytes[1];
                }
            }
        }
        #endregion

        #region Wild Encounters
        private void ReadEncounterData()
        {
            //Register Encounter Data
            routeEncounters.Clear();
            int oldSelectedIndex = encounterRouteNameDropdown.SelectedIndex;
            encounterRouteNameDropdown.Items.Clear();
            bool newDropdownList = encounterRouteNameDropdown.Items.Count == 0;
            for (int i = 0; i < (HexOffsets.encountersTotalBytes - HexOffsets.encountersFirstEntry) / HexOffsets.encountersEntrySize; i++)
            {
                int byteLoc = HexOffsets.encountersLocation + HexOffsets.encountersFirstEntry + i * HexOffsets.encountersEntrySize;
                if (i < Routes.nameList.Count) encounterRouteNameDropdown.Items.Add(Routes.nameList[i] + " - " + i);

                EncounterData ed = new EncounterData();
                ed.byteLocation = byteLoc;

                #region Assign Encounter Arrays
                List<int> encounterRates = new List<int>() { 20, 20, 10, 10, 10, 10, 5, 5, 4, 4, 1, 1 };

                int loc = byteLoc + 8;
                ed.landSlots = new List<EncounterSlot[]>();
                for (int x = 0; x < 3; x++)
                {
                    EncounterSlot[] e = new EncounterSlot[12];
                    for (int y = 0; y < e.Length; y++)
                    {
                        e[y] = new EncounterSlot() { pokemonID = BitConverter.ToInt16(romData.GetRange(loc, 2).ToArray(), 0), minLevel = romData[loc + 2], maxLevel = romData[loc + 3], rate = encounterRates[y] };
                        while (e[y].pokemonID > 2048)
                        {
                            e[y].pokemonID -= 2048;
                            e[y].pokemonForm++;
                        }
                        loc += 4;
                    }
                    ed.landSlots.Add(e);
                }

                encounterRates = new List<int>() { 60, 30, 5, 4, 1 };
                ed.waterSlots = new List<EncounterSlot[]>();
                for (int x = 0; x < 4; x++)
                {
                    EncounterSlot[] e = new EncounterSlot[5];
                    for (int y = 0; y < e.Length; y++)
                    {
                        e[y] = new EncounterSlot() { pokemonID = BitConverter.ToInt16(romData.GetRange(loc, 2).ToArray(), 0), minLevel = romData[loc + 2], maxLevel = romData[loc + 3], rate = encounterRates[y] };
                        while (e[y].pokemonID > 2048)
                        {
                            e[y].pokemonID -= 2048;
                            e[y].pokemonForm++;
                        }
                        loc += 4;
                    }
                    ed.waterSlots.Add(e);
                }


                EncounterSlotsToGroups(ed);
                #endregion

                routeEncounters.Add(ed);
            }
            encounterRouteNameDropdown.SelectedIndex = oldSelectedIndex;
        }

        private void EncounterSlotsToGroups(EncounterData ed)
        {
            ed.groupedLandSlots = new List<List<EncounterSlot>>();
            ed.groupedLandSlots.Add(new List<EncounterSlot>());
            ed.groupedLandSlots.Add(new List<EncounterSlot>());
            ed.groupedLandSlots.Add(new List<EncounterSlot>());
            for (int j = 0; j < 3; j++) foreach (EncounterSlot e in ed.landSlots[j])
                {
                    bool foundGroup = false;
                    foreach (EncounterSlot e2 in ed.groupedLandSlots[j]) if (e.pokemonID == e2.pokemonID && e.pokemonForm == e2.pokemonForm)
                        {
                            foundGroup = true;
                            e2.rate += e.rate;
                            e2.minLevel = Math.Min(e2.minLevel, e.minLevel);
                            e2.maxLevel = Math.Max(e2.maxLevel, e.maxLevel);
                            break;
                        }
                    if (!foundGroup)
                    {
                        ed.groupedLandSlots[j].Add(new EncounterSlot() { pokemonID = e.pokemonID, pokemonForm = e.pokemonForm, minLevel = e.minLevel, maxLevel = e.maxLevel, rate = e.rate });
                    }
                }

            ed.groupedWaterSlots = new List<List<EncounterSlot>>();
            ed.groupedWaterSlots.Add(new List<EncounterSlot>());
            ed.groupedWaterSlots.Add(new List<EncounterSlot>());
            ed.groupedWaterSlots.Add(new List<EncounterSlot>());
            ed.groupedWaterSlots.Add(new List<EncounterSlot>());
            for (int j = 0; j < 4; j++) foreach (EncounterSlot e in ed.waterSlots[j])
                {
                    bool foundGroup = false;
                    foreach (EncounterSlot e2 in ed.groupedWaterSlots[j]) if (e.pokemonID == e2.pokemonID && e.pokemonForm == e2.pokemonForm)
                        {
                            foundGroup = true;
                            e2.rate += e.rate;
                            e2.minLevel = Math.Min(e2.minLevel, e.minLevel);
                            e2.maxLevel = Math.Max(e2.maxLevel, e.maxLevel);
                            break;
                        }
                    if (!foundGroup)
                    {
                        ed.groupedWaterSlots[j].Add(new EncounterSlot() { pokemonID = e.pokemonID, pokemonForm = e.pokemonForm, minLevel = e.minLevel, maxLevel = e.maxLevel, rate = e.rate });
                    }
                }
        }

        private void encounterRouteNameDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            routeEncounterTypesPanel.Enabled = true;
            encounterGroupListBox.Enabled = true;
            addEncounterGroupButton.Enabled = true;
            removeEncounterGroupButton.Enabled = true;
            encounterPokemonNameDropDown.Enabled = true;
            encounterGroupFormNumber.Enabled = true;
            encounterGroupMinLvNumber.Enabled = true;
            encounterGroupMaxLvNumber.Enabled = true;
            encounterGroupRateNumber.Enabled = true;
            encounterSlotApplyButton.Enabled = true;
            copyGrassToDarkGrassButton.Enabled = true;
            copyEncounterGroupButton.Enabled = true;
            encounterGroupApplyRouteButton.Enabled = true;

            EncounterSlotsToGroups(routeEncounters[encounterRouteNameDropdown.SelectedIndex]);
            SetupEncounterListBox(true);
        }

        private void encounterGroupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupEncounterListBox(false);
        }

        private void ChangeRouteEncounterType(object sender, EventArgs e)
        {
            SetupEncounterListBox(true);
        }

        private void SetupEncounterListBox(bool redoListBox)
        {
            int selectedType = 0;
            for (int i = 6; i >= 0; i--) if (((RadioButton)routeEncounterTypesPanel.Controls[i]).Checked) selectedType = 6 - i;

            if (redoListBox)
            {
                encounterGroupListBox.Items.Clear();
                if (selectedType <= 2) foreach (EncounterSlot slot in routeEncounters[encounterRouteNameDropdown.SelectedIndex].groupedLandSlots[selectedType])
                    {
                        encounterGroupListBox.Items.Add(Pokemon.nameListWith0Index[slot.pokemonID] + " - " + slot.rate + "%");
                    }
                else foreach (EncounterSlot slot in routeEncounters[encounterRouteNameDropdown.SelectedIndex].groupedWaterSlots[selectedType - 3])
                    {
                        encounterGroupListBox.Items.Add(Pokemon.nameListWith0Index[slot.pokemonID] + " - " + slot.rate + "%");
                    }
                encounterGroupListBox.SelectedIndex = 0;
            }

            EncounterData route = routeEncounters[encounterRouteNameDropdown.SelectedIndex];
            if (selectedType <= 2)
            {
                encounterPokemonNameDropDown.SelectedIndex = route.groupedLandSlots[selectedType][encounterGroupListBox.SelectedIndex].pokemonID;
                encounterGroupFormNumber.Value = route.groupedLandSlots[selectedType][encounterGroupListBox.SelectedIndex].pokemonForm;
                encounterGroupMinLvNumber.Value = Math.Max(route.groupedLandSlots[selectedType][encounterGroupListBox.SelectedIndex].minLevel, 1);
                encounterGroupMaxLvNumber.Value = Math.Max(route.groupedLandSlots[selectedType][encounterGroupListBox.SelectedIndex].maxLevel, 1);
                encounterGroupRateNumber.Value = route.groupedLandSlots[selectedType][encounterGroupListBox.SelectedIndex].rate;
            }
            else
            {
                encounterPokemonNameDropDown.SelectedIndex = route.groupedWaterSlots[selectedType - 3][encounterGroupListBox.SelectedIndex].pokemonID;
                encounterGroupFormNumber.Value = route.groupedWaterSlots[selectedType - 3][encounterGroupListBox.SelectedIndex].pokemonForm;
                encounterGroupMinLvNumber.Value = Math.Max(route.groupedWaterSlots[selectedType - 3][encounterGroupListBox.SelectedIndex].minLevel, 1);
                encounterGroupMaxLvNumber.Value = Math.Max(route.groupedWaterSlots[selectedType - 3][encounterGroupListBox.SelectedIndex].maxLevel, 1);
                encounterGroupRateNumber.Value = route.groupedWaterSlots[selectedType - 3][encounterGroupListBox.SelectedIndex].rate;
            }
        }

        private void encounterGroupApplyButton_Click(object sender, EventArgs e)
        {
            int selectedType = 0;
            for (int i = 6; i >= 0; i--) if (((RadioButton)routeEncounterTypesPanel.Controls[i]).Checked) selectedType = 6 - i;

            List<EncounterSlot> slotList = new List<EncounterSlot>();
            if (selectedType <= 2) slotList = routeEncounters[encounterRouteNameDropdown.SelectedIndex].groupedLandSlots[selectedType];
            else slotList = routeEncounters[encounterRouteNameDropdown.SelectedIndex].groupedWaterSlots[selectedType - 3];

            slotList[encounterGroupListBox.SelectedIndex].pokemonID = encounterPokemonNameDropDown.SelectedIndex;
            slotList[encounterGroupListBox.SelectedIndex].pokemonForm = (int)encounterGroupFormNumber.Value;
            slotList[encounterGroupListBox.SelectedIndex].minLevel = (int)encounterGroupMinLvNumber.Value;
            slotList[encounterGroupListBox.SelectedIndex].maxLevel = (int)encounterGroupMaxLvNumber.Value;
            slotList[encounterGroupListBox.SelectedIndex].rate = (int)encounterGroupRateNumber.Value;

            int storeIndex = encounterGroupListBox.SelectedIndex;
            SetupEncounterListBox(true);
            encounterGroupListBox.SelectedIndex = Math.Min(storeIndex, slotList.Count - 1);
        }

        private void addEncounterGroupButton_Click(object sender, EventArgs e)
        {
            int selectedType = 0;
            for (int i = 6; i >= 0; i--) if (((RadioButton)routeEncounterTypesPanel.Controls[i]).Checked) selectedType = 6 - i;

            List<EncounterSlot> slotList = new List<EncounterSlot>();
            if (selectedType <= 2) slotList = routeEncounters[encounterRouteNameDropdown.SelectedIndex].groupedLandSlots[selectedType];
            else slotList = routeEncounters[encounterRouteNameDropdown.SelectedIndex].groupedWaterSlots[selectedType - 3];

            if (slotList.Count < 12) slotList.Add(new EncounterSlot() { pokemonID = 0, pokemonForm = 0, maxLevel = 1, minLevel = 1, rate = 0 });
            SetupEncounterListBox(true);
            encounterGroupListBox.SelectedIndex = slotList.Count - 1;
        }

        private void removeEncounterSlotButton_Click(object sender, EventArgs e)
        {
            int selectedType = 0;
            for (int i = 6; i >= 0; i--) if (((RadioButton)routeEncounterTypesPanel.Controls[i]).Checked) selectedType = 6 - i;

            List<EncounterSlot> slotList = new List<EncounterSlot>();
            if (selectedType <= 2) slotList = routeEncounters[encounterRouteNameDropdown.SelectedIndex].groupedLandSlots[selectedType];
            else slotList = routeEncounters[encounterRouteNameDropdown.SelectedIndex].groupedWaterSlots[selectedType - 3];

            if (slotList.Count > 1) slotList.RemoveAt(encounterGroupListBox.SelectedIndex);
            int storeIndex = encounterGroupListBox.SelectedIndex;
            SetupEncounterListBox(true);
            encounterGroupListBox.SelectedIndex = Math.Min(storeIndex, slotList.Count - 1);
        }

        private void KeepEncounterSlotMinLevelBelowMaxLevel(object sender, EventArgs e)
        {
            if (encounterGroupMinLvNumber.Value > encounterGroupMaxLvNumber.Value) encounterGroupMaxLvNumber.Value = encounterGroupMinLvNumber.Value;
        }

        private void encounterGroupApplyRouteButton_Click(object sender, EventArgs e)
        {
            EncounterData route = routeEncounters[encounterRouteNameDropdown.SelectedIndex];
            for (int i = 0; i < 7; i++)
            {
                //Identify the list to use
                List<EncounterSlot> slotList = new List<EncounterSlot>();
                if (i <= 2) slotList = route.groupedLandSlots[i];
                else slotList = route.groupedWaterSlots[i - 3];

                //Make sure the rates add up to 100%
                int sum = 0;
                foreach (EncounterSlot slot in slotList.ToArray())
                {
                    if ((slot.pokemonID == 0 || slot.rate == 0) && slotList.Count > 1) slotList.Remove(slot);
                    else sum += slot.rate;
                }
                if (sum != 100)
                {
                    MessageBox.Show("One of the encounter rate distributions doesn't add up to 100%\nsum = " + sum + "%");
                    //Reset the Encounter UI
                    SetupEncounterListBox(true);
                    return;
                }

                //Sort groups by rate
                slotList.Sort(CompareEncounterSlots);
                //Setup slots
                EncounterSlot[] slotAssignment = new EncounterSlot[12];
                if (i > 2) slotAssignment = new EncounterSlot[5];
                List<int> encounterRates = new List<int>() { 20, 20, 10, 10, 10, 10, 5, 5, 4, 4, 1, 1 };
                if (i > 2) encounterRates = new List<int>() { 60, 30, 5, 4, 1 };

                //Assign Groups to slots
                foreach (EncounterSlot slot in slotList.ToArray())
                {
                    int remainingRate = slot.rate;
                    for (int n = 0; n < slotAssignment.Length; n++)
                    {
                        if (slotAssignment.Length == 12 && remainingRate == 8 && slotAssignment[8] == null && slotAssignment[9] == null)
                        {
                            slotAssignment[8] = new EncounterSlot() { pokemonID = slot.pokemonID, pokemonForm = slot.pokemonForm, minLevel = slot.minLevel, maxLevel = slot.maxLevel, rate = 4 };
                            slotAssignment[9] = new EncounterSlot() { pokemonID = slot.pokemonID, pokemonForm = slot.pokemonForm, minLevel = slot.minLevel, maxLevel = slot.maxLevel, rate = 4 };
                            remainingRate = 0;
                            break;
                        }
                        if (slotAssignment[n] == null && remainingRate >= encounterRates[n])
                        {
                            slotAssignment[n] = new EncounterSlot() { pokemonID = slot.pokemonID, pokemonForm = slot.pokemonForm, minLevel = slot.minLevel, maxLevel = slot.maxLevel, rate = encounterRates[n] };
                            remainingRate -= encounterRates[n];
                            if (remainingRate == 0) break;
                        }
                    }
                    if (remainingRate != 0)
                    {
                        MessageBox.Show("Could not find a suitable distribution for the encounter rates");
                        //Reset the Encounter UI
                        SetupEncounterListBox(true);
                        return;
                    }
                }

                //Set the actual encounter set
                if (i <= 2) route.landSlots[i] = slotAssignment;
                else route.waterSlots[i - 3] = slotAssignment;
            }
            EncounterSlotsToGroups(route);

            //Apply the route to the rom data
            WriteEncounterData();

            //Reset the Encounter UI
            SetupEncounterListBox(true);
        }

        private void encounterPokemonNameDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (encounterPokemonNameDropDown.SelectedIndex > 0) encounterSlotToPokemonEditorButton.Enabled = true;
            else encounterSlotToPokemonEditorButton.Enabled = false;
        }

        private void encounterSlotToPokemonEditorButton_Click(object sender, EventArgs e)
        {
            pokemonEditorNameDropdown.SelectedIndex = Pokemon.nameListWithFormsReordered.IndexOf(Pokemon.nameListWithFormsReordered.FirstOrDefault(s => s.Contains(encounterPokemonNameDropDown.Text)));
            ToolTabs.SelectTab("pokemonEditorTab");
        }

        private void WriteEncounterData()
        {
            EncounterData route = routeEncounters[encounterRouteNameDropdown.SelectedIndex];
            int byteLoc = HexOffsets.encountersLocation + HexOffsets.encountersFirstEntry + routeEncounters.IndexOf(route) * HexOffsets.encountersEntrySize;

            int loc = byteLoc + 8;
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < route.landSlots[x].Length; y++)
                {
                    int id = route.landSlots[x][y].pokemonID + route.landSlots[x][y].pokemonForm * 2048;
                    byte[] b = BitConverter.GetBytes(id);
                    romData[loc] = b[0];
                    romData[loc + 1] = b[1];
                    romData[loc + 2] = BitConverter.GetBytes(route.landSlots[x][y].minLevel)[0];
                    romData[loc + 3] = BitConverter.GetBytes(route.landSlots[x][y].maxLevel)[0];
                    loc += 4;
                }
            }

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < route.waterSlots[x].Length; y++)
                {
                    int id = route.waterSlots[x][y].pokemonID + route.waterSlots[x][y].pokemonForm * 2048;
                    byte[] b = BitConverter.GetBytes(id);
                    romData[loc] = b[0];
                    romData[loc + 1] = b[1];
                    romData[loc + 2] = BitConverter.GetBytes(route.waterSlots[x][y].minLevel)[0];
                    romData[loc + 3] = BitConverter.GetBytes(route.waterSlots[x][y].maxLevel)[0];
                    loc += 4;
                }
            }
        }

        private void copyGrassToDarkGrassButton_Click(object sender, EventArgs e)
        {
            EncounterData route = routeEncounters[encounterRouteNameDropdown.SelectedIndex];
            route.groupedLandSlots[1].Clear();
            foreach (EncounterSlot slot in route.groupedLandSlots[0])
            {
                EncounterSlot s = slot.Clone();
                s.minLevel = Math.Min((int)Math.Ceiling(s.minLevel * 1.1f), 100);
                s.maxLevel = Math.Min((int)Math.Ceiling(s.maxLevel * 1.1f), 100);
                route.groupedLandSlots[1].Add(s);
            }

            SetupEncounterListBox(true);
        }

        private void copyEncounterGroupButton_Click(object sender, EventArgs e)
        {
            EncounterData route = routeEncounters[encounterRouteNameDropdown.SelectedIndex];
            //Identify the list to use
            int selectedType = 0;
            for (int i = 6; i >= 0; i--) if (((RadioButton)routeEncounterTypesPanel.Controls[i]).Checked) selectedType = 6 - i;
            List<EncounterSlot> slotList = new List<EncounterSlot>();
            if (selectedType <= 2) slotList = route.groupedLandSlots[selectedType];
            else slotList = route.groupedWaterSlots[selectedType - 3];

            encounterGroupClipboard = new List<EncounterSlot>();
            foreach (EncounterSlot slot in slotList) encounterGroupClipboard.Add(slot.Clone());
            pasteEncounterGroupButton.Enabled = true;
        }
        
        private void pasteEncounterGroupButton_Click(object sender, EventArgs e)
        {
            EncounterData route = routeEncounters[encounterRouteNameDropdown.SelectedIndex];
            //Identify the list to use
            int selectedType = 0;
            for (int i = 6; i >= 0; i--) if (((RadioButton)routeEncounterTypesPanel.Controls[i]).Checked) selectedType = 6 - i;
            List<EncounterSlot> slotList = new List<EncounterSlot>();
            if (selectedType <= 2) slotList = route.groupedLandSlots[selectedType];
            else slotList = route.groupedWaterSlots[selectedType - 3];

            slotList.Clear();
            foreach (EncounterSlot slot in encounterGroupClipboard) slotList.Add(slot.Clone());
            SetupEncounterListBox(true);
        }

        private static int CompareEncounterSlots(EncounterSlot e1, EncounterSlot e2) => Math.Sign(e2.rate - e1.rate);
        #endregion

        #region Overworld
        private void ReadOverworldData()
        {
            //Register Overworld Data
            int i = 0;
            int pointerLoc = HexOffsets.overworldsLocation + 32;
            while (i < HexOffsets.overworldsTotalBytes - HexOffsets.overworldsFirstEntry)
            {
                int byteLoc = HexOffsets.overworldsLocation + HexOffsets.overworldsFirstEntry + i;

                OverworldData o = new OverworldData();
                o.byteLocation = byteLoc;
                //Setup lists
                o.furniture = new List<OverworldFurniture>();
                o.NPCs = new List<OverworldNPC>();
                o.warps = new List<OverworldWarp>();
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
                        n.id = romData[byteLoc] + romData[byteLoc + 1] * 256;
                        n.spriteID = romData[byteLoc + 2] + romData[byteLoc + 3] * 256;
                        n.movementPermissions = romData[byteLoc + 4] + romData[byteLoc + 5] * 256;
                        n.flag = romData[byteLoc + 8] + romData[byteLoc + 9] * 256;
                        n.script = romData[byteLoc + 10] + romData[byteLoc + 11] * 256;
                        n.direction = romData[byteLoc + 12];
                        n.sightRange = romData[byteLoc + 14] + romData[byteLoc + 15] * 256;
                        n.xLeash = romData[byteLoc + 20] + romData[byteLoc + 21] * 256;
                        n.yLeash = romData[byteLoc + 22] + romData[byteLoc + 23] * 256;
                        n.x = romData[byteLoc + 28] + romData[byteLoc + 29] * 256;
                        n.y = romData[byteLoc + 30] + romData[byteLoc + 31] * 256;
                        n.z = romData[byteLoc + 34] + romData[byteLoc + 35] * 256;
                        byteLoc += 36;
                    }
                }
                i = BitConverter.ToInt32(romData.GetRange(pointerLoc, 4).ToArray(), 0);
                pointerLoc += 8;
                overworldData.Add(o);

                overworldEditorIndexDropdown.Items.Add("Overworld " + overworldEditorIndexDropdown.Items.Count);
            }
        }

        private void overworldEditorIndexDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            overworldNPCDropdown.Enabled = true;
            overworldNPCDropdown.Items.Clear();
            foreach (OverworldNPC n in overworldData[overworldEditorIndexDropdown.SelectedIndex].NPCs) overworldNPCDropdown.Items.Add(n.id);
            if (overworldNPCDropdown.Items.Count > 0) overworldNPCDropdown.SelectedIndex = 0;
            else overworldNPCDropdown.SelectedItem = null;
        }

        private void overworldNPCDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            OverworldNPC npc = overworldData[overworldEditorIndexDropdown.SelectedIndex].NPCs[overworldNPCDropdown.SelectedIndex];
            owNPCScriptNumberBox.Enabled = true;
            owNPCSpriteNumberBox.Enabled = true;
            owNPCPositionXNumberBox.Enabled = true;
            owNPCPositionYNumberBox.Enabled = true;

            owNPCScriptNumberBox.Value = npc.script;
            owNPCSpriteNumberBox.Value = npc.spriteID;
            owNPCPositionXNumberBox.Value = npc.x;
            owNPCPositionYNumberBox.Value = npc.y;
        }
        #endregion

        #region Fixing Stuff
        //Metadata fixing
        private void FixHexLocationHeaders(int startingNARCHeaderAddress, int newNARCAddressPlusSize)
        {
            int i = startingNARCHeaderAddress + 4;
            int oldSize = BitConverter.ToInt32(romData.GetRange(i, 4).ToArray(), 0);
            int changeBy = newNARCAddressPlusSize - oldSize;

            while (!(romData[i] == 0xFF && romData[i + 1] == 0xFF && romData[i + 2] == 0xFF && romData[i + 3] == 0xFF && romData[i + 4] == 0xFF && romData[i + 5] == 0xFF))
            {
                int num = BitConverter.ToInt32(romData.GetRange(i, 4).ToArray(), 0);
                num += changeBy;
                byte[] numAsBytes = BitConverter.GetBytes(num);
                for (int j = 0; j < 4; j++) romData[i + j] = numAsBytes[j];
                i += 4;
            }
        }

        private void FixPokeLearnsetNARCHeader()
        {
            byte[] num = BitConverter.GetBytes(HexOffsets.levelUpMovesTotalBytes);
            romData[HexOffsets.levelUpMovesLocation + 8] = num[0];
            romData[HexOffsets.levelUpMovesLocation + 9] = num[1];
            romData[HexOffsets.levelUpMovesLocation + 10] = num[2];
            romData[HexOffsets.levelUpMovesLocation + 11] = num[3];

            int addressValue = 4;
            int numEntries = BitConverter.ToInt16(romData.GetRange(HexOffsets.levelUpMovesLocation + 24, 2).ToArray(), 0);
            int learnsetByteLoc = HexOffsets.levelUpMovesLocation + HexOffsets.levelUpMovesFirstEntry;
            for (int i = 0; i < numEntries; i++)
            {
                int location = HexOffsets.levelUpMovesLocation + 32 + (i * 8);

                byte[] bytes = BitConverter.GetBytes(addressValue);
                romData[location] = bytes[0];
                romData[location + 1] = bytes[1];
                romData[location + 2] = bytes[2];
                romData[location + 3] = bytes[3];
                if (i != numEntries - 1)
                {
                    romData[location + 4] = bytes[0];
                    romData[location + 5] = bytes[1];
                    romData[location + 6] = bytes[2];
                    romData[location + 7] = bytes[3];
                }

                while (!(romData[learnsetByteLoc] == 0xFF && romData[learnsetByteLoc + 1] == 0xFF && romData[learnsetByteLoc + 2] == 0xFF && romData[learnsetByteLoc + 3] == 0xFF))
                {
                    learnsetByteLoc += 4;
                    addressValue += 4;
                }
                learnsetByteLoc += 4;
                addressValue += 4;
            }
        }

        private void FixTrPokeNARCHeader()
        {
            byte[] num = BitConverter.GetBytes(HexOffsets.trPokeTotalBytes);
            romData[HexOffsets.trPokeLocation + 8] = num[0];
            romData[HexOffsets.trPokeLocation + 9] = num[1];
            romData[HexOffsets.trPokeLocation + 10] = num[2];
            romData[HexOffsets.trPokeLocation + 11] = num[3];

            int addressValue = 8;
            for (int i = 1; i < trainerData.Count; i++)
            {
                int location = HexOffsets.trPokeLocation + 24 + (i * 8);
                int entrySize = 0;
                if (trainerData[i].pokeDataFormat == 0) entrySize = 8;
                if (trainerData[i].pokeDataFormat == 1) entrySize = 16;
                if (trainerData[i].pokeDataFormat == 2) entrySize = 10;
                if (trainerData[i].pokeDataFormat == 3) entrySize = 18;

                if (i == 1)
                {
                    byte[] bytes = BitConverter.GetBytes(addressValue);
                    for (int j = 0; j < 4; j++) romData[location + j + 4] = bytes[j];
                }
                else if (i == trainerData.Count - 1)
                {
                    byte[] bytes = BitConverter.GetBytes(addressValue);
                    for (int j = 0; j < 4; j++) romData[location + j] = bytes[j];
                }
                else if (weirdTrPokeHeaderIndexes.Contains(i))
                {
                    byte[] bytes = BitConverter.GetBytes(addressValue);
                    for (int j = 0; j < 4; j++) romData[location + j] = bytes[j];
                    addressValue += 2;
                    bytes = BitConverter.GetBytes(addressValue);
                    for (int j = 0; j < 4; j++) romData[location + j + 4] = bytes[j];
                }
                else
                {
                    byte[] bytes = BitConverter.GetBytes(addressValue);
                    for (int j = 0; j < 4; j++)
                    {
                        romData[location + j] = bytes[j];
                        romData[location + j + 4] = bytes[j];
                    }
                }

                addressValue += entrySize * trainerData[i].numPokemon;
            }
        }

        #endregion

        #region Handy Methods
        PokemonData PokemonDataFromID(int pokemonNumber)
        {
            foreach (PokemonData p in pokemonData) if (p.pokeNum == pokemonNumber) return p;
            return null;
        }
        #endregion

        private void WriteChangeLog()
        {
            SaveFileDialog prompt = new SaveFileDialog();
            prompt.Filter = "Text File|*.txt";

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string changeLogPath = prompt.FileName;

                StreamWriter fileStream = new StreamWriter(changeLogPath);
                fileStream.WriteLine("Pokemon:");

                foreach (PokemonData p in pokemonData)
                {
                    fileStream.WriteLine("\n--------------------------------------------------\n");
                    fileStream.WriteLine(pokemonEditorNameDropdown.Items[pokemonData.IndexOf(p)]);
                    fileStream.WriteLine("\nType: " + (p.type1 == p.type2 ? Pokemon.typeList[p.type1] : Pokemon.typeList[p.type1] + ", " + Pokemon.typeList[p.type2]));
                    fileStream.WriteLine("Abilities: " + (p.ability2 == 0 ? Pokemon.abilityList[p.ability1] : Pokemon.abilityList[p.ability1] + ", " + Pokemon.abilityList[p.ability2]) + "\nHidden Ability: " + Pokemon.abilityList[p.ability3]);
                    
                    //Stats
                    fileStream.Write("\nBase Hp:  " + p.baseHp.ToString("D") + '\t');
                    for (int i = 5; i < p.baseHp; i += 10) fileStream.Write('|');
                    fileStream.Write("\nBase Att: " + p.baseAtt.ToString("D") + '\t');
                    for (int i = 5; i < p.baseAtt; i += 10) fileStream.Write('|');
                    fileStream.Write("\nBase Def: " + p.baseDef.ToString("D") + '\t');
                    for (int i = 5; i < p.baseDef; i += 10) fileStream.Write('|');
                    fileStream.Write("\nBase SpA: " + p.baseSpA.ToString("D") + '\t');
                    for (int i = 5; i < p.baseSpA; i += 10) fileStream.Write('|');
                    fileStream.Write("\nBase SpD: " + p.baseSpD.ToString("D") + '\t');
                    for (int i = 5; i < p.baseSpD; i += 10) fileStream.Write('|');
                    fileStream.Write("\nBase Spe: " + p.baseSpe.ToString("D") + '\t');
                    for (int i = 5; i < p.baseSpe; i += 10) fileStream.Write('|');
                    fileStream.Write('\n');

                    //Evolutions
                    fileStream.Write("\nEvolutions: ");
                    bool anyEvos = false;
                    foreach ((int, int, int) ev in p.evolutions) if (ev.Item3 != 0)
                        {
                            anyEvos = true;
                            if (p.evolutions.IndexOf(ev) != 0) fileStream.Write(", ");
                            fileStream.Write(Pokemon.nameListWith0Index[ev.Item3] + (ev.Item3 != 0 ? " by " + Pokemon.evolutionMethodList[ev.Item1] : ""));

                            int conditionType = 0;
                            switch (ev.Item1)
                            {
                                //Level
                                case 4: conditionType = 1; break;
                                case 9: conditionType = 1; break;
                                case 10: conditionType = 1; break;
                                case 11: conditionType = 1; break;
                                case 12: conditionType = 1; break;
                                case 13: conditionType = 1; break;
                                case 14: conditionType = 1; break;
                                case 15: conditionType = 1; break;
                                case 23: conditionType = 1; break;
                                case 24: conditionType = 1; break;

                                //Item
                                case 6: conditionType = 2; break;
                                case 8: conditionType = 2; break;
                                case 17: conditionType = 2; break;
                                case 18: conditionType = 2; break;
                                case 19: conditionType = 2; break;
                                case 20: conditionType = 2; break;

                                //Beauty
                                case 16: conditionType = 3; break;

                                //Move
                                case 21: conditionType = 4; break;

                                //With Pokemon
                                case 22: conditionType = 5; break;

                                //No Condition
                                default: conditionType = 0; break;
                            }

                            if (conditionType == 1) fileStream.Write(" (Level " + ev.Item2.ToString("D") + ")");
                            if (conditionType == 2) fileStream.Write(" (" + Items.nameList[ev.Item2] + ")");
                            if (conditionType == 3) fileStream.Write(" " + ev.Item2.ToString("D"));
                            if (conditionType == 4) fileStream.Write(" (" + Pokemon.moveList[ev.Item2] + ")");
                            if (conditionType == 5) fileStream.Write(" (" + Pokemon.nameListWith0Index[ev.Item2] + ")");
                        }
                    if (!anyEvos) fileStream.Write("None\n");
                    else fileStream.Write("\n");

                    fileStream.Write("\nLevel up moves:\n");

                    //Moves
                    foreach ((int, int) move in p.learnset)
                    {
                        fileStream.WriteLine(" -" + Pokemon.moveList[move.Item1] + " at lv " + move.Item2.ToString("D"));
                    }
                }
                fileStream.Close();
            }
        }

        private void learnsetMoveDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void learnsetLevelNumberBox_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }

    public class PokemonData
    {
        public int byteLocation;
        public int pokeNum;

        public int baseHp;
        public int baseAtt;
        public int baseDef;
        public int baseSpA;
        public int baseSpD;
        public int baseSpe;

        public int type1;
        public int type2;

        public int ability1;
        public int ability2;
        public int ability3;

        public int levelRate;

        public int learnsetByteLocation;
        public List<(int, int)> learnset;

        public int evolutionByteLocation;
        public List<(int, int, int)> evolutions;

        public bool EqualTo(PokemonData other)
        {
            //Compare Learnsets
            if (learnset.Count != other.learnset.Count) return false;
            else foreach ((int, int) move in learnset) if (!other.learnset.Contains(move)) return false;

            //Compare Evolutions
            foreach ((int, int, int) evo in evolutions) if (!other.evolutions.Contains(evo)) return false;
            foreach ((int, int, int) evo in other.evolutions) if (!evolutions.Contains(evo)) return false;

            return baseHp == other.baseHp &&
                   baseAtt == other.baseAtt &&
                   baseDef == other.baseDef &&
                   baseSpA == other.baseSpA &&
                   baseSpD == other.baseSpD &&
                   baseSpe == other.baseSpe &&
                   type1 == other.type1 &&
                   type2 == other.type2 &&
                   ability1 == other.ability1 &&
                   ability2 == other.ability2 &&
                   ability3 == other.ability3;
        }
    }

    public class TrainerData
    {
        public int byteLocation;
        public int pokeByteLocation;
        public int numPokemon;
        public int pokeDataFormat;
        public int battleType;
    }

    public class EncounterData
    {
        public int byteLocation;
        public List<EncounterSlot[]> landSlots;
        public List<EncounterSlot[]> waterSlots;
        public List<List<EncounterSlot>> groupedLandSlots;
        public List<List<EncounterSlot>> groupedWaterSlots;
    }

    public class EncounterSlot
    {
        public int pokemonID;
        public int pokemonForm;
        public int minLevel;
        public int maxLevel;
        public int rate;

        public EncounterSlot Clone()
        {
            return new EncounterSlot() { pokemonID = pokemonID, pokemonForm = pokemonForm, minLevel = minLevel, maxLevel = maxLevel, rate = rate };
        }
    }

    public class OverworldData
    {
        public int byteLocation;
        public List<OverworldFurniture> furniture;
        public List<OverworldNPC> NPCs;
        public List<OverworldWarp> warps;
        public List<OverworldTrigger> triggers;
    }

    public class OverworldFurniture { }

    public class OverworldNPC
    {
        public int id;
        public int spriteID;
        public int x;
        public int y;
        public int z;
        public int direction;
        public int flag;
        public int script;
        public int sightRange;
        public int movementPermissions;
        public int xLeash;
        public int yLeash;
    }

    public class OverworldWarp { }

    public class OverworldTrigger { }
}
