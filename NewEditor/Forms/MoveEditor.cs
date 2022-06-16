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
    public partial class MoveEditor : Form
    {
        TextNARC textNARC => MainEditor.textNarc;
        MoveDataNARC moveDataNARC => MainEditor.moveDataNarc;

        public MoveEditor()
        {
            InitializeComponent();

            moveNameDropdown.Items.AddRange(moveDataNARC.moves.ToArray());
        }
    }
}
