using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTakeProData.Models
{
    public class InsuranceModel
    {
        public int ID { get; set; }

        public string InsuranceCompany { get; set; }
        public string Memberid { get; set; }

        public string Group_Number { get; set; }
        public string Policy_Holder { get; set; }
        public string Updated_by { get; set; }

        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }
    }
}
