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
    public partial class TextViewer : Form
    {
        TextNARC activeNarc = MainEditor.textNarc;

        public TextViewer()
        {
            InitializeComponent();

            ChangeNarc(null, null);
        }

        private void ChangeNarc(object sender, EventArgs e)
        {
            activeNarc = miscTextRadioButton.Checked ? MainEditor.textNarc : MainEditor.storyTextNarc;

            searchTextBox.Text = "";

            fileNumComboBox.Items.Clear();

            for (int i = 0; i < activeNarc.textFiles.Count; i++) fileNumComboBox.Items.Add(i);
            fileNumComboBox.SelectedIndex = 0;

            LoadTextbox(sender, e);
        }

        private void LoadTextbox(object sender, EventArgs e)
        {
            int fileID;
            if (int.TryParse(fileNumComboBox.Text, out fileID) && fileID >= 0 && activeNarc != null && fileID < activeNarc.textFiles.Count)
            {
                StringBuilder text = new StringBuilder();
                foreach (string str in activeNarc.textFiles[fileID].text) text.Append(str + '\n');
                if (text.Length > 0) text.Remove(text.Length - 1, 1);
                textBoxDisplay.Text = text.ToString();
            }
        }

        private void FilterFiles(object sender, EventArgs e)
        {
            if (activeNarc != null)
            {
                fileNumComboBox.Items.Clear();

                for (int i = 0; i < activeNarc.textFiles.Count; i++)
                {
                    bool search = false;

                    foreach (string str in activeNarc.textFiles[i].text) if (str.Contains(searchTextBox.Text))
                        {
                            search = true;
                            break;
                        }

                    if (search) fileNumComboBox.Items.Add(i);
                }
            }

            if (fileNumComboBox.Items.Count > 0) fileNumComboBox.SelectedIndex = 0;
            else
            {
                textBoxDisplay.Text = "";
                fileNumComboBox.Items.Add("");
                fileNumComboBox.SelectedIndex = 0;
            }
        }
    }
}
