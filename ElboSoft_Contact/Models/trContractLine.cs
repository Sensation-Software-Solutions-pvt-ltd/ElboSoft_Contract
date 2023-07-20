﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElboSoft_Contact.Models
{
    public class trContractLine
    {
        public int ContractLineID { get; set; }
        public int ContractHeaderID { get; set; }
        public int SubcompartmentID { get; set; }
        public int Month { get; set; }
        public int VidoviEdinecniMeriID { get; set; }
        public int VidoviSortimentiID { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }
        public int PlanID { get; set; }
        public int PriceDetailID { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedUserName { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}