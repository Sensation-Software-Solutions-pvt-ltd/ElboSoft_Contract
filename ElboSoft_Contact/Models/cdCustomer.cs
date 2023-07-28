using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElboSoft_Contact.Models
{
    public class cdCustomer
    {
        public int CustomerID { get; set; }
        public string CustomerDescription { get; set; }
        public int CustomerTypeID { get; set; }
        public int IsPerson { get; set; }
        public int SubmissionDeadline { get; set; }
        public int PersonalNumberID { get; set; }
        public int TaxNumber { get; set; }
        public int BankAccountNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string IDNumebr { get; set; }
        public string IssuedBy { get; set; }
    }
}