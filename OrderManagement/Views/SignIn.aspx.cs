using OrderManagement.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderManagement.Views
{
    public partial class SignIn : System.Web.UI.Page
    {
        private SqlConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('hello')", true);
            // Response.Redirect("Login.aspx");

            
             string constring = ConfigurationManager.ConnectionStrings["studentconn"].ToString();
             con = new SqlConnection(constring);

            Customer customer = new Customer();
            customer.Name = Nametxt.Text.ToString();
            customer.Address = Addresstxt.Text.ToString();
            customer.City = Citytxt.Text.ToString();
            customer.Pincode = Pincodetxt.Text.ToString();
            customer.Mobile = Mobiletxt.Text.ToString();
            customer.Email = Emailtxt.Text.ToString();
            customer.Password = Passwordtxt.Text.ToString();
           var response =  AddCustomer(customer);
            if (response)
            {
                Response.Redirect("Login.aspx");
            }

        }

        public bool AddCustomer(Customer smodel)
        {
          
            SqlCommand cmd = new SqlCommand("AddNewCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@name", smodel.Name);
            cmd.Parameters.AddWithValue("@city", smodel.City);
            cmd.Parameters.AddWithValue("@address", smodel.Address);
            cmd.Parameters.AddWithValue("@pincode", smodel.Pincode);
            cmd.Parameters.AddWithValue("@mobile", smodel.Mobile);
            cmd.Parameters.AddWithValue("@email", smodel.Email);
            cmd.Parameters.AddWithValue("@password", smodel.Password);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}