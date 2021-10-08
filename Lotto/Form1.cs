using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lotto
{
    public partial class Form1 : Form
    {
        int db = 0;
        int típus = 0; // 1: 5-ös lottó
                       // 2: 6-ös lottó
                       // 1: skandináv lottó
        int ennyikell = -1;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime kiindulo;

            StreamWriter sw = new StreamWriter("tippek.txt", true);

            switch(típus)
            {
                case 1:
                    sw.WriteLine("5-ös");
                    break;
                case 2:
                    sw.WriteLine("6-ös");
                    break;
                default:
                    sw.WriteLine("skandináv");
                    break;
            }

            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));
            sw.WriteLine(DateTime.Now.ToString("H:mm:ss"));
        
            sw.WriteLine(textBox1.Text);

            foreach (Control c in tableLayoutPanel1.Controls)
            {
                CheckBox cb2 = c as CheckBox;
                if (cb2.Checked)
                    sw.Write(cb2.Text+" ");
            }

            sw.WriteLine("");

            sw.Flush();
            sw.Close();

            label2.Text = "Kiírás megtörtént.";
            label2.ForeColor = Color.Green;
            label2.Visible = true;
        }

        private void ÖtösLottoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 5-ös lottó 
            típus = 1;
            ennyikell = 5;
            db = 0;
            textBox1.Enabled = false;
            button1.Enabled = false;
            label2.Visible = false;
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
            for (int i = 1; i <= 90; i++)
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
            label2.Visible = false;
            CheckBox cb = (CheckBox)sender;
            if (cb.CheckState == CheckState.Checked)
                db++;
            else
                db--;
            if (db == ennyikell)
            {
                foreach (Control c in tableLayoutPanel1.Controls)
                {
                    CheckBox cb2 = c as CheckBox;
                    if (!cb2.Checked)
                        cb2.Enabled = false;
                }
                textBox1.Enabled = true;
                CheckNev();
            }
            else
            {
                foreach (Control c in tableLayoutPanel1.Controls)
                    c.Enabled = true;
                textBox1.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void HatosLottóToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 6-os lottó
            típus = 2;
            ennyikell = 6;
            db = 0;
            textBox1.Enabled = false;
            button1.Enabled = false;
            label2.Visible = false;
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
            for (int i = 1; i <= 45; i++)
            {
                CheckBox cb = new CheckBox();
                cb.AutoSize = true;
                cb.Text = "" + i;
                cb.CheckedChanged += Kattintás;
                tableLayoutPanel1.Controls.Add(cb);
            }
        }

        private void skandinávLottoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // skandináv lottó
            típus = 3;
            ennyikell = 7;
            db = 0;
            textBox1.Enabled = false;
            button1.Enabled = false;
            label2.Visible = false;
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
            for (int i = 1; i <= 35; i++)
            {
                CheckBox cb = new CheckBox();
                cb.AutoSize = true;
                cb.Text = "" + i;
                cb.CheckedChanged += Kattintás;
                tableLayoutPanel1.Controls.Add(cb);
            }

        }

        void CheckNev()
        {
            string[] nev = textBox1.Text.Split(' ');
            if (nev.Length > 1 && nev[0].Length > 0 && nev[1].Length > 0)
                // MessageBox.Show(""+nev.Length);
                button1.Enabled = true;
            else
                button1.Enabled = false;
            label2.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CheckNev();
        }        
    }
}
