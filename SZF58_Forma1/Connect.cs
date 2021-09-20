using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace SZF58_Forma1
{
    static class Connect
    {
        static public MySqlConnection InitDB()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "forma1";

            string constr = builder.ToString();
            
            try
            {
                MySqlConnection Conn = new MySqlConnection(constr);
                return Conn;
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Hiba adatbázis csatlakozáskor: " + ex.Message);
            }

            return default(MySqlConnection);
        }
    }
}
