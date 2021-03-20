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
    public partial class Login : System.Web.UI.Page
    {
        private SqlConnection con;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var username = TextBox1.Text;
            var password = TextBox2.Text;

            string constring = ConfigurationManager.ConnectionStrings["studentconn"].ToString();
            con = new SqlConnection(constring);

            //if (username == "Admin" && password == "Admin")
            //{
            //    Response.Write("<script>alert('Login Done!')</script>");                
            //    Response.Redirect("Dashboard.aspx");
            //    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('hello')", true);
            //}
            var listCustomer = GetStudent(username);

            if(listCustomer.Count > 0)
            {
               if(listCustomer[0].Email == username && listCustomer[0].Password == password)
                {
                    Session["UserName"] = username;

                    Response.Redirect("Dashboard.aspx");
                }
            }

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
                        Email = Convert.ToString(dr["Email"])
                    });
            }
            return studentlist;
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignIn.aspx");
        }
    }
}