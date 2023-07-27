using Dapper;
using ElboSoft_Contact.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElboSoft_Contact.CommonClasses;
using System.Data.Odbc;


namespace ElboSoft_Contact
{
    public partial class Request : System.Web.UI.Page
    {
        public int RequestHeaderId = 0;
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
                int count = 0;
                GetRequestType();
                GetCustomer();
                GetPaymentType();
                GetPurpose();
                if (Request.QueryString["RequestId"] != null && !string.IsNullOrEmpty(Request.QueryString["RequestId"].ToString()))
                {
                    string RequestId = Request.QueryString["RequestId"].ToString();
                   count= BindRequestdata(RequestId);
                    adddiv.Visible = false;
                    updatediv.Visible = true;
                }
                SetInitialRow(count);
            }


        }
        private int BindRequestdata(string RequestId)
        {
            int count = 1;
            string sqlQuery = string.Format("select * from public.\"trRequestHeader\" where \"RequestHeaderID\"='{0}'", RequestId);
            trRequestHeader trRequestHeaders = new trRequestHeader();
            try
            {
                using (var conn = new OdbcConnection(GetConString()))
                {
                    trRequestHeaders = conn.Query<trRequestHeader>(sqlQuery).FirstOrDefault();
                    RequestType.SelectedValue = trRequestHeaders.RequestTypeID.ToString();
                    Requestnumber.Text = trRequestHeaders.RequestNumber;
                    AmountNeeded.Text = trRequestHeaders.TotalAmountNeeded.ToString();
                    CustomerID.SelectedValue = trRequestHeaders.CustomerID.ToString();
                    PaymentTypeID.SelectedValue = trRequestHeaders.PaymentTypeID.ToString();
                    AdvanceAmount.Text = trRequestHeaders.AdvanceAmount.ToString();
                    InstallmentNo.Text = trRequestHeaders.Installments.ToString();
                    BankGuaranteeAmount.Text = trRequestHeaders.BankGaranteeAmount.ToString();
                    PurposeList.SelectedValue = trRequestHeaders.PurposeID.ToString();
                    RequestDate.Text = trRequestHeaders.RequestDate.ToString("yyyy-MM-dd");
                    ContractCreated.Checked = Convert.ToBoolean(trRequestHeaders.IsCreatedContract);
                    IDCopy.Checked = Convert.ToBoolean(trRequestHeaders.IDCopyPresented);
                    BankAccountId.Checked = Convert.ToBoolean(trRequestHeaders.IDBankAccountPresented);
                    PensionCheck.Checked = Convert.ToBoolean(trRequestHeaders.PensionCheckPresented);
                    RegisterCopy.Checked = Convert.ToBoolean(trRequestHeaders.CentralRegisterCopy);
                    PowerAttorney.Checked = Convert.ToBoolean(trRequestHeaders.PowerOfAttorney);
                    Affiadavit.Checked = Convert.ToBoolean(trRequestHeaders.AffidavitPresented);
                    Confirmation.Checked = Convert.ToBoolean(trRequestHeaders.ConfirmationPresented);
                    DRDForm.Checked = Convert.ToBoolean(trRequestHeaders.DRDFormPresented);
                    DeclarationReceipt.Checked = Convert.ToBoolean(trRequestHeaders.DeclarationOfReceiptPresented);
                    Agreement.Checked = Convert.ToBoolean(trRequestHeaders.AgtreementPresented);
                    RequestHeaderId = trRequestHeaders.RequestHeaderID;
                    sqlQuery = "select * from public.\"trRequestLine\" where \"RequestHeaderID\"=" + trRequestHeaders.RequestHeaderID;
                    List<trRequestLine> requestLine = conn.Query<trRequestLine>(sqlQuery).ToList();
                    RequestGrid.DataSource = requestLine;
                    RequestGrid.DataBind();
                    count = requestLine.Count;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return count;
        }
        private void GetRequestType()
        {

            string sqlQuery = "select * from public.\"cdRequestType\"";
            List<cdRequestType> requestType = new List<cdRequestType>();
            try
            {
               
                using (var conn = new OdbcConnection(GetConString()))
                {
                    requestType = conn.Query<cdRequestType>(sqlQuery).ToList();
                    RequestType.DataSource = requestType;
                    RequestType.DataValueField = "RequestTypeID";
                    RequestType.DataTextField = "RequestTypeDescription";
                    RequestType.DataBind();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
           
        }

        private int AddRequestType()
        {
            int RequestTypeID = 0;
            string RequestTypeDes = RequestTypeDescription.Text;
            string sqlquery = string.Format("INSERT INTO public.\"cdRequestType\"(\"RequestTypeDescription\",\"SubmissionDeadline\")VALUES('{0}',1) RETURNING \"RequestTypeID\" ", RequestTypeDes);

            using (var conn = new OdbcConnection(GetConString()))
            {
                RequestTypeID = conn.Query<int>(sqlquery).FirstOrDefault();
            }
            GetRequestType();
            RequestType.SelectedValue = RequestTypeID.ToString();
            RequestTypeDescription.Text = string.Empty;
            return RequestTypeID;
        }
        private void GetPurpose()
        {

            string sqlQuery = "select * from public.\"cdPurpose\"";
            List<cdPurpose> purposes = new List<cdPurpose>();
            try
            {
                using (var conn = new OdbcConnection(GetConString()))
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
        private void GetCustomer()
        {
            string sqlQuery = "select * from public.\"cdCustomer\"";
            List<cdCustomer> customers = new List<cdCustomer>();
            try
            {
                using (var conn = new OdbcConnection(GetConString()))
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
                using (var conn = new OdbcConnection(GetConString()))
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
        protected void RequestGrid_RowDataBound(object sender, GridViewRowEventArgs e)
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
                string rcenter = (e.Row.FindControl("lblReginalCenter") as Label).Text;
                string munit = (e.Row.FindControl("lblManagementUnit") as Label).Text;
                try
                {
                    using (var conn = new OdbcConnection(GetConString()))
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
                        if (!string.IsNullOrEmpty(month)&&month!="0")
                        {
                            Month.Items.FindByValue(month).Selected = true;
                        }
                        if (!string.IsNullOrEmpty(edinecnamera) && edinecnamera!="0")
                        {
                            Edinecnamera.Items.FindByValue(edinecnamera).Selected = true;
                        }
                        if (!string.IsNullOrEmpty(vidsortiment)&& vidsortiment!="0")
                        {
                            Vidsortiment.Items.FindByValue(vidsortiment).Selected = true;
                        }
                        if (!string.IsNullOrEmpty(rcenter))
                        {
                            ReginalCenter.Items.FindByValue(rcenter).Selected = true;
                        }
                        if (!string.IsNullOrEmpty(munit))
                        {
                            ManagementUnit.Items.FindByValue(munit).Selected = true;
                        }
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
            if (string.IsNullOrEmpty(AdvanceAmount.Text) || string.IsNullOrEmpty(InstallmentNo.Text) || string.IsNullOrEmpty(AmountNeeded.Text) || string.IsNullOrEmpty(BankGuaranteeAmount.Text) || string.IsNullOrEmpty(RequestDate.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Kindly Fill all the values.')", true);
                return;
            }
            else
            {
                int RequestTypeID = Convert.ToInt32(RequestType.SelectedItem.Value.Trim());
                if (!string.IsNullOrEmpty(RequestTypeDescription.Text))
                {
                    RequestTypeID = AddRequestType();
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
                int iscreatedcontract =  0;
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
                string sqlquery = string.Format("INSERT INTO public.\"trRequestHeader\"(\"RequestTypeID\", \"CustomerID\", \"PaymentTypeID\", \"AdvanceAmount\", \"Installments\", \"TotalAmountNeeded\", \"BankGaranteeAmount\", \"SubcompartmentID\", \"PurposeID\", \"RequestDate\", \"IsCreatedContract\", \"IDCopyPresented\", \"IDBankAccountPresented\", \"PensionCheckPresented\", \"CentralRegisterCopy\", \"PowerOfAttorney\", \"AffidavitPresented\", \"ConfirmationPresented\", \"DRDFormPresented\", \"DeclarationOfReceiptPresented\", \"AgtreementPresented\", \"CreatedUserName\", \"CreatedDate\", \"LastUpdatedUserName\", \"LastUpdatedDate\",\"RequestNumber\")VALUES({0},{1},{2},cast({3} as money),{4},{5},cast({6} as money),{7},{8},cast('{9}' as date),cast({10} as bit),cast({11} as bit),cast({12} as bit),cast({13} as bit),cast({14} as bit),cast({15} as bit),cast({16} as bit),cast({17} as bit),cast({18} as bit),cast({19} as bit),cast({20} as bit),'{21}',cast('{22}' as date),'{23}',cast('{24}' as date),'{25}') RETURNING \"RequestHeaderID\" ", RequestTypeID, CustomerId, paymenttypeid, advanceamount, installments, totalamountneeded, bankgaranteeamount, subcompartmentid, purposeid, Requestdate, iscreatedcontract, idcopypresented, idbankaccountpresented, pensioncheckpresented, centralregistercopy, powerofattorney, affidavitpresented, confirmationpresented, drdformpresented, declarationofreceiptpresented, agtreementpresented, createdusername, createddate, lastupdatedusername, createddate, RequestNumber);

                using (var conn = new OdbcConnection(GetConString()))
                {
                    RequestHeaderId = conn.Query<int>(sqlquery).FirstOrDefault();
                }
                int result = 0;
                for (int i = 0; i < RequestGrid.Rows.Count; i++)
                {
                    string month = ((DropDownList)RequestGrid.Rows[i].FindControl("Month")).SelectedItem.Value;
                    string VidoviEdinecniMeriID = ((DropDownList)RequestGrid.Rows[i].FindControl("Edinecnamera")).SelectedItem.Value;
                    string VidoviSortimentiID = ((DropDownList)RequestGrid.Rows[i].FindControl("Vidsortiment")).SelectedItem.Value;
                    string ReginalCentralId = ((DropDownList)RequestGrid.Rows[i].FindControl("ReginalCenter")).SelectedItem.Value;
                    string ManagementUnitId = ((DropDownList)RequestGrid.Rows[i].FindControl("ManagementUnit")).SelectedItem.Value;
                    string Qty = ((TextBox)RequestGrid.Rows[i].FindControl("Qty")).Text;
                    string subcomp = ((TextBox)RequestGrid.Rows[i].FindControl("SubcompartmentID")).Text;
                    string compId = ((TextBox)RequestGrid.Rows[i].FindControl("Compartment")).Text;
                    int PriceDetailID = 0;
                    sqlquery = string.Format("INSERT INTO public.\"trRequestLine\"(\"RequestHeaderID\", \"SubcompartmentID\", \"Month\", \"VidoviEdinecniMeriID\", \"VidoviSortimentiID\", \"Qty\", \"PriceDetailID\", \"CreatedUserName\", \"CreatedDate\", \"LastUpdatedUserName\", \"LastUpdatedDate\", \"RegionalCenterId\", \"ManagementUnitId\", \"CompartmentId\")VALUES({0},{1},{2},{3},{4},cast({5} as money),{6},'{7}',cast('{8}' as date),'{9}',cast('{10}' as date),{11},{12},{13}) RETURNING \"RequestLineID\" ", RequestHeaderId, string.IsNullOrEmpty(subcomp) ? 0 : Convert.ToInt32(subcomp), string.IsNullOrEmpty(month) ? 0 : Convert.ToInt32(month), string.IsNullOrEmpty(VidoviEdinecniMeriID) ? 0 : Convert.ToInt32(VidoviEdinecniMeriID), string.IsNullOrEmpty(VidoviSortimentiID) ? 0 : Convert.ToInt32(VidoviSortimentiID), string.IsNullOrEmpty(Qty) ? 0 : Convert.ToDecimal(Qty), PriceDetailID, createdusername, createddate, lastupdatedusername, createddate, string.IsNullOrEmpty(ReginalCentralId) ? 0 : Convert.ToInt32(ReginalCentralId), string.IsNullOrEmpty(ManagementUnitId) ? 0 : Convert.ToInt32(ManagementUnitId), string.IsNullOrEmpty(compId) ? 0 : Convert.ToInt32(compId));
                    using (var conn = new OdbcConnection(GetConString()))
                    {
                        result = conn.Query<int>(sqlquery).FirstOrDefault();
                    }
                }
                if (result > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Created Successfully')", true);
                    Requestnumber.Text = "";
                    AdvanceAmount.Text = "";
                    InstallmentNo.Text = "";
                    AmountNeeded.Text = "";
                    BankGuaranteeAmount.Text = "";
                    RequestDate.Text = "";
                    ContractCreated.Checked = false;
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

        protected void CreateAgreement_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["RequestId"] != null && !string.IsNullOrEmpty(Request.QueryString["RequestId"].ToString()))
            {
                Response.Redirect("Contract.aspx?RequestId=" + Request.QueryString["RequestId"].ToString());
            }
            else
            {

                if (string.IsNullOrEmpty(AdvanceAmount.Text) || string.IsNullOrEmpty(InstallmentNo.Text) || string.IsNullOrEmpty(AmountNeeded.Text) || string.IsNullOrEmpty(BankGuaranteeAmount.Text) || string.IsNullOrEmpty(RequestDate.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Kindly Fill all the values.')", true);
                    return;
                }
                else
                {
                    int RequestTypeID = Convert.ToInt32(RequestType.SelectedItem.Value.Trim());
                    if (!string.IsNullOrEmpty(RequestTypeDescription.Text))
                    {
                        RequestTypeID = AddRequestType();
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
                    int iscreatedcontract = 0;
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

                    string sqlquery = string.Format("INSERT INTO public.\"trRequestHeader\"(\"RequestTypeID\", \"CustomerID\", \"PaymentTypeID\", \"AdvanceAmount\", \"Installments\", \"TotalAmountNeeded\", \"BankGaranteeAmount\", \"SubcompartmentID\", \"PurposeID\", \"RequestDate\", \"IsCreatedContract\", \"IDCopyPresented\", \"IDBankAccountPresented\", \"PensionCheckPresented\", \"CentralRegisterCopy\", \"PowerOfAttorney\", \"AffidavitPresented\", \"ConfirmationPresented\", \"DRDFormPresented\", \"DeclarationOfReceiptPresented\", \"AgtreementPresented\", \"CreatedUserName\", \"CreatedDate\", \"LastUpdatedUserName\", \"LastUpdatedDate\",\"RequestNumber\")VALUES({0},{1},{2},cast({3} as money),{4},{5},cast({6} as money),{7},{8},cast('{9}' as date),cast({10} as bit),cast({11} as bit),cast({12} as bit),cast({13} as bit),cast({14} as bit),cast({15} as bit),cast({16} as bit),cast({17} as bit),cast({18} as bit),cast({19} as bit),cast({20} as bit),'{21}',cast('{22}' as date),'{23}',cast('{24}' as date),'{25}') RETURNING \"RequestHeaderID\" ", RequestTypeID, CustomerId, paymenttypeid, advanceamount, installments, totalamountneeded, bankgaranteeamount, subcompartmentid, purposeid, Requestdate, iscreatedcontract, idcopypresented, idbankaccountpresented, pensioncheckpresented, centralregistercopy, powerofattorney, affidavitpresented, confirmationpresented, drdformpresented, declarationofreceiptpresented, agtreementpresented, createdusername, createddate, lastupdatedusername, createddate, RequestNumber);

                    using (var conn = new OdbcConnection(GetConString()))
                    {
                        RequestHeaderId = conn.Query<int>(sqlquery).FirstOrDefault();
                    }
                    int result = 0;
                    //string month = ((DropDownList)RequestGrid.Rows[0].FindControl("ManagementUnit")).SelectedItem.Value;
                    for (int i = 0; i < RequestGrid.Rows.Count; i++)
                    {
                        string month = ((DropDownList)RequestGrid.Rows[i].FindControl("Month")).SelectedItem.Value;
                        string VidoviEdinecniMeriID = ((DropDownList)RequestGrid.Rows[i].FindControl("Edinecnamera")).SelectedItem.Value;
                        string VidoviSortimentiID = ((DropDownList)RequestGrid.Rows[i].FindControl("Vidsortiment")).SelectedItem.Value;
                        string ReginalCentralId = ((DropDownList)RequestGrid.Rows[i].FindControl("ReginalCenter")).SelectedItem.Value;
                        string ManagementUnitId = ((DropDownList)RequestGrid.Rows[i].FindControl("ManagementUnit")).SelectedItem.Value;
                        string Qty = ((TextBox)RequestGrid.Rows[i].FindControl("Qty")).Text;
                        string subcomp = ((TextBox)RequestGrid.Rows[i].FindControl("SubcompartmentID")).Text;
                        string compId = ((TextBox)RequestGrid.Rows[i].FindControl("Compartment")).Text;
                        int PriceDetailID = 0;
                        sqlquery = string.Format("INSERT INTO public.\"trRequestLine\"(\"RequestHeaderID\", \"SubcompartmentID\", \"Month\", \"VidoviEdinecniMeriID\", \"VidoviSortimentiID\", \"Qty\", \"PriceDetailID\", \"CreatedUserName\", \"CreatedDate\", \"LastUpdatedUserName\", \"LastUpdatedDate\", \"RegionalCenterId\", \"ManagementUnitId\", \"CompartmentId\")VALUES({0},{1},{2},{3},{4},cast({5} as money),{6},'{7}',cast('{8}' as date),'{9}',cast('{10}' as date),{11},{12},{13}) RETURNING \"RequestLineID\" ", RequestHeaderId, string.IsNullOrEmpty(subcomp) ? 0 : Convert.ToInt32(subcomp), string.IsNullOrEmpty(month) ? 0 : Convert.ToInt32(month), string.IsNullOrEmpty(VidoviEdinecniMeriID) ? 0 : Convert.ToInt32(VidoviEdinecniMeriID), string.IsNullOrEmpty(VidoviSortimentiID) ? 0 : Convert.ToInt32(VidoviSortimentiID), string.IsNullOrEmpty(Qty) ? 0 : Convert.ToDecimal(Qty), PriceDetailID, createdusername, createddate, lastupdatedusername, createddate, string.IsNullOrEmpty(ReginalCentralId) ? 0 : Convert.ToInt32(ReginalCentralId), string.IsNullOrEmpty(ManagementUnitId) ? 0 : Convert.ToInt32(ManagementUnitId), string.IsNullOrEmpty(compId) ? 0 : Convert.ToInt32(compId));
                        using (var conn = new OdbcConnection(GetConString()))
                        {
                            result = conn.Query<int>(sqlquery).FirstOrDefault();
                        }
                    }
                    if (result > 0)
                    {
                        Response.Redirect("Contract.aspx?RequestId=" + RequestHeaderId);
                    }
                }
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }
        private void SetInitialRow(int initialRowCount)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RegionalCenterId", typeof(string)));
            dt.Columns.Add(new DataColumn("ManagementUnitId", typeof(string)));
            dt.Columns.Add(new DataColumn("CompartmentId", typeof(string)));
            dt.Columns.Add(new DataColumn("SubcompartmentID", typeof(string)));
            dt.Columns.Add(new DataColumn("Month", typeof(string)));
            dt.Columns.Add(new DataColumn("VidoviEdinecniMeriID", typeof(string)));
            dt.Columns.Add(new DataColumn("VidoviSortimentiID", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("Price", typeof(string)));
            if (initialRowCount == 0)
            {
                dr = dt.NewRow();
                dr["RegionalCenterId"] = "1";
                dr["ManagementUnitId"] = "1";
                dr["CompartmentId"] = string.Empty;
                dr["SubcompartmentID"] = string.Empty;
                dr["Month"] = "1";
                dr["VidoviEdinecniMeriID"] = "1";
                dr["VidoviSortimentiID"] = "1";
                dr["Qty"] = string.Empty;
                dr["Price"] = string.Empty;
                dt.Rows.Add(dr);
                RequestGrid.DataSource = dt;
                RequestGrid.DataBind();
            }
            else
            {
                for (int i = 1; i <= initialRowCount; i++)
                {
                    dr = dt.NewRow();
                    dr["RegionalCenterId"] = "1";
                    dr["ManagementUnitId"] = "1";
                    dr["CompartmentId"] = string.Empty;
                    dr["SubcompartmentID"] = string.Empty;
                    dr["Month"] = "1";
                    dr["VidoviEdinecniMeriID"] = "1";
                    dr["VidoviSortimentiID"] = "1";
                    dr["Qty"] = string.Empty;
                    dr["Price"] = string.Empty;
                    dt.Rows.Add(dr);
                }
            }
            ViewState["CurrentTable"] = dt;
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
                        DropDownList ReginalCenter = (DropDownList)RequestGrid.Rows[rowIndex].Cells[0].FindControl("ReginalCenter");
                        DropDownList ManagementUnit = (DropDownList)RequestGrid.Rows[rowIndex].Cells[1].FindControl("ManagementUnit");
                        TextBox Compartment = (TextBox)RequestGrid.Rows[rowIndex].Cells[2].FindControl("Compartment");
                        TextBox SubCompartment = (TextBox)RequestGrid.Rows[rowIndex].Cells[3].FindControl("SubcompartmentID");
                        DropDownList Month = (DropDownList)RequestGrid.Rows[rowIndex].Cells[4].FindControl("Month");
                        DropDownList Edinecnamera = (DropDownList)RequestGrid.Rows[rowIndex].Cells[5].FindControl("Edinecnamera");
                        DropDownList Vidsortiment = (DropDownList)RequestGrid.Rows[rowIndex].Cells[6].FindControl("Vidsortiment");
                        TextBox Qty = (TextBox)RequestGrid.Rows[rowIndex].Cells[7].FindControl("Qty");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["RegionalCenterId"] = ReginalCenter.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ManagementUnitId"] = ManagementUnit.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["CompartmentId"] = Compartment.Text;
                        dtCurrentTable.Rows[i - 1]["SubcompartmentID"] = SubCompartment.Text;
                        dtCurrentTable.Rows[i - 1]["Month"] = Month.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["VidoviEdinecniMeriID"] = Edinecnamera.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["VidoviSortimentiID"] = Vidsortiment.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Qty"] = Qty.Text;
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    RequestGrid.DataSource = dtCurrentTable;
                    RequestGrid.DataBind();
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
                        DropDownList ReginalCenter = (DropDownList)RequestGrid.Rows[rowIndex].Cells[1].FindControl("ReginalCenter");
                        DropDownList ManagementUnit = (DropDownList)RequestGrid.Rows[rowIndex].Cells[2].FindControl("ManagementUnit");
                        TextBox Compartment = (TextBox)RequestGrid.Rows[rowIndex].Cells[3].FindControl("Compartment");
                        TextBox SubCompartment = (TextBox)RequestGrid.Rows[rowIndex].Cells[1].FindControl("SubcompartmentID");
                        DropDownList Month = (DropDownList)RequestGrid.Rows[rowIndex].Cells[2].FindControl("Month");
                        DropDownList Edinecnamera = (DropDownList)RequestGrid.Rows[rowIndex].Cells[3].FindControl("Edinecnamera");
                        DropDownList Vidsortiment = (DropDownList)RequestGrid.Rows[rowIndex].Cells[1].FindControl("Vidsortiment");
                        TextBox Qty = (TextBox)RequestGrid.Rows[rowIndex].Cells[2].FindControl("Qty");
                        TextBox Price = (TextBox)RequestGrid.Rows[rowIndex].Cells[2].FindControl("Price");

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

        protected void updateRequest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AdvanceAmount.Text) || string.IsNullOrEmpty(InstallmentNo.Text) || string.IsNullOrEmpty(AmountNeeded.Text) || string.IsNullOrEmpty(BankGuaranteeAmount.Text)|| string.IsNullOrEmpty(RequestDate.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Kindly Fill all the values.')", true);
                return;
            }
            else
            {
                int RequestTypeID = Convert.ToInt32(RequestType.SelectedItem.Value.Trim());
                if (!string.IsNullOrEmpty(RequestTypeDescription.Text))
                {
                    RequestTypeID = AddRequestType();
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
                int iscreatedcontract = ContractCreated.Checked == true ? 1 : 0;
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
                string RequestHeaderID = Request.QueryString["RequestId"].ToString();
                string RequestNumber = Requestnumber.Text.Trim();
                string sqlquery = string.Format("UPDATE public.\"trRequestHeader\" SET \"RequestTypeID\"={0}, \"CustomerID\"={1}, \"PaymentTypeID\"={2}, \"AdvanceAmount\"=cast({3} as money), \"Installments\"={4}, \"TotalAmountNeeded\"={5}, \"BankGaranteeAmount\"=cast({6} as money), \"SubcompartmentID\"={7}, \"PurposeID\"={8}, \"RequestDate\"=cast('{9}' as date), \"IsCreatedContract\"=cast({10} as bit), \"IDCopyPresented\"=cast({11} as bit), \"IDBankAccountPresented\"=cast({12} as bit), \"PensionCheckPresented\"=cast({13} as bit), \"CentralRegisterCopy\"=cast({14} as bit), \"PowerOfAttorney\"=cast({15} as bit), \"AffidavitPresented\"=cast({16} as bit), \"ConfirmationPresented\"=cast({17} as bit), \"DRDFormPresented\"=cast({18} as bit), \"DeclarationOfReceiptPresented\"=cast({19} as bit), \"AgtreementPresented\"=cast({20} as bit),  \"LastUpdatedUserName\"='{21}', \"LastUpdatedDate\"=cast('{22}' as date), \"RequestNumber\"='{23}' WHERE \"RequestHeaderID\"={24}; ", RequestTypeID, CustomerId, paymenttypeid, advanceamount, installments, totalamountneeded, bankgaranteeamount, subcompartmentid, purposeid, Requestdate, iscreatedcontract, idcopypresented, idbankaccountpresented, pensioncheckpresented, centralregistercopy, powerofattorney, affidavitpresented, confirmationpresented, drdformpresented, declarationofreceiptpresented, agtreementpresented, lastupdatedusername, createddate, RequestNumber, RequestHeaderID);

                using (var conn = new OdbcConnection(GetConString()))
                {
                    var res = conn.Query(sqlquery);
                }
                int result = 0;
                //string month = ((DropDownList)RequestGrid.Rows[0].FindControl("ManagementUnit")).SelectedItem.Value;
                for (int i = 0; i < RequestGrid.Rows.Count; i++)
                {
                    string month = ((DropDownList)RequestGrid.Rows[i].FindControl("Month")).SelectedItem.Value;
                    string VidoviEdinecniMeriID = ((DropDownList)RequestGrid.Rows[i].FindControl("Edinecnamera")).SelectedItem.Value;
                    string VidoviSortimentiID = ((DropDownList)RequestGrid.Rows[i].FindControl("Vidsortiment")).SelectedItem.Value;
                    string ReginalCentralId = ((DropDownList)RequestGrid.Rows[i].FindControl("ReginalCenter")).SelectedItem.Value;
                    string ManagementUnitId = ((DropDownList)RequestGrid.Rows[i].FindControl("ManagementUnit")).SelectedItem.Value;
                    string Qty = ((TextBox)RequestGrid.Rows[i].FindControl("Qty")).Text;
                    string subcomp = ((TextBox)RequestGrid.Rows[i].FindControl("SubcompartmentID")).Text;
                    string compId = ((TextBox)RequestGrid.Rows[i].FindControl("Compartment")).Text;
                    int PriceDetailID = 0;
                    sqlquery = string.Format("UPDATE public.\"trRequestLine\" SET \"SubcompartmentID\"={0}, \"Month\"={1}, \"VidoviEdinecniMeriID\"={2}, \"VidoviSortimentiID\"={3}, \"Qty\"=cast({4} as money), \"PriceDetailID\"={5},  \"LastUpdatedUserName\"='{6}', \"LastUpdatedDate\"=cast('{7}' as date) , \"RegionalCenterId\"={8}, \"ManagementUnitId\"={9}, \"CompartmentId\"={10} WHERE \"RequestHeaderID\"={11} ", string.IsNullOrEmpty(subcomp) ? 0 : Convert.ToInt32(subcomp), string.IsNullOrEmpty(month) ? 0 : Convert.ToInt32(month), string.IsNullOrEmpty(VidoviEdinecniMeriID) ? 0 : Convert.ToInt32(VidoviEdinecniMeriID), string.IsNullOrEmpty(VidoviSortimentiID) ? 0 : Convert.ToInt32(VidoviSortimentiID), string.IsNullOrEmpty(Qty) ? 0 : Convert.ToDecimal(Qty), PriceDetailID, lastupdatedusername, createddate, string.IsNullOrEmpty(ReginalCentralId) ? 0 : Convert.ToInt32(ReginalCentralId), string.IsNullOrEmpty(ManagementUnitId) ? 0 : Convert.ToInt32(ManagementUnitId), string.IsNullOrEmpty(compId) ? 0 : Convert.ToInt32(compId), Request.QueryString["RequestId"].ToString());
                    using (var conn = new OdbcConnection(GetConString()))
                    {
                        result = conn.Query<int>(sqlquery).FirstOrDefault();
                    }
                }
                if (result == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request Updated Successfully')", true);
                    Requestnumber.Text = "";
                    AdvanceAmount.Text = "";
                    InstallmentNo.Text = "";
                    AmountNeeded.Text = "";
                    BankGuaranteeAmount.Text = "";
                    RequestDate.Text = "";
                    ContractCreated.Checked = false;
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