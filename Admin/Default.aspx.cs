using RJLou.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/************** THINGS TO BE DONE ***********************
 * 
 * 1. Add Case
 * 2. Role Management
 * 3. Victim/Affiliate/Offender
 *      - Adding/Deleting refreshes the page weird
 * 4. Notes
 *      - Edit
 *      - New
 *      - Delete(?)
 * 5. Charges
 *      - View
 *      - Add
 *      - Delete
 * 6. Documents
 *      - All of it
 * 7. Responsive Design
 * 
 * *****************************************************/
namespace RJLou
{
    public partial class Default : System.Web.UI.Page
    {
        Case thisCase;
        List<Case> cases;
        int PersonID = -1;
        List<Victim> allVictims;
        List<Offender> allOffenders;
        List<Affiliate> allAffiliates;
        List<InternalUser> allEmployees;

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
            int caseID = -1;

            try { caseID = thisCase.CaseID; }
            catch { }

            if (caseID <= 0)
                thisCase = Case.Get(Convert.ToInt32(Session["CaseID"]));

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

            CaseManagersRepeater.DataSource = thisCase.CaseManagers;
            CaseManagersRepeater.DataBind();
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

        protected void AddCase(object sender, EventArgs e)
        {
            NewCaseModalPanel.CssClass += " visible";
        }

        protected internal void CreateNewCase(object sender, EventArgs e)
        {
            if (ModalCourtID.Text.Length == 0)
                return;
            if (ModalStatus.Text.Length == 0)
                return;

            int newCaseID = Case.Add(
                Convert.ToInt32(ModalCourtID.Text), 
                (ModalReferralDate.Text.Length > 0 ? Convert.ToDateTime(ModalReferralDate.Text) : default(DateTime)),
                (ModalReferralNumber.Text.Length > 0 ? Convert.ToInt32(ModalReferralNumber.Text) : 0), 
                (ModalCourtDate.Text.Length > 0 ? Convert.ToDateTime(ModalCourtDate.Text) : default(DateTime)),
                ModalDistrict.Text, 
                (ModalDateFinalConf.Text.Length > 0 ? Convert.ToDateTime(ModalDateFinalConf.Text) : default(DateTime)),
                (ModalDateCompletion.Text.Length > 0 ? Convert.ToDateTime(ModalDateCompletion.Text) : default(DateTime)), 
                ModalStatus.Text);

            thisCase = Case.Get(newCaseID);
            SwitchCaseList(null, null);
            LoadHeader();
            BindData();

            Session["CaseID"] = CaseID;

            NewCaseModalPanel.CssClass = "modal-background";
        }

        protected internal void CancelNewCase(object sender, EventArgs e)
        {
            NewCaseModalPanel.CssClass = "modal-background";
        }

        protected void SetStatus(object sender, EventArgs e)
        {
            int caseID = -1;

            try { caseID = thisCase.CaseID; }
            catch { }

            if (caseID <= 0)
                thisCase = Case.Get(Convert.ToInt32(Session["CaseID"]));

            string newStatus = (((LinkButton)sender).CommandArgument).ToString();
            thisCase.Status = newStatus;
            LoadHeader();
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
                Label NoteAuthor = (Label)e.Item.FindControl("NoteAuthor");
                Label NoteDate = (Label)e.Item.FindControl("NoteDate");
                TextBox NoteText = (TextBox)e.Item.FindControl("NoteText");
                Note currentNote = (Note)e.Item.DataItem;

                if (currentNote.Author != null)
                {
                    NoteAuthor.Text = currentNote.Author.FirstName + " " + currentNote.Author.LastName;
                }
                else
                {
                    NoteAuthor.Text = "[[NO AUTHOR]]";
                }

                if (!string.IsNullOrEmpty(currentNote.EditDate.ToString()) && currentNote.EditDate != default(DateTime))
                {
                    NoteDate.Text = " - " + ((DateTime)currentNote.EditDate).ToString("MM/dd/yyyy");
                }
                else if (!string.IsNullOrEmpty(currentNote.CreateDate.ToString()) && currentNote.CreateDate != default(DateTime))
                {
                    NoteDate.Text = " - " + currentNote.CreateDate.ToString("MM/dd/yyyy");
                }

                NoteText.Text = currentNote.NoteText;
            }
        }

        protected void ChargesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void DocumentsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label DocumentPersonModifier = (Label)e.Item.FindControl("DocumentPersonModifier");
                Document thisDocument = (Document)e.Item.DataItem;

                DocumentPersonModifier.Text = thisDocument.PersonWhoModified.FirstName + " " + thisDocument.PersonWhoModified.LastName;
            }
        }

        protected void CaseManagersRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label casemanagerName = (Label)e.Item.FindControl("CaseManagerName");
                Label casemanagerRole = (Label)e.Item.FindControl("CaseManagerRole");
                InternalUser currentCaseManager = (InternalUser)e.Item.DataItem;

                string name = currentCaseManager.FirstName + " " + currentCaseManager.LastName;

                casemanagerName.Text = name;

                string role = currentCaseManager.Role.ToString();

                casemanagerRole.Text = role;
            }
        }

        protected internal void LoadCase(object sender, EventArgs e)
        {
            int CaseID = -1;
            int.TryParse(((LinkButton)sender).CommandArgument, out CaseID);

            thisCase = Case.Get(CaseID);

            Session["CaseID"] = CaseID;

            BindData();
            LoadHeader();
            RightContainer.Attributes.CssStyle["display"] = "block";
            UnloadCaseButton.CssClass = "undo show";
        }

        protected internal void UnloadCase(object sender, EventArgs e)
        {
            RightContainer.Attributes.CssStyle["display"] = "none";
            UnloadCaseButton.CssClass = "undo";
        }

        protected internal void LoadHeader()
        {
            case_info.InnerText = "Case #" + thisCase.CourtID;
            Status.InnerText = thisCase.Status;

            CaseID.Text = thisCase.CaseID.ToString();
            CaseID.ReadOnly = true;
            CourtID.Text = thisCase.CourtID.ToString("000000");
            ReferralDate.Text = (thisCase.ReferralDate == default(DateTime) ? "" : thisCase.ReferralDate.ToString("MM/dd/yyyy"));
            ReferralNumber.Text = thisCase.ReferralNumber.ToString();
            CourtDate.Text = (thisCase.CourtDate == default(DateTime) ? "" : thisCase.CourtDate.ToString("MM/dd/yyyy"));
            DateFinalConference.Text = (thisCase.DateOfFinalConference == default(DateTime) ? "" : thisCase.DateOfFinalConference.ToString("MM/dd/yyyy"));
            DateCompletion.Text = (thisCase.DateOfCompletion == default(DateTime) ? "" : thisCase.DateOfCompletion.ToString("MM/dd/yyyy"));
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

            thisCase.District = Convert.ToInt32(District.Text);

            thisCase.Status = Status.InnerText;


          
            thisCase.Update();
            CaseUpdatedPanel.CssClass += " visible";
        }

        protected internal void DeleteVictim(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            Victim thisVictim = Victim.Get(PersonID);
            int CaseID = Convert.ToInt32(Session["CaseID"]);
            thisCase = Case.Get(CaseID);

            thisCase.DeleteVictim(thisVictim);
            thisCase = Case.Get(CaseID);
            BindData();
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

        protected internal void AddVictim(object sender, EventArgs e)
        {
            ModalType.InnerText = "Select Victim";
            allVictims = Victim.GetVictims();

            foreach (Victim victim in allVictims)
                NewCasePersonList.Items.Add(new ListItem(victim.FirstName + " " + victim.LastName, victim.PersonID.ToString()));
            //NewCasePersonList.DataSource = allVictims;
            //NewCasePersonList.DataBind();

            AddPersonModalPanel.CssClass += " visible";

            Session["AddType"] = "Victim";
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
            int PersonID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            Offender thisOffender = Offender.Get(PersonID);
            int CaseID = Convert.ToInt32(Session["CaseID"]);
            thisCase = Case.Get(CaseID);

            thisCase.DeleteOffender(thisOffender);
            thisCase = Case.Get(CaseID);
            BindData();
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

        protected internal void AddOffender(object sender, EventArgs e)
        {
            ModalType.InnerText = "Select Offender";
            allOffenders = Offender.GetOffenders();

            foreach (Offender offender in allOffenders)
                NewCasePersonList.Items.Add(new ListItem(offender.FirstName + " " + offender.LastName, offender.PersonID.ToString()));
            //NewCasePersonList.DataSource = allVictims;
            //NewCasePersonList.DataBind();

            AddPersonModalPanel.CssClass += " visible";

            Session["AddType"] = "Offender";
            BindData();
        }

        protected internal void DeleteAffiliate(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            Affiliate thisAffiliate = Affiliate.Get(PersonID);
            int CaseID = Convert.ToInt32(Session["CaseID"]);
            thisCase = Case.Get(CaseID);

            thisCase.DeleteAffiliate(thisAffiliate);
            thisCase = Case.Get(CaseID);
            BindData();
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

        protected internal void AddAffiliate(object sender, EventArgs e)
        {
            ModalType.InnerText = "Select Affiliate";
            allAffiliates = Affiliate.GetAffiliates();

            foreach (Affiliate affiliate in allAffiliates)
                NewCasePersonList.Items.Add(new ListItem(affiliate.FirstName + " " + affiliate.LastName, affiliate.PersonID.ToString()));
            //NewCasePersonList.DataSource = allVictims;
            //NewCasePersonList.DataBind();

            AddPersonModalPanel.CssClass += " visible";

            Session["AddType"] = "Affiliate";
            BindData();
        }

        protected internal void AddPersonToCaseList(object sender, EventArgs e)
        {
            int PersonID = -1;
            int CaseID = -1;

            try
            {
                PersonID = Convert.ToInt32(NewCasePersonList.SelectedValue);
                CaseID = Convert.ToInt32(Session["CaseID"]);
            }
            catch { }

            if (PersonID <= 0 || CaseID <= 0)
                return;

            if (Session["AddType"].ToString().Equals("CaseManager"))
            {
                Case thisCase = Case.Get(CaseID);
                thisCase.AddCaseManager(InternalUser.Get(PersonID));
                BindData();
            }
            else
            {
                Case.AddPerson(PersonID, CaseID);
                BindData();
            }

            NewCasePersonList.Items.Clear();
            AddPersonModalPanel.CssClass = "modal-background";
        }

        protected internal void CloseAddPerson(object sender, EventArgs e)
        {
            NewCasePersonList.Items.Clear();
            AddPersonModalPanel.CssClass = "modal-background";
        }

        protected internal void EditNote(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            Label NoteAuthor = (Label)item.FindControl("NoteAuthor");
            Label NoteDate = (Label)item.FindControl("NoteDate");
            TextBox NoteText = (TextBox)item.FindControl("NoteText");

            int NoteID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            Note thisNote = Note.Get(NoteID);

            thisNote.NoteText = NoteText.Text;
            thisNote.Author = InternalUser.Get(Convert.ToInt32(Session["PersonID"]));
            thisNote.EditDate = DateTime.Now;

            thisNote.Update();

            BindData();
        }

        protected internal void CreateNote(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            TextBox NoteText = (TextBox)item.FindControl("NewNote");

            if (NoteText.Text.Length > 0)
            {
                int NoteID = Note.Add(DateTime.Now, InternalUser.Get(Convert.ToInt32(Session["PersonID"])), NoteText.Text);

                Note newNote = Note.Get(NoteID);
                Case thisCase = Case.Get(Convert.ToInt32(Session["CaseID"]));

                thisCase.AddNote(newNote);
            }

            BindData();
        }

        protected internal void DeleteCharge(object sender, EventArgs e)
        {
            int ChargeID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            Charge thisCharge = Charge.Get(ChargeID);
            int CaseID = Convert.ToInt32(Session["CaseID"]);
            thisCase = Case.Get(CaseID);

            thisCase.DeleteCharge(thisCharge);
            thisCase = Case.Get(CaseID);
            BindData();
        }

        protected internal void ViewCharge(object sender, EventArgs e)
        {
            int ChargeID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            Charge thisCharge = Charge.Get(ChargeID);

            ModalChargeNumber.InnerText = thisCharge.ChargeID.ToString();
            ModalUORCode.Text = thisCharge.UORCode.ToString();
            ModalDescription.Text = thisCharge.Description;

            ViewChargesModalPanel.CssClass += " visible";
        }

        protected internal void AddCharge(object sender, EventArgs e)
        {
            List<Charge> allCharges = Charge.GetCharges();

            foreach (Charge charge in allCharges)
                ChargesList.Items.Add(new ListItem(charge.UORCode + ", " + charge.Description, charge.ChargeID.ToString()));

            AddChargeModalPanel.CssClass += " visible";
        }

        protected internal void CloseCharges(object sender, EventArgs e)
        {
            ViewChargesModalPanel.CssClass = "modal-background";
        }

        protected internal void AddChargeToCase(object sender, EventArgs e)
        {
            int ChargeID = -1;
            int CaseID = -1;

            try
            {
                ChargeID = Convert.ToInt32(ChargesList.SelectedValue);
                CaseID = Convert.ToInt32(Session["CaseID"]);
            }
            catch { }

            if (ChargeID <= 0 || CaseID <= 0)
                return;

            Case thisCase = Case.Get(CaseID);
            thisCase.AddCharge(Charge.Get(ChargeID));
            BindData();

            AddChargeModalPanel.CssClass = "modal-background";
        }

        protected internal void CloseAddCharge(object sender, EventArgs e)
        {
            AddChargeModalPanel.CssClass = "modal-background";
        }

        protected internal void DeleteDocument(object sender, EventArgs e)
        {
            int documentID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            int caseID = Convert.ToInt32(Session["CaseID"]);
            Case thisCase = Case.Get(caseID);

            thisCase.DeleteDocument(Document.Get(documentID));
            thisCase = Case.Get(caseID);
            BindData();
        }

        protected internal void AddDocument(object sender, EventArgs e)
        {
            AddDocumentModalPanel.CssClass += " visible";
        }

        protected internal void UploadDocument(object sender, EventArgs e)
        {
            if (!ModalFileUploader.HasFile)
                return;

            string fileName = ModalFileUploader.PostedFile.FileName;
            string fileType = ModalFileUploader.PostedFile.ContentType;
            int fileSize = ModalFileUploader.PostedFile.ContentLength;
            int personID = Convert.ToInt32(Session["PersonID"]);
            int caseID = Convert.ToInt32(Session["CaseID"]);

            string uploadFolder = "~/documents/";

            ModalFileUploader.SaveAs(string.Format("{0}{1}", Server.MapPath(uploadFolder), fileName));

            string sql = "INSERT INTO Document (Case_ID, File_Location, Person_ID) VALUES (@CaseID, @FileLocation, @PersonID)";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", caseID);
                cmd.Parameters.AddWithValue("FileLocation", fileName);
                cmd.Parameters.AddWithValue("PersonID", personID);

                cmd.ExecuteNonQuery();
            }

            BindData();
            AddDocumentModalPanel.CssClass = "modal-background";
        }

        protected internal void CloseAddDocument(object sender, EventArgs e)
        {
            AddDocumentModalPanel.CssClass = "modal-background";
        }

        protected internal void SwitchCaseList(object sender, EventArgs e)
        {
            string commandArg = "";

            try { commandArg = (((LinkButton)sender).CommandArgument).ToString(); }
            catch { }
            InternalUser thisUser;

            switch (commandArg)
            {
                case "all":
                    thisUser = InternalUser.Get(PersonID);
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
                    break;
                case "open":
                case "pending":
                case "closed":
                    thisUser = InternalUser.Get(PersonID);
                    if (thisUser.Role == Role.CASE_MANAGER)
                    {
                        cases = Case.GetCases(true, thisUser.PersonID, commandArg);
                        CasesRepeater.DataSource = cases;
                        CasesRepeater.DataBind();
                    }
                    else
                    {
                        cases = Case.GetCases(true, commandArg);
                        CasesRepeater.DataSource = cases;
                        CasesRepeater.DataBind();
                    }
                    break;
                default:
                    thisUser = InternalUser.Get(PersonID);
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
                    break;
            }
        }

        //protected void NewCasePersonList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    NewCasePersonList.PageIndex = e.NewPageIndex;
        //    if (Session["AddType"].Equals("Victim"))
        //        NewCasePersonList.DataSource = allVictims;
        //    else if (Session["AddType"].Equals("Offender"))
        //        NewCasePersonList.DataSource = allOffenders;
        //    else if (Session["AddType"].Equals("Affiliate"))
        //        NewCasePersonList.DataSource = allAffiliates;
        //    NewCasePersonList.DataBind();
        //}

        //protected void NewCasePersonList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label PersonName = (Label)e.Row.FindControl("PersonName");
        //        Person thisPerson = (Person)e.Row.DataItem;

        //        PersonName.Text = thisPerson.FirstName + " " + thisPerson.LastName;
        //    }
        //}

        protected internal void ClosePerson(object sender, EventArgs e)
        {
            ViewPersonModalPanel.CssClass = "modal-background";
        }

        protected internal void AddManager(object Sender, EventArgs e)
        {
            thisCase = Case.Get(int.Parse(CaseID.Text));
            LinkButton b = Sender as LinkButton;
            if (b != null)
            {
                DropDownList c = (DropDownList)b.Parent.FindControl("ManagerDropDown");
                if (c != null)
                {
                    if (c.SelectedItem.Text != "")
                    {
                        int personID = int.Parse(c.SelectedValue);
                        InternalUser addedManager = InternalUser.Get(personID);
                        thisCase.AddCaseManager(addedManager);

                    }
                }
            }
        }

        protected internal void ViewCaseManager(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            InternalUser thisCaseManager = InternalUser.Get(PersonID);

            ModalName.InnerText = thisCaseManager.FirstName + " " + thisCaseManager.LastName;
            ModalDateOfBirth.Text = thisCaseManager.DateOfBirth.ToString("MM/dd/yyyy");
            ModalGender.Text = thisCaseManager.Gender;
            ModalRace.Text = thisCaseManager.Race;
            ModalPhoneNumbers.DataSource = thisCaseManager.PhoneNumbers;
            ModalPhoneNumbers.DataBind();
            ModalAddresses.DataSource = thisCaseManager.Addresses;
            ModalAddresses.DataBind();

            ViewPersonModalPanel.CssClass += " visible";
        }

        protected internal void DeleteCaseManager(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            InternalUser thisEmployee = InternalUser.Get(PersonID);
            int CaseID = Convert.ToInt32(Session["CaseID"]);
            thisCase = Case.Get(CaseID);

            thisCase.DeleteCaseManager(thisEmployee);
            thisCase = Case.Get(CaseID);
            BindData();
        }

        protected internal void OpenManagerModalPanel(object Sender, EventArgs e)
        {
            ModalType.InnerText = "Select Employee";
            allEmployees = InternalUser.GetInternalUsers();

            foreach (InternalUser employee in allEmployees)
                NewCasePersonList.Items.Add(new ListItem(employee.FirstName + " " + employee.LastName, employee.PersonID.ToString()));
            //NewCasePersonList.DataSource = allVictims;
            //NewCasePersonList.DataBind();

            AddPersonModalPanel.CssClass += " visible";

            Session["AddType"] = "CaseManager";
            //thisCase = Case.Get(int.Parse(CaseID.Text));
            //List<InternalUser> UserList = InternalUser.GetInternalUsers();
            //List<InternalUser> currentManagers = thisCase.CaseManagers;
            //LinkButton b = Sender as LinkButton;
            //if (b != null)
            //{
            //    DropDownList c = (DropDownList)b.Parent.FindControl("ManagerDropDown");
            //    if (c != null)
            //    {
            //        c.Items.Clear();
            //        for (int i = 0; i < UserList.Count; i++)
            //        {
            //            ListItem newItem = new ListItem();
            //            if (UserList[i].Role == Role.CASE_MANAGER)
            //            {
            //                newItem.Text = UserList[i].FirstName + " " + UserList[i].LastName;
            //                newItem.Value = UserList[i].PersonID.ToString();
            //                c.Items.Add(newItem);
            //            }
            //        }
            //    }

            //    DropDownList d = (DropDownList)b.Parent.FindControl("ddlCurrentManagers");
            //    if (d != null)
            //    {
            //        d.Items.Clear();
            //        if (currentManagers.Count != 0)
            //        {
            //            for (int i = 0; i < currentManagers.Count; i++)
            //            {
            //                ListItem newItem = new ListItem();
            //                newItem.Text = currentManagers[i].FirstName + " " + currentManagers[i].LastName;
            //                newItem.Value = "Value";
            //                d.Items.Add(newItem);
            //            }
            //        }
            //    }
            //}

            //addManagerPanel.CssClass += " visible";
        }
    }
}