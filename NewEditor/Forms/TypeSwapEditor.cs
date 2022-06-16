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

namespace NewEditor.Forms
{
    public partial class TypeSwapEditor : Form
    {
        TextNARC textNARC => MainEditor.textNarc;

        List<ComboBox> dropdowns;

        public TypeSwapEditor()
        {
            InitializeComponent();

            dropdowns = new List<ComboBox>();
            for (int i = 0; i < textNARC.textFiles[VersionConstants.BW2_TypeNameTextFileID].text.Count; i++)
            {
                ComboBox c = new ComboBox() { Location = new Point(100 + 200 * (i / 9), 157 + 40 * (i % 9)), Size = new Size(75, 24) };
                c.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_TypeNameTextFileID].text.ToArray());
                c.SelectedIndex = i;
                Label l = new Label() { Location = new Point(20 + 200 * (i / 9), 160 + 40 * (i % 9)), Text = textNARC.textFiles[VersionConstants.BW2_TypeNameTextFileID].text[i] + ":" };
                Controls.Add(c);
                Controls.Add(l);

                dropdowns.Add(c);
            }
        }

        private void RandomizeElements(object sender, EventArgs e)
        {
            List<int> nums = new List<int>();
            for (int i = 0; i < textNARC.textFiles[VersionConstants.BW2_TypeNameTextFileID].text.Count; i++) nums.Add(i);

            Random rand = new Random();

            foreach (ComboBox c in dropdowns)
            {
                int i = nums[rand.Next(nums.Count)];
                c.SelectedIndex = i;
                nums.Remove(i);
            }
        }

        private void OppositeElements(object sender, EventArgs e)
        {
            List<int> nums = new List<int>()
            {
                7, 8, 4, 5, 2, 3, 11, 0, 1, 14, 12, 6, 10, 16, 9, 15, 13
            };

            foreach (ComboBox c in dropdowns)
            {
                c.SelectedIndex = nums[dropdowns.IndexOf(c)];
            }
        }

        private void NormalizeElements(object sender, EventArgs e)
        {
            foreach (ComboBox c in dropdowns)
            {
                c.SelectedIndex = 0;
            }
        }

        private void CycleElements(object sender, EventArgs e)
        {
            for (int i = 0; i < dropdowns.Count; i++)
            {
                int num = (i + (int)numCyclesBox.Value) % dropdowns.Count;

                dropdowns[num].SelectedIndex = i;
            }
        }

        private void ApplyTypeSwap(object sender, EventArgs e)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < dropdowns.Count; i++) map.Add(i, dropdowns[i].SelectedIndex);

            foreach (MoveDataEntry move in MainEditor.moveDataNarc.moves)
            {
                if (map.ContainsKey(move.element)) move.element = (byte)map[move.element];
                move.ApplyData();
            }

            foreach (PokemonEntry pk in MainEditor.pokemonDataNarc.pokemon)
            {
                if (map.ContainsKey(pk.type1)) pk.type1 = (byte)map[pk.type1];
                if (map.ContainsKey(pk.type2)) pk.type2 = (byte)map[pk.type2];
                pk.ApplyData();
            }
        }
    }
}
