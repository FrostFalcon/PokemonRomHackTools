using NewEditor.Data;
using NewEditor.Data.NARCTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListBox;

namespace NewEditor.Forms
{
    public partial class PokemonEditor : Form
    {
        TextNARC textNARC => MainEditor.textNarc;
        PokemonDataNARC pokemonNARC => MainEditor.pokemonDataNarc;
        LearnsetNARC learnsetNarc => MainEditor.learnsetNarc;

        static List<TextValue> levelRates = new List<TextValue>()
        {
            new TextValue(1, "Erratic"),
            new TextValue(4, "Fast"),
            new TextValue(0, "Medium Fast"),
            new TextValue(3, "Medium Slow"),
            new TextValue(5, "Slow"),
            new TextValue(2, "Fluctuating"),
        };
        static List<TextValue> genderRatios = new List<TextValue>()
        {
            new TextValue(0, "100% male"),
            new TextValue(0x1F, "87.5% M / 12.5% F"),
            new TextValue(0x3F, "75% M / 25% F"),
            new TextValue(0x7F, "50% M / 50% F"),
            new TextValue(0xBF, "25% M / 75% F"),
            new TextValue(0xFE, "100% female"),
            new TextValue(0xFF, "Genderless"),
        };

        public List<LevelUpMoveSlot> learnsetClipboard;

