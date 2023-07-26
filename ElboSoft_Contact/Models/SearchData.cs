using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElboSoft_Contact.Models
{
    public class SearchData
    {
        public int RequestHeaderID { get; set; }
        public int ContractHeaderID { get; set; }
        public int CustomerID { get; set; }
        public int PurposeID { get; set; }
        public DateTime RequestDate { get; set; }
        public int IsCreatedContract { get; set; }
        public string RequestNumber { get; set; }
        
    }
}