using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ElboSoft_Contact.CommonClasses
{
    public class CommonClass
    {
        public void BindDropDown<T>(DropDownList dropdownList, string sqlQuery, List<T> list, string datavalueField, string dataTextField) where T : new()
        {

            try
            {
                using (var conn = new NpgsqlConnection(GetConString()))
                {
                    list = conn.Query<T>(sqlQuery).ToList();
                    dropdownList.DataSource = list;
                    dropdownList.DataValueField = datavalueField;
                    dropdownList.DataTextField = dataTextField;
                    dropdownList.DataBind();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }
        private string GetConString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}