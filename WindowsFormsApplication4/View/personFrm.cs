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
using WindowsFormsApplication4.Controller;

namespace WindowsFormsApplication4
{
    public partial class personFrm : Form
    {
        DataGridView dgv2 = null;
        bool add_2 = true;
        ToolStripLabel lblCount2;
        public personFrm(DataGridView dgv1, bool add_1, ToolStripLabel lblCount1)
        {
            InitializeComponent();
            dgv2 = dgv1;
            add_2 = add_1;
            lblCount2 = lblCount1;
        }

        C_province cProv = new C_province();
        M_person mPer = new M_person();
        C_person cPer = new C_person();
        C_datagridview viewDB = new C_datagridview();

        void clear()
        {
            txtFrstName.Text = "";
            txtMiddlename.Text = "";
            txtLastname.Text = "";
            txtAge.Text = "";
            rbMale.Checked = false;
            rbFemale.Checked = false;
            txtContactnumber.Text = "";
            txtAddress.Text = "";
            cbProvince.SelectedIndex = 0;
        }
        private void personFrm_Load(object sender, EventArgs e)
        {
            cbProvince.SelectedIndex = 0;
            foreach (M_province prov in cProv.dataArrayProv())
            {
                cbProvince.Items.Add(prov.province_name);
            }
            if (!add_2)
            {
                foreach(M_person per in cPer.RetrieveArrayPerson())
                { 
                    txtFrstName.Text = per.M_firstname;
                    txtMiddlename.Text = per.M_middlename;
                    txtLastname.Text = per.M_lastname;
                    txtAge.Text = per.M_age.ToString();
                    if(per.M_gender == "Male") { rbMale.Checked = true; } else { rbFemale.Checked = true; }
                    txtContactnumber.Text = per.M_contactnumber;
                    txtAddress.Text = per.M_address;
                    cbProvince.SelectedIndex = cbProvince.FindStringExact(per.M_provinceID); 
                }
            }
            else
            {
                button2.Enabled = false;
            }
        }

        bool isEmpty()
        {
            string msg = "";
            if (cbProvince.SelectedIndex == 0)
            {
                msg = "Please select Province";
            }
            if (txtAddress.Text == "")
            {
                msg = "Address Field is Empty";
            }
            if (txtContactnumber.Text == "")
            {
                msg = "Contact Number Field is Empty";
            }
            if (!rbMale.Checked && !rbFemale.Checked)
            {
                msg = "Gender Field is Empty";
            }
            if (txtAge.Text == "")
            {
                msg = "Age Field is Empty";
            }
            if (txtLastname.Text == "")
            {
                msg = "Last Name Field is Empty";
            }
            if (txtMiddlename.Text == "")
            {
                msg = "Middle Name Field is Empty";
            } 
            if (txtFrstName.Text == "")
            {
                msg = "First Name Field is Empty";
            }
            if(msg.Length > 0) { MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); } 
            return (msg.Length != 0);
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            if (isEmpty())
            {
                return;
            }
            mPer.M_firstname = txtFrstName.Text;
            mPer.M_middlename = txtMiddlename.Text;
            mPer.M_lastname = txtLastname.Text;
            mPer.M_age = Convert.ToInt32(txtAge.Text);
            if (rbMale.Checked){ mPer.M_gender = "Male"; }
            if (rbFemale.Checked) { mPer.M_gender = "Female"; }
            mPer.M_contactnumber = txtContactnumber.Text;
            mPer.M_address = txtAddress.Text;
            mPer.M_provinceID = cProv.prov_id(this.cbProvince.GetItemText(this.cbProvince.SelectedItem)).ToString();
            cPer.insert_(mPer, add_2);
            viewDB.viewDB(dgv2, C_person.qry);
            lblCount2.Text = "Total Count: " + dgv2.Rows.Count;
            clear(); 
            if (!add_2)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to delete?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                cPer.delete_();
                viewDB.viewDB(dgv2, C_person.qry);
                lblCount2.Text = "Total Count: " + dgv2.Rows.Count;
                Close();
            } 
        }
    }
}
