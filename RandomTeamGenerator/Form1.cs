using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomTeamGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            bool hasSurf = false;
            bool duplicate = false;

            do
            {
                duplicate = false;

                //Randomize
                starterText.Text = PokemonLists.starters[random.Next(PokemonLists.starters.Count)];

                pk1Text.Text = PokemonLists.gym1[random.Next(PokemonLists.gym1.Count)];

                List<string> p2List = new List<string>();
                p2List.AddRange(PokemonLists.gym2);
                p2List.AddRange(PokemonLists.gym3);
                pk2Text.Text = p2List[random.Next(p2List.Count)];

                List<string> p3List = new List<string>();
                p3List.AddRange(PokemonLists.gym3);
                p3List.AddRange(PokemonLists.gym4);
                pk3Text.Text = p3List[random.Next(p3List.Count)];

                List<string> p4List = new List<string>();
                p4List.AddRange(PokemonLists.gym5);
                p4List.AddRange(PokemonLists.gym6);
                pk4Text.Text = p4List[random.Next(p4List.Count)];

                pk5Text.Text = PokemonLists.gym7[random.Next(PokemonLists.gym7.Count)];

                List<string> p6List = new List<string>();
                p6List.AddRange(PokemonLists.gym8);
                p6List.AddRange(PokemonLists.elite4);
                pk6Text.Text = p6List[random.Next(p6List.Count)];

                //Check overlapping pools
                if (pk2Text.Text == pk3Text.Text) duplicate = true;

                //Randomize Eevee
                if (pk2Text.Text == "Eevee") pk2Text.Text = PokemonLists.eevee[random.Next(PokemonLists.eevee.Count)];
                if (pk3Text.Text == "Eevee") pk3Text.Text = PokemonLists.eevee[random.Next(PokemonLists.eevee.Count)];

                //Check for surf
                if (PokemonLists.pkWithSurf.Contains(starterText.Text) || PokemonLists.pkWithSurf.Contains(pk1Text.Text) || PokemonLists.pkWithSurf.Contains(pk2Text.Text)
                    || PokemonLists.pkWithSurf.Contains(pk3Text.Text) || PokemonLists.pkWithSurf.Contains(pk4Text.Text))
                    hasSurf = true;
                else hasSurf = false;

                Label[] texts = new Label[] { starterText, pk1Text, pk2Text, pk3Text, pk4Text, pk5Text, pk6Text };
                foreach (Label label in texts)
                {
                    if (label.Text.Contains('/'))
                    {
                        if (b2Button.Checked) label.Text = label.Text.Remove(label.Text.IndexOf('/') - 1);
                        else label.Text = label.Text.Remove(0, label.Text.IndexOf('/') + 2);
                    }
                }

            } while (!hasSurf || duplicate);
        }
    }
}
