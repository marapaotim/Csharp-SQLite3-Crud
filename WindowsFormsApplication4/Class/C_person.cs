using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication4.Model;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WindowsFormsApplication4.Class
{
    public class C_person
    {
        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;
        SQLiteDataReader sqlite_datareader;

        connection connector = new connection();
        M_person entityPerson = new M_person();
        List<M_person> listPerson = new List<M_person>();

        public static string qry = @"select 
 p.id, 
 p.first_name as `First Name`, 
 p.middle_name  as `Middle Name`,
 p.last_name  as `Last Name`,
 p.age as `Age`,
 p.contact_number as `Contact Number`,
 p.gender as `Gender`,
 p.address as `Address`,
 pr.Province_name as `Province`
from person p left join Province pr on p.province_id = pr.id;";


        public static string Provinceqry(string prov_name)
        {
            string a = @"select 
 p.id, 
 p.first_name as `First Name`, 
 p.middle_name  as `Middle Name`,
 p.last_name  as `Last Name`,
 p.age as `Age`,
 p.contact_number as `Contact Number`,
 p.gender as `Gender`,
 p.address as `Address`,
 pr.Province_name as `Province`
from person p left join Province pr on p.province_id = pr.id where pr.Province_name like '%"+ prov_name +"%';";
            return a;
        } 


        public static int Person_id;
        public List<M_person> RetrieveArrayPerson()
        {
            sqlite_conn = connector.con();
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = @"select 
 p.id, 
 p.first_name as `First Name`, 
 p.middle_name  as `Middle Name`,
 p.last_name  as `Last Name`,
 p.age as `Age`,
 p.contact_number as `Contact Number`,
 p.gender as `Gender`,
 p.address as `Address`,
 pr.Province_name as `Province`
from person p left join Province pr on p.province_id = pr.id where p.id = '"+ Person_id + "';"; 
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            { 
                entityPerson.M_firstname = sqlite_datareader.GetString(1);
                entityPerson.M_middlename = sqlite_datareader.GetString(2);
                entityPerson.M_lastname = sqlite_datareader.GetString(3);
                entityPerson.M_age = sqlite_datareader.GetInt32(4);
                entityPerson.M_contactnumber = sqlite_datareader.GetString(5);
                entityPerson.M_gender = sqlite_datareader.GetString(6);
                entityPerson.M_address = sqlite_datareader.GetString(7);
                entityPerson.M_provinceID = sqlite_datareader.GetString(8);
                listPerson.Add(entityPerson);
            }
            sqlite_conn.Close();
            return listPerson;
        }

        public void insert_(M_person entity, bool add_)
        {
            string qryExecute = "";
            string msg = "";
            sqlite_conn = connector.con();
            sqlite_conn.Open();
            SQLiteCommand comm = sqlite_conn.CreateCommand();
            if (add_)
            {
                qryExecute = @"INSERT INTO `person`
                    (
                        `first_name`,
                        `middle_name`,
                        `last_name`,
                        `age`,
                        `contact_number`,
                        `gender`,
                        `address`,
                        `province_id`
                    )
                    VALUES
                    (
                        @firstname,
                        @middlename,
                        @lastname,
                        @age,
                        @contactnumber,
                        @gender,
                        @address,
                        @provid
                    );";

                msg = "Succesfully Added";
            }
            else
            {
                qryExecute = @"UPDATE `person`
                    SET
                    `first_name` = @firstname,
                    `middle_name` = @middlename,
                    `last_name` = @lastname,
                    `age` = @age,
                    `contact_number` = @contactnumber,
                    `gender` = @gender,
                    `address` = @address,
                    `province_id` = @provid
                    WHERE `id` = '"+ Person_id + "';";

                msg = "Successfully Updated";
            }
            comm.CommandText = qryExecute; 
            comm.Parameters.AddWithValue("@firstname", entity.M_firstname);
            comm.Parameters.AddWithValue("@middlename", entity.M_middlename);
            comm.Parameters.AddWithValue("@lastname", entity.M_lastname);
            comm.Parameters.AddWithValue("@age", entity.M_age);
            comm.Parameters.AddWithValue("@contactnumber", entity.M_contactnumber);
            comm.Parameters.AddWithValue("@gender", entity.M_gender);
            comm.Parameters.AddWithValue("@address", entity.M_address);
            comm.Parameters.AddWithValue("@provid", entity.M_provinceID); 
            comm.ExecuteNonQuery();
            sqlite_conn.Close();
            MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public void delete_()
        {
            string qryExecute = "";
            string msg = "";
            sqlite_conn = connector.con();
            sqlite_conn.Open();
            SQLiteCommand comm = sqlite_conn.CreateCommand(); 
            qryExecute = @"DELETE FROM `person` WHERE id = @id;"; 
            msg = "Succesfully Deleted"; 
            comm.CommandText = qryExecute;
            comm.Parameters.AddWithValue("@id", Person_id); 
            comm.ExecuteNonQuery();
            sqlite_conn.Close();
            MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
