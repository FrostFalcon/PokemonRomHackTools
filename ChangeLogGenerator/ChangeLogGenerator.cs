using Editor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Editor;
using System.IO;

namespace ChangeLogGenerator
{
    public partial class ChangeLogGenerator : Form
    {
        string originalRomPath = "";
        List<byte> originalRomData;
        public List<TrainerData> originalTrainerData = new List<TrainerData>();
        public List<PokemonData> originalPokemonData = new List<PokemonData>();
        public List<EncounterData> originalRouteEncounters = new List<EncounterData>();

        string newRomPath = "";
        List<byte> newRomData;
        public List<TrainerData> newTrainerData = new List<TrainerData>();
        public List<PokemonData> newPokemonData = new List<PokemonData>();
        public List<EncounterData> newRouteEncounters = new List<EncounterData>();

        public ChangeLogGenerator()
        {
            InitializeComponent();
        }
        
        private void selectOriginalRomButton_Click(object sender, EventArgs e)
        {
            using (var editor = new MainEditor())
            {
                editor.LoadRom(this, null);
                originalRomPath = editor.loadedRomPath;
                originalRomData = editor.romData;
                originalTrainerData = editor.trainerData;
                originalPokemonData = editor.pokemonData;
                originalRouteEncounters = editor.routeEncounters;

                originalRomPathText.Text = "-" + editor.loadedRomPath.Substring(editor.loadedRomPath.LastIndexOf('\\') + 1, editor.loadedRomPath.Length - (editor.loadedRomPath.LastIndexOf('\\') + 1));
            }

            if (newRomData != null) generateChangeLogButton.Enabled = true;
        }

        private void selectNewRomButton_Click(object sender, EventArgs e)
        {
            using (var editor = new MainEditor())
            {
                editor.LoadRom(this, null);
                newRomPath = editor.loadedRomPath;
                newRomData = editor.romData;
                newTrainerData = editor.trainerData;
                newPokemonData = editor.pokemonData;
                newRouteEncounters = editor.routeEncounters;

                newRomPathText.Text = "-" + editor.loadedRomPath.Substring(editor.loadedRomPath.LastIndexOf('\\') + 1, editor.loadedRomPath.Length - (editor.loadedRomPath.LastIndexOf('\\') + 1));
            }

            if (originalRomData != null) generateChangeLogButton.Enabled = true;
        }

        private void generateChangeLogButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog prompt = new SaveFileDialog();
            prompt.Filter = "Text File|*.txt";

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string changeLogPath = prompt.FileName;

                StreamWriter fileStream = new StreamWriter(changeLogPath);

