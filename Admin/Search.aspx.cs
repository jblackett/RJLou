using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RJLou.Admin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void search1(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                //string query = "select * from Address_List where Street_Address like'%" + TextBox1.Text + "%'";
                string query = "Exec SP_SearchTables @tablenames ='%',@searchstr='%" + TextBox1.Text + "%'";
                SqlDataAdapter da= new SqlDataAdapter(query, conn);
                DataSet ds= new DataSet();
                da.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;


                //ds.Read();
                //rep_bind();
                //GridView1.Visible = true;
                //TextBox1.Text = "";
                //Label2.Text="";
                
            }
        }
    }

}