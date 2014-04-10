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
        const string FROMQUERYSEGMENT = " FROM RJL_Case C JOIN CASE_CHARGE CC ON C.Case_ID = CC.Case_ID JOIN CHARGE CH ON CH.Charge_ID = CC.Charge_ID " +
                           "JOIN Case_File CF ON CF.Case_ID = C.Case_ID JOIN Case_Manager CM ON CM.Case_ID = C.Case_ID JOIN CASE_NOTE CN ON CN.CASE_ID = C.Case_ID " +
                           "JOIN Document D ON D.Case_ID = C.Case_ID JOIN Person P ON CF.Person_ID = P.Person_ID " +
                           "JOIN OFFENDER O ON O.Person_ID = P.Person_ID ";
        string finalFromQuerySegment = "";
        string finalSelectQuerySegment = "";
        string finalWhereQuerySegment = "";
        string finalGroupByQuerySegment = "";
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
                    SqlDataSource dynamicQuery = (SqlDataSource)reportClick.Parent.FindControl("DynamicSqlDataSource");
                    ReportsRepeater.DataSourceID = "DynamicSqlDataSource";
                    dynamicQuery.Select(DataSourceSelectArguments.Empty);
                    ReportsRepeater.DataBind();
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
            Panel panel4 = (Panel)choiceList.Parent.Parent.FindControl("listPanel4");
            LinkButton generateButton = (LinkButton)choiceList.Parent.Parent.FindControl("GenerateReport");

                if (choiceList.SelectedItem.Value=="Cases")
                {
                    panel2.Visible = true;
                    panel3.Visible = false;
                    panel4.Visible = false;
                    generateButton.Visible = false;
                    selectQuerySegment = "";
                    whereQuerySegment = "WHERE C.Case_ID > 0";
                }

                else if (choiceList.SelectedItem.Value=="People")
                {

                    panel2.Visible = false;
                    panel3.Visible = true;
                    panel4.Visible = false;
                    generateButton.Visible = false;
                    selectQuerySegment = "";
                    whereQuerySegment = "WHERE P.Person_ID = O.Person_ID";
                }
            }

        protected void ReportPrep2(object sender, EventArgs e)
        {
            RadioButtonList choiceList2 = sender as RadioButtonList;
            Panel panel4 = (Panel)choiceList2.Parent.Parent.FindControl("listPanel4");
            LinkButton generateButton = (LinkButton)choiceList2.Parent.Parent.FindControl("GenerateReport");
            Label selectLabel = (Label)choiceList2.Parent.Parent.FindControl("invisibleSelectQuery");
            Label fromLabel = (Label)choiceList2.Parent.Parent.FindControl("invisibleFromQuery");

            if(choiceList2.SelectedItem.Value=="Name")
            {
                selectQuerySegment = "SELECT DISTINCT C.Case_ID, P.Last_Name + ', ' + P.First_Name AS [Column1]";
                finalFromQuerySegment = FROMQUERYSEGMENT;
                panel4.Visible = true;
                generateButton.Visible = false;
            }

            else if (choiceList2.SelectedItem.Value == "Age")
            {
                selectQuerySegment = "SELECT DISTINCT C.Case_ID, FLOOR(DATEDIFF(MM,Person.Date_Of_Birth,GETDATE())/12) AS [Column1]";
                finalFromQuerySegment = FROMQUERYSEGMENT;
                panel4.Visible = true;
                generateButton.Visible = false;
            }

            else if (choiceList2.SelectedItem.Value == "Race")
            {
                selectQuerySegment = "SELECT DISTINCT C.Case_ID, P.RACE AS [Column1]";
                finalFromQuerySegment = FROMQUERYSEGMENT;
                panel4.Visible = true;
                generateButton.Visible = false;
            }

            else if (choiceList2.SelectedItem.Value == "Gender")
            {
                selectQuerySegment = "SELECT DISTINCT P.Gender AS [Column1]";
                finalFromQuerySegment = FROMQUERYSEGMENT;
                panel4.Visible = true;
                generateButton.Visible = false;
            }
        }

        protected void ReportPrep3(object sender, EventArgs e)
        {
            RadioButtonList choiceList3 = sender as RadioButtonList;
            Panel panel4 = (Panel)choiceList3.Parent.Parent.FindControl("listPanel4");
            LinkButton generateButton = (LinkButton)choiceList3.Parent.Parent.FindControl("GenerateButton");
            Label selectLabel = (Label)choiceList3.Parent.Parent.FindControl("invisibleSelectQuery");
            Label fromLabel = (Label)choiceList3.Parent.Parent.FindControl("invisibleFromQuery");

            if(choiceList3.SelectedItem.Value=="Status")
            {
                selectQuerySegment = "SELECT DISTINCT C.Case_ID, C.STATUS AS [Column1]";
                finalFromQuerySegment = " FROM RJL_CASE C";
                panel4.Visible = true;
            }

            else if(choiceList3.SelectedItem.Value=="District")
            {
                selectQuerySegment = "SELECT C.Case_ID, C.District AS [Column1]";
                finalFromQuerySegment = " FROM RJL_Case C";
                panel4.Visible = true;
            }

            else if(choiceList3.SelectedItem.Value=="CaseManager")
            {
                selectQuerySegment = "SELECT DISTINCT C.Case_ID AS [Column1], P.Last_Name AS [Column2], P.First_Name AS [Column3]";
                finalFromQuerySegment = " FROM RJL_CASE C JOIN CASE_MANAGER CM ON C.Case_ID = CM.Case_ID JOIN PERSON P ON CM.Person_ID = P.Person_ID";
                panel4.Visible = true;
            }
        }

        protected void ReportPrep4(object sender, EventArgs e)
        {
            RadioButtonList choiceList4 = sender as RadioButtonList;
            RadioButtonList choiceList = (RadioButtonList)choiceList4.Parent.Parent.FindControl("ReportTypeList");
            RadioButtonList choiceList2 = (RadioButtonList)choiceList4.Parent.Parent.FindControl("PersonReportTypeList");
            RadioButtonList choiceList3 = (RadioButtonList)choiceList4.Parent.Parent.FindControl("CaseReportTypeList");
            LinkButton generateButton = (LinkButton)choiceList4.Parent.FindControl("GenerateReport");
            Label header1 = (Label)ReportsRepeater.FindControl("Header1");
            Label header2 = (Label)choiceList4.Parent.Parent.FindControl("Header2");
            Label header3 = (Label)choiceList4.Parent.Parent.FindControl("Header3");
            SqlDataSource dynamicQuery = (SqlDataSource)choiceList4.Parent.Parent.FindControl("DynamicSqlDataSource");
            Repeater reportRepeater = (Repeater)choiceList4.Parent.Parent.Parent.FindControl("ReportsRepeater");

            if(choiceList4.SelectedItem.Value=="All")
            {
                if(choiceList.SelectedItem.Value=="Cases")
                {
                    if (choiceList2.SelectedItem.Value == "Name")
                    {
                        selectQuerySegment = "SELECT DISTINCT C.Case_ID AS [Column1], P.Last_Name + ', ' + P.First_Name AS [Column2], '' AS [Column3]";
                        finalFromQuerySegment = FROMQUERYSEGMENT;
                        fullQuery = selectQuerySegment + finalFromQuerySegment;
                        dynamicQuery.SelectCommand = selectQuerySegment + finalFromQuerySegment;
                        reportRepeater.DataSourceID="DynamicSqlDataSource";

                    }

                    else if (choiceList2.SelectedItem.Value == "Age")
                    {
                        selectQuerySegment = "SELECT DISTINCT C.Case_ID AS [Column1], FLOOR(DATEDIFF(MM,P.Date_Of_Birth,GETDATE())/12) AS [Column3], P.Last_Name + ', ' + P.First_Name AS [Column2]";
                        finalFromQuerySegment = FROMQUERYSEGMENT;
                        fullQuery = selectQuerySegment + finalFromQuerySegment;
                        dynamicQuery.SelectCommand = fullQuery;
                        reportRepeater.DataSourceID = "DynamicSqlDataSource";
                    }

                    else if (choiceList2.SelectedItem.Value == "Race")
                    {
                        selectQuerySegment = "SELECT DISTINCT C.Case_ID AS [Column1], P.RACE AS [Column3], P.Last_Name + ', ' + P.First_Name AS [Column2]";
                        finalFromQuerySegment = FROMQUERYSEGMENT;
                        fullQuery = selectQuerySegment + finalFromQuerySegment;
                        dynamicQuery.SelectCommand = fullQuery;
                        reportRepeater.DataSourceID = "DynamicSqlDataSource";
                    }

                    else if (choiceList2.SelectedItem.Value == "Gender")
                    {
                        selectQuerySegment = "SELECT DISTINCT C.Case_ID AS [Column1], P.Gender AS [Column3], P.Last_Name + ', ' + P.First_Name AS [Column2]";
                        finalFromQuerySegment = FROMQUERYSEGMENT;
                        fullQuery = selectQuerySegment + finalFromQuerySegment;
                        dynamicQuery.SelectCommand = fullQuery;
                        reportRepeater.DataSourceID = "DynamicSqlDataSource";
                    }
                }

                else if(choiceList.SelectedItem.Value=="People")
                {
                    if(choiceList3.SelectedItem.Value=="Status")
                    {
                        selectQuerySegment = "SELECT DISTINCT C.Case_ID AS [Column1], C.STATUS AS [Column2], '' AS [Column3]";
                        finalFromQuerySegment = " FROM RJL_CASE C";
                        fullQuery = selectQuerySegment + finalFromQuerySegment;
                        dynamicQuery.SelectCommand = fullQuery;
                        reportRepeater.DataSourceID = "DynamicSqlDataSource";
                    }

                    else if (choiceList3.SelectedItem.Value == "District")
                    {
                        selectQuerySegment = "SELECT C.Case_ID AS [Column1], C.District AS [Column2], '' AS [Column3]";
                        finalFromQuerySegment = " FROM RJL_Case C";
                        fullQuery = selectQuerySegment + finalFromQuerySegment;
                        dynamicQuery.SelectCommand = fullQuery;
                        reportRepeater.DataSourceID = "DynamicSqlDataSource";
                    }

                    else if (choiceList3.SelectedItem.Value == "CaseManager")
                    {
                        selectQuerySegment = "SELECT DISTINCT C.Case_ID AS [Column1], P.Last_Name AS [Column2], P.First_Name AS [Column3]";
                        finalFromQuerySegment = " FROM RJL_CASE C JOIN CASE_MANAGER CM ON C.Case_ID = CM.Case_ID JOIN PERSON P ON CM.Person_ID = P.Person_ID";
                        fullQuery = selectQuerySegment + finalFromQuerySegment;
                        dynamicQuery.SelectCommand = fullQuery;
                        reportRepeater.DataSourceID = "DynamicSqlDataSource";
                    }
                }
            }

            else if(choiceList4.SelectedItem.Value=="Count")
            {
                if(choiceList.SelectedItem.Value=="Cases")
                {
                    if (choiceList2.SelectedItem.Value == "Name")
                    {
                        selectQuerySegment = "SELECT P.Last_Name + ', ' + P.First_Name AS [Column1], COUNT(*) AS [Column2], '' AS [Column3]";
                        finalFromQuerySegment = " FROM RJL_CASE C JOIN CASE_FILE CF ON C.Case_ID = CF.Case_ID JOIN PERSON P ON CF.Person_ID = P.Person_ID";
                        finalGroupByQuerySegment = " GROUP BY P.Last_Name + ', ' + P.First_Name";
                        fullQuery = selectQuerySegment + finalFromQuerySegment + finalGroupByQuerySegment;
                        dynamicQuery.SelectCommand = fullQuery;
                        reportRepeater.DataSourceID = "DynamicSqlDataSource";
                    }

                    else if(choiceList2.SelectedItem.Value=="Age")
                    {
                        selectQuerySegment = "SELECT FLOOR(DATEDIFF(MM,P.Date_Of_Birth,GETDATE())/12) AS [Column1], COUNT(*) AS [Column2], '' AS [Column3]";
                        finalFromQuerySegment = " FROM RJL_CASE C JOIN CASE_FILE CF ON C.Case_ID = CF.Case_ID JOIN PERSON P ON CF.Person_ID = P.Person_ID";
                        finalGroupByQuerySegment = " GROUP BY FLOOR(DATEDIFF(MM,P.Date_Of_Birth,GETDATE())/12)";
                        fullQuery = selectQuerySegment + finalFromQuerySegment + finalGroupByQuerySegment;
                        dynamicQuery.SelectCommand = fullQuery;
                        reportRepeater.DataSourceID = "DynamicSqlDataSource";
                    }

                    else if (choiceList2.SelectedItem.Value == "Race")
                    {
                        selectQuerySegment = "SELECT P.Race AS [Column1], COUNT(*) AS [Column2], '' AS [Column3]";
                        finalFromQuerySegment = " FROM RJL_CASE C JOIN CASE_FILE CF ON C.Case_ID = CF.Case_ID JOIN PERSON P ON CF.Person_ID = P.Person_ID";
                        finalGroupByQuerySegment = " GROUP BY P.Race";
                        fullQuery = selectQuerySegment + finalFromQuerySegment + finalGroupByQuerySegment;
                        dynamicQuery.SelectCommand = fullQuery;
                        reportRepeater.DataSourceID = "DynamicSqlDataSource";
                    }

                    else if (choiceList2.SelectedItem.Value == "Gender")
                    {
                        selectQuerySegment = "SELECT P.Gender AS [Column1], COUNT(*) AS [Column2], '' AS [Column3]";
                        finalFromQuerySegment = " FROM RJL_CASE C JOIN CASE_FILE CF ON C.Case_ID = CF.Case_ID JOIN PERSON P ON CF.Person_ID = P.Person_ID";
                        finalGroupByQuerySegment = " GROUP BY P.Gender";
                        fullQuery = selectQuerySegment + finalFromQuerySegment + finalGroupByQuerySegment;
                        dynamicQuery.SelectCommand = fullQuery;
                        reportRepeater.DataSourceID = "DynamicSqlDataSource";
                    }
                }

                else if(choiceList.SelectedItem.Value=="People")
                {
                    if(choiceList3.SelectedItem.Value=="Status")
                    {
                        selectQuerySegment = "SELECT C.Status AS [Column1], COUNT(*) AS [Column2], '' AS [Column3]";
                        finalFromQuerySegment = " FROM RJL_Case C";
                        finalGroupByQuerySegment = " GROUP BY C.Status";
                        fullQuery = selectQuerySegment + finalFromQuerySegment + finalGroupByQuerySegment;
                        dynamicQuery.SelectCommand = fullQuery;
                        reportRepeater.DataSourceID = "DynamicSqlDataSource";
                    }

                    else if(choiceList3.SelectedItem.Value == "District")
                    {
                        selectQuerySegment = "SELECT C.District AS [Column1], COUNT(*) AS [Column2], '' AS [Column3]";
                        finalFromQuerySegment = " FROM RJL_Case C";
                        finalGroupByQuerySegment = " GROUP BY C.District";
                        fullQuery = selectQuerySegment + finalFromQuerySegment + finalGroupByQuerySegment;
                        dynamicQuery.SelectCommand = fullQuery;
                        reportRepeater.DataSourceID = "DynamicSqlDataSource";
                    }

                    else if(choiceList3.SelectedItem.Value == "CaseManager")
                    {
                        selectQuerySegment = "SELECT P.Last_Name + ', ' + P.First_Name AS [Column1], COUNT(*) AS [Column2], '' AS [Column3]";
                        finalFromQuerySegment = " FROM RJL_CASE C JOIN CASE_MANAGER CM ON C.Case_ID = CM.Case_ID JOIN PERSON P ON CM.Person_ID = P.Person_ID";
                        finalGroupByQuerySegment = " GROUP BY P.Last_Name + ', ' + P.First_Name";
                        fullQuery = selectQuerySegment + finalFromQuerySegment + finalGroupByQuerySegment;
                        dynamicQuery.SelectCommand = fullQuery;
                        reportRepeater.DataSourceID = "DynamicSqlDataSource";
                    }
                }
            }
        }
        }
    }

