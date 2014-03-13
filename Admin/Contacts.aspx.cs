using RJLou.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RJLou.Admin
{
    public partial class Contacts : System.Web.UI.Page
    {
        List<Victim> victims;
        List<Offender> offenders;
        List<InternalUser> employees;
        List<Guardian> guardians;
        List<Affiliate> affiliates;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                SwitchPersonList();
        }

        protected internal void SwitchPersonList(object sender, EventArgs e)
        {
            string commandArg = (((LinkButton)sender).CommandArgument).ToString();

            SwitchPersonList(commandArg);
        }

        protected internal void SwitchPersonList(string commandArg = "default")
        {
            switch (commandArg)
            {
                case "victims":
                    victims = Victim.GetVictims();
                    Session["PersonType"] = "victim";
                    PersonsRepeater.DataSource = victims;
                    PersonsRepeater.DataBind();
                    break;
                case "offenders":
                    offenders = Offender.GetOffenders();
                    Session["PersonType"] = "offender";
                    PersonsRepeater.DataSource = offenders;
                    PersonsRepeater.DataBind();
                    break;
                case "guardians":
                    guardians = Guardian.GetGuardians();
                    Session["PersonType"] = "guardian";
                    PersonsRepeater.DataSource = guardians;
                    PersonsRepeater.DataBind();
                    break;
                case "employees":
                    employees = InternalUser.GetInternalUsers();
                    Session["PersonType"] = "employee";
                    PersonsRepeater.DataSource = employees;
                    PersonsRepeater.DataBind();
                    break;
                case "affiliates":
                    affiliates = Affiliate.GetAffiliates();
                    Session["PersonType"] = "affiliate";
                    PersonsRepeater.DataSource = affiliates;
                    PersonsRepeater.DataBind();
                    break;
                default:
                    employees = InternalUser.GetInternalUsers();
                    Session["PersonType"] = "employee";
                    PersonsRepeater.DataSource = employees;
                    PersonsRepeater.DataBind();
                    break;
            }
        }

        protected internal void BindData(InternalUser thisPerson)
        {
            PhoneNumbersRepeater.DataSource = thisPerson.PhoneNumbers;
            PhoneNumbersRepeater.DataBind();

            AddressesRepeater.DataSource = thisPerson.Addresses;
            AddressesRepeater.DataBind();

            CasesRepeater.DataSource = thisPerson.GetCases();
            CasesRepeater.DataBind();
        }

        protected internal void LoadHeader(InternalUser thisPerson)
        {
            HeaderRelationship.Visible = false;
            HeaderPassword.Visible = true;
            HeaderUserType.Visible = true;
            HeaderOffenderNumber.Visible = false;

            FirstName.Text = thisPerson.FirstName.ToString();
            LastName.Text = thisPerson.LastName.ToString();
            DateOfBirth.Text = (thisPerson.DateOfBirth == default(DateTime) ? "" : thisPerson.DateOfBirth.ToString("MM/dd/yyyy"));
            Gender.Text = thisPerson.Gender.ToString();
            Email.Text = thisPerson.Email.ToString();
            Race.Text = thisPerson.Race.ToString();
            UserType.Text = thisPerson.Role.ToString();

            //foreach (ListItem item in UserType.Items)
            //{
            //    if (item.Value == thisPerson.Role.ToString().ToLower())
            //    {
            //        item.Selected = true;
            //        break;
            //    }
            //}

            MainContainer.Visible = true;
            GuardiansPanel.Visible = false;
        }

        protected internal void BindData(Victim thisPerson)
        {
            PhoneNumbersRepeater.DataSource = thisPerson.PhoneNumbers;
            PhoneNumbersRepeater.DataBind();

            AddressesRepeater.DataSource = thisPerson.Addresses;
            AddressesRepeater.DataBind();

            GuardiansRepeater.DataSource = thisPerson.Guardians;
            GuardiansRepeater.DataBind();

            CasesRepeater.DataSource = thisPerson.GetCases();
            CasesRepeater.DataBind();
        }

        protected internal void LoadHeader(Victim thisPerson)
        {
            HeaderRelationship.Visible = false;
            HeaderPassword.Visible = false;
            HeaderUserType.Visible = false;
            HeaderOffenderNumber.Visible = false;


            FirstName.Text = thisPerson.FirstName.ToString();
            LastName.Text = thisPerson.LastName.ToString();
            DateOfBirth.Text = (thisPerson.DateOfBirth == default(DateTime) ? "" : thisPerson.DateOfBirth.ToString("MM/dd/yyyy"));
            Gender.Text = thisPerson.Gender.ToString();
            Email.Text = thisPerson.Email.ToString();
            Race.Text = thisPerson.Race.ToString();

            MainContainer.Visible = true;
            GuardiansPanel.Visible = true;
        }

        protected internal void BindData(Offender thisPerson)
        {
            PhoneNumbersRepeater.DataSource = thisPerson.PhoneNumbers;
            PhoneNumbersRepeater.DataBind();

            AddressesRepeater.DataSource = thisPerson.Addresses;
            AddressesRepeater.DataBind();

            GuardiansRepeater.DataSource = thisPerson.Guardians;
            GuardiansRepeater.DataBind();

            CasesRepeater.DataSource = thisPerson.GetCases();
            CasesRepeater.DataBind();
        }

        protected internal void LoadHeader(Offender thisPerson)
        {
            HeaderRelationship.Visible = false;
            HeaderPassword.Visible = false;
            HeaderUserType.Visible = false;
            HeaderOffenderNumber.Visible = true;

            FirstName.Text = thisPerson.FirstName.ToString();
            LastName.Text = thisPerson.LastName.ToString();
            DateOfBirth.Text = (thisPerson.DateOfBirth == default(DateTime) ? "" : thisPerson.DateOfBirth.ToString("MM/dd/yyyy"));
            Gender.Text = thisPerson.Gender.ToString();
            Email.Text = thisPerson.Email.ToString();
            Race.Text = thisPerson.Race.ToString();
            OffenderNumber.Text = thisPerson.CourtID;

            MainContainer.Visible = true;
            GuardiansPanel.Visible = true;
        }

        protected internal void BindData(Guardian thisPerson)
        {
            PhoneNumbersRepeater.DataSource = thisPerson.PhoneNumbers;
            PhoneNumbersRepeater.DataBind();

            AddressesRepeater.DataSource = thisPerson.Addresses;
            AddressesRepeater.DataBind();

            CasesRepeater.DataSource = thisPerson.GetCases();
            CasesRepeater.DataBind();
        }

        protected internal void LoadHeader(Guardian thisPerson)
        {
            HeaderRelationship.Visible = true;
            HeaderPassword.Visible = false;
            HeaderUserType.Visible = false;
            HeaderOffenderNumber.Visible = false;

            FirstName.Text = thisPerson.FirstName.ToString();
            LastName.Text = thisPerson.LastName.ToString();
            DateOfBirth.Text = (thisPerson.DateOfBirth == default(DateTime) ? "" : thisPerson.DateOfBirth.ToString("MM/dd/yyyy"));
            Gender.Text = thisPerson.Gender.ToString();
            Email.Text = thisPerson.Email.ToString();
            Race.Text = thisPerson.Race.ToString();
            Relationship.Text = thisPerson.Relationship;

            MainContainer.Visible = true;
            GuardiansRepeater.Visible = false;
        }

        protected internal void BindData(Affiliate thisPerson)
        {
            PhoneNumbersRepeater.DataSource = thisPerson.PhoneNumbers;
            PhoneNumbersRepeater.DataBind();

            AddressesRepeater.DataSource = thisPerson.Addresses;
            AddressesRepeater.DataBind();

            CasesRepeater.DataSource = thisPerson.GetCases();
            CasesRepeater.DataBind();
        }

        protected internal void LoadHeader(Affiliate thisPerson)
        {
            HeaderRelationship.Visible = false;
            HeaderPassword.Visible = false;
            HeaderUserType.Visible = false;
            HeaderOffenderNumber.Visible = false;

            FirstName.Text = thisPerson.FirstName.ToString();
            LastName.Text = thisPerson.LastName.ToString();
            DateOfBirth.Text = (thisPerson.DateOfBirth == default(DateTime) ? "" : thisPerson.DateOfBirth.ToString("MM/dd/yyyy"));
            Gender.Text = thisPerson.Gender.ToString();
            Email.Text = thisPerson.Email.ToString();
            Race.Text = thisPerson.Race.ToString();

            MainContainer.Visible = true;
            GuardiansPanel.Visible = false;
        }

        protected internal void LoadPerson(object sender, EventArgs e)
        {
            int personID = -1;
            int.TryParse(((LinkButton)sender).CommandArgument, out personID); 
            string PersonType = "";
            try { PersonType = Session["PersonType"].ToString(); }
            catch { return; }

            switch (PersonType)
            {
                case "employee":
                    InternalUser thisEmployee = InternalUser.Get(personID);
                    LoadHeader(thisEmployee);
                    BindData(thisEmployee);
                    break;
                case "victim":
                    Victim thisVictim = Victim.Get(personID);
                    BindData(thisVictim);
                    LoadHeader(thisVictim);
                    break;
                case "offender":
                    Offender thisOffender = Offender.Get(personID);
                    BindData(thisOffender);
                    LoadHeader(thisOffender);
                    break;
                case "guardian":
                    Guardian thisGuardian = Guardian.Get(personID);
                    BindData(thisGuardian);
                    LoadHeader(thisGuardian);
                    break;
                case "affiliate":
                    Affiliate thisAffiliate = Affiliate.Get(personID);
                    BindData(thisAffiliate);
                    LoadHeader(thisAffiliate);
                    break;
                default:
                    InternalUser thisDefaultUser = InternalUser.Get(personID);
                    LoadHeader(thisDefaultUser);
                    BindData(thisDefaultUser);
                    break;
            }
        }

        protected void PersonsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton PersonButton = (LinkButton)e.Item.FindControl("PersonButton");
                Label Phone = (Label)e.Item.FindControl("Phone");
                Label Address = (Label)e.Item.FindControl("Address");
                string PersonType = Session["PersonType"].ToString();

                switch (PersonType)
                {
                    case "employee":
                        InternalUser thisInternalUser = (InternalUser)e.Item.DataItem;
                        PersonButton.Text = thisInternalUser.FirstName + " " + thisInternalUser.LastName;
                        Phone.Text = (thisInternalUser.PhoneNumbers.Count > 0 ? thisInternalUser.PhoneNumbers[0].Number.ToString() : "");
                        Address.Text = (thisInternalUser.Addresses.Count > 0 ? thisInternalUser.Addresses[0].ToString() : "");
                        break;
                    case "victim":
                        Victim thisVictim = (Victim)e.Item.DataItem;
                        PersonButton.Text = thisVictim.FirstName + " " + thisVictim.LastName;
                        Phone.Text = (thisVictim.PhoneNumbers.Count > 0 ? thisVictim.PhoneNumbers[0].Number.ToString() : "");
                        Address.Text = (thisVictim.Addresses.Count > 0 ? thisVictim.Addresses[0].ToString() : "");
                        break;
                    case "offender":
                        Offender thisOffender = (Offender)e.Item.DataItem;
                        PersonButton.Text = thisOffender.FirstName + " " + thisOffender.LastName;
                        Phone.Text = (thisOffender.PhoneNumbers.Count > 0 ? thisOffender.PhoneNumbers[0].Number.ToString() : "");
                        Address.Text = (thisOffender.Addresses.Count > 0 ? thisOffender.Addresses[0].ToString() : "");
                        break;
                    case "guardian":
                        Guardian thisGuardian = (Guardian)e.Item.DataItem;
                        PersonButton.Text = thisGuardian.FirstName + " " + thisGuardian.LastName;
                        Phone.Text = (thisGuardian.PhoneNumbers.Count > 0 ? thisGuardian.PhoneNumbers[0].Number.ToString() : "");
                        Address.Text = (thisGuardian.Addresses.Count > 0 ? thisGuardian.Addresses[0].ToString() : "");
                        break;
                    case "affiliate":
                        Affiliate thisAffiliate = (Affiliate)e.Item.DataItem;
                        PersonButton.Text = thisAffiliate.FirstName + " " + thisAffiliate.LastName;
                        Phone.Text = (thisAffiliate.PhoneNumbers.Count > 0 ? thisAffiliate.PhoneNumbers[0].Number.ToString() : "");
                        Address.Text = (thisAffiliate.Addresses.Count > 0 ? thisAffiliate.Addresses[0].ToString() : "");
                        break;
                    default:
                        InternalUser thisDefaultUser = (InternalUser)e.Item.DataItem;
                        PersonButton.Text = thisDefaultUser.FirstName + " " + thisDefaultUser.LastName;
                        Phone.Text = (thisDefaultUser.PhoneNumbers.Count > 0 ? thisDefaultUser.PhoneNumbers[0].Number.ToString() : "");
                        Address.Text = (thisDefaultUser.Addresses.Count > 0 ? thisDefaultUser.Addresses[0].ToString() : "");
                        break;
                }
            }
        }

        protected internal void SavePerson(object sender, EventArgs e)
        {

        }

        protected void PhoneNumbersRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected internal void DeletePhone(object sender, EventArgs e)
        {

        }

        protected internal void AddPhone(object sender, EventArgs e)
        {

        }

        protected void AddressesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected internal void DeleteAddress(object sender, EventArgs e)
        {

        }

        protected internal void AddAddress(object sender, EventArgs e)
        {

        }

        protected void GuardiansRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label guardianName = (Label)e.Item.FindControl("GuardianName");
                Guardian currentGuardian = (Guardian)e.Item.DataItem;

                string name = currentGuardian.FirstName + " " + currentGuardian.LastName;

                guardianName.Text = name;
            }
        }

        protected internal void ViewGuardian(object sender, EventArgs e)
        {

        }

        protected internal void DeleteGuardian(object sender, EventArgs e)
        {

        }

        protected internal void AddGuardian(object sender, EventArgs e)
        {

        }

        protected void CasesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected internal void ViewCase(object sender, EventArgs e)
        {

        }
    }
}