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
    public partial class Order : System.Web.UI.Page
    {
        DataTable table = new DataTable();
        List<OrderModal> myOrder = new List<OrderModal>();
        OrderModal modal = new OrderModal();
        private SqlConnection con;

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox2.Text = Session["UserName"].ToString();
            string constring = ConfigurationManager.ConnectionStrings["studentconn"].ToString();
            con = new SqlConnection(constring);
            //TextBox2.Text = "Navdeep";
            if (!this.IsPostBack)
            {

                table.Columns.Add("Product");
                table.Columns.Add("Quantity");
                table.Columns.Add("Price Per Unit");
                table.Columns.Add("Total Price");


                var orderItem = GetOrderItem(Session["UserName"].ToString());
                for (int i = 0; i < orderItem.Count; i++)
                {
                    table.Rows.Add(orderItem[i].Product, orderItem[i].ProductQty, orderItem[i].price, orderItem[i].Total);
                }


                DataGrid1.DataSource = table;
                DataGrid1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            var listProduct = GetProduct(product_txt.Text);





            modal.Product = product_txt.Text;
            modal.price = listProduct[0].Price;
            modal.ProductQty = quantity_txt.Text;

            var total = int.Parse(listProduct[0].Price) * int.Parse(quantity_txt.Text.ToString());
            modal.Total = total.ToString();
            modal.Name = Session["UserName"].ToString();

            string constring = ConfigurationManager.ConnectionStrings["studentconn"].ToString();
            con = new SqlConnection(constring);

            var listCustomer = AddOrderItem(modal);

            if (listCustomer)
            {
                var orderItem = GetOrderItem(modal.Name);
                table.Columns.Add("Product");
                table.Columns.Add("Quantity");
                table.Columns.Add("Price Per Unit");
                table.Columns.Add("Total Price");
                for (int i= 0; i< orderItem.Count; i++)
                {
                    
                    table.Rows.Add(orderItem[i].Product, orderItem[i].ProductQty, orderItem[i].price, orderItem[i].Total);
                }

                DataGrid1.DataSource = table;
                DataGrid1.DataBind();
            }


            product_txt.Text = "";
            quantity_txt.Text = "";
            //total_txt.Text = "" ;


        }


        public List<OrderModal> GetOrderItem(string name)
        {
            List<OrderModal> orderList = new List<OrderModal>();

            SqlCommand cmd = new SqlCommand("OrderItems", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                orderList.Add(
                    new OrderModal
                    {                       
                        Product = Convert.ToString(dr["Product"]),
                        ProductQty = Convert.ToString(dr["Product_Qty"]),
                        price = Convert.ToString(dr["Price"]),
                        Total = Convert.ToString(dr["Total"]),                      
                    });
            }
            return orderList;
        }

        public bool AddOrderItem(OrderModal smodel)
        {

            SqlCommand cmd = new SqlCommand("AddItem", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@name", smodel.Name);
            cmd.Parameters.AddWithValue("@product", smodel.Product);
            cmd.Parameters.AddWithValue("@price", smodel.price);
            cmd.Parameters.AddWithValue("@product_qty", smodel.ProductQty);
            cmd.Parameters.AddWithValue("@total", smodel.Total);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteItem(OrderModal smodel)
        {
            SqlCommand cmd = new SqlCommand("DeleteItem", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", smodel.Name);
            
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }


        protected void Button2_Click(object sender, EventArgs e)
        {

            modal.Name = Session["UserName"].ToString();

            string constring = ConfigurationManager.ConnectionStrings["studentconn"].ToString();
            con = new SqlConnection(constring);

            var listCustomer = DeleteItem(modal);

            table.Columns.Add("Product");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price Per Unit");
            table.Columns.Add("Total Price");
            DataGrid1.DataSource = table;
            DataGrid1.DataBind();

            product_txt.Text = "";
            quantity_txt.Text = "";
        }

        public List<Product> GetProduct(string productName)
        {
            List<Product> productlist = new List<Product>();

            SqlCommand cmd = new SqlCommand("ProductDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", productName);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                productlist.Add(
                    new Product
                    {
                        Name = Convert.ToString(dr["Name"]),
                        Price = Convert.ToString(dr["Price"])
                    });
            }
            return productlist;
        }


    }
}