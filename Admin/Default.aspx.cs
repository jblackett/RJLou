using RJLou.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RJLou
{
    public partial class Default : System.Web.UI.Page
    {
        Case thisCase;
        List<Case> cases;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cases = Case.GetCases();
                CasesRepeater.DataSource = cases;
                CasesRepeater.DataBind();
            }
        }

        protected internal void BindData()
        {
            VictimsRepeater.DataSource = thisCase.Victims;
            VictimsRepeater.DataBind();

            OffendersRepeater.DataSource = thisCase.Offenders;
            OffendersRepeater.DataBind();

            AffiliatesRepeater.DataSource = thisCase.Affiliates;
            AffiliatesRepeater.DataBind();

            NotesRepeater.DataSource = thisCase.Notes;
            NotesRepeater.DataBind();

            ChargesRepeater.DataSource = thisCase.Charges;
            ChargesRepeater.DataBind();

            DocumentsRepeater.DataSource = thisCase.Documents;
            DocumentsRepeater.DataBind();
        }

        protected internal void CasesRepeater_Databind(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label thisLabel = (Label)e.Item.FindControl("Name");
                Case currentCase = (Case)e.Item.DataItem;

                string Name = currentCase.Offenders[0].FirstName + " " + currentCase.Offenders[0].LastName;

                thisLabel.Text = Name;
            }
        }

        protected void VictimsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label victimName = (Label)e.Item.FindControl("VictimName");
                Victim currentVictim = (Victim)e.Item.DataItem;

                string name = currentVictim.FirstName + " " + currentVictim.LastName;

                victimName.Text = name;
            }
        }

        protected void OffendersRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label offenderName = (Label)e.Item.FindControl("OffenderName");
                Offender currentOffender = (Offender)e.Item.DataItem;

                string name = currentOffender.FirstName + " " + currentOffender.LastName;

                offenderName.Text = name;
            }
        }

        protected void AffiliatesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label affiliateName = (Label)e.Item.FindControl("AffiliateName");
                Affiliate currentAffiliate = (Affiliate)e.Item.DataItem;

                string name = currentAffiliate.FirstName + " " + currentAffiliate.LastName;

                affiliateName.Text = name;
            }
        }

        protected void NotesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void ChargesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void DocumentsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected internal void LoadCase(object sender, EventArgs e)
        {
            int CaseID = -1;
            int.TryParse(((LinkButton)sender).CommandArgument, out CaseID);

            thisCase = Case.Get(CaseID);

            BindData();
            LoadHeader();
        }

        protected internal void LoadHeader()
        {
            CaseID.Text = thisCase.CaseID.ToString();
            CaseID.ReadOnly = true;
            CourtID.Text = thisCase.CourtID.ToString();
            ReferralDate.Text = (thisCase.ReferralDate == default(DateTime) ? "" : thisCase.ReferralDate.ToString("MM/dd/yyyy"));
            ReferralNumber.Text = thisCase.ReferralNumber.ToString();
            CourtDate.Text = (thisCase.CourtDate == default(DateTime) ? "" : thisCase.CourtDate.ToString("MM/dd/yyyy"));
            DateFinalConference.Text = (thisCase.DateOfFinalConference == default(DateTime) ? "" : thisCase.DateOfFinalConference.ToString("MM/dd/yyyy"));
            DateCompletion.Text = (thisCase.DateOfCompletion == default(DateTime) ? "" : thisCase.DateOfCompletion.ToString("MM/dd/yyyy"));
            Status.Text = thisCase.Status;
            District.Text = thisCase.District;

            MainContainer.Visible = true;
        }

        protected internal void DeleteVictim(object sender, EventArgs e)
        {

        }

        protected internal void ViewVictim(object sender, EventArgs e)
        {

        }

        protected internal void DeleteOffender(object sender, EventArgs e)
        {

        }

        protected internal void ViewOffender(object sender, EventArgs e)
        {

        }

        protected internal void DeleteAffiliate(object sender, EventArgs e)
        {

        }

        protected internal void ViewAffiliate(object sender, EventArgs e)
        {

        }

        protected internal void DeleteCharge(object sender, EventArgs e)
        {

        }

        protected internal void ViewCharge(object sender, EventArgs e)
        {

        }
    }
}