using RJLou.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RJLou.Admin
{
    public partial class Reports : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Report1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT Status,Case_ID FROM RJLCase ORDER BY Status WHERE Status = @Status AND Case_ID = @CaseID";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Status", status);
                cmd.Parameters.AddWithValue("CaseID", caseID);

                SqlDataReader read = cmd.ExecuteReader();

                if(read.Read())
                {
                    Case result = new Case()
                    {
                        Status = read["Status"].ToString(),
                        CaseID = Convert.ToInt32(read["Case_ID"])
                    };                    
                }
            }
        }

        protected void Report2_Click(object sender, EventArgs e)
        {
            string sql = "SELECT Description, a.Case_ID, Status FROM RJL_CASE a JOIN CASE_CHARGE b ON a.Case_ID = b.Case_ID JOIN CHARGE c ON c.Charge_ID = b.Charge_ID ORDER BY Description, a.Case_IDWHERE Description = @Description AND Case_ID = @CaseID AND Status = @Status";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Status", status);
                cmd.Parameters.AddWithValue("CaseID", caseID);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Case result = new Case()
                    {
                        Status = read["Status"].ToString(),
                        CaseID = Convert.ToInt32(read["Case_ID"])
                    };
                }
            }
        }

        protected void Report3_Click(object sender, EventArgs e)
        {
            string sql = "SELECT Race, Status, a.Case_ID FROM RJL_CASE a JOIN CASE_FILE b ON a.Case_ID = b.Case_ID JOIN PERSON c ON c.Person_ID = b.Person_ID JOIN OFFENDER d ON d.Person_ID = c.Person_ID ORDER BY Race WHERE Race = @Race AND CaseID = @CaseID AND Status = @Status"

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Race", race);
                cmd.Parameters.AddWithValue("CaseID", caseID);
                cmd.Parameters.AddWithValue("Status", status);
                
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                
                }
            }
        }

        protected void Report4_Click(object sender, EventArgs e)
        {
            string sql = "SELECT Gender, Status, a.Case_ID FROM RJL_CASE a JOIN CASE_FILE b ON a.Case_ID = b.Case_ID JOIN PERSON c ON c.Person_ID = b.Person_ID JOIN OFFENDER d ON d.Person_ID = c.Person_ID ORDER BY GenderWHERE Gender = @Gender AND Case_ID = @CaseID AND Status = @Status";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Status", status);
                cmd.Parameters.AddWithValue("CaseID", caseID);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Case result = new Case()
                    {
                        Status = read["Status"].ToString(),
                        CaseID = Convert.ToInt32(read["Case_ID"])
                    };
                }
            }
        }

        protected void Report5_Click(object sender, EventArgs e)
        {
            string sql = "SELECT Date_Of_Birth, Status, a.Case_ID FROM RJL_CASE a JOIN CASE_FILE b ON a.Case_ID = b.Case_ID JOIN PERSON c ON c.Person_ID = b.Person_ID JOIN OFFENDER d ON d.Person_ID = c.Person_ID ORDER BY Date_Of_Birth WHERE Date_Of_Birth = @age AND Case_ID = @CaseID AND Status = @status";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Status", status);
                cmd.Parameters.AddWithValue("CaseID", caseID);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Case result = new Case()
                    {
                        Status = read["Status"].ToString(),
                        CaseID = Convert.ToInt32(read["Case_ID"])
                    };
                }
            }
        }
    }
}
