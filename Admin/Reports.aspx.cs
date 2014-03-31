using RJLou.Classes;
using RJLou.Admin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RJLou
{
    public partial class Reports : System.Web.UI.Page
    {
        //String variable to hold the desired report query (as determined by button commandarguments) to pass on
        //to the SqlDataAdapter. By default this variable is null because we don't want any information displayed until
        //a report type is selected.
        string Query = "";
        SqlConnection conn = new SqlConnection(Constants.DSN);

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected internal void ReportClick(object sender, EventArgs e)
        {
            int QueryType = Convert.ToInt32((((LinkButton)sender).CommandArgument));
            LinkButton reportClick = sender as LinkButton;
            Repeater ReportsRepeater = (Repeater)reportClick.Parent.FindControl("ReportsRepeater");



            switch (QueryType)
            {

                //Thought this was how I could set the query value based on which report button the user clicks.
                //The default.aspx.cs page for SwitchCaseList has <case "all"> <case "open"> and I'm not sure as to what
                //the case is pointing to. I was trying to replicate it here with the <reports "1"> and so on...
                case 1:
                    Query = "SELECT Status, Case_ID FROM RJL_Case ORDER BY Status";
                    SqlDataAdapter cmd = new SqlDataAdapter(Query, conn);
                    DataSet ds = new DataSet();
                    cmd.Fill(ds);
                    ReportsRepeater.DataSource = ds;
                    ReportsRepeater.DataBind();
                    break;
                case 2:
                    Query = "SELECT Description, C.Status, C.Case_ID FROM RJL_Case C JOIN CASE_CHARGE CC ON C.Case_ID = CC.Case_ID JOIN CHARGE CH ON CH.Charge_ID = CC.Charge_ID ORDER BY Description";
                    SqlDataAdapter cmd2 = new SqlDataAdapter(Query, conn);
                    DataSet ds2 = new DataSet();
                    cmd2.Fill(ds2);
                    ReportsRepeater.DataSource = ds2;
                    ReportsRepeater.DataBind();
                    break;
                case 3:
                    Query = "SELECT Race, C.Status, C.Case_ID FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY Race";
                    SqlDataAdapter cmd3 = new SqlDataAdapter(Query, conn);
                    DataSet ds3 = new DataSet();
                    cmd3.Fill(ds3);
                    ReportsRepeater.DataSource = ds3;
                    ReportsRepeater.DataBind();
                    break;
                case 4:
                    Query = "SELECT Gender, C.Status, C.Case_ID FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY Gender";
                    SqlDataAdapter cmd4 = new SqlDataAdapter(Query, conn);
                    DataSet ds4 = new DataSet();
                    cmd4.Fill(ds4);
                    ReportsRepeater.DataSource = ds4;
                    ReportsRepeater.DataBind();
                    break;
                case 5:
                    Query = "SELECT Date_Of_Birth, C.Status, C.Case_ID FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY Date_Of_Birth DESC";
                    SqlDataAdapter cmd5 = new SqlDataAdapter(Query, conn);
                    DataSet ds5 = new DataSet();
                    cmd5.Fill(ds5);
                    ReportsRepeater.DataSource = ds5;
                    ReportsRepeater.DataBind();
                    break;
                default:
                    Query = "";
                    break;
            }
        }

        protected void ReportsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label filterItemLabel = (Label)e.Item.FindControl("FilterItem");
            Label caseIDLabel = (Label)e.Item.FindControl("CaseID");
            Label statusLabel = (Label)e.Item.FindControl("Status");

            filterItemLabel.Text = e.Item.DataItem.ToString();
            /*SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            SqlDataReader read = cmd.ExecuteReader();*/

        }
    }
}
