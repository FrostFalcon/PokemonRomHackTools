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
    public partial class OverworldEditor : Form
    {
        TextNARC textNARC => MainEditor.textNarc;
        ZoneDataNARC zoneNARC => MainEditor.zoneDataNarc;
        MapMatrixNARC mapMatrixNarc => MainEditor.mapMatrixNarc;

        public OverworldEditor()
        {
            InitializeComponent();

            zoneIdDropdown.Items.AddRange(zoneNARC.zones.ToArray());
            mapNameDropdown.Items.AddRange(textNARC.textFiles[VersionConstants.BW2_ZoneNameTextFileID].text.ToArray());
        }

        private void LoadZoneIntoEditor(object sender, EventArgs e)
        {
            if (zoneIdDropdown.SelectedItem is ZoneDataEntry z && z.bytes.Length == 48)
            {
                mapTypeNumberBox.Value = z.mapType;
                mapMatrixNumberBox.Value = z.matrix;
                scriptFileNumberBox.Value = z.scriptFile;
                textFileNumberBox.Value = z.storyTextFile;
                encounterFileNumberBox.Value = z.encounterFile;
                mapIDNumberBox.Value = z.mapId;
                parentMapIDNumberBox.Value = z.parentMapId;
                mapNameDropdown.SelectedIndex = z.nameId;

                mapTypeNumberBox.Enabled = true;
                mapMatrixNumberBox.Enabled = true;
                scriptFileNumberBox.Enabled = true;
                textFileNumberBox.Enabled = true;
                encounterFileNumberBox.Enabled = true;
                mapIDNumberBox.Enabled = true;
                parentMapIDNumberBox.Enabled = true;
                mapNameDropdown.Enabled = true;
                applyZoneButton.Enabled = true;
            }
            else
            {
                mapTypeNumberBox.Enabled = false;
                mapMatrixNumberBox.Enabled = false;
                scriptFileNumberBox.Enabled = false;
                textFileNumberBox.Enabled = false;
                encounterFileNumberBox.Enabled = false;
                mapIDNumberBox.Enabled = false;
                parentMapIDNumberBox.Enabled = false;
                mapNameDropdown.Enabled = false;
                applyZoneButton.Enabled = false;
            }
        }

        private void ApplyZoneData(object sender, EventArgs e)
        {
            if (zoneIdDropdown.SelectedItem is ZoneDataEntry z && z.bytes.Length == 48)
            {
                z.mapType = (byte)mapTypeNumberBox.Value;
                z.matrix = (short)mapMatrixNumberBox.Value;
                z.scriptFile = (short)scriptFileNumberBox.Value;
                z.storyTextFile = (short)textFileNumberBox.Value;
                z.encounterFile = (byte)encounterFileNumberBox.Value;
                z.mapId = (short)mapIDNumberBox.Value;
                z.parentMapId = (short)parentMapIDNumberBox.Value;
                z.nameId = (byte)mapNameDropdown.SelectedIndex;
            }
        }

        private void openTextFileButton_Click(object sender, EventArgs e)
        {
            MainEditor.OpenTextViewer(sender, e);
            if (MainEditor.textViewer != null)
            {
                MainEditor.textViewer.storyTextRadioButton.Checked = true;
                if (textFileNumberBox.Value > 0 && textFileNumberBox.Value < MainEditor.textViewer.fileNumComboBox.Items.Count) MainEditor.textViewer.fileNumComboBox.SelectedIndex = (int)textFileNumberBox.Value;
                else MessageBox.Show("Could not find the text file by index");
            }
        }

        private void openScriptFileButton_Click(object sender, EventArgs e)
        {
            MainEditor.OpenScriptEditor(sender, e);
            if (MainEditor.scriptEditor != null)
            {
                if (scriptFileNumberBox.Value > 0 && scriptFileNumberBox.Value < MainEditor.scriptEditor.scriptFileDropdown.Items.Count) MainEditor.scriptEditor.scriptFileDropdown.SelectedIndex = (int)scriptFileNumberBox.Value;
                else MessageBox.Show("Could not find the script file by index");
            }
        }
    }
}
