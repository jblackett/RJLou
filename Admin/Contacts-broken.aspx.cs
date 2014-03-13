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
        int PersonID = -1;
        List<Offender> offenders;
        List<Victim> victims;
        List<InternalUser> internalUsers;
        List<object> persons;
        Person thisPerson;

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
                victims = Victim.GetVictims();
                offenders = Offender.GetOffenders();
                internalUsers = InternalUser.GetInternalUsers();
                foreach(var person in victims)
                {
                    persons.Add(person);
                }
                foreach (var person in offenders)
                {
                    persons.Add(person);
                }
                foreach (var person in internalUsers)
                {
                    persons.Add(person);
                }

                PersonsRepeater.DataSource = persons;
                PersonsRepeater.DataBind();
            }
        }

        
        protected internal void BindData()
        {
            //PersonCasesRepeater.DataSource = thisPerson.Cases;
            //PersonCasesRepeater.DataBind();

            //GuardiansRepeater.DataSource = thisPerson.Guardians;
            //GuardiansRepeater.DataBind();
        }

        protected internal void PersonsRepeater_Databind(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label thisLabel = (Label)e.Item.FindControl("Name");
                Person currentPerson = (Person)e.Item.DataItem;

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