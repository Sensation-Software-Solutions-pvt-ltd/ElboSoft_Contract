using Dapper;
using ElboSoft_Contact.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElboSoft_Contact
{
    public partial class Contact1 : System.Web.UI.Page
    {
        public int RequestHeaderId = 0;
        public int ContractHeaderId = 0;
        public string createdusername = "test";
        public string lastupdatedusername = "test";
        private string GetConString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                SetInitialRow();
                GetContractType();
                GetCustomer();
                GetPaymentType();
                GetPurpose();
                if (Request.QueryString["RequestId"] != null && !string.IsNullOrEmpty(Request.QueryString["RequestId"].ToString()))
                {
                    string RequestId = Request.QueryString["RequestId"].ToString();
                    BindRequestdata(RequestId);
                }
                if (Request.QueryString["ContractId"] != null && !string.IsNullOrEmpty(Request.QueryString["ContractId"].ToString()))
                {
                    string RequestId = Request.QueryString["ContractId"].ToString();
                    BindContractdata(RequestId);
                    adddiv.Visible = false;
                    updatediv.Visible = true;
                    ButtonAdd.Visible = false;
                }
            }

        }
        private int AddRequestType()
        {
            int RequestTypeID = 0;
            string RequestTypeDes = ContractTypeDescription.Text;
            string sqlquery = string.Format("INSERT INTO public.\"cdRequestType\"(\"RequestTypeDescription\",\"SubmissionDeadline\")VALUES('{0}',1) RETURNING \"RequestTypeID\" ", RequestTypeDes);

            using (var conn = new NpgsqlConnection(GetConString()))
            {
                RequestTypeID = conn.Query<int>(sqlquery).FirstOrDefault();
            }
            GetContractType();
            ContractTypeID.SelectedValue = RequestTypeID.ToString();
            ContractTypeDescription.Text = string.Empty;
            return RequestTypeID;
        }
        private void BindRequestdata(string RequestId)
        {
            
            string sqlQuery = string.Format("select * from public.\"trRequestHeader\" where \"RequestHeaderID\"={0}", RequestId);
            ViewState["Id"] = RequestId;
            trRequestHeader trContractHeaders = new trRequestHeader();
            try
            {
                using (var conn = new NpgsqlConnection(GetConString()))
                {
                    trContractHeaders = conn.Query<trRequestHeader>(sqlQuery).FirstOrDefault();
                    ContractTypeID.SelectedItem.Value = trContractHeaders.RequestTypeID.ToString();
                    Requestnumber.Text = trContractHeaders.RequestNumber;
                    AmountNeeded.Text = trContractHeaders.TotalAmountNeeded.ToString();
                    CustomerID.SelectedItem.Value = trContractHeaders.CustomerID.ToString();
                    PaymentTypeID.SelectedItem.Value = trContractHeaders.PaymentTypeID.ToString();
                    AdvanceAmount.Text = trContractHeaders.AdvanceAmount.ToString();
                    InstallmentNo.Text = trContractHeaders.Installments.ToString();
                    BankGuaranteeAmount.Text = trContractHeaders.BankGaranteeAmount.ToString();
                    PurposeList.SelectedItem.Value = trContractHeaders.PurposeID.ToString();
                    RequestDate.Text = trContractHeaders.RequestDate.ToString("yyyy-MM-dd");
                    IDCopy.Checked = trContractHeaders.IDCopyPresented;
                    BankAccountId.Checked = trContractHeaders.IDBankAccountPresented;
                    PensionCheck.Checked = trContractHeaders.PensionCheckPresented;
                    RegisterCopy.Checked = trContractHeaders.CentralRegisterCopy;
                    PowerAttorney.Checked = trContractHeaders.PowerOfAttorney;
                    Affiadavit.Checked = trContractHeaders.AffidavitPresented;
                    Confirmation.Checked = trContractHeaders.ConfirmationPresented;
                    DRDForm.Checked = trContractHeaders.DRDFormPresented;
                    DeclarationReceipt.Checked = trContractHeaders.DeclarationOfReceiptPresented;
                    Agreement.Checked = trContractHeaders.AgtreementPresented;

                    sqlQuery = "select * from public.\"trContractLine\" where \"ContractHeaderID\" in(select \"ContractHeaderID\" from public.\"trContractHeader\" where \"RequestHeaderID\"=" + RequestId + ")";
                    List<trContractLine> requestLine = conn.Query<trContractLine>(sqlQuery).ToList();
                    ContractGrid.DataSource = requestLine;
                    ContractGrid.DataBind();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }
        private void BindContractdata(string RequestId)
        {
            string sqlQuery = string.Empty;
            if (RequestId.Contains("c"))
            {
                RequestId = RequestId.Replace("c", "");
                ViewState["Id"] = RequestId;
                sqlQuery = string.Format("select * from public.\"trContractHeader\" where \"ContractHeaderID\"={0}", RequestId);
                trContractHeader trContractHeaders = new trContractHeader();
                try
                {
                    using (var conn = new NpgsqlConnection(GetConString()))
                    {
                        trContractHeaders = conn.Query<trContractHeader>(sqlQuery).FirstOrDefault();
                        ContractTypeID.SelectedItem.Value = trContractHeaders.ContractTypeID.ToString();
                        Requestnumber.Text = trContractHeaders.RequestNumber;
                        AmountNeeded.Text = trContractHeaders.TotalAmountNeeded.ToString();
                        CustomerID.SelectedItem.Value = trContractHeaders.CustomerID.ToString();
                        PaymentTypeID.SelectedItem.Value = trContractHeaders.PaymentTypeID.ToString();
                        AdvanceAmount.Text = trContractHeaders.AdvanceAmount.ToString();
                        InstallmentNo.Text = trContractHeaders.Installments.ToString();
                        BankGuaranteeAmount.Text = trContractHeaders.BankGaranteeAmount.ToString();
                        PurposeList.SelectedItem.Value = trContractHeaders.PurposeID.ToString();
                        RequestDate.Text = trContractHeaders.RequestDate.ToString("yyyy-MM-dd");
                        IDCopy.Checked = trContractHeaders.IDCopyPresented;
                        BankAccountId.Checked = trContractHeaders.IDBankAccountPresented;
                        PensionCheck.Checked = trContractHeaders.PensionCheckPresented;
                        RegisterCopy.Checked = trContractHeaders.CentralRegisterCopy;
                        PowerAttorney.Checked = trContractHeaders.PowerOfAttorney;
                        Affiadavit.Checked = trContractHeaders.AffidavitPresented;
                        Confirmation.Checked = trContractHeaders.ConfirmationPresented;
                        DRDForm.Checked = trContractHeaders.DRDFormPresented;
                        DeclarationReceipt.Checked = trContractHeaders.DeclarationOfReceiptPresented;
                        Agreement.Checked = trContractHeaders.AgtreementPresented;

                        RequestHeaderId = trContractHeaders.RequestHeaderID;
                        sqlQuery = "select * from public.\"trContractLine\" where \"ContractHeaderID\" in(select \"ContractHeaderID\" from public.\"trContractHeader\" where \"RequestHeaderID\"=" + RequestId + ")";
                        List<trContractLine> requestLine = conn.Query<trContractLine>(sqlQuery).ToList();
                        ContractGrid.DataSource = requestLine;
                        ContractGrid.DataBind();
                        ViewState["ConractId"] = requestLine[0].ContractHeaderID;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
            else
            {
                sqlQuery = string.Format("select * from public.\"trRequestHeader\" where \"RequestHeaderID\"={0}", RequestId);
                ViewState["Id"] = RequestId;
                trRequestHeader trContractHeaders = new trRequestHeader();
                try
                {
                    using (var conn = new NpgsqlConnection(GetConString()))
                    {
                        trContractHeaders = conn.Query<trRequestHeader>(sqlQuery).FirstOrDefault();
                        ContractTypeID.SelectedItem.Value = trContractHeaders.RequestTypeID.ToString();
                        Requestnumber.Text = trContractHeaders.RequestNumber;
                        AmountNeeded.Text = trContractHeaders.TotalAmountNeeded.ToString();
                        CustomerID.SelectedItem.Value = trContractHeaders.CustomerID.ToString();
                        PaymentTypeID.SelectedItem.Value = trContractHeaders.PaymentTypeID.ToString();
                        AdvanceAmount.Text = trContractHeaders.AdvanceAmount.ToString();
                        InstallmentNo.Text = trContractHeaders.Installments.ToString();
                        BankGuaranteeAmount.Text = trContractHeaders.BankGaranteeAmount.ToString();
                        PurposeList.SelectedItem.Value = trContractHeaders.PurposeID.ToString();
                        RequestDate.Text = trContractHeaders.RequestDate.ToString("yyyy-MM-dd");
                        IDCopy.Checked = trContractHeaders.IDCopyPresented;
                        BankAccountId.Checked = trContractHeaders.IDBankAccountPresented;
                        PensionCheck.Checked = trContractHeaders.PensionCheckPresented;
                        RegisterCopy.Checked = trContractHeaders.CentralRegisterCopy;
                        PowerAttorney.Checked = trContractHeaders.PowerOfAttorney;
                        Affiadavit.Checked = trContractHeaders.AffidavitPresented;
                        Confirmation.Checked = trContractHeaders.ConfirmationPresented;
                        DRDForm.Checked = trContractHeaders.DRDFormPresented;
                        DeclarationReceipt.Checked = trContractHeaders.DeclarationOfReceiptPresented;
                        Agreement.Checked = trContractHeaders.AgtreementPresented;

                        RequestHeaderId = trContractHeaders.RequestHeaderID;
                        sqlQuery = "select * from public.\"trContractLine\" where \"ContractHeaderID\" in(select \"ContractHeaderID\" from public.\"trContractHeader\" where \"RequestHeaderID\"=" + RequestId + ")";
                        List<trContractLine> requestLine = conn.Query<trContractLine>(sqlQuery).ToList();
                        ContractGrid.DataSource = requestLine;
                        ContractGrid.DataBind();
                        ViewState["ConractId"] = requestLine[0].ContractHeaderID;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }

            

        }
        protected void Savebutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AdvanceAmount.Text) || string.IsNullOrEmpty(InstallmentNo.Text) || string.IsNullOrEmpty(AmountNeeded.Text) || string.IsNullOrEmpty(BankGuaranteeAmount.Text)|| string.IsNullOrEmpty(RequestDate.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Kindly Fill all the values.')", true);
                return;
            }
            else
            {
                int ContractId = Convert.ToInt32(ContractTypeID.SelectedItem.Value.Trim());
                if (!string.IsNullOrEmpty(ContractTypeDescription.Text))
                {
                    ContractId = AddRequestType();
                }
                int CustomerId = Convert.ToInt32(CustomerID.SelectedItem.Value.Trim());
                int paymenttypeid = Convert.ToInt32(PaymentTypeID.SelectedItem.Value.Trim());
                decimal advanceamount = Convert.ToDecimal(AdvanceAmount.Text.Trim());
                int installments = Convert.ToInt32(InstallmentNo.Text.Trim());
                int totalamountneeded = Convert.ToInt32(AmountNeeded.Text.Trim());
                decimal bankgaranteeamount = Convert.ToDecimal(BankGuaranteeAmount.Text.Trim());
                int subcompartmentid = 0;
                int purposeid = Convert.ToInt32(PurposeList.SelectedItem.Value.Trim());
                string Requestdate = RequestDate.Text.Trim();
                int idcopypresented = IDCopy.Checked == true ? 1 : 0;
                int idbankaccountpresented = BankAccountId.Checked == true ? 1 : 0;
                int pensioncheckpresented = PensionCheck.Checked == true ? 1 : 0;
                int centralregistercopy = RegisterCopy.Checked == true ? 1 : 0;
                int powerofattorney = PowerAttorney.Checked == true ? 1 : 0;
                int affidavitpresented = Affiadavit.Checked == true ? 1 : 0;
                int confirmationpresented = Confirmation.Checked == true ? 1 : 0;
                int drdformpresented = DRDForm.Checked == true ? 1 : 0;
                int declarationofreceiptpresented = DeclarationReceipt.Checked == true ? 1 : 0;
                int agtreementpresented = Agreement.Checked == true ? 1 : 0;
              
                string createddate = DateTime.Now.ToString("yyyy-MM-dd");
                string RequestNumber = Requestnumber.Text.Trim();
                // string sqlquery = string.Format("call public.insert_trrequestheader (1,{0},{1},{2},cast({3} as money),{4},{5},cast({6} as money),{7},{8},cast('{9}' as date),cast({10} as bit),cast({11} as bit),cast({12} as bit),cast({13} as bit),cast({14} as bit),cast({15} as bit),cast({16} as bit),cast({17} as bit),cast({18} as bit),cast({19} as bit),cast({20} as bit),'{21}','{22}')", RequestId, CustomerId, paymenttypeid, advanceamount, installments, totalamountneeded, bankgaranteeamount, subcompartmentid, purposeid, Requestdate, iscreatedcontract, idcopypresented, idbankaccountpresented, pensioncheckpresented, centralregistercopy, powerofattorney, affidavitpresented, confirmationpresented, drdformpresented, declarationofreceiptpresented, agtreementpresented, createdusername, lastupdatedusername);
                string sqlquery = string.Format("INSERT INTO public.\"trContractHeader\"(\"RequestHeaderID\", \"ContractTypeID\", \"CustomerID\",\"PaymentTypeID\", \"AdvanceAmount\", \"Installments\", \"TotalAmountNeeded\", \"BankGaranteeAmount\", \"SubcompartmentID\", \"PurposeID\",\"PilanaID\", \"ContractDate\", \"IDCopyPresented\", \"IDBankAccountPresented\", \"PensionCheckPresented\", \"CentralRegisterCopy\", \"PowerOfAttorney\", \"AffidavitPresented\", \"ConfirmationPresented\", \"DRDFormPresented\", \"DeclarationOfReceiptPresented\", \"AgtreementPresented\", \"CreatedUserName\", \"CreatedDate\", \"LastUpdatedUserName\", \"LastUpdatedDate\",\"RequestNumber\")VALUES({0},{1},{2},{3},cast({4} as money),{5},{6},cast({7} as money),{8},{9},{10},cast('{11}' as date),cast({12} as bit),cast({13} as bit),cast({14} as bit),cast({15} as bit),cast({16} as bit),cast({17} as bit),cast({18} as bit),cast({19} as bit),cast({20} as bit),cast({21} as bit),'{22}',cast('{23}' as date),'{24}',cast('{25}' as date),'{26}') RETURNING \"ContractHeaderID\" ", Convert.ToInt32(Request.QueryString["RequestId"]), ContractId, CustomerId, paymenttypeid, advanceamount, installments, totalamountneeded, bankgaranteeamount, subcompartmentid, purposeid, 0, Requestdate, idcopypresented, idbankaccountpresented, pensioncheckpresented, centralregistercopy, powerofattorney, affidavitpresented, confirmationpresented, drdformpresented, declarationofreceiptpresented, agtreementpresented, createdusername, createddate, lastupdatedusername, createddate, RequestNumber);
                ContractHeaderId = 0;
                using (var conn = new NpgsqlConnection(GetConString()))
                {
                    ContractHeaderId = conn.Query<int>(sqlquery).FirstOrDefault();
                }
                int result = 0;
                //string month = ((DropDownList)RequestGrid.Rows[0].FindControl("ManagementUnit")).SelectedItem.Value;
                for (int i = 0; i < ContractGrid.Rows.Count; i++)
                {
                    int SubcompartmentID = 0;
                    string month = ((DropDownList)ContractGrid.Rows[i].FindControl("Month")).SelectedItem.Value;
                    string VidoviEdinecniMeriID = ((DropDownList)ContractGrid.Rows[i].FindControl("Edinecnamera")).SelectedItem.Value;
                    string VidoviSortimentiID = ((DropDownList)ContractGrid.Rows[i].FindControl("Vidsortiment")).SelectedItem.Value;
                    string Qty = ((TextBox)ContractGrid.Rows[i].FindControl("Qty")).Text;
                    string Price = ((TextBox)ContractGrid.Rows[i].FindControl("Price")).Text;
                    string subcomp = ((TextBox)ContractGrid.Rows[i].FindControl("SubcompartmentID")).Text;
                    int PriceDetailID = 0;
                    int PlanId = 0;
                    
                    sqlquery = string.Format("INSERT INTO public.\"trContractLine\"(\"ContractHeaderID\", \"SubcompartmentID\", \"Month\", \"VidoviEdinecniMeriID\", \"VidoviSortimentiID\", \"Qty\", \"PriceDetailID\",\"Price\",\"PlanID\", \"CreatedUserName\", \"CreatedDate\", \"LastUpdatedUserName\", \"LastUpdatedDate\")VALUES({0},{1},{2},{3},{4},cast({5} as money),{6},cast({7} as money),{8},'{9}',cast('{10}' as date),'{11}',cast('{12}' as date)) RETURNING \"ContractLineID\" ", ContractHeaderId, string.IsNullOrEmpty(subcomp)?0:Convert.ToInt32(subcomp), string.IsNullOrEmpty(month) ? 0 : Convert.ToInt32(month), string.IsNullOrEmpty(VidoviEdinecniMeriID) ? 0 : Convert.ToInt32(VidoviEdinecniMeriID), string.IsNullOrEmpty(VidoviSortimentiID) ? 0 : Convert.ToInt32(VidoviSortimentiID), string.IsNullOrEmpty(Qty) ? 0 : Convert.ToDecimal(Qty), PriceDetailID, Price, 0, createdusername, createddate, lastupdatedusername, createddate);
                    using (var conn = new NpgsqlConnection(GetConString()))
                    {
                        result = conn.Query<int>(sqlquery).FirstOrDefault();
                    }
                }
                if (result > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Contract Created Successfully')", true);
                    Requestnumber.Text = "";
                    AdvanceAmount.Text = "";
                    InstallmentNo.Text = "";
                    AmountNeeded.Text = "";
                    BankGuaranteeAmount.Text = "";
                    RequestDate.Text = "";
                    IDCopy.Checked = false;
                    BankAccountId.Checked = false;
                    PensionCheck.Checked = false;
                    RegisterCopy.Checked = false;
                    PowerAttorney.Checked = false;
                    Affiadavit.Checked = false;
                    Confirmation.Checked = false;
                    DRDForm.Checked = false;
                    DeclarationReceipt.Checked = false;
                    Agreement.Checked = false;
                }
            }
        }


        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }
        private void GetPurpose()
        {

            string sqlQuery = "select * from public.\"cdPurpose\"";
            List<cdPurpose> purposes = new List<cdPurpose>();
            try
            {
                using (var conn = new NpgsqlConnection(GetConString()))
                {
                    purposes = conn.Query<cdPurpose>(sqlQuery).ToList();
                    PurposeList.DataSource = purposes;
                    PurposeList.DataValueField = "PurposeID";
                    PurposeList.DataTextField = "PurposeDescription";
                    PurposeList.DataBind();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }
        private void GetContractType()
        {

            string sqlQuery = "select * from public.\"cdRequestType\"";
            List<cdRequestType> requestType = new List<cdRequestType>();
            try
            {
                using (var conn = new NpgsqlConnection(GetConString()))
                {
                    requestType = conn.Query<cdRequestType>(sqlQuery).ToList();
                    ContractTypeID.DataSource = requestType;
                    ContractTypeID.DataValueField = "RequestTypeID";
                    ContractTypeID.DataTextField = "RequestTypeDescription";
                    ContractTypeID.DataBind();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }
        private void GetCustomer()
        {
            string sqlQuery = "select * from public.\"cdCustomer\"";
            List<cdCustomer> customers = new List<cdCustomer>();
            try
            {
                using (var conn = new NpgsqlConnection(GetConString()))
                {
                    customers = conn.Query<cdCustomer>(sqlQuery).ToList();
                    CustomerID.DataSource = customers;
                    CustomerID.DataValueField = "CustomerID";
                    CustomerID.DataTextField = "CustomerDescription";
                    CustomerID.DataBind();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }
        private void GetPaymentType()
        {
            string sqlQuery = "select * from public.\"cdPaymentType\"";
            List<cdPaymentType> paymentTypes = new List<cdPaymentType>();
            try
            {
                using (var conn = new NpgsqlConnection(GetConString()))
                {
                    paymentTypes = conn.Query<cdPaymentType>(sqlQuery).ToList();
                    PaymentTypeID.DataSource = paymentTypes;
                    PaymentTypeID.DataValueField = "PaymentTypeID";
                    PaymentTypeID.DataTextField = "PaymentTypeDescription";
                    PaymentTypeID.DataBind();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }

        protected void ContractGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ReginalCenter = (e.Row.FindControl("ReginalCenter") as DropDownList);
                DropDownList ManagementUnit = (e.Row.FindControl("ManagementUnit") as DropDownList);
                DropDownList Month = (e.Row.FindControl("Month") as DropDownList);
                DropDownList Edinecnamera = (e.Row.FindControl("Edinecnamera") as DropDownList);
                DropDownList Vidsortiment = (e.Row.FindControl("Vidsortiment") as DropDownList);
                string month = (e.Row.FindControl("lblMonth") as Label).Text;
                string edinecnamera = (e.Row.FindControl("lblEdinecnamera") as Label).Text;
                string vidsortiment = (e.Row.FindControl("lblVidsortiment") as Label).Text;
                try
                {
                    using (var conn = new NpgsqlConnection(GetConString()))
                    {
                        List<RegionalCenters> paymentTypes = conn.Query<RegionalCenters>("select * from public.\"RegionalCenters\"").ToList();
                        ReginalCenter.DataSource = paymentTypes;
                        ReginalCenter.DataValueField = "ID";
                        ReginalCenter.DataTextField = "Code";
                        ReginalCenter.DataBind();
                        List<ManagementUnits> management = conn.Query<ManagementUnits>("select * from public.\"ManagementUnits\"").ToList();
                        ManagementUnit.DataSource = management;
                        ManagementUnit.DataValueField = "ID";
                        ManagementUnit.DataTextField = "Code";
                        ManagementUnit.DataBind();
                        List<VidoviSortimenti> Vidsortiments = conn.Query<VidoviSortimenti>("select * from public.\"VidoviSortimenti\"").ToList();
                        Vidsortiment.DataSource = Vidsortiments;
                        Vidsortiment.DataValueField = "ID";
                        Vidsortiment.DataTextField = "Name";
                        Vidsortiment.DataBind();
                        List<VidoviEdinecniMeri> edinecniMeris = conn.Query<VidoviEdinecniMeri>("select * from public.\"VidoviEdinecniMeri\"").ToList();
                        Edinecnamera.DataSource = Vidsortiments;
                        Edinecnamera.DataValueField = "ID";
                        Edinecnamera.DataTextField = "Name";
                        Edinecnamera.DataBind();
                        List<Months> months = conn.Query<Months>("select * from public.\"Months\"").ToList();
                        Month.DataSource = months;
                        Month.DataValueField = "ID";
                        Month.DataTextField = "Name";
                        Month.DataBind();
                        if (!string.IsNullOrEmpty(month))
                        {
                            Month.Items.FindByValue(month).Selected = true;
                        }
                        if (!string.IsNullOrEmpty(edinecnamera))
                        {
                            Edinecnamera.Items.FindByValue(edinecnamera).Selected = true;
                        }
                        if (!string.IsNullOrEmpty(vidsortiment))
                        {
                            Vidsortiment.Items.FindByValue(vidsortiment).Selected = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }

            }
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("ReginalCenter", typeof(string)));
            dt.Columns.Add(new DataColumn("ManagementUnit", typeof(string)));
            dt.Columns.Add(new DataColumn("Compartment", typeof(string)));
            dt.Columns.Add(new DataColumn("SubcompartmentID", typeof(string)));
            dt.Columns.Add(new DataColumn("Month", typeof(string)));
            dt.Columns.Add(new DataColumn("VidoviEdinecniMeriID", typeof(string)));
            dt.Columns.Add(new DataColumn("VidoviSortimentiID", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("Price", typeof(string)));
            dr = dt.NewRow();
            dr["ReginalCenter"] = string.Empty;
            dr["ManagementUnit"] = string.Empty;
            dr["Compartment"] = string.Empty;
            dr["SubcompartmentID"] = string.Empty;
            dr["Month"] = string.Empty;
            dr["VidoviEdinecniMeriID"] = string.Empty;
            dr["VidoviSortimentiID"] = string.Empty;
            dr["Qty"] = string.Empty;
            dr["Price"] = string.Empty;
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            ContractGrid.DataSource = dt;
            ContractGrid.DataBind();

        }
        private void AddNewRowToGrid()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ReginalCenter = (DropDownList)ContractGrid.Rows[rowIndex].Cells[1].FindControl("ReginalCenter");
                        DropDownList ManagementUnit = (DropDownList)ContractGrid.Rows[rowIndex].Cells[2].FindControl("ManagementUnit");
                        TextBox Compartment = (TextBox)ContractGrid.Rows[rowIndex].Cells[3].FindControl("Compartment");
                        TextBox SubCompartment = (TextBox)ContractGrid.Rows[rowIndex].Cells[1].FindControl("SubcompartmentID");
                        DropDownList Month = (DropDownList)ContractGrid.Rows[rowIndex].Cells[2].FindControl("Month");
                        DropDownList Edinecnamera = (DropDownList)ContractGrid.Rows[rowIndex].Cells[3].FindControl("Edinecnamera");
                        DropDownList Vidsortiment = (DropDownList)ContractGrid.Rows[rowIndex].Cells[1].FindControl("Vidsortiment");
                        TextBox Qty = (TextBox)ContractGrid.Rows[rowIndex].Cells[2].FindControl("Qty");
                        TextBox Price = (TextBox)ContractGrid.Rows[rowIndex].Cells[2].FindControl("Price");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["ReginalCenter"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["ManagementUnit"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["Compartment"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["SubcompartmentID"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["Month"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["Edinecnamera"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["Vidsortiment"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["Qty"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["Price"] = string.Empty;
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    ContractGrid.DataSource = dtCurrentTable;
                    ContractGrid.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            //SetPreviousData();
        }
        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ReginalCenter = (DropDownList)ContractGrid.Rows[rowIndex].Cells[1].FindControl("ReginalCenter");
                        DropDownList ManagementUnit = (DropDownList)ContractGrid.Rows[rowIndex].Cells[2].FindControl("ManagementUnit");
                        TextBox Compartment = (TextBox)ContractGrid.Rows[rowIndex].Cells[3].FindControl("Compartment");
                        TextBox SubCompartment = (TextBox)ContractGrid.Rows[rowIndex].Cells[1].FindControl("SubcompartmentID");
                        DropDownList Month = (DropDownList)ContractGrid.Rows[rowIndex].Cells[2].FindControl("Month");
                        DropDownList Edinecnamera = (DropDownList)ContractGrid.Rows[rowIndex].Cells[3].FindControl("Edinecnamera");
                        DropDownList Vidsortiment = (DropDownList)ContractGrid.Rows[rowIndex].Cells[1].FindControl("Vidsortiment");
                        TextBox Qty = (TextBox)ContractGrid.Rows[rowIndex].Cells[2].FindControl("Qty");
                        TextBox Price = (TextBox)ContractGrid.Rows[rowIndex].Cells[2].FindControl("Price");

                        ReginalCenter.SelectedItem.Text = dt.Rows[i]["ReginalCenter"].ToString();
                        ManagementUnit.SelectedItem.Text = dt.Rows[i]["ManagementUnit"].ToString();
                        Compartment.Text = dt.Rows[i]["Compartment"].ToString();
                        SubCompartment.Text = dt.Rows[i]["SubcompartmentID"].ToString();
                        Month.Text = dt.Rows[i]["Month"].ToString();
                        Edinecnamera.Text = dt.Rows[i]["Edinecnamera"].ToString();
                        Vidsortiment.Text = dt.Rows[i]["Vidsortiment"].ToString();
                        Qty.Text = dt.Rows[i]["Qty"].ToString();
                        Price.Text = dt.Rows[i]["Price"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        protected void updateContract_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AdvanceAmount.Text) || string.IsNullOrEmpty(InstallmentNo.Text) || string.IsNullOrEmpty(AmountNeeded.Text) || string.IsNullOrEmpty(BankGuaranteeAmount.Text) || string.IsNullOrEmpty(RequestDate.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Kindly Fill all the values.')", true);
                return;
            }
            else
            {
                int ContractId = Convert.ToInt32(ContractTypeID.SelectedItem.Value.Trim());
                if (!string.IsNullOrEmpty(ContractTypeDescription.Text))
                {
                    ContractId = AddRequestType();
                }
                int CustomerId = Convert.ToInt32(CustomerID.SelectedItem.Value.Trim());
                int paymenttypeid = Convert.ToInt32(PaymentTypeID.SelectedItem.Value.Trim());
                decimal advanceamount = Convert.ToDecimal(AdvanceAmount.Text.Trim());
                int installments = Convert.ToInt32(InstallmentNo.Text.Trim());
                int totalamountneeded = Convert.ToInt32(AmountNeeded.Text.Trim());
                decimal bankgaranteeamount = Convert.ToDecimal(BankGuaranteeAmount.Text.Trim());
                int subcompartmentid = 0;
                int purposeid = Convert.ToInt32(PurposeList.SelectedItem.Value.Trim());
                string Requestdate = RequestDate.Text.Trim();
                int idcopypresented = IDCopy.Checked == true ? 1 : 0;
                int idbankaccountpresented = BankAccountId.Checked == true ? 1 : 0;
                int pensioncheckpresented = PensionCheck.Checked == true ? 1 : 0;
                int centralregistercopy = RegisterCopy.Checked == true ? 1 : 0;
                int powerofattorney = PowerAttorney.Checked == true ? 1 : 0;
                int affidavitpresented = Affiadavit.Checked == true ? 1 : 0;
                int confirmationpresented = Confirmation.Checked == true ? 1 : 0;
                int drdformpresented = DRDForm.Checked == true ? 1 : 0;
                int declarationofreceiptpresented = DeclarationReceipt.Checked == true ? 1 : 0;
                int agtreementpresented = Agreement.Checked == true ? 1 : 0;
                string createddate = DateTime.Now.ToString("yyyy-MM-dd");
                string RequestNumber = Requestnumber.Text;

                string sqlquery = string.Format("Update public.\"trContractHeader\" set  \"ContractTypeID\"={0}, \"CustomerID\"={1},\"PaymentTypeID\"={2}, \"AdvanceAmount\"=cast({3} as money), \"Installments\"={4}, \"TotalAmountNeeded\"={5}, \"BankGaranteeAmount\"=cast({6} as money), \"SubcompartmentID\"={7}, \"PurposeID\"={8},\"PilanaID\"={9}, \"ContractDate\"=cast('{10}' as date), \"IDCopyPresented\"=cast({11} as bit), \"IDBankAccountPresented\"=cast({12} as bit), \"PensionCheckPresented\"=cast({13} as bit), \"CentralRegisterCopy\"=cast({14} as bit), \"PowerOfAttorney\"=cast({15} as bit), \"AffidavitPresented\"=cast({16} as bit), \"ConfirmationPresented\"=cast({17} as bit), \"DRDFormPresented\"=cast({18} as bit), \"DeclarationOfReceiptPresented\"=cast({19} as bit), \"AgtreementPresented\"=cast({20} as bit), \"LastUpdatedUserName\"='{21}', \"LastUpdatedDate\"=cast('{22}' as date),\"RequestNumber\"='{23}' where \"RequestHeaderID\"={24}", ContractId, CustomerId, paymenttypeid, advanceamount, installments, totalamountneeded, bankgaranteeamount, subcompartmentid, purposeid, 0, Requestdate, idcopypresented, idbankaccountpresented, pensioncheckpresented, centralregistercopy, powerofattorney, affidavitpresented, confirmationpresented, drdformpresented, declarationofreceiptpresented, agtreementpresented, lastupdatedusername, createddate, RequestNumber,Convert.ToInt32(ViewState["Id"]));
                int result = 0;
                using (var conn = new NpgsqlConnection(GetConString()))
                {
                    result = conn.Query<int>(sqlquery).FirstOrDefault();
                }
                
                //string month = ((DropDownList)RequestGrid.Rows[0].FindControl("ManagementUnit")).SelectedItem.Value;
                for (int i = 0; i < ContractGrid.Rows.Count; i++)
                {
                    string month = ((DropDownList)ContractGrid.Rows[i].FindControl("Month")).SelectedItem.Value;
                    string VidoviEdinecniMeriID = ((DropDownList)ContractGrid.Rows[i].FindControl("Edinecnamera")).SelectedItem.Value;
                    string VidoviSortimentiID = ((DropDownList)ContractGrid.Rows[i].FindControl("Vidsortiment")).SelectedItem.Value;
                    string Qty = ((TextBox)ContractGrid.Rows[i].FindControl("Qty")).Text;
                    string Price = ((TextBox)ContractGrid.Rows[i].FindControl("Price")).Text;
                    string subcomp = ((TextBox)ContractGrid.Rows[i].FindControl("SubcompartmentID")).Text;
                    int PriceDetailID = 0;
                    sqlquery = string.Format("Update public.\"trContractLine\" set \"SubcompartmentID\"={0}, \"Month\"={1}, \"VidoviEdinecniMeriID\"={2}, \"VidoviSortimentiID\"={3}, \"Qty\"=cast({4} as money), \"PriceDetailID\"={5},\"Price\"=cast({6} as money),\"PlanID\"={7},  \"LastUpdatedUserName\"='{8}', \"LastUpdatedDate\"=cast('{9}' as date) where \"ContractHeaderID\"={10}", string.IsNullOrEmpty(subcomp) ? 0 : Convert.ToInt32(subcomp), string.IsNullOrEmpty(month) ? 0 : Convert.ToInt32(month), string.IsNullOrEmpty(VidoviEdinecniMeriID) ? 0 : Convert.ToInt32(VidoviEdinecniMeriID), string.IsNullOrEmpty(VidoviSortimentiID) ? 0 : Convert.ToInt32(VidoviSortimentiID), string.IsNullOrEmpty(Qty) ? 0 : Convert.ToDecimal(Qty), PriceDetailID, Price, 0, lastupdatedusername, createddate, Convert.ToInt32(ViewState["ConractId"]));
                    using (var conn = new NpgsqlConnection(GetConString()))
                    {
                        result = conn.Query<int>(sqlquery).FirstOrDefault();
                    }
                }
                if (result == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Contract Updated Successfully')", true);
                    Requestnumber.Text = "";
                    AdvanceAmount.Text = "";
                    InstallmentNo.Text = "";
                    AmountNeeded.Text = "";
                    BankGuaranteeAmount.Text = "";
                    RequestDate.Text = "";
                    IDCopy.Checked = false;
                    BankAccountId.Checked = false;
                    PensionCheck.Checked = false;
                    RegisterCopy.Checked = false;
                    PowerAttorney.Checked = false;
                    Affiadavit.Checked = false;
                    Confirmation.Checked = false;
                    DRDForm.Checked = false;
                    DeclarationReceipt.Checked = false;
                    Agreement.Checked = false;
                }
            }
        }
    }
}