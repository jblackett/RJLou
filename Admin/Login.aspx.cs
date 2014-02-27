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
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PersonID"] != null)
                Response.Redirect("Default.aspx");

            if (Page.IsPostBack)
            {
                ErrorText.Visible = true;
            }
        }

        protected void verifyInfo(object sender, EventArgs e)
        {
            string email = Email.Text;
            string password = Password.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return;

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                string sql = @"
                    SELECT      p.Person_ID
                    FROM        Internal_User iu
                    INNER JOIN  Person p ON iu.Person_ID = p.Person_ID
                    WHERE       p.Email = @Email
                    AND         iu.Password = @Password";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Email", email);
                cmd.Parameters.AddWithValue("Password", password);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Session["PersonID"] = read["Person_ID"].ToString();
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
}