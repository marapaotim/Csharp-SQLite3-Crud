using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication4.Model
{
    public struct M_person
    {
        public int M_id { get; set; }
        public string M_firstname { get; set; }
        public string M_middlename { get; set; }
        public string M_lastname { get; set; }
        public int M_age { get; set; }
        public string M_contactnumber { get; set; }
        public string M_gender { get; set; }
        public string M_address { get; set; }
        public string M_provinceID { get; set; }
        public bool add_ { get; set; }
    }
}
