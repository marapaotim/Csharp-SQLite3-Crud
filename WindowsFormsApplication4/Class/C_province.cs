using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication4.Model;


namespace WindowsFormsApplication4.Class
{
    public class C_province
    {
        SQLiteConnection sqlite_conn; 
        SQLiteCommand sqlite_cmd;
        SQLiteDataReader sqlite_datareader;

        connection connector = new connection();

        M_province provEntity = new M_province();
        List<M_province> listProv = new List<M_province>(); 
        public List<M_province> dataArrayProv()
        {
            sqlite_conn = connector.con();
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Province";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                provEntity.province_name = sqlite_datareader.GetString(1);
                listProv.Add(provEntity);
            }
            sqlite_conn.Close();
            return listProv;
        }

        public void insert_(M_province entity)
        {
            sqlite_conn = connector.con();
            sqlite_conn.Open();
            SQLiteCommand comm = sqlite_conn.CreateCommand(); 
            comm.CommandText = @"INSERT INTO `Province`
                    (
                        `Province_name`
                    )
                    VALUES
                    (
                        @province
                    );"; 
            comm.Parameters.AddWithValue("@province", entity.province_name); 
            comm.ExecuteNonQuery();
            sqlite_conn.Close();
            MessageBox.Show("Succesfully Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        public int prov_id(string Province)
        {
            int id = 0;
            sqlite_conn = connector.con();
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Province where Province_name = @prov";
            sqlite_cmd.Parameters.AddWithValue("@prov", Province);
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                id = sqlite_datareader.GetInt32(0);
            }
            sqlite_datareader.Close();
            sqlite_conn.Close();
            return id;
        }
    }
}
