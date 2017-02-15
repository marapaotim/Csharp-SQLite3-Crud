using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication4.Class;
using WindowsFormsApplication4.Model;


namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        C_province cProv = new C_province();
        C_person cPer = new C_person();
        C_datagridview viewDB = new C_datagridview();
        private void Form1_Load(object sender, EventArgs e)
        { 
            cbProvince.SelectedIndex = 0;
            foreach (M_province prov in cProv.dataArrayProv())
            {
                cbProvince.Items.Add(prov.province_name);
            }
            viewDB.viewDB(dataGridView1, C_person.qry);
            toolStripLabel1.Text = "Total Count: " + dataGridView1.Rows.Count;
        }

        private void button1_Click(object sender, EventArgs e)
        {  
            provinceFrm show = new provinceFrm(cbProvince);
            show.ShowDialog();
        }

        bool add_;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            add_ = true;
            personFrm show = new personFrm(dataGridView1, add_, toolStripLabel1);
            show.ShowDialog();
        }
         
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            C_person.Person_id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value);
            personFrm show = new personFrm(dataGridView1, add_ , toolStripLabel1);
            show.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        { 
            add_ = false;
        }

        private void cbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbProvince.SelectedIndex == 0)
                viewDB.viewDB(dataGridView1, C_person.qry);
            else
                viewDB.viewDB(dataGridView1, C_person.Provinceqry(this.cbProvince.GetItemText(this.cbProvince.SelectedItem)));  
        }
    }
}
