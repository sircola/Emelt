using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SZF58_Forma1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void nemÚjoncokListájaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            nemUjonc1.FillDGV();
            nemUjonc1.Visible = true;
            ujVersenyzo1.Visible = false;
        }

        private void kezdőlapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nemUjonc1.Visible = false;
            pictureBox1.Visible = true;
            ujVersenyzo1.Visible = false;
        }

        private void újVersenyzőFelvételToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            nemUjonc1.Visible = false;
            ujVersenyzo1.Visible = true;
        }
    }
}
