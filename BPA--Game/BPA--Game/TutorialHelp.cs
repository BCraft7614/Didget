using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPA__Game
{
    public partial class TutorialHelp : Form
    {
        public TutorialHelp()
        {
            InitializeComponent();
        }

        private void Next_btn_Click(object sender, EventArgs e)
        {
            DialogResult diaRes = MessageBox.Show("Movement is the WASD keys or the Arrow Pad", "Movement Help", MessageBoxButtons.OK);
            if(diaRes == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