        public PokemonEditor()
        {
            InitializeComponent();

            List<PokemonEntry> pk = new List<PokemonEntry>(pokemonNARC.pokemon);
            pk.Sort((p1, p2) => p1.nameID - p2.nameID);

            pokemonNameDropdown.Items.AddRange(pk.ToArray());

            pokemonTypeDropdown1.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_TypeNameTextFileID].text.ToArray());
            pokemonTypeDropdown2.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_TypeNameTextFileID].text.ToArray());

            pokeAbilityDropdown1.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_AbilityNameTextFileID].text.ToArray());
            pokeAbilityDropdown2.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_AbilityNameTextFileID].text.ToArray());
            pokeAbilityDropdown3.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_AbilityNameTextFileID].text.ToArray());

            pkLevelRateDropdown.Items.AddRange(levelRates.ToArray());
            pkGenderRatioDropdown.Items.AddRange(genderRatios.ToArray());

            heldItem1Dropdown.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_ItemNameTextFileID].text.ToArray());
            heldItem2Dropdown.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_ItemNameTextFileID].text.ToArray());
            heldItem3Dropdown.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_ItemNameTextFileID].text.ToArray());
            heldItem1Dropdown.Items[0] = "---";
            heldItem2Dropdown.Items[0] = "---";
            heldItem3Dropdown.Items[0] = "---";

            learnsetMoveDropdown.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_MoveNameTextFileID].text.ToArray());

            tmMovesListBox.Items.AddRange(VersionConstants.BW2_TMNames.ToArray());

            evolutionIntoDropdown.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_PokemonNameTextFileID].text.ToArray());
            evolutionMethodDropdown.Items.AddRange(VersionConstants.BW2_EvolutionMethodNames.ToArray());

            //Universal xp increase
            //foreach (PokemonEntry p in pokemonNARC.pokemon)
            //{
            //    List<string> excludePokemon = new List<string>()
            //    {
            //        "Snivy - 495",
            //        "Tepig - 498",
            //        "Oshawott - 501",
            //        "Chansey - 113",
            //        "Blissey - 242",
            //        "Audino - 531"
            //    };
            //    if (!excludePokemon.Contains(p.ToString()))
            //    {
            //        p.xpYield = (short)(p.xpYield * 1.3f);
            //        p.ApplyData();
            //    }
            //}
        }

        private void LoadPokemonIntoEditor(object sender, EventArgs e)
        {
            if (pokemonNameDropdown.SelectedItem is PokemonEntry p && p.nameID > 0 && p.bytes.Length == 76)
            {
                pokemonBaseHpNumberBox.Value = p.baseHP;
                pokemonBaseAttackNumberBox.Value = p.baseAttack;
                pokemonBaseDefenseNumberBox.Value = p.baseDefense;
                pokemonBaseSpAttNumberBox.Value = p.baseSpAtt;
                pokemonBaseSpDefNumberBox.Value = p.baseSpDef;
                pokemonBaseSpeedNumberBox.Value = p.baseSpeed;

                pokemonTypeDropdown1.SelectedIndex = p.type1;
                pokemonTypeDropdown2.SelectedIndex = p.type2;

                pokeAbilityDropdown1.SelectedIndex = p.ability1;
                pokeAbilityDropdown2.SelectedIndex = p.ability2;
                pokeAbilityDropdown3.SelectedIndex = p.ability3;

                pkLevelRateDropdown.SelectedItem = levelRates.First(l => l.hexID == p.levelRate);
                pkGenderRatioDropdown.SelectedItem = genderRatios.First(g => g.hexID == p.genderRatio);
                xpYieldNumberBox.Value = p.xpYield;

                heldItem1Dropdown.SelectedIndex = p.heldItem1;
                heldItem2Dropdown.SelectedIndex = p.heldItem2;
                heldItem3Dropdown.SelectedIndex = p.heldItem3;

                SetupPokemonLearnsetList();

                baseStatsGroup.Enabled = true;
                miscStatsGroup.Enabled = true;
                applyPokemonButton.Enabled = true;
                levelUpMovesGroup.Enabled = true;
                tmMovesGroupBox.Enabled = true;
                evolutionsGroupBox.Enabled = true;

                for (int i = 0; i < p.TMs.Length; i++) tmMovesListBox.SetItemChecked(i, p.TMs[i]);

                evolutionsListBox.Items.Clear();
                for (int i = 0; i < 7; i++) evolutionsListBox.Items.Add(p.evolutions.methods[i]);
                evolutionsListBox.SelectedIndex = 0;
            }
            else
            {
                baseStatsGroup.Enabled = false;
                miscStatsGroup.Enabled = false;
                applyPokemonButton.Enabled = false;
                levelUpMovesGroup.Enabled = false;
                tmMovesGroupBox.Enabled = false;
                evolutionsGroupBox.Enabled = false;
            }
        }

        private void ApplyPokemon(object sender, EventArgs e)
        {
            if (pokemonNameDropdown.SelectedItem is PokemonEntry p)
            {
                p.baseHP = (byte)pokemonBaseHpNumberBox.Value;
                p.baseAttack = (byte)pokemonBaseAttackNumberBox.Value;
                p.baseDefense = (byte)pokemonBaseDefenseNumberBox.Value;
                p.baseSpAtt = (byte)pokemonBaseSpAttNumberBox.Value;
                p.baseSpDef = (byte)pokemonBaseSpDefNumberBox.Value;
                p.baseSpeed = (byte)pokemonBaseSpeedNumberBox.Value;

                p.type1 = (byte)pokemonTypeDropdown1.SelectedIndex;
                p.type2 = (byte)pokemonTypeDropdown2.SelectedIndex;

                p.ability1 = (byte)pokeAbilityDropdown1.SelectedIndex;
                p.ability2 = (byte)pokeAbilityDropdown2.SelectedIndex;
                p.ability3 = (byte)pokeAbilityDropdown3.SelectedIndex;

                p.levelRate = ((TextValue)pkLevelRateDropdown.SelectedItem).hexID;
                p.genderRatio = ((TextValue)pkGenderRatioDropdown.SelectedItem).hexID;
                p.xpYield = (short)xpYieldNumberBox.Value;

                p.heldItem1 = (short)heldItem1Dropdown.SelectedIndex;
                p.heldItem2 = (short)heldItem2Dropdown.SelectedIndex;
                p.heldItem3 = (short)heldItem3Dropdown.SelectedIndex;

                p.levelUpMoves.moves = new List<LevelUpMoveSlot>();
                foreach (object o in learnsetListBox.Items) if (o is LevelUpMoveSlot move)
                    {
                        p.levelUpMoves.moves.Add(move);
                    }
                p.levelUpMoves.moves.Sort(CompareLearnsetMoves);

                for (int i = 0; i < p.TMs.Length; i++) p.TMs[i] = tmMovesListBox.GetItemChecked(i);

                for (int i = 0; i < 7; i++) p.evolutions.methods[i] = (EvolutionMethod)evolutionsListBox.Items[i];

                p.ApplyData();

                SetupPokemonLearnsetList();
            }
        }

        private void SetupPokemonLearnsetList()
        {
            if (pokemonNameDropdown.SelectedItem is PokemonEntry p)
            {
                learnsetListBox.Items.Clear();
                foreach (LevelUpMoveSlot move in p.levelUpMoves.moves)
                {
                    learnsetListBox.Items.Add(move);
                }
                learnsetListBox.SelectedIndex = 0;
            }
        }

        private void UpdateBaseStatTotal(object sender, EventArgs e)
        {
            pokemonBSTText.Text = "Total: " + (pokemonBaseHpNumberBox.Value + pokemonBaseAttackNumberBox.Value + pokemonBaseDefenseNumberBox.Value
                + pokemonBaseSpAttNumberBox.Value + pokemonBaseSpDefNumberBox.Value + pokemonBaseSpeedNumberBox.Value);
        }

        private void CopyLearnset(object sender, EventArgs e)
        {
            learnsetClipboard = new List<LevelUpMoveSlot>();
            foreach (LevelUpMoveSlot move in learnsetListBox.Items)
            {
                learnsetClipboard.Add(move);
            }
            pasteLearnsetButton.Enabled = true;
        }

        private void PasteLearnset(object sender, EventArgs e)
        {
            learnsetListBox.Items.Clear();
            foreach (LevelUpMoveSlot move in learnsetClipboard)
            {
                learnsetListBox.Items.Add(move);
            }
        }

        private void AddToLearnset(object sender, EventArgs e)
        {
            if (pokemonNameDropdown.SelectedItem is PokemonEntry p)
            {
                learnsetListBox.Items.Add(new LevelUpMoveSlot(1, 1));
                learnsetListBox.SelectedIndex = learnsetListBox.Items.Count - 1;
            }
        }

        private void RemoveFromLearnset(object sender, EventArgs e)
        {
            if (pokemonNameDropdown.SelectedItem is PokemonEntry p)
            {
                int storeIndex = learnsetListBox.SelectedIndex;
                if (learnsetListBox.Items.Count > 1) learnsetListBox.Items.Remove((LevelUpMoveSlot)learnsetListBox.SelectedItem);

                learnsetListBox.SelectedIndex = Math.Min(storeIndex, learnsetListBox.Items.Count - 1);
            }
        }

        private void ChangeMoveSlot(object sender, EventArgs e)
        {
            if (learnsetListBox.SelectedItem is LevelUpMoveSlot l)
            {
                learnsetMoveDropdown.SelectedIndex = l.moveID;
                learnsetLevelNumberBox.Value = l.level;
            }
        }

        private void ApplyMoveSlot(object sender, EventArgs e)
        {
            if (learnsetListBox.SelectedItem is LevelUpMoveSlot)
            {
                learnsetListBox.Items[learnsetListBox.SelectedIndex] = new LevelUpMoveSlot((short)learnsetMoveDropdown.SelectedIndex, (short)learnsetLevelNumberBox.Value);
            }
        }

        private void LoadEvolutionMethod(object sender, EventArgs ev)
        {
            if (evolutionsListBox.SelectedItem is EvolutionMethod e)
            {
                evolutionIntoDropdown.SelectedIndex = e.newPokemonID;
                evolutionMethodDropdown.SelectedIndex = e.method;

                evolutionConditionDropdown.Items.Clear();
                int conditionType = EvolutionConditionType(e.method);

                if (conditionType == 0) evolutionConditionDropdown.Items.Add("---");
                if (conditionType == 1) for (int i = 0; i <= 100; i++) evolutionConditionDropdown.Items.Add(i);
                if (conditionType == 2) evolutionConditionDropdown.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_ItemNameTextFileID].text.ToArray());
                if (conditionType == 3) for (int i = 0; i <= 255; i++) evolutionConditionDropdown.Items.Add(i);
                if (conditionType == 4) evolutionConditionDropdown.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_MoveNameTextFileID].text.ToArray());
                if (conditionType == 5) evolutionConditionDropdown.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_PokemonNameTextFileID].text.ToArray());

                evolutionConditionDropdown.SelectedIndex = e.condition;
            }
        }

        private void ApplyEvolution(object sender, EventArgs ev)
        {
            evolutionsListBox.Items[evolutionsListBox.SelectedIndex] = new EvolutionMethod((short)evolutionMethodDropdown.SelectedIndex,
                (short)evolutionConditionDropdown.SelectedIndex,
                (short)evolutionIntoDropdown.SelectedIndex);
        }

        private void ChangeEvolutionMethod(object sender, EventArgs e)
        {
            int saveIndex = evolutionConditionDropdown.SelectedIndex;
            evolutionConditionDropdown.Items.Clear();
            int conditionType = EvolutionConditionType(evolutionMethodDropdown.SelectedIndex);

            if (conditionType == 0) evolutionConditionDropdown.Items.Add("---");
            if (conditionType == 1) for (int i = 0; i <= 100; i++) evolutionConditionDropdown.Items.Add(i);
            if (conditionType == 2) evolutionConditionDropdown.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_ItemNameTextFileID].text.ToArray());
            if (conditionType == 3) for (int i = 0; i <= 255; i++) evolutionConditionDropdown.Items.Add(i);
            if (conditionType == 4) evolutionConditionDropdown.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_MoveNameTextFileID].text.ToArray());
            if (conditionType == 5) evolutionConditionDropdown.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_PokemonNameTextFileID].text.ToArray());

            if (saveIndex < evolutionConditionDropdown.Items.Count) evolutionConditionDropdown.SelectedIndex = saveIndex;
            else evolutionConditionDropdown.SelectedIndex = 0;
        }

        private static int CompareLearnsetMoves(LevelUpMoveSlot m1, LevelUpMoveSlot m2) => m1.level == m2.level ? Math.Sign(m1.moveID - m2.moveID) : Math.Sign(m1.level - m2.level);
    
        private int EvolutionConditionType(int method)
        {
            int conditionType = 0;
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

            return conditionType;
        }
    }

    class TextValue
    {
        public byte hexID;
        public string name;

        public TextValue(byte hexID, string name)
        {
            this.hexID = hexID;
            this.name = name;
        }

        public override string ToString() => name;
    }
}