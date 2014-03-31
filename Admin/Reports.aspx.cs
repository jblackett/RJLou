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
        //String variable to hold the desired report query (as determined by button commandarguments) to pass on
        //to the SqlDataAdapter. By default this variable is null because we don't want any information displayed until
        //a report type is selected.
        string Query = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Constants.DSN);
            SqlDataAdapter cmd = new SqlDataAdapter(Query, conn);
        }

        protected internal void ReportClick(object sender, EventArgs e)
        {
            string Query = (((LinkButton)sender).CommandArgument).ToString();

            switch (Query)
            {
                
                //Thought this was how I could set the query value based on which report button the user clicks.
                //The default.aspx.cs page for SwitchCaseList has <case "all"> <case "open"> and I'm not sure as to what
                //the case is pointing to. I was trying to replicate it here with the <reports "1"> and so on...
                reports "1":
                    Query = "SELECT Status, Case_ID FROM RJL_Case ORDER BY Status";
                    DataSet ds = new DataSet();
                    cmd.Fill(ds);
                    ReportsRepeater.DataSource = ds;
                    ReportsRepeater.DataBind();
                    break;
                reports "2":
                    Query = "SELECT Description, C.Status, C.Case_ID FROM RJL_Case C JOIN CASE_CHARGE CC ON C.Case_ID = CC.Case_ID JOIN CHARGE CH ON CH.Charge_ID = CC.Charge_ID ORDER BY Description";
                    DataSet ds = new DataSet();
                    cmd.Fill(ds);
                    ReportsRepeater.DataSource = ds;
                    ReportsRepeater.DataBind();
                    break;
                reports "3":
                    Query = "SELECT Race, C.Status, C.Case_ID FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY Race";
                    DataSet ds = new DataSet();
                    cmd.Fill(ds);
                    ReportsRepeater.DataSource = ds;
                    ReportsRepeater.DataBind();
                    break;
                reports "4":
                    Query = "SELECT Gender, C.Status, C.Case_ID FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY Gender";
                    DataSet ds = new DataSet();
                    cmd.Fill(ds);
                    ReportsRepeater.DataSource = ds;
                    ReportsRepeater.DataBind();
                    break;
                reports "5":
                    Query = "SELECT Date_Of_Birth, C.Status, C.Case_ID FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY Date_Of_Birth DESC";
                    DataSet ds = new DataSet();
                    cmd.Fill(ds);
                    ReportsRepeater.DataSource = ds;
                    ReportsRepeater.DataBind();
                    break;
                default:
                    Query = "";
                    break;
            }
        }
    }
}
