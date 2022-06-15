using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SZF58_Forma1
{
    public partial class UjVersenyzo : UserControl
    {
        MySqlConnection Conn;
        bool errNev = false, errRajt = false, errDatum = false;

        public UjVersenyzo()
        {
            InitializeComponent();
            Conn = Connect.InitDB();
        }

        private void UjVersenyzo_Load(object sender, EventArgs e)
        {
            string sql = "CALL pr_Csapatok();";
            MySqlCommand cmd = new MySqlCommand(sql, Conn);

            Conn.Open();

            MySqlDataReader er = cmd.ExecuteReader();

            while(er.Read())
            {
                InputCsapat.Items.Add(
                    er["nev"] + " (" +
                    er["azon"] +")"
                    );
            }

            Conn.Close();

            InputCsapat.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if( InputVNev.Text.Length < 6 )
            {
                ErrorNev.Text = "Túl rövid a versenyző neve";
                errNev = false;
            }
            else
            {
                ErrorNev.Text = "";
                errNev = true;
                if (CheckError())
                    Insert.Enabled = true;
            }

        }

        private void Insert_Click(object sender, EventArgs e)
        {
            string vnev = InputVNev.Text;
            int rajtszam = (int)InputVRajt.Value;
            string csapat = InputCsapat.Items[InputCsapat.SelectedIndex].ToString();
            csapat = csapat.Remove(0, csapat.IndexOf("(")+1);
            csapat = csapat.Remove(csapat.IndexOf(")"));
            int csapatazon = int.Parse(csapat);
            bool ujonc = InputUjonc.Checked;
            DateTime datum = InputDatum.Value;

            string sDatum = datum.Year + "-" + datum.Month + "-" + datum.Day;

            string sql = "CALL pr_UjVersenyzo(@vNev,@vRajt,@csAzon,@ujonc,@belep);";

            MySqlCommand cmd = new MySqlCommand(sql, Conn);
            cmd.Parameters.AddWithValue("@vNev", vnev);
            cmd.Parameters["@vNev"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@vRajt", rajtszam);
            cmd.Parameters["@vRajt"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@csAzon", csapatazon);
            cmd.Parameters["@csAzon"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@ujonc", ujonc?1:0);
            cmd.Parameters["@ujonc"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@belep", sDatum);
            // cmd.Parameters.AddWithValue("@belep", datum.ToString("yyyy-MM-dd"));
            cmd.Parameters["@belep"].Direction = ParameterDirection.Input;

            Conn.Open();
            int res = cmd.ExecuteNonQuery();
            Conn.Close();

            if(res==0)
            {
                InsertResult.ForeColor = Color.Red;
                InsertResult.Text = "Nem sikerült a felvétel.";
            }
            else
            {
                InsertResult.ForeColor = Color.Green;
                InsertResult.Text = "Sikerült a felvétel.";
            }
        }

        private void InputVRajt_ValueChanged(object sender, EventArgs e)
        {
            int rajtszam = (int)InputVRajt.Value;

            string sql = " CALL pr_CheckRajtszam(@rajtszam);";
            MySqlCommand cmd = new MySqlCommand(sql, Conn);
            cmd.Parameters.AddWithValue("@rajtszam", rajtszam);
            cmd.Parameters["@rajtszam"].Direction = ParameterDirection.Input;

            Conn.Open();
            MySqlDataReader er = cmd.ExecuteReader();

            er.Read();
            if( Convert.ToInt32(er["van"]) == 0)
            {
                ErrorRajtszam.Text = "";
                errRajt = true;
                if (CheckError())
                    Insert.Enabled = true;
            }
            else
            {
                ErrorRajtszam.Text = "Foglalt ez a rajtszám";
                errRajt = false;

            }

            Conn.Close();
        }

        private void InputDatum_ValueChanged(object sender, EventArgs e)
        {
            DateTime datum = InputDatum.Value;

            if (datum.Year < 1950)
            {
                ErrorDatum.Text = "Túl régi dátum";
                errDatum = false;
            }
            else if (datum.Year > 2021)
            {
                ErrorDatum.Text = "Túl messzi dátum";
                errDatum = false;
            }
            else
            {
                ErrorDatum.Text = "";
                errDatum = true;
                if (CheckError())
                    Insert.Enabled = true;
            }
        }

        private bool CheckError()
        {
            return errNev && errRajt && errDatum;
        }
    }
}
