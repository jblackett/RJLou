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
            //clear everything and hide the error label before the next search
            errorlabel.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView4.DataSource = null;
            GridView4.DataBind();
            GridView5.DataSource = null;
            GridView5.DataBind();
            GridView6.DataSource = null;
            GridView6.DataBind();
            GridView7.DataSource = null;
            GridView7.DataBind();

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                //connect to the datasource and run a sql query, then create a dataset from said query
                conn.Open();
                //for more information on the stored procedure can look at the code through server explorer. Only searched these selected tables.
                string query = "Exec SP_SearchTables @tablenames ='Address_list,Donation,person,note,phone_list,rjl_case,searchTMP',@searchstr='%" + TextBox1.Text + "%'";
                SqlDataAdapter da= new SqlDataAdapter(query, conn);
                DataSet ds= new DataSet();
                da.Fill(ds);

                //display grids based on the results of the data
                if (ds.Tables.Count == 1)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                }
                if (ds.Tables.Count == 2)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    GridView2.DataSource = ds.Tables[1];
                    GridView2.DataBind();
                    GridView2.Visible = true;
                   
                }
                if (ds.Tables.Count == 3)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    GridView2.DataSource = ds.Tables[1];
                    GridView2.DataBind();
                    GridView2.Visible = true;
                    GridView3.DataSource = ds.Tables[2];
                    GridView3.DataBind();
                    GridView3.Visible = true;
                }
                if (ds.Tables.Count == 4)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    GridView2.DataSource = ds.Tables[1];
                    GridView2.DataBind();
                    GridView2.Visible = true;
                    GridView3.DataSource = ds.Tables[2];
                    GridView3.DataBind();
                    GridView3.Visible = true;
                    GridView4.DataSource = ds.Tables[3];
                    GridView4.DataBind();
                    GridView4.Visible = true;

                }
                if (ds.Tables.Count == 5)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    GridView2.DataSource = ds.Tables[1];
                    GridView2.DataBind();
                    GridView2.Visible = true;
                    GridView3.DataSource = ds.Tables[2];
                    GridView3.DataBind();
                    GridView3.Visible = true;
                    GridView4.DataSource = ds.Tables[3];
                    GridView4.DataBind();
                    GridView4.Visible = true;
                    GridView5.DataSource = ds.Tables[4];
                    GridView5.DataBind();
                    GridView5.Visible = true;

                }
                if (ds.Tables.Count == 6)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    GridView2.DataSource = ds.Tables[1];
                    GridView2.DataBind();
                    GridView2.Visible = true;
                    GridView3.DataSource = ds.Tables[2];
                    GridView3.DataBind();
                    GridView3.Visible = true;
                    GridView4.DataSource = ds.Tables[3];
                    GridView4.DataBind();
                    GridView4.Visible = true;
                    GridView5.DataSource = ds.Tables[4];
                    GridView5.DataBind();
                    GridView5.Visible = true;
                    GridView6.DataSource = ds.Tables[5];
                    GridView6.DataBind();
                    GridView6.Visible = true;
                }
                if (ds.Tables.Count == 7)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    GridView2.DataSource = ds.Tables[1];
                    GridView2.DataBind();
                    GridView2.Visible = true;
                    GridView3.DataSource = ds.Tables[2];
                    GridView3.DataBind();
                    GridView3.Visible = true;
                    GridView4.DataSource = ds.Tables[3];
                    GridView4.DataBind();
                    GridView4.Visible = true;
                    GridView5.DataSource = ds.Tables[4];
                    GridView5.DataBind();
                    GridView5.Visible = true;
                    GridView6.DataSource = ds.Tables[5];
                    GridView6.DataBind();
                    GridView6.Visible = true;
                    GridView7.DataSource = ds.Tables[6];
                    GridView7.DataBind();
                    GridView7.Visible = true;
                }
                else if (ds.Tables.Count == 0)
                {
                    errorlabel.Text = "No results found";
                    errorlabel.Visible =true;
                }
            }
        }
    }

}