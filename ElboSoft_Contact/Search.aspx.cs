using Dapper;
using ElboSoft_Contact.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElboSoft_Contact
{
    public partial class Search : System.Web.UI.Page
    {
        private string GetConString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetRequestType();
                GetCustomer();
                GetGridData(RequestType.SelectedItem.Value, RequestNumber.Text.Trim(), Customer.SelectedItem.Value, RequestDate.Text, ContractCreated.Checked);
            } 
        }
        private void GetGridData(string RequestTypeId,string RequestNumber,string CustomerTyperId, string RequestDate,bool IsContractCreated)
        {
            string WhereCondition = string.Empty;
            string sqlQuery = string.Empty;
            if (RequestTypeId!="0")
            {
                WhereCondition = WhereCondition + " and \"RequestTypeID\"=" + Convert.ToInt32(RequestTypeId);
            }
            if (!string.IsNullOrEmpty(RequestNumber))
            {
                WhereCondition = WhereCondition + " and \"RequestNumber\"='" + RequestNumber+"'" ;
            }
            if (CustomerTyperId!="0")
            {
                WhereCondition = WhereCondition + " and \"CustomerID\"=" + Convert.ToInt32(CustomerTyperId);
            }
            if (!string.IsNullOrEmpty(RequestDate))
            {
                WhereCondition = WhereCondition + " and \"RequestDate\"=cast('" + RequestDate + "' as date)";
            }
            if (IsContractCreated)
            {
                WhereCondition = WhereCondition + " and \"IsCreatedContract\"=cast(1 as bit)";
            }
            if (!string.IsNullOrEmpty(WhereCondition))
            {
                sqlQuery = "select * from(select \"RequestHeaderID\",0 as \"ContractHeaderID\", \"CustomerID\",\"RequestTypeID\", \"PurposeID\",\"RequestDate\", \"IsCreatedContract\",\"RequestNumber\" from public.\"trRequestHeader\"  union select \"RequestHeaderID\",\"ContractHeaderID\", \"CustomerID\",\"ContractTypeID\", \"PurposeID\",\"ContractDate\", cast(1 as bit),\"RequestNumber\" from public.\"trContractHeader\" where \"RequestHeaderID\" not in(select \"RequestHeaderID\" from public.\"trRequestHeader\")) as tbl  where 1=1  " + WhereCondition +" order by \"RequestHeaderID\";";
            }
            else
            {
                 sqlQuery = "select * from(select \"RequestHeaderID\",0 as \"ContractHeaderID\", \"CustomerID\",\"RequestTypeID\", \"PurposeID\",\"RequestDate\", \"IsCreatedContract\",\"RequestNumber\" from public.\"trRequestHeader\"  union select \"RequestHeaderID\",\"ContractHeaderID\", \"CustomerID\",\"ContractTypeID\", \"PurposeID\",\"ContractDate\", cast(1 as bit),\"RequestNumber\" from public.\"trContractHeader\" where \"RequestHeaderID\" not in(select \"RequestHeaderID\" from public.\"trRequestHeader\")) as tbl  order by \"RequestHeaderID\";";
            }
            
            List<SearchData> searchData = new List<SearchData>();
            try
            {
                using (var conn = new OdbcConnection(GetConString()))
                {
                    searchData = conn.Query<SearchData>(sqlQuery).ToList();

                    SearchGrid.DataSource = searchData;
                    SearchGrid.DataBind();
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
                    Customer.DataSource = customers;
                    Customer.DataValueField = "CustomerID";
                    Customer.DataTextField = "CustomerDescription";
                    Customer.DataBind();
                    Customer.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

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
                    RequestType.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }
        protected void SearchGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void deleterecord_Click(object sender, EventArgs e)
        {
            int id=Convert.ToInt32(hiddenrquestno.Value);
            List<cdRequestType> requestType = new List<cdRequestType>();
            try
            {
                using (var conn = new OdbcConnection(GetConString()))
                {
                 var   deletedRequest = conn.Query<cdRequestType>("delete from public.\"trRequestLine\" where \"RequestHeaderID\"="+id);
                    deletedRequest = conn.Query<cdRequestType>("delete from public.\"trRequestHeader\" where \"RequestHeaderID\"=" + id);
                }
                GetGridData(RequestType.SelectedItem.Value, RequestNumber.Text.Trim(), Customer.SelectedItem.Value, RequestDate.Text, ContractCreated.Checked);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetGridData(RequestType.SelectedItem.Value, RequestNumber.Text.Trim(), Customer.SelectedItem.Value, RequestDate.Text, ContractCreated.Checked);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            RequestType.SelectedIndex = 0;
            RequestNumber.Text = "";
            Customer.SelectedIndex = 0;
            RequestDate.Text = "";
            ContractCreated.Checked = false;
        }
    }
}