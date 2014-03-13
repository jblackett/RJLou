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

            PersonID.Text = thisPerson.PersonID.ToString();
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


            PersonID.Text = thisPerson.PersonID.ToString();
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

            PersonID.Text = thisPerson.PersonID.ToString();
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

            PersonID.Text = thisPerson.PersonID.ToString();
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

            PersonID.Text = thisPerson.PersonID.ToString();
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
            int personID = Convert.ToInt32(PersonID.Text);
            string PersonType = Session["PersonType"].ToString();

            switch (PersonType)
            {
                case "employee":
                    InternalUser thisEmployee = InternalUser.Get(personID);
                    if (!FirstName.Text.Equals(thisEmployee.FirstName))
                        thisEmployee.FirstName = FirstName.Text;
                    if (!LastName.Text.Equals(thisEmployee.LastName))
                        thisEmployee.LastName = LastName.Text;
                    if (!DateOfBirth.Text.Equals(thisEmployee.DateOfBirth.ToString()))
                    {
                        if (!string.IsNullOrEmpty(DateOfBirth.Text))
                            thisEmployee.DateOfBirth = Convert.ToDateTime(DateOfBirth.Text);
                        else
                            thisEmployee.DateOfBirth = default(DateTime);
                    }
                    if (!Gender.Text.Equals(thisEmployee.Gender))
                        thisEmployee.Gender = Gender.Text;
                    if (!Email.Text.Equals(thisEmployee.Email))
                        thisEmployee.Email = Email.Text;
                    if (!Race.Text.Equals(thisEmployee.Race))
                        thisEmployee.Race = Race.Text;

                    if (!string.IsNullOrEmpty(NewPassword.Text))
                        thisEmployee.Password = NewPassword.Text;
                    if (!UserType.Text.Equals(thisEmployee.Role.ToString()))
                        thisEmployee.GetRole(UserType.Text);

                    thisEmployee.Update();
                    break;
                case "victim":
                    Victim thisVictim = Victim.Get(personID);
                    if (!FirstName.Text.Equals(thisVictim.FirstName))
                        thisVictim.FirstName = FirstName.Text;
                    if (!LastName.Text.Equals(thisVictim.LastName))
                        thisVictim.LastName = LastName.Text;
                    if (!DateOfBirth.Text.Equals(thisVictim.DateOfBirth.ToString()))
                    {
                        if (!string.IsNullOrEmpty(DateOfBirth.Text))
                            thisVictim.DateOfBirth = Convert.ToDateTime(DateOfBirth.Text);
                        else
                            thisVictim.DateOfBirth = default(DateTime);
                    }
                    if (!Gender.Text.Equals(thisVictim.Gender))
                        thisVictim.Gender = Gender.Text;
                    if (!Email.Text.Equals(thisVictim.Email))
                        thisVictim.Email = Email.Text;
                    if (!Race.Text.Equals(thisVictim.Race))
                        thisVictim.Race = Race.Text;

                    thisVictim.Update();
                    break;
                case "offender":
                    Offender thisOffender = Offender.Get(personID);
                    if (!FirstName.Text.Equals(thisOffender.FirstName))
                        thisOffender.FirstName = FirstName.Text;
                    if (!LastName.Text.Equals(thisOffender.LastName))
                        thisOffender.LastName = LastName.Text;
                    if (!DateOfBirth.Text.Equals(thisOffender.DateOfBirth.ToString()))
                    {
                        if (!string.IsNullOrEmpty(DateOfBirth.Text))
                            thisOffender.DateOfBirth = Convert.ToDateTime(DateOfBirth.Text);
                        else
                            thisOffender.DateOfBirth = default(DateTime);
                    }
                    if (!Gender.Text.Equals(thisOffender.Gender))
                        thisOffender.Gender = Gender.Text;
                    if (!Email.Text.Equals(thisOffender.Email))
                        thisOffender.Email = Email.Text;
                    if (!Race.Text.Equals(thisOffender.Race))
                        thisOffender.Race = Race.Text;

                    if (!OffenderNumber.Text.Equals(thisOffender.CourtID))
                        thisOffender.CourtID = OffenderNumber.Text;

                    thisOffender.Update();
                    break;
                case "guardian":
                    Guardian thisGuardian = Guardian.Get(personID);
                    if (!FirstName.Text.Equals(thisGuardian.FirstName))
                        thisGuardian.FirstName = FirstName.Text;
                    if (!LastName.Text.Equals(thisGuardian.LastName))
                        thisGuardian.LastName = LastName.Text;
                    if (!DateOfBirth.Text.Equals(thisGuardian.DateOfBirth.ToString()))
                    {
                        if (!string.IsNullOrEmpty(DateOfBirth.Text))
                            thisGuardian.DateOfBirth = Convert.ToDateTime(DateOfBirth.Text);
                        else
                            thisGuardian.DateOfBirth = default(DateTime);
                    }
                    if (!Gender.Text.Equals(thisGuardian.Gender))
                        thisGuardian.Gender = Gender.Text;
                    if (!Email.Text.Equals(thisGuardian.Email))
                        thisGuardian.Email = Email.Text;
                    if (!Race.Text.Equals(thisGuardian.Race))
                        thisGuardian.Race = Race.Text;

                    if (!Relationship.Text.Equals(thisGuardian.Relationship))
                        thisGuardian.Relationship = Relationship.Text;

                    thisGuardian.Update();
                    break;
                case "affiliate":
                    Affiliate thisAffiliate = Affiliate.Get(personID);
                    if (!FirstName.Text.Equals(thisAffiliate.FirstName))
                        thisAffiliate.FirstName = FirstName.Text;
                    if (!LastName.Text.Equals(thisAffiliate.LastName))
                        thisAffiliate.LastName = LastName.Text;
                    if (!DateOfBirth.Text.Equals(thisAffiliate.DateOfBirth.ToString()))
                    {
                        if (!string.IsNullOrEmpty(DateOfBirth.Text))
                            thisAffiliate.DateOfBirth = Convert.ToDateTime(DateOfBirth.Text);
                        else
                            thisAffiliate.DateOfBirth = default(DateTime);
                    }
                    if (!Gender.Text.Equals(thisAffiliate.Gender))
                        thisAffiliate.Gender = Gender.Text;
                    if (!Email.Text.Equals(thisAffiliate.Email))
                        thisAffiliate.Email = Email.Text;
                    if (!Race.Text.Equals(thisAffiliate.Race))
                        thisAffiliate.Race = Race.Text;

                    thisAffiliate.Update();
                    break;
                default:
                    break;
            }

            PersonUpdatedPanel.CssClass += " visible";
        }

        protected void PhoneNumbersRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }

        protected internal void DeletePhone(object sender, EventArgs e)
        {
            int phoneID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            PhoneNumber thisNumber = PhoneNumber.Get(phoneID);

            thisNumber.Delete();

            string PersonType = Session["PersonType"].ToString();
            int personID = Convert.ToInt32(PersonID.Text);

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
                    break;
            }
        }

        protected internal void AddPhone(object sender, EventArgs e)
        {
            NewPhoneNumber.CssClass += " visible";
        }

        protected internal void SavePhone(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NewNumber.Text))
                return;

            int newNumber = Convert.ToInt32(NewNumber.Text);
            string newPhoneType = NewType.Text;
            int personID = Convert.ToInt32(PersonID.Text);

            PhoneNumber.Add(newNumber, personID, newPhoneType);

            string PersonType = Session["PersonType"].ToString();

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
                    break;
            }

            NewPhoneNumber.CssClass = "modal-background";
        }

        protected void AddressesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected internal void DeleteAddress(object sender, EventArgs e)
        {
            int addressID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            Address thisAddress = Address.Get(addressID);

            thisAddress.Delete();

            string PersonType = Session["PersonType"].ToString();
            int personID = Convert.ToInt32(PersonID.Text);

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
                    break;
            }
        }

        protected internal void AddAddress(object sender, EventArgs e)
        {
            NewAddressModal.CssClass += " visible";
        }

        protected internal void SaveAddress(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NewStreetAddress.Text) || string.IsNullOrEmpty(NewCity.Text) || string.IsNullOrEmpty(NewState.Text)
                || string.IsNullOrEmpty(NewZipCode.Text))
                return;

            string streetAddress = NewStreetAddress.Text;
            string city = NewCity.Text;
            string state = NewState.Text;
            int zip = Convert.ToInt32(NewZipCode.Text);
            string type = NewAddressType.Text;
            int personID = Convert.ToInt32(PersonID.Text);

            Address.Add(personID, streetAddress, city, state, zip, type);

            string PersonType = Session["PersonType"].ToString();

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
                    break;
            }

            NewAddressModal.CssClass = "modal-background";
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
            int PersonID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            Guardian thisGuardian = Guardian.Get(PersonID);

            ModalName.InnerText = thisGuardian.FirstName + " " + thisGuardian.LastName;
            ModalDateOfBirth.Text = thisGuardian.DateOfBirth.ToString("MM/dd/yyyy");
            ModalGender.Text = thisGuardian.Gender;
            ModalRace.Text = thisGuardian.Race;
            ModalPhoneNumbers.DataSource = thisGuardian.PhoneNumbers;
            ModalPhoneNumbers.DataBind();
            ModalAddresses.DataSource = thisGuardian.Addresses;
            ModalAddresses.DataBind();

            ViewPersonModalPanel.CssClass += " visible";
        }

        protected internal void DeleteGuardian(object sender, EventArgs e)
        {
            int guardianID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            int personID = Convert.ToInt32(PersonID.Text);

            Guardian thisGuardian = Guardian.GetByGID(guardianID);

            string PersonType = Session["PersonType"].ToString();

            switch (PersonType)
            {
                case "offender":
                    Offender thisOffender = Offender.Get(personID);
                    thisOffender.DeleteGuardian(thisGuardian);
                    thisOffender.GetGuardians();
                    BindData(thisOffender);
                    break;
                case "victim":
                    Victim thisVictim = Victim.Get(personID);
                    thisVictim.DeleteGuardian(thisGuardian);
                    thisVictim.GetGuardians();
                    BindData(thisVictim);
                    break;
                default:
                    break;
            }
        }

        protected internal void AddGuardian(object sender, EventArgs e)
        {
            List<Guardian> guardianList = Guardian.GetGuardians();
            var newGuardianList =   from x in guardianList
                                    select new
                                    {
                                        x.GuardianID,
                                        Name = String.Format("{0} {1}", x.FirstName, x.LastName)
                                    };

            NewGuardian.DataSource = newGuardianList;
            NewGuardian.DataTextField = "Name";
            NewGuardian.DataValueField = "GuardianID";
            NewGuardian.DataBind();

            NewGuardianModal.CssClass += " visible";
        }

        protected internal void SaveGuardian(object sender, EventArgs e)
        {
            int guardianPersonID = -1;
            int personID = Convert.ToInt32(PersonID.Text);

            foreach (ListItem item in NewGuardian.Items)
            {
                if (item.Selected)
                {
                    guardianPersonID = Convert.ToInt32(item.Value);
                }
            }

            Guardian thisGuardian = Guardian.GetByGID(guardianPersonID);

            string PersonType = Session["PersonType"].ToString();

            switch (PersonType)
            {
                case "victim":
                    Victim thisVictim = Victim.Get(personID);
                    thisVictim.AddGuardian(thisGuardian);
                    thisVictim.GetGuardians();
                    BindData(thisVictim);
                    break;
                case "offender":
                    Offender thisOffender = Offender.Get(personID);
                    thisOffender.AddGuardian(thisGuardian);
                    thisOffender.GetGuardians();
                    BindData(thisOffender);
                    break;
                default:
                    break;
            }

            NewGuardianModal.CssClass = "modal-background";
        }

        protected internal void CancelGuardian(object sender, EventArgs e)
        {
            NewGuardianModal.CssClass = "modal-background";
        }

        protected void CasesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected internal void ViewCase(object sender, EventArgs e)
        {
            int CaseID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            Case thisCase = Case.Get(CaseID);

            ModalCaseNumber.InnerText = thisCase.CourtID.ToString();
            ModalCaseID.Text = thisCase.CaseID.ToString();
            ModalReferralDate.Text = (thisCase.ReferralDate == default(DateTime) ? "" : thisCase.ReferralDate.ToString("MM/dd/yyyy"));
            ModalReferralNumber.Text = thisCase.ReferralNumber.ToString();
            ModalCourtDate.Text = (thisCase.CourtDate == default(DateTime) ? "" : thisCase.CourtDate.ToString("MM/dd/yyyy"));
            ModalDateFinalConference.Text = (thisCase.DateOfFinalConference == default(DateTime) ? "" : thisCase.DateOfFinalConference.ToString("MM/dd/yyyy"));
            ModalDateCompletion.Text = (thisCase.DateOfCompletion == default(DateTime) ? "" : thisCase.DateOfCompletion.ToString("MM/dd/yyyy"));
            ModalStatus.Text = thisCase.Status;
            ModalDistrict.Text = thisCase.District.ToString();

            ViewCaseModalPanel.CssClass += " visible";
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

        protected internal void CloseModal(object sender, EventArgs e)
        {
            ViewPersonModalPanel.CssClass = "modal-background";
            ViewCaseModalPanel.CssClass = "modal-background";
        }
    }
}