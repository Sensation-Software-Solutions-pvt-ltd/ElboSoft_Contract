using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElboSoft_Contact.Models
{
    public class trRequestHeader
    {
        public int RequestHeaderID { get; set; }
        public int RequestTypeID { get; set; }
        public int CustomerID { get; set; }
        public int PaymentTypeID { get; set; }
        public decimal AdvanceAmount { get; set; }
        public int Installments { get; set; }
        public int TotalAmountNeeded { get; set; }
        public decimal BankGaranteeAmount { get; set; }
        public int SubcompartmentID { get; set; }
        public int PurposeID { get; set; }
        public DateTime RequestDate { get; set; }
        public int IsCreatedContract { get; set; }
        public int IDCopyPresented { get; set; }
        public int IDBankAccountPresented { get; set; }
        public int PensionCheckPresented { get; set; }
        public int CentralRegisterCopy { get; set; }
        public int PowerOfAttorney { get; set; }
        public int AffidavitPresented { get; set; }
        public int ConfirmationPresented { get; set; }
        public int DRDFormPresented { get; set; }
        public int DeclarationOfReceiptPresented { get; set; }
        public int AgtreementPresented { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedUserName { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string RequestNumber { get; set; }
    }
}