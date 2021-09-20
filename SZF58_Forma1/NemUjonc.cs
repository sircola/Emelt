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
    public partial class NemUjonc : UserControl
    {
        MySqlConnection Conn;
        public NemUjonc()
        {
            InitializeComponent();
            Conn = Connect.InitDB();
        }

        public void FillDGV()
        {
            string sql = "CALL pr_NemUjoncVersenyzok();";
            MySqlCommand cmd = new MySqlCommand(sql, Conn);

            Conn.Open();

            MySqlDataReader er = cmd.ExecuteReader();

            while (er.Read())
            {
                dataGridView1.Rows.Add(
                    new object[]
                    {
                        Convert.ToInt32(er["vRajt"]),
                        er["vNev"].ToString(),
                        er["vNemz"].ToString(),
                        er["csNev"].ToString()
                    }
                    );
            }

            Conn.Close();
        }
    }
}
