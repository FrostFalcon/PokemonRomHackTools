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
    public partial class ScriptEditor : Form
    {
        public ScriptEditor()
        {
            InitializeComponent();

            for (int i = 0; i < MainEditor.scriptNarc.scriptFiles.Count; i++) scriptFileDropdown.Items.Add(i);

            for (int i = 0; i < CommandReference.commandList.Count; i++) commandTypeDropdown.Items.Add(CommandReference.commandList[i].name);
        }

        private void LoadScriptFile(object sender, EventArgs e)
        {
            if (scriptFileDropdown.SelectedIndex >= 0 && scriptFileDropdown.SelectedIndex < scriptFileDropdown.Items.Count)
            {
                ScriptFile sf = MainEditor.scriptNarc.scriptFiles[scriptFileDropdown.SelectedIndex];

                if (sf.valid && sf.sequences != null && sf.sequences.Count > 0)
                {
                    sequenceIDNumberBox.Value = 0;
                    sequenceIDNumberBox.Maximum = sf.sequences.Count - 1;
                    sequenceCountLabel.Text = "/ " + (sf.sequences.Count - 1).ToString();

                    LoadSequence(sender, e);

                    sequenceIDNumberBox.Enabled = true;
                    commandsListBox.Enabled = true;
                    commandTypeDropdown.Enabled = true;
                    saveButton.Enabled = true;
                }
                else
                {
                    sequenceIDNumberBox.Enabled = false;
                    commandsListBox.Enabled = false;
                    commandTypeDropdown.Enabled = false;
                    saveButton.Enabled = false;
                }
            }
        }

        private void LoadSequence(object sender, EventArgs e)
        {
            if (scriptFileDropdown.SelectedIndex >= 0 && scriptFileDropdown.SelectedIndex < scriptFileDropdown.Items.Count)
            {
                ScriptFile sf = MainEditor.scriptNarc.scriptFiles[scriptFileDropdown.SelectedIndex];
                ScriptSequence seq = sf.sequences[(int)sequenceIDNumberBox.Value];

                commandsListBox.Items.Clear();
                foreach (ScriptCommand c in seq.commands) commandsListBox.Items.Add(c);
            }
        }

        private void ChangeSelectedListboxCommand(object sender, EventArgs e)
        {

        }

        private void DoubleClickListBox(object sender, EventArgs e)
        {
            if (commandsListBox.SelectedItem is ScriptCommand c)
            {
                //Jump
                int jumpDistance = -1;
                if (c.commandID == 0x1E) jumpDistance = c.parameters[0];
                if (c.commandID == 0x1F) jumpDistance = c.parameters[1];

                if (jumpDistance != -1)
                {
                    int listPos = commandsListBox.SelectedIndex;
                    int jumpPos = 0;
                    while (listPos < commandsListBox.Items.Count - 1 && jumpPos <= jumpDistance)
                    {
                        listPos++;
                        jumpPos += ((ScriptCommand)commandsListBox.Items[listPos]).ByteLength;
                    }

                    if (listPos < commandsListBox.Items.Count)
                    {
                        commandsListBox.SelectedIndex = listPos;
                    }
                }
            }
        }

        private void SaveScriptFile(object sender, EventArgs e)
        {
            if (scriptFileDropdown.SelectedIndex >= 0 && scriptFileDropdown.SelectedIndex < scriptFileDropdown.Items.Count)
            {
                ScriptFile sf = MainEditor.scriptNarc.scriptFiles[scriptFileDropdown.SelectedIndex];

                sf.ApplyData();
            }
        }
    }
}
