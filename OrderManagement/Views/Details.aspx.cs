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
    public partial class Details : System.Web.UI.Page
    {
        private SqlConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                var username = Session["UserName"].ToString();
                string constring = ConfigurationManager.ConnectionStrings["studentconn"].ToString();
                con = new SqlConnection(constring);
                var listCustomer = GetStudent(username);
                if (listCustomer.Count > 0)
                {
                    txt_Nametxt.Text = listCustomer[0].Name;
                    txt_Addresstxt.Text = listCustomer[0].Address;
                    txt_Citytxt.Text = listCustomer[0].City;
                    txt_Pincodetxt.Text = listCustomer[0].Pincode;
                    txt_Mobiletxt.Text = listCustomer[0].Mobile;
                    txt_Emailtxt.Text = listCustomer[0].Email;
                    txt_Passwordtxt.Text = listCustomer[0].Password;
                }
            }
                
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string constring = ConfigurationManager.ConnectionStrings["studentconn"].ToString();
            con = new SqlConnection(constring);

           // var value  = text1.Text; 

            Customer customer = new Customer();
            customer.Name = txt_Nametxt.Text.ToString();
            customer.Address = txt_Addresstxt.Text.ToString();
            customer.City = txt_Citytxt.Text.ToString();
            customer.Pincode = txt_Pincodetxt.Text.ToString();
            customer.Mobile = txt_Mobiletxt.Text.ToString();
            customer.Email = txt_Emailtxt.Text.ToString();
            customer.Password = txt_Passwordtxt.Text.ToString();
            var response = UpdateCustomer(customer);
            if (response)
            {
                Response.Redirect("Login.aspx");
            }
        }

        public bool UpdateCustomer(Customer smodel)
        {

            SqlCommand cmd = new SqlCommand("UpdateCustomer", con);
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




        public List<Customer> GetStudent(string email)
        {
            List<Customer> studentlist = new List<Customer>();

            SqlCommand cmd = new SqlCommand("CustomerDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", email);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                studentlist.Add(
                    new Customer
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        City = Convert.ToString(dr["City"]),
                        Address = Convert.ToString(dr["Address"]),
                        Password = Convert.ToString(dr["Password"]),
                        Email = Convert.ToString(dr["Email"]),
                        Mobile = Convert.ToString(dr["Mobile"]),
                        Pincode = Convert.ToString(dr["Pincode"])
                    });
            }
            return studentlist;
        }


    }
}