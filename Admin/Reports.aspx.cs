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
        string selectQuerySegment = "";
        string fromQuerySegment = " FROM RJL_Case C JOIN CASE_CHARGE CC ON C.Case_ID = CC.Case_ID JOIN CHARGE CH ON CH.Charge_ID = CC.Charge_ID " +
                           "JOIN Case_File CF ON CF.Case_ID = C.Case_ID JOIN Case_Manager CM ON CM.Case_ID = C.Case_ID JOIN CASE_NOTE CN ON CN.CASE_ID = C.Case_ID " +
                           "JOIN Document D ON D.Case_ID = C.Case_ID JOIN Person P ON CF.Person_ID = P.Person_ID " +
                           "JOIN OFFENDER O ON O.Person_ID = P.Person_ID ";
        string whereQuerySegment = "";
        string fullQuery = "";
        SqlConnection conn = new SqlConnection(Constants.DSN);

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected internal void ReportClick(object sender, EventArgs e)
        {
            int QueryType = Convert.ToInt32((((LinkButton)sender).CommandArgument));
            LinkButton reportClick = sender as LinkButton;
            Repeater ReportsRepeater = (Repeater)reportClick.Parent.FindControl("ReportsRepeater");
            Label secondColumn = (Label)reportClick.Parent.FindControl("secondColumn");
            Label thirdColumn = (Label)reportClick.Parent.FindControl("thirdColumn");
            SqlDataSource dynamicQuery = (SqlDataSource)reportClick.Parent.FindControl("DynamicSqlDataSource");



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
                    ReportsRepeater.DataSourceID = "StatusSqlDataSource";

                    break;
                case 2:
                    Query = "SELECT Description, C.Status, C.Case_ID FROM RJL_Case C JOIN CASE_CHARGE CC ON C.Case_ID = CC.Case_ID JOIN CHARGE CH ON CH.Charge_ID = CC.Charge_ID ORDER BY Description";
                    SqlDataAdapter cmd2 = new SqlDataAdapter(Query, conn);
                    DataSet ds2 = new DataSet();
                    cmd2.Fill(ds2);
                    ReportsRepeater.DataSourceID = "OffenseSqlDataSource";
                    break;
                case 3:
                    Query = "SELECT Race, C.Status, C.Case_ID FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY Race";
                    SqlDataAdapter cmd3 = new SqlDataAdapter(Query, conn);
                    DataSet ds3 = new DataSet();
                    cmd3.Fill(ds3);
                    ReportsRepeater.DataSourceID = "EthnicitySqlDataSource";
                    break;
                case 4:
                    Query = "SELECT Gender, C.Status, C.Case_ID FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY Gender";
                    SqlDataAdapter cmd4 = new SqlDataAdapter(Query, conn);
                    DataSet ds4 = new DataSet();
                    cmd4.Fill(ds4);
                    ReportsRepeater.DataSourceID = "GenderSqlDataSource";
                    break;
                case 5:
                    Query = "SELECT Date_Of_Birth, C.Status, C.Case_ID FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY Date_Of_Birth DESC";
                    SqlDataAdapter cmd5 = new SqlDataAdapter(Query, conn);
                    DataSet ds5 = new DataSet();
                    cmd5.Fill(ds5);
                    ReportsRepeater.DataSourceID = "AgeSqlDataSource";
                    break;
                case 6:
                    fullQuery = selectQuerySegment + fromQuerySegment + whereQuerySegment;
                    dynamicQuery.SelectCommand = fullQuery;
                    ReportsRepeater.DataSourceID = "DynamicSqlDataSource";
                    break;

                default:
                    Query = "";
                    break;
            }
        }

        protected void ReportsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label filterItemLabel = (Label)e.Item.Page.FindControl("FilterItem");
            Label caseIDLabel = (Label)e.Item.FindControl("CaseID");
            Label statusLabel = (Label)e.Item.FindControl("Status");



           // DataSet thisDataSet = (DataSet)e.Item.DataItem;
            //DataView thisDataView = new DataView((DataTable)thisDataSet.Tables);


            //filterItemLabel.Text = "something";//thisDataSet.Tables[0].Rows[0][0].ToString();
            //caseIDLabel.Text = thisDataSet.Tables[0].Rows[0][1].ToString();
            //statusLabel.Text = thisDataSet.Tables[0].Rows[0][1].ToString();
            /*SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            SqlDataReader read = cmd.ExecuteReader();*/

        }

        protected void ReportPrep(object sender, EventArgs e)
        {
            RadioButtonList choiceList = sender as RadioButtonList;
            Panel panel2 = (Panel)choiceList.Parent.Parent.FindControl("listPanel2");
            Panel panel3 = (Panel)choiceList.Parent.Parent.FindControl("listPanel3");

                if (choiceList.SelectedItem.Value=="Cases")
                {
                    panel2.Visible = true;
                    panel3.Visible = false;
                    selectQuerySegment = "";
                    whereQuerySegment = "WHERE C.Case_ID > 0";
                }

                else if (choiceList.SelectedItem.Value=="People")
                {

                    panel2.Visible = false;
                    panel3.Visible = true;
                    selectQuerySegment = "";
                    whereQuerySegment = "WHERE P.Person_ID = O.Person_ID";
                }
            }

        protected void ReportPrep2(object sender, EventArgs e)
        {
            RadioButtonList choiceList2 = sender as RadioButtonList;
            Panel panel4 = (Panel)choiceList2.Parent.Parent.FindControl("listPanel4");

            if(choiceList2.SelectedItem.Value=="Name")
            {
                selectQuerySegment = "SELECT P.Last_Name + ', ' + P.First_Name AS [Column1]";
                panel4.Visible = true;
            }

            else if (choiceList2.SelectedItem.Value == "Age")
            {
                selectQuerySegment = "SELECT FLOOR(DATEDIFF(MM,Person.Date_Of_Birth,GETDATE())/12) AS [Column1]";
                panel4.Visible = true;
            }

            else if (choiceList2.SelectedItem.Value == "Race")
            {
                selectQuerySegment = "SELECT P.RACE AS [Column1]";
                panel4.Visible = true;
            }

            else if (choiceList2.SelectedItem.Value == "Gender")
            {
                selectQuerySegment = "SELECT P.Gender AS [Column1]";
                panel4.Visible = true;
            }
        }

        protected void ReportPrep3(object sender, EventArgs e)
        {

        }

        protected void ReportPrep4(object sender, EventArgs e)
        {

        }
        }
    }

