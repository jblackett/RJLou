using RJLou.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RJLou
{
    public partial class Donors : System.Web.UI.Page
    {
        //Donor thisDonor;
        //List<Donor> donors;
        //int PersonID = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            //try { PersonID = Convert.ToInt32(Session["PersonID"]); }
            //catch { }

            //if (PersonID <= 0)
            //{
            //    Response.Redirect("Login.aspx");
            //}

            //if (!Page.IsPostBack)
            //{
            //    InternalUser thisUser = InternalUser.Get(PersonID);
            //    if (thisUser.Role == Role.CASE_MANAGER)
            //    {
            //        donors = Donor.GetDonors(true, thisUser.PersonID);
            //        DonorsRepeater.DataSource = donors;
            //        DonorsRepeater.DataBind();
            //    }
            //    else
            //    {
            //        donors = Donor.GetDonors(true);
            //        DonorsRepeater.DataSource = donors;
            //        DonorsRepeater.DataBind();
            //    }
            //}
        }

        //protected internal void BindData()
        //{
        //    DonorInformationRepeater.DataSource = thisDonor.DonorInformation;
        //    DonorInformationRepeater.DataBind();

        //    DonationsRepeater.DataSource = thisDonor.Donations;
        //    DonationsRepeater.DataBind();

        //    EventsAttendedRepeater.DataSource = thisDonor.EventsAttended;
        //    EventsAttendedRepeater.DataBind();

          
        //}

        //protected internal void DonorsRepeater_Databind(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        Label thisLabel = (Label)e.Item.FindControl("Name");
        //        Donor currentCase = (Donor)e.Item.DataItem;

        //        string Name;

        //        if (currentDonor.DonorInformation.Count > 0)
        //            Name = currentDonor.DonorInformation[0].FirstName + " " + currentDonor.DonorInformation[0].LastName;
        //        else if (currentDonor.DonorInformation.Count > 0)
        //            Name = currentDonor.DonorInformation[0].FirstName + " " + currentDonor.DonorInformation[0].LastName;
        //        else
        //            Name = "";

        //        thisLabel.Text = Name;
        //    }
        //}

        //protected void DonorInformationRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        Label donorName = (Label)e.Item.FindControl("DonorName");
        //        Donor currentDonor = (Donor)e.Item.DataItem;

        //        string name = currentDonor.FirstName + " " + currentDonor.LastName;

        //        donorName.Text = name;
        //    }
        //}

        //protected void OffendersRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        Label offenderName = (Label)e.Item.FindControl("OffenderName");
        //        Offender currentOffender = (Offender)e.Item.DataItem;

        //        string name = currentOffender.FirstName + " " + currentOffender.LastName;

        //        offenderName.Text = name;
        //    }
        //}

        //protected void AffiliatesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        Label affiliateName = (Label)e.Item.FindControl("AffiliateName");
        //        Affiliate currentAffiliate = (Affiliate)e.Item.DataItem;

        //        string name = currentAffiliate.FirstName + " " + currentAffiliate.LastName;

        //        affiliateName.Text = name;
        //    }
        //}

        

        //protected internal void LoadDonor(object sender, EventArgs e)
        //{
        //    int DonorID = -1;
        //    int.TryParse(((LinkButton)sender).CommandArgument, out DonorID);

        //    thisDonor = Donor.Get(DonorID);

        //    BindData();
        //    LoadHeader();
        //}

        //protected internal void LoadHeader()
        //{
        //    CaseID.Text = thisCase.CaseID.ToString();
        //    CaseID.ReadOnly = true;
        //    CourtID.Text = thisCase.CourtID.ToString("000000");
        //    ReferralDate.Text = (thisCase.ReferralDate == default(DateTime) ? "" : thisCase.ReferralDate.ToString("MM/dd/yyyy"));
        //    ReferralNumber.Text = thisCase.ReferralNumber.ToString();
        //    CourtDate.Text = (thisCase.CourtDate == default(DateTime) ? "" : thisCase.CourtDate.ToString("MM/dd/yyyy"));
        //    DateFinalConference.Text = (thisCase.DateOfFinalConference == default(DateTime) ? "" : thisCase.DateOfFinalConference.ToString("MM/dd/yyyy"));
        //    DateCompletion.Text = (thisCase.DateOfCompletion == default(DateTime) ? "" : thisCase.DateOfCompletion.ToString("MM/dd/yyyy"));
        //    Status.Text = thisCase.Status;
        //    District.Text = thisCase.District.ToString();

        //    MainContainer.Visible = true;
        //}

        //protected internal void SaveDonor(object sender, EventArgs e)
        //{
        //    int DonorID = int.Parse(DonorID.Text);
        //    thisDonor = Donor.Get(DonorID);



        //    thisDonor.CourtID = Convert.ToInt32(CourtID.Text);

        //    if (!string.IsNullOrEmpty(ReferralDate.Text))
        //        thisCase.ReferralDate = Convert.ToDateTime(ReferralDate.Text);
        //    else
        //        thisCase.ReferralDate = default(DateTime);

        //    thisCase.ReferralNumber = Convert.ToInt32(ReferralNumber.Text);

        //    if (!string.IsNullOrEmpty(CourtDate.Text))
        //        thisCase.CourtDate = Convert.ToDateTime(CourtDate.Text);
        //    else
        //        thisCase.CourtDate = default(DateTime);

        //    if (!string.IsNullOrEmpty(DateFinalConference.Text))
        //        thisCase.DateOfFinalConference = Convert.ToDateTime(DateFinalConference.Text);
        //    else
        //        thisCase.DateOfFinalConference = default(DateTime);

        //    if (!string.IsNullOrEmpty(DateCompletion.Text))
        //        thisCase.DateOfCompletion = Convert.ToDateTime(DateCompletion.Text);
        //    else
        //        thisCase.DateOfCompletion = default(DateTime);

        //    thisCase.Status = Status.Text;

        //    thisCase.District = Convert.ToInt32(District.Text);



        //    thisCase.Update();
        //    CaseUpdatedPanel.CssClass += " visible";
        //}

        //protected internal void DeleteVictim(object sender, EventArgs e)
        //{

        //}

        //protected internal void ViewVictim(object sender, EventArgs e)
        //{
        //    int PersonID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        //    Victim thisVictim = Victim.Get(PersonID);

        //    ModalName.InnerText = thisVictim.FirstName + " " + thisVictim.LastName;
        //    ModalDateOfBirth.Text = thisVictim.DateOfBirth.ToString("MM/dd/yyyy");
        //    ModalGender.Text = thisVictim.Gender;
        //    ModalRace.Text = thisVictim.Race;
        //    ModalPhoneNumbers.DataSource = thisVictim.PhoneNumbers;
        //    ModalPhoneNumbers.DataBind();
        //    ModalAddresses.DataSource = thisVictim.Addresses;
        //    ModalAddresses.DataBind();

        //    ViewPersonModalPanel.CssClass += " visible";
        //}

        //protected void ModalPhoneNumbers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        PhoneNumber thisNumber = (PhoneNumber)e.Item.DataItem;
        //        TextBox ModalPhoneNum = (TextBox)e.Item.FindControl("ModalPhoneNum");

        //        ModalPhoneNum.Text = thisNumber.ToString();
        //    }
        //}

        //protected void ModalAddresses_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        Address thisAddress = (Address)e.Item.DataItem;
        //        TextBox ModalAddress = (TextBox)e.Item.FindControl("ModalAddress");

        //        ModalAddress.Text = thisAddress.ToString();
        //    }
        //}

        //protected internal void DeleteOffender(object sender, EventArgs e)
        //{

        //}

        //protected internal void ViewOffender(object sender, EventArgs e)
        //{
        //    int PersonID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        //    Offender thisOffender = Offender.Get(PersonID);

        //    ModalName.InnerText = thisOffender.FirstName + " " + thisOffender.LastName;
        //    ModalDateOfBirth.Text = thisOffender.DateOfBirth.ToString("MM/dd/yyyy");
        //    ModalGender.Text = thisOffender.Gender;
        //    ModalRace.Text = thisOffender.Race;
        //    ModalPhoneNumbers.DataSource = thisOffender.PhoneNumbers;
        //    ModalPhoneNumbers.DataBind();
        //    ModalAddresses.DataSource = thisOffender.Addresses;
        //    ModalAddresses.DataBind();

        //    ViewPersonModalPanel.CssClass += " visible";
        //}

        //protected internal void DeleteAffiliate(object sender, EventArgs e)
        //{

        //}

        //protected internal void ViewAffiliate(object sender, EventArgs e)
        //{
        //    int PersonID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        //    Affiliate thisAffiliate = Affiliate.Get(PersonID);

        //    ModalName.InnerText = thisAffiliate.FirstName + " " + thisAffiliate.LastName;
        //    ModalDateOfBirth.Text = thisAffiliate.DateOfBirth.ToString("MM/dd/yyyy");
        //    ModalGender.Text = thisAffiliate.Gender;
        //    ModalRace.Text = thisAffiliate.Race;
        //    ModalPhoneNumbers.DataSource = thisAffiliate.PhoneNumbers;
        //    ModalPhoneNumbers.DataBind();
        //    ModalAddresses.DataSource = thisAffiliate.Addresses;
        //    ModalAddresses.DataBind();

        //    ViewPersonModalPanel.CssClass += " visible";
        //}

        //protected internal void DeleteCharge(object sender, EventArgs e)
        //{

        //}

        //protected internal void ViewCharge(object sender, EventArgs e)
        //{

        //}

        //protected internal void SwitchCaseList(object sender, EventArgs e)
        //{
        //    string commandArg = (((LinkButton)sender).CommandArgument).ToString();

        //    switch (commandArg)
        //    {
        //        case "all":
        //            cases = Case.GetCases();
        //            CasesRepeater.DataSource = cases;
        //            CasesRepeater.DataBind();
        //            break;
        //        case "open":
        //        case "pending":
        //        case "closed":
        //            cases = Case.GetCases(commandArg);
        //            CasesRepeater.DataSource = cases;
        //            CasesRepeater.DataBind();
        //            break;
        //        default:
        //            cases = Case.GetCases();
        //            CasesRepeater.DataSource = cases;
        //            CasesRepeater.DataBind();
        //            break;
        //    }
        //}
    }
}