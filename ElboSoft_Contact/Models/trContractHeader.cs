using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElboSoft_Contact.Models
{
    public class trContractHeader
    {
        public int ContractHeaderID { get; set; }
        public int RequestHeaderID { get; set; }
        public int ContractTypeID { get; set; }
        public int CustomerID { get; set; }
        public int PaymentTypeID { get; set; }
        public decimal AdvanceAmount { get; set; }
        public int Installments { get; set; }
        public int TotalAmountNeeded { get; set; }
        public decimal BankGaranteeAmount { get; set; }
        public int SubcompartmentID { get; set; }
        public int PurposeID { get; set; }
        public int PilanaID { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IDCopyPresented { get; set; }
        public bool IDBankAccountPresented { get; set; }
        public bool PensionCheckPresented { get; set; }
        public bool CentralRegisterCopy { get; set; }
        public bool PowerOfAttorney { get; set; }
        public bool AffidavitPresented { get; set; }
        public bool ConfirmationPresented { get; set; }
        public bool DRDFormPresented { get; set; }
        public bool DeclarationOfReceiptPresented { get; set; }
        public bool AgtreementPresented { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedUserName { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string RequestNumber { get; set; }
    }
}