﻿using RJLou.Classes;
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
        int PersonID = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            try { PersonID = Convert.ToInt32(Session["PersonID"]); }
            catch { }

            if (PersonID <= 0)
            {
                Response.Redirect("Login.aspx");
            }

            if (!Page.IsPostBack)
            {
                InternalUser thisUser = InternalUser.Get(PersonID);
                if (thisUser.Role == Role.CASE_MANAGER)
                {
                    cases = Case.GetCases(true, thisUser.PersonID);
                    CasesRepeater.DataSource = cases;
                    CasesRepeater.DataBind();
                }
                else
                {
                    cases = Case.GetCases(true);
                    CasesRepeater.DataSource = cases;
                    CasesRepeater.DataBind();
                }
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

                string Name;

                if (currentCase.Offenders.Count > 0)
                    Name = currentCase.Offenders[0].FirstName + " " + currentCase.Offenders[0].LastName;
                else if (currentCase.Victims.Count > 0)
                    Name = currentCase.Victims[0].FirstName + " " + currentCase.Victims[0].LastName;
                else
                    Name = "";

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
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label NoteAuthorAndDate = (Label)e.Item.FindControl("NoteAuthorAndDate");
                TextBox NoteText = (TextBox)e.Item.FindControl("NoteText");
                Note currentNote = (Note)e.Item.DataItem;

                string text = "";

                if (currentNote.Author != null)
                {
                    text += currentNote.Author.FirstName + " " + currentNote.Author.LastName;
                }
                else
                {
                    text += "[[NO AUTHOR]]";
                }

                if (!string.IsNullOrEmpty(currentNote.EditDate.ToString()) && currentNote.EditDate != default(DateTime))
                {
                    text += " - " + ((DateTime)currentNote.EditDate).ToString("MM/dd/yyyy");
                }
                else if (!string.IsNullOrEmpty(currentNote.CreateDate.ToString()) && currentNote.CreateDate != default(DateTime))
                {
                    text += " - " + currentNote.CreateDate.ToString("MM/dd/yyyy");
                }

                NoteAuthorAndDate.Text = text;

                NoteText.Text = currentNote.NoteText;
            }
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
            CourtID.Text = thisCase.CourtID.ToString("000000");
            ReferralDate.Text = (thisCase.ReferralDate == default(DateTime) ? "" : thisCase.ReferralDate.ToString("MM/dd/yyyy"));
            ReferralNumber.Text = thisCase.ReferralNumber.ToString();
            CourtDate.Text = (thisCase.CourtDate == default(DateTime) ? "" : thisCase.CourtDate.ToString("MM/dd/yyyy"));
            DateFinalConference.Text = (thisCase.DateOfFinalConference == default(DateTime) ? "" : thisCase.DateOfFinalConference.ToString("MM/dd/yyyy"));
            DateCompletion.Text = (thisCase.DateOfCompletion == default(DateTime) ? "" : thisCase.DateOfCompletion.ToString("MM/dd/yyyy"));
            Status.Text = thisCase.Status;
            District.Text = thisCase.District.ToString();

            MainContainer.Visible = true;
        }

        protected internal void SaveCase(object sender, EventArgs e)
        {
            int caseID = int.Parse(CaseID.Text);
            thisCase = Case.Get(caseID);

            
            
            thisCase.CourtID = Convert.ToInt32(CourtID.Text);

            if (!string.IsNullOrEmpty(ReferralDate.Text))
                thisCase.ReferralDate = Convert.ToDateTime(ReferralDate.Text);
            else
                thisCase.ReferralDate = default(DateTime);

            thisCase.ReferralNumber = Convert.ToInt32(ReferralNumber.Text);

            if (!string.IsNullOrEmpty(CourtDate.Text))
                thisCase.CourtDate = Convert.ToDateTime(CourtDate.Text);
            else
                thisCase.CourtDate = default(DateTime);

            if (!string.IsNullOrEmpty(DateFinalConference.Text))
                thisCase.DateOfFinalConference = Convert.ToDateTime(DateFinalConference.Text);
            else
                thisCase.DateOfFinalConference = default(DateTime);

            if (!string.IsNullOrEmpty(DateCompletion.Text))
                thisCase.DateOfCompletion = Convert.ToDateTime(DateCompletion.Text);
            else
                thisCase.DateOfCompletion = default(DateTime);
            
            thisCase.Status = Status.Text;

            thisCase.District = Convert.ToInt32(District.Text);


          
            thisCase.Update();
            CaseUpdatedPanel.CssClass += " visible";
        }

        protected internal void DeleteVictim(object sender, EventArgs e)
        {

        }

        protected internal void ViewVictim(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            Victim thisVictim = Victim.Get(PersonID);

            ModalName.InnerText = thisVictim.FirstName + " " + thisVictim.LastName;
            ModalDateOfBirth.Text = thisVictim.DateOfBirth.ToString("MM/dd/yyyy");
            ModalGender.Text = thisVictim.Gender;
            ModalRace.Text = thisVictim.Race;
            ModalPhoneNumbers.DataSource = thisVictim.PhoneNumbers;
            ModalPhoneNumbers.DataBind();
            ModalAddresses.DataSource = thisVictim.Addresses;
            ModalAddresses.DataBind();

            ViewPersonModalPanel.CssClass += " visible";
        }

        protected void ModalPhoneNumbers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PhoneNumber thisNumber = (PhoneNumber)e.Item.DataItem;
                TextBox ModalPhoneNum = (TextBox)e.Item.FindControl("ModalPhoneNum");

                ModalPhoneNum.Text = thisNumber.ToString();
            }
        }

        protected void ModalAddresses_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Address thisAddress = (Address)e.Item.DataItem;
                TextBox ModalAddress = (TextBox)e.Item.FindControl("ModalAddress");

                ModalAddress.Text = thisAddress.ToString();
            }
        }

        protected internal void DeleteOffender(object sender, EventArgs e)
        {

        }

        protected internal void ViewOffender(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            Offender thisOffender = Offender.Get(PersonID);

            ModalName.InnerText = thisOffender.FirstName + " " + thisOffender.LastName;
            ModalDateOfBirth.Text = thisOffender.DateOfBirth.ToString("MM/dd/yyyy");
            ModalGender.Text = thisOffender.Gender;
            ModalRace.Text = thisOffender.Race;
            ModalPhoneNumbers.DataSource = thisOffender.PhoneNumbers;
            ModalPhoneNumbers.DataBind();
            ModalAddresses.DataSource = thisOffender.Addresses;
            ModalAddresses.DataBind();

            ViewPersonModalPanel.CssClass += " visible";
        }

        protected internal void DeleteAffiliate(object sender, EventArgs e)
        {

        }

        protected internal void ViewAffiliate(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            Affiliate thisAffiliate = Affiliate.Get(PersonID);

            ModalName.InnerText = thisAffiliate.FirstName + " " + thisAffiliate.LastName;
            ModalDateOfBirth.Text = thisAffiliate.DateOfBirth.ToString("MM/dd/yyyy");
            ModalGender.Text = thisAffiliate.Gender;
            ModalRace.Text = thisAffiliate.Race;
            ModalPhoneNumbers.DataSource = thisAffiliate.PhoneNumbers;
            ModalPhoneNumbers.DataBind();
            ModalAddresses.DataSource = thisAffiliate.Addresses;
            ModalAddresses.DataBind();

            ViewPersonModalPanel.CssClass += " visible";
        }

        protected internal void DeleteCharge(object sender, EventArgs e)
        {

        }

        protected internal void ViewCharge(object sender, EventArgs e)
        {

        }

        protected internal void SwitchCaseList(object sender, EventArgs e)
        {
            string commandArg = (((LinkButton)sender).CommandArgument).ToString();

            switch (commandArg)
            {
                case "all":
                    cases = Case.GetCases();
                    CasesRepeater.DataSource = cases;
                    CasesRepeater.DataBind();
                    break;
                case "open":
                case "pending":
                case "closed":
                    cases = Case.GetCases(commandArg);
                    CasesRepeater.DataSource = cases;
                    CasesRepeater.DataBind();
                    break;
                default:
                    cases = Case.GetCases();
                    CasesRepeater.DataSource = cases;
                    CasesRepeater.DataBind();
                    break;
            }
        }

        protected internal void ClosePersonModalPanel(object Sender, EventArgs e)
        {
            ViewPersonModalPanel.CssClass = "modal-background";
        }

        protected internal void AddManager(object Sender, EventArgs e)
        {
            //thisCase.AddCaseManagerToCase()
        }

        protected internal void OpenManagerModalPanel(object Sender, EventArgs e)
        {
                        List<InternalUser> UserList = InternalUser.GetInternalUsers();
                        DropDownList populatedList = (DropDownList)FindControl("managerDropDown");

                        for (int i = 0; i < UserList.Count; i++)
                        {
                            ListItem newItem = new ListItem();
                            if (UserList[i].Role == Role.CASE_MANAGER)
                            {
                                newItem.Text = UserList[i].FirstName + " " + UserList[i].LastName;
                                newItem.Value = UserList[i].PersonID.ToString();
                                populatedList.Items.Add(newItem);
                            }
                        }

            addManagerPanel.CssClass += " visible";
        }
    }
}