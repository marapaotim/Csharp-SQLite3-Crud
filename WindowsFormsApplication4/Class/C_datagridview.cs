using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApplication4.Class
{
    public class C_datagridview
    {

        public SQLiteConnection connection { set; get; }
        public SQLiteDataReader reader { set; get; }

        connection connector = new connection();
        public DataGridView viewDB(DataGridView dvg, string query)
        {
            connection = connector.con();
            DataSet DS;
            SQLiteCommand cmd = new SQLiteCommand(query, connector.con());

            SQLiteDataAdapter mySqlDataAdapter = new SQLiteDataAdapter(cmd);
            DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            // dvg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dvg.AllowUserToAddRows = false;
            dvg.Invoke(new Action(() => dvg.DataSource = DS.Tables[0]));
            //dvg.DataSource = DS.Tables[0];  
            return dvg;
        }
    }
}
