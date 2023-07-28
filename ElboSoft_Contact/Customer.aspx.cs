using Dapper;
using ElboSoft_Contact.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElboSoft_Contact
{
    public partial class Customer : System.Web.UI.Page
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
                GetCUstomerType();
                if (Request.QueryString["CustomertId"] != null && !string.IsNullOrEmpty(Request.QueryString["CustomertId"].ToString()))
                {
                    string customerId = Request.QueryString["CustomertId"].ToString();
                    save.Visible = false;
                    BindCustomerdata(customerId);
                }
                else
                {
                    Update.Visible = false;
                }
                ViewState["previouspage"] = Request.UrlReferrer.AbsoluteUri.ToString();
            }

        }
        private void GetCUstomerType()
        {
            string sqlQuery = "select * from public.\"CustomerType\"";
            List<CustomerType> customerType = new List<CustomerType>();
            try
            {

                using (var conn = new OdbcConnection(GetConString()))
                {
                    customerType = conn.Query<CustomerType>(sqlQuery).ToList();
                    CustomerType.DataSource = customerType;
                    CustomerType.DataValueField = "ID";
                    CustomerType.DataTextField = "Description";
                    CustomerType.DataBind();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }
        private void BindCustomerdata(string CustomerId)
        {
            string sqlQuery = string.Format("select * from public.\"cdCustomer\" where \"CustomerID\"={0}", CustomerId);
            cdCustomer Customer = new cdCustomer();
            try
            {
                using (var conn = new OdbcConnection(GetConString()))
                {
                    Customer = conn.Query<cdCustomer>(sqlQuery).FirstOrDefault();
                    CustomerDescription.Text = Customer.CustomerDescription;
                    CustomerType.SelectedValue = Customer.CustomerTypeID.ToString();
                    IsPerson.Checked = Convert.ToBoolean(Customer.IsPerson);
                    SubmissionDeadline.Text = Customer.SubmissionDeadline.ToString();
                    PersonalNumberID.Text = Customer.PersonalNumberID.ToString();
                    TaxNumber.Text = Customer.TaxNumber.ToString();
                    BankAccountNumber.Text = Customer.BankAccountNumber.ToString();
                    Address.Text = Customer.Address;
                    PhoneNumber.Text = Customer.PhoneNumber;
                    Email.Text = Customer.Email;
                    ContactPerson.Text = Customer.ContactPerson;
                    IDNumebr.Text = Customer.IDNumebr;
                    IssuedBy.Text = Customer.IssuedBy;

                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }


        protected void save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SubmissionDeadline.Text) || string.IsNullOrEmpty(PersonalNumberID.Text) || string.IsNullOrEmpty(TaxNumber.Text) || string.IsNullOrEmpty(BankAccountNumber.Text) || string.IsNullOrEmpty(CustomerDescription.Text) || string.IsNullOrEmpty(Address.Text) || string.IsNullOrEmpty(CustomerType.SelectedItem.Value) || string.IsNullOrEmpty(PhoneNumber.Text) || string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(IDNumebr.Text) || string.IsNullOrEmpty(IssuedBy.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Kindly Fill all the values.')", true);
                return;
            }
            else
            {
                string cdescription = CustomerDescription.Text;
                int cutomertypeid = Convert.ToInt32(CustomerType.SelectedItem.Value);
                int isperson = Convert.ToInt32(IsPerson.Checked);
                int submissiondeadline = Convert.ToInt32(SubmissionDeadline.Text);
                int personlanumberid = Convert.ToInt32(PersonalNumberID.Text);
                int taxnumber = Convert.ToInt32(TaxNumber.Text);
                int bankaccountnumber = Convert.ToInt32(BankAccountNumber.Text);
                string address = Address.Text;
                string phonenumber = PhoneNumber.Text;
                string email = Email.Text;
                string contractperson = ContactPerson.Text;
                string idnumber = IDNumebr.Text;
                string issuedby = IssuedBy.Text;

                try
                {
                    using (var conn = new OdbcConnection(GetConString()))
                    {
                        string SqlQuery = string.Format("INSERT INTO public.\"cdCustomer\"(\"CustomerDescription\", \"CustomerTypeID\", \"IsPerson\", \"SubmissionDeadline\", \"PersonalNumberID\", \"TaxNumber\", \"BankAccountNumber\", \"Address\", \"PhoneNumber\", \"ContactPerson\", \"IDNumebr\", \"IssuedBy\", \"Email\")VALUES('{0}',{1},cast({2} as bit),{3},{4},{5},{6},'{7}','{8}','{9}','{10}','{11}','{12}'); ", cdescription, cutomertypeid, isperson, submissiondeadline, personlanumberid, taxnumber, bankaccountnumber, address, phonenumber, contractperson,idnumber, email, issuedby);
                        var result = conn.Query(SqlQuery);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Customer Created Successfully')", true);
                        Response.Redirect(ViewState["previouspage"].ToString()+".aspx");
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }

        }

        protected void Update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SubmissionDeadline.Text) || string.IsNullOrEmpty(PersonalNumberID.Text) || string.IsNullOrEmpty(TaxNumber.Text) || string.IsNullOrEmpty(BankAccountNumber.Text) || string.IsNullOrEmpty(CustomerDescription.Text) || string.IsNullOrEmpty(Address.Text) || string.IsNullOrEmpty(CustomerType.SelectedItem.Value) || string.IsNullOrEmpty(PhoneNumber.Text) || string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(IDNumebr.Text) || string.IsNullOrEmpty(IssuedBy.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Kindly Fill all the values.')", true);
                return;
            }
            else
            {
                string cdescription = CustomerDescription.Text;
                int cutomertypeid = Convert.ToInt32(CustomerType.SelectedItem.Value);
                int isperson = Convert.ToInt32(IsPerson.Checked);
                int submissiondeadline = Convert.ToInt32(SubmissionDeadline.Text);
                int personlanumberid = Convert.ToInt32(PersonalNumberID.Text);
                int taxnumber = Convert.ToInt32(TaxNumber.Text);
                int bankaccountnumber = Convert.ToInt32(BankAccountNumber.Text);
                string address = Address.Text;
                string phonenumber = PhoneNumber.Text;
                string email = Email.Text;
                string contractperson = ContactPerson.Text;
                string idnumber = IDNumebr.Text;
                string issuedby = IssuedBy.Text;
                int customerid = Convert.ToInt32(Request.QueryString["CustomertId"]);
                try
                {
                    using (var conn = new OdbcConnection(GetConString()))
                    {
                        string SqlQuery = string.Format("UPDATE public.\"cdCustomer\" SET \"CustomerDescription\" ='{0}', \"CustomerTypeID\" ={1}, \"IsPerson\" =cast({2} as bit), \"SubmissionDeadline\" ={3}, \"PersonalNumberID\" ={4}, \"TaxNumber\" ={5}, \"BankAccountNumber\" ={6}, \"Address\" ='{7}', \"PhoneNumber\" ='{8}', \"ContactPerson\" ='{9}', \"IDNumebr\" ='{10}', \"IssuedBy\" ='{11}', \"Email\" ='{12}' WHERE \"CustomerID\"='{13}'", cdescription, cutomertypeid, isperson, submissiondeadline, personlanumberid, taxnumber, bankaccountnumber, address, phonenumber, email, contractperson, idnumber, issuedby, customerid);
                        var result = conn.Query(SqlQuery);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Customer Updated Successfully')", true);
                        Response.Redirect(ViewState["previouspage"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
        }
    }
}