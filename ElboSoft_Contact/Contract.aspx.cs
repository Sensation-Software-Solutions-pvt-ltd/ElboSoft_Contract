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
        public  int RequestHeaderId = 0;
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
                if (Request.QueryString["RequestNumber"] != null && !string.IsNullOrEmpty(Request.QueryString["RequestNumber"].ToString()))
                {
                    string RequestId = Request.QueryString["RequestNumber"].ToString();
                    BindContractdata(RequestId);
                    adddiv.Visible = false;
                    updatediv.Visible = true;
                    ButtonAdd.Visible = false;
                }
            }
           
        }
        private void BindContractdata(string RequestHeaderId)
        {

            string sqlQuery = "select * from public.\"trRequestHeader\" where \"RequestHeaderID\"="+RequestHeaderId;
            List<trContractHeader> trContractHeaders = new List<trContractHeader>();
            try
            {
                using (var conn = new NpgsqlConnection(GetConString()))
                {
                    trContractHeaders = conn.Query<trContractHeader>(sqlQuery).ToList();
                   
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }
        protected void Savebutton_Click(object sender, EventArgs e)
        {
            int ContractId = string.IsNullOrEmpty(Contractnumber.Text) ? Convert.ToInt32(ContractTypeID.SelectedItem.Value.Trim()) : Convert.ToInt32(Contractnumber.Text.Trim()); ;
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
            string createdusername = "test";
            string lastupdatedusername = "test";
            string createddate = DateTime.Now.ToString("yyyy-MM-dd");
            string RequestNumber = Requestnumber.Text.Trim();
            // string sqlquery = string.Format("call public.insert_trrequestheader (1,{0},{1},{2},cast({3} as money),{4},{5},cast({6} as money),{7},{8},cast('{9}' as date),cast({10} as bit),cast({11} as bit),cast({12} as bit),cast({13} as bit),cast({14} as bit),cast({15} as bit),cast({16} as bit),cast({17} as bit),cast({18} as bit),cast({19} as bit),cast({20} as bit),'{21}','{22}')", RequestId, CustomerId, paymenttypeid, advanceamount, installments, totalamountneeded, bankgaranteeamount, subcompartmentid, purposeid, Requestdate, iscreatedcontract, idcopypresented, idbankaccountpresented, pensioncheckpresented, centralregistercopy, powerofattorney, affidavitpresented, confirmationpresented, drdformpresented, declarationofreceiptpresented, agtreementpresented, createdusername, lastupdatedusername);
            string sqlquery = string.Format("INSERT INTO public.\"trContractHeader\"(\"RequestHeaderID\", \"ContractTypeID\", \"CustomerID\",\"PaymentTypeID\", \"AdvanceAmount\", \"Installments\", \"TotalAmountNeeded\", \"BankGaranteeAmount\", \"SubcompartmentID\", \"PurposeID\",\"PilanaID\", \"ContractDate\", \"IDCopyPresented\", \"IDBankAccountPresented\", \"PensionCheckPresented\", \"CentralRegisterCopy\", \"PowerOfAttorney\", \"AffidavitPresented\", \"ConfirmationPresented\", \"DRDFormPresented\", \"DeclarationOfReceiptPresented\", \"AgtreementPresented\", \"CreatedUserName\", \"CreatedDate\", \"LastUpdatedUserName\", \"LastUpdatedDate\",\"RequestNumber\")VALUES({0},{1},{2},{3},cast({4} as money),{5},{6},cast({7} as money),{8},{9},{10},cast('{11}' as date),cast({12} as bit),cast({13} as bit),cast({14} as bit),cast({15} as bit),cast({16} as bit),cast({17} as bit),cast({18} as bit),cast({19} as bit),cast({20} as bit),cast({21} as bit),'{22}',cast('{23}' as date),'{24}',cast('{25}' as date),'{26}') RETURNING \"ContractHeaderID\" ", RequestHeaderId,ContractId, CustomerId, paymenttypeid, advanceamount, installments, totalamountneeded, bankgaranteeamount, subcompartmentid, purposeid,0, Requestdate,  idcopypresented, idbankaccountpresented, pensioncheckpresented, centralregistercopy, powerofattorney, affidavitpresented, confirmationpresented, drdformpresented, declarationofreceiptpresented, agtreementpresented, createdusername, createddate, lastupdatedusername, createddate, RequestNumber);
            int ContractHeaderId = 0;
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
                int PriceDetailID = 0;
                int PlanId = 0;
                sqlquery = string.Format("INSERT INTO public.\"trContractLine\"(\"ContractHeaderID\", \"SubcompartmentID\", \"Month\", \"VidoviEdinecniMeriID\", \"VidoviSortimentiID\", \"Qty\", \"PriceDetailID\",\"Price\",\"PlanID\", \"CreatedUserName\", \"CreatedDate\", \"LastUpdatedUserName\", \"LastUpdatedDate\")VALUES({0},{1},{2},{3},{4},cast({5} as money),{6},cast({7} as money),{8},'{9}',cast('{10}' as date),'{11}',cast('{12}' as date)) RETURNING \"ContractLineID\" ", ContractHeaderId, subcompartmentid, Convert.ToInt32(month), Convert.ToInt32(VidoviEdinecniMeriID), Convert.ToInt32(VidoviSortimentiID), Convert.ToDecimal(Qty), PriceDetailID,Price,0, createdusername, createddate, lastupdatedusername, createddate);
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

            string sqlQuery = "select * from public.\"cdContractType\"";
            List<cdContractType> requestType = new List<cdContractType>();
            try
            {
                using (var conn = new NpgsqlConnection(GetConString()))
                {
                    requestType = conn.Query<cdContractType>(sqlQuery).ToList();
                    ContractTypeID.DataSource = requestType;
                    ContractTypeID.DataValueField = "ContractTypeId";
                    ContractTypeID.DataTextField = "ContractTypeDescription";
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
            dt.Columns.Add(new DataColumn("SubCompartment", typeof(string)));
            dt.Columns.Add(new DataColumn("Month", typeof(string)));
            dt.Columns.Add(new DataColumn("Edinecnamera", typeof(string)));
            dt.Columns.Add(new DataColumn("Vidsortiment", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("Price", typeof(string)));
            dr = dt.NewRow();
            dr["ReginalCenter"] = string.Empty;
            dr["ManagementUnit"] = string.Empty;
            dr["Compartment"] = string.Empty;
            dr["SubCompartment"] = string.Empty;
            dr["Month"] = string.Empty;
            dr["Edinecnamera"] = string.Empty;
            dr["Vidsortiment"] = string.Empty;
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
                        TextBox SubCompartment = (TextBox)ContractGrid.Rows[rowIndex].Cells[1].FindControl("SubCompartment");
                        DropDownList Month = (DropDownList)ContractGrid.Rows[rowIndex].Cells[2].FindControl("Month");
                        DropDownList Edinecnamera = (DropDownList)ContractGrid.Rows[rowIndex].Cells[3].FindControl("Edinecnamera");
                        DropDownList Vidsortiment = (DropDownList)ContractGrid.Rows[rowIndex].Cells[1].FindControl("Vidsortiment");
                        TextBox Qty = (TextBox)ContractGrid.Rows[rowIndex].Cells[2].FindControl("Qty");
                        TextBox Price = (TextBox)ContractGrid.Rows[rowIndex].Cells[2].FindControl("Price");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["ReginalCenter"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["ManagementUnit"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["Compartment"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["SubCompartment"] = string.Empty;
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
                        TextBox SubCompartment = (TextBox)ContractGrid.Rows[rowIndex].Cells[1].FindControl("SubCompartment");
                        DropDownList Month = (DropDownList)ContractGrid.Rows[rowIndex].Cells[2].FindControl("Month");
                        DropDownList Edinecnamera = (DropDownList)ContractGrid.Rows[rowIndex].Cells[3].FindControl("Edinecnamera");
                        DropDownList Vidsortiment = (DropDownList)ContractGrid.Rows[rowIndex].Cells[1].FindControl("Vidsortiment");
                        TextBox Qty = (TextBox)ContractGrid.Rows[rowIndex].Cells[2].FindControl("Qty");
                        TextBox Price = (TextBox)ContractGrid.Rows[rowIndex].Cells[2].FindControl("Price");

                        ReginalCenter.SelectedItem.Text = dt.Rows[i]["ReginalCenter"].ToString();
                        ManagementUnit.SelectedItem.Text = dt.Rows[i]["ManagementUnit"].ToString();
                        Compartment.Text = dt.Rows[i]["Compartment"].ToString();
                        SubCompartment.Text = dt.Rows[i]["SubCompartment"].ToString();
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

        }
    }
}