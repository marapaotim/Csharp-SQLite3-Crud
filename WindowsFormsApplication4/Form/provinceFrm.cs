using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication4.Model;
using WindowsFormsApplication4.Class;

namespace WindowsFormsApplication4
{
    public partial class provinceFrm : Form
    {
        ComboBox cbProv2;
        public provinceFrm(ComboBox cbProv1)
        {
            InitializeComponent();
            cbProv2 = cbProv1;
        }

        M_province mProv = new M_province();
        C_province cProv = new C_province();
        private void button1_Click(object sender, EventArgs e)
        {
            if(txtProvince.Text == "")
            {
                MessageBox.Show("Province Name is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            mProv.province_name = txtProvince.Text;
            cProv.insert_(mProv);
            txtProvince.Text = "";
            cbProv2.Items.Clear();
            cbProv2.Items.Add("-Province-");
            cbProv2.SelectedIndex = 0;
            foreach (M_province prov in cProv.dataArrayProv())
            {
                cbProv2.Items.Add(prov.province_name);
            }
        }
    }
}
