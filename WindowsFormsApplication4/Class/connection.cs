using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WindowsFormsApplication4.Class
{
    public class connection
    {
        SQLiteConnection sqlite_conn;

        string locationDB = Application.StartupPath + "\\database2.db";
        public SQLiteConnection con()
        { 
            sqlite_conn = new SQLiteConnection();
            sqlite_conn.ConnectionString = "Data Source=" + locationDB + ";Version=3;New=false;Compress=false;";
            try
            {
                sqlite_conn.Open();
                sqlite_conn.Close(); 
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString());
            }

            return sqlite_conn;
        }
    }
}