                if (includePokemonCheckBox.Checked)
                {
                    fileStream.WriteLine("Pokemon:");
                    foreach (PokemonData poke in newPokemonData)
                    {
                        PokemonData oldPoke = originalPokemonData[newPokemonData.IndexOf(poke)];

                        if (!poke.EqualTo(oldPoke))
                        {
                            fileStream.WriteLine("\n--------------------------------------------------\n");
                            fileStream.WriteLine(Pokemon.nameListWithFormsReordered[newPokemonData.IndexOf(poke)]);
                            //Type
                            if (poke.type1 != oldPoke.type1 || poke.type2 != oldPoke.type2)
                                fileStream.WriteLine("\nType: " + (oldPoke.type1 == oldPoke.type2 ? Pokemon.typeList[oldPoke.type1] : Pokemon.typeList[oldPoke.type1] + ", " + Pokemon.typeList[oldPoke.type2]) + " -> " + (poke.type1 == poke.type2 ? Pokemon.typeList[poke.type1] : Pokemon.typeList[poke.type1] + ", " + Pokemon.typeList[poke.type2]));
                            //Ability
                            if (poke.ability1 != oldPoke.ability1 || poke.ability2 != oldPoke.ability2 || poke.ability3 != oldPoke.ability3)
                                fileStream.WriteLine("Abilities: " + (oldPoke.ability2 == 0 ? Pokemon.abilityList[oldPoke.ability1] : Pokemon.abilityList[oldPoke.ability1] + ", " + Pokemon.abilityList[oldPoke.ability2]) + " -> " + (poke.ability2 == 0 ? Pokemon.abilityList[poke.ability1] : Pokemon.abilityList[poke.ability1] + ", " + Pokemon.abilityList[poke.ability2]) + "\nHidden Ability: " + Pokemon.abilityList[oldPoke.ability3] + " -> " + Pokemon.abilityList[poke.ability3]);
                            fileStream.Write('\n');
                            //Stats
                            if (poke.baseHp != oldPoke.baseHp) fileStream.Write("Base Hp: " + oldPoke.baseHp + " -> " + poke.baseHp + '\n');
                            if (poke.baseAtt != oldPoke.baseAtt) fileStream.Write("Base Att: " + oldPoke.baseAtt + " -> " + poke.baseAtt + '\n');
                            if (poke.baseDef != oldPoke.baseDef) fileStream.Write("Base Def: " + oldPoke.baseDef + " -> " + poke.baseDef + '\n');
                            if (poke.baseSpA != oldPoke.baseSpA) fileStream.Write("Base SpA: " + oldPoke.baseSpA + " -> " + poke.baseSpA + '\n');
                            if (poke.baseSpD != oldPoke.baseSpD) fileStream.Write("Base SpD: " + oldPoke.baseSpD + " -> " + poke.baseSpD + '\n');
                            if (poke.baseSpe != oldPoke.baseSpe) fileStream.Write("Base Spe: " + oldPoke.baseSpe + " -> " + poke.baseSpe + '\n');

                            //Evolutions
                            List<int> differentEvos = new List<int>();
                            foreach ((int, int, int) ev in poke.evolutions)
                            {
                                if (oldPoke.evolutions[poke.evolutions.IndexOf(ev)] != ev) differentEvos.Add(poke.evolutions.IndexOf(ev));
                            }
                            if (differentEvos.Count > 0)
                            {
                                fileStream.Write("Evolutions:\n");
                                foreach (int i in differentEvos)
                                {
                                    for (int n = 0; n < 2; n++)
                                    {
                                        (int, int, int) ev = n == 0 ? oldPoke.evolutions[i] : poke.evolutions[i];
                                        if (n == 0 || poke.evolutions[i].Item1 != oldPoke.evolutions[i].Item1 || poke.evolutions[i].Item3 != oldPoke.evolutions[i].Item3) fileStream.Write(" " + Pokemon.nameListWith0Index[ev.Item3] + (ev.Item3 != 0 ? " by " + Pokemon.evolutionMethodList[ev.Item1] : ""));

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

                                        if (n == 0) fileStream.Write(" ->");
                                        else fileStream.Write('\n');
                                    }
                                }
                            }

                            //Convert learnset lists to dictionaries
                            Dictionary<int, List<int>> newLSByMoveID = new Dictionary<int, List<int>>();
                            foreach ((int, int) move in poke.learnset)
                            {
                                if (newLSByMoveID.ContainsKey(move.Item1)) newLSByMoveID[move.Item1].Add(move.Item2);
                                else newLSByMoveID.Add(move.Item1, new List<int>() { move.Item2 });
                            }
                            Dictionary<int, List<int>> oldLSByMoveID = new Dictionary<int, List<int>>();
                            foreach ((int, int) move in oldPoke.learnset)
                            {
                                if (oldLSByMoveID.ContainsKey(move.Item1)) oldLSByMoveID[move.Item1].Add(move.Item2);
                                else oldLSByMoveID.Add(move.Item1, new List<int>() { move.Item2 });
                            }

                            Dictionary<int, List<int>> addedMoves = new Dictionary<int, List<int>>();
                            Dictionary<int, List<int>> removedMoves = new Dictionary<int, List<int>>();
                            List<int> changedMoves = new List<int>();

                            //Find added moves
                            foreach (KeyValuePair<int, List<int>> move in newLSByMoveID) if (!oldLSByMoveID.ContainsKey(move.Key)) addedMoves.Add(move.Key, move.Value);
                            //Find removed moves
                            foreach (KeyValuePair<int, List<int>> move in oldLSByMoveID) if (!newLSByMoveID.ContainsKey(move.Key)) removedMoves.Add(move.Key, move.Value);
                            //Find moves with changed levels
                            foreach (KeyValuePair<int, List<int>> move in newLSByMoveID) if (oldLSByMoveID.ContainsKey(move.Key) && move.Value.Except(oldLSByMoveID[move.Key]).Count() > 0) changedMoves.Add(move.Key);

                            if (addedMoves.Count > 0 || removedMoves.Count > 0 || changedMoves.Count > 0) fileStream.Write("\nLevel up moves:\n");

                            //Moves
                            foreach (KeyValuePair<int, List<int>> move in removedMoves)
                            {
                                fileStream.Write(" - Removed " + Pokemon.moveList[move.Key] + " at lv ");
                                foreach (int i in move.Value)
                                {
                                    if (move.Value.IndexOf(i) == 0) fileStream.Write(i);
                                    else fileStream.Write(", " + i);
                                }
                                fileStream.Write('\n');
                            }
                            foreach (KeyValuePair<int, List<int>> move in addedMoves)
                            {
                                fileStream.Write(" + Added " + Pokemon.moveList[move.Key] + " at lv ");
                                foreach (int i in move.Value)
                                {
                                    if (move.Value.IndexOf(i) == 0) fileStream.Write(i);
                                    else fileStream.Write(", " + i);
                                }
                                fileStream.Write('\n');
                            }
                            foreach (int move in changedMoves)
                            {
                                fileStream.Write(" = Moved " + Pokemon.moveList[move] + " from lv ");
                                foreach (int i in oldLSByMoveID[move])
                                {
                                    if (oldLSByMoveID[move].IndexOf(i) == 0) fileStream.Write(i);
                                    else fileStream.Write(", " + i);
                                }
                                fileStream.Write(" to lv ");
                                foreach (int i in newLSByMoveID[move])
                                {
                                    if (newLSByMoveID[move].IndexOf(i) == 0) fileStream.Write(i);
                                    else fileStream.Write(", " + i);
                                }
                                fileStream.Write('\n');
                            }
                        }
                    }
                }
                fileStream.Close();
            }
        }
    }
}
