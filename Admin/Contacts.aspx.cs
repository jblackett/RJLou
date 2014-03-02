using RJLou.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RJLou
{
    public partial class Contacts : System.Web.UI.Page
    {
        Person thisPerson;
        List<Person> persons;
        int PersonID = -1;

        //Guessing this is for logging in, so i'm not changing anything besides the caes, casesprepeater stuff
        //However, in our Persons class, we do not have a GetPersons method, so not sure what
        //you all want to do about that.
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
                    persons = Person.GetPersons(true, thisUser.PersonID);
                    PersonsRepeater.DataSource = persons;
                    PersonsRepeater.DataBind();
                }
                else
                {
                    persons = Person.GetPersons(true);
                    PersonsRepeater.DataSource = persons;
                    PersonsRepeater.DataBind();
                }
            }
        }

        //Have not changed since back in my Contacts.aspx page I didn't do anything with 
        //this seciton.
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

        protected internal void PersonsRepeater_Databind(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Guessing we still need this? changed case to person
                Label thisLabel = (Label)e.Item.FindControl("Name");
                Person currentPerson = (Person)e.Item.DataItem;

                string Name;

                //is this wehre we would bind phonenumbers or addresses?
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

        /* Not relevant
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
         */

        protected internal void LoadPerson(object sender, EventArgs e)
        {
            //hmm...used lowercase personID because I believe we were using uppercase
            //PersonID for the logged in user, correct?
            int personID = -1;
            int.TryParse(((LinkButton)sender).CommandArgument, out personID);

            thisPerson = Person.Get(PersonID);

            BindData();
            LoadHeader();
        }

        protected internal void LoadHeader()
        {
            FirstName.Text = thisPerson.FirstName.ToString();
            LastName.Text = thisPerson.LastName.ToString();
            DateOfBirth.Text = (thisPerson.DateOfBirth == default(DateTime) ? "" : thisPerson.DateOfBirth.ToString("MM/dd/yyyy"));
            Gender.Text = thisPerson.Gender.ToString();
            Email.Text = thisPerson.Email.ToString();
            Race.Text = thisPerson.Race.ToString();

            MainContainer.Visible = true;
        }

        protected internal void SaveCase(object sender, EventArgs e)
        {
            //when to use personID vs PersonID since PersonID at the top is also used for logging in?
            int personID = int.Parse(PersonID.Text);
            thisPerson = Person.Get(PersonID);



            thisPerson.PersonID = Convert.ToInt32(PersonID.Text);

            thisPerson.FirstName = FirstName.Text;
            thisPerson.LastName = LastName.Text;
            thisPerson.DateOfBirth = Convert.ToDateTime(ReferralDate.Text);
            thisPerson.Gender = Gender.Text;
            thisPerson.Email = Email.Text;
            thisPerson.Race = Race.Text;

            thisPerson.Update();
            PersonUpdatedPanel.CssClass += " visible";
        }

        /*
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
         */

        protected internal void SwitchPersonList(object sender, EventArgs e)
        {
            string commandArg = (((LinkButton)sender).CommandArgument).ToString();

            switch (commandArg)
            {
                case "all":
                    persons = Person.GetPersons();
                    PersonsRepeater.DataSource = persons;
                    PersonsRepeater.DataBind();
                    break;
                //Right now we have the other two columns as phone numbers and addresses,
                //In my notes in the other page, I think maybe do a title (victim, offender, case manager, etc)
                //and possible email instead.
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
    }
}