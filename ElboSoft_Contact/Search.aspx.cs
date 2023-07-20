using Dapper;
using ElboSoft_Contact.Models;
using Npgsql;
using System;
using System.Collections.Generic;
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
                GetGridData();
            }

        }
        private void GetGridData()
        {
            string sqlQuery = "select \"RequestHeaderID\", \"CustomerID\", \"PurposeID\",\"RequestDate\", \"IsCreatedContract\",\"RequestNumber\" from public.\"trRequestHeader\" order by \"RequestHeaderID\";";
            List<SearchData> searchData = new List<SearchData>();
            try
            {
                using (var conn = new NpgsqlConnection(GetConString()))
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
                using (var conn = new NpgsqlConnection(GetConString()))
                {
                    customers = conn.Query<cdCustomer>(sqlQuery).ToList();
                    Customer.DataSource = customers;
                    Customer.DataValueField = "CustomerID";
                    Customer.DataTextField = "CustomerDescription";
                    Customer.DataBind();
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
                using (var conn = new NpgsqlConnection(GetConString()))
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
        protected void SearchGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void deleterecord_Click(object sender, EventArgs e)
        {
            var id=hiddenrquestno.Value;

        }
    }
}