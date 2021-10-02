using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SZF58Okt2
{
    public partial class Form1 : Form
    {
        int db = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Generálás.Enabled = true;
        }

        private void Generálás_Click(object sender, EventArgs e)
        {
            string adószám = "8";
            int nap = dateTimePicker1.Value.Subtract(new DateTime(1871, 1, 1)).Days;
            Random rng = new Random();
            // int sorszám = rng.Next(100, 1000);
            int sorszám = rng.Next(0, 1000);
            string ssz = "";
            if (sorszám < 10) ssz = "" + sorszám;
            else if (sorszám >= 10 && sorszám < 100) ssz = "0" + sorszám;
            else ssz = ssz + sorszám;
            adószám = adószám + nap + ssz;
            while(true)
            {
                int szorzatösszeg = 0;
                for(int i =0; i<9; i++)
                {
                    szorzatösszeg += (adószám[i] - 48)*(i+1);
                    // int.Parse(adószám.Substring(i, 1));
                }
                int maradék = szorzatösszeg % 11;
                if( maradék < 10)
                {
                    adószám += maradék;
                    break;
                }
                else
                {
                    sorszám++;
                    ssz = "" + sorszám;
                    adószám = "8" + nap + ssz;
                }
            }
            Graphics rl = panel1.CreateGraphics();
            // rl.DrawString(adószám, this.Font, new SolidBrush(Color.Yellow), 10, 10);
            Font f = new Font("Arial", 30);
            rl.DrawString(adószám, f, new SolidBrush(Color.Yellow), 10, 10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.Visible = true;
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.ColumnCount = 9;
            for (int i = 1; i <= 9; i++)
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.RowCount = 5;
            for (int i = 1; i <= 5; i++)
                tableLayoutPanel1.RowStyles.Add(new RowStyle());
            for (int i=1; i<=45; i++)
            {
                CheckBox cb = new CheckBox();
                cb.AutoSize = true;
                cb.Text = "" + i;
                cb.CheckedChanged += Kattintás;
                tableLayoutPanel1.Controls.Add(cb);
            }
        }

        void Kattintás(object sender, EventArgs e)
        {
            // MessageBox.Show(""+sender);
            CheckBox cb = (CheckBox)sender;
            if (cb.CheckState == CheckState.Checked)
                db++;
            else
                db--;
            // if (db == 6) MessageBox.Show("Nincs több tipp");
            if (db == 6)
            {
                foreach (Control c in tableLayoutPanel1.Controls)
                {
                    CheckBox cb2 = c as CheckBox;
                    if (!cb2.Checked)
                        cb2.Enabled = false;
                }
            }
            else
            {
                foreach (Control c in tableLayoutPanel1.Controls)
                        c.Enabled = true;
            }
        }
    }
}
