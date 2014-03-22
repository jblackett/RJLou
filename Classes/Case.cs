using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public class Case
    {
        #region Private Variables
        private int _caseID;
        private int _courtID;
        private List<Victim> _victims;
        private List<Offender> _offenders;
        private List<Affiliate> _affiliates;
        private List<Note> _notes;
        private DateTime _referralDate;
        private int _referralNum;
        private List<Charge> _charges;
        private DateTime _courtDate;
        private DateTime _dateFinalConf;
        private DateTime _dateCompletion;
        private string _status;
        private int _district;
        private List<Document> _documents;
        private List<InternalUser> _caseManagers;
        #endregion

        #region Public Properties
        public int CaseID
        {
            get
            {
                return _caseID;
            }
            set
            {
                _caseID = value;
            }
        }
        public int CourtID
        {
            get
            {
                return _courtID;
            }
            set
            {
                _courtID = value;
            }
        }
        public List<Victim> Victims
        {
            get
            {
                return _victims;
            }
            set
            {
                _victims = value;
            }
        }
        public List<Offender> Offenders
        {
            get
            {
                return _offenders;
            }
            set
            {
                _offenders = value;
            }
        }
        public List<Affiliate> Affiliates
        {
            get
            {
                return _affiliates;
            }
            set
            {
                _affiliates = value;
            }
        }
        public List<Note> Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                _notes = value;
            }
        }
        public DateTime ReferralDate
        {
            get
            {
                return _referralDate;
            }
            set
            {
                _referralDate = value;
            }
        }
        public int ReferralNumber
        {
            get
            {
                return _referralNum;
            }
            set
            {
                _referralNum = value;
            }
        }
        public List<Charge> Charges
        {
            get
            {
                return _charges;
            }
            set
            {
                _charges = value;
            }
        }
        public DateTime CourtDate
        {
            get
            {
                return _courtDate;
            }
            set
            {
                _courtDate = value;
            }
        }
        public DateTime DateOfFinalConference
        {
            get
            {
                return _dateFinalConf;
            }
            set
            {
                _dateFinalConf = value;
            }
        }
        public DateTime DateOfCompletion
        {
            get
            {
                return _dateCompletion;
            }
            set
            {
                _dateCompletion = value;
            }
        }
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }
        public int District
        {
            get
            {
                return _district;
            }
            set
            {
                _district = value;
            }
        }
        public List<Document> Documents
        {
            get
            {
                return _documents;
            }
            set
            {
                _documents = value;
            }
        }
        public List<InternalUser> CaseManagers
        {
            get
            {
                return _caseManagers;
            }
            set
            {
                _caseManagers = value;
            }
        }
        #endregion

        #region Constructors

        #endregion

        #region Methods
        public static Case Get(int caseID)
        {
            string sql = "SELECT * FROM RJL_Case WHERE Case_ID = @CaseID";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", caseID);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Case result = new Case()
                    {
                        CaseID = Convert.ToInt32(read["Case_ID"]),
                        CourtID = Convert.ToInt32(read["Court_ID"]),
                        ReferralDate = read["Referral_Date"] as DateTime? ?? default(DateTime),
                        ReferralNumber = Convert.ToInt32(read["Referral_Number"]),
                        CourtDate = read["Court_Date"] as DateTime? ?? default(DateTime),
                        DateOfFinalConference = read["Final_Conference_Date"] as DateTime? ?? default(DateTime),
                        DateOfCompletion = read["Closure_Date"] as DateTime? ?? default(DateTime),
                        Status = read["Status"].ToString(),
                        District = Convert.ToInt32(read["District"])
                    };

                    result.GetOffenders();
                    result.GetVictims();
                    result.GetAffiliates();
                    result.GetNotes();
                    result.GetCharges();
                    result.GetDocuments();
                    result.GetCaseManagers();
                    return result;
                }
            }

            return null;
        }

        #region Victim Methods
        internal void GetVictims()
        {
            string sql = @"
                SELECT      v.Person_ID
                FROM        Victim v
                INNER JOIN  Case_File cf ON v.Person_ID = cf.Person_ID
                WHERE       cf.Case_ID = @CaseID";
            List<Victim> results = new List<Victim>();

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(Victim.Get(Convert.ToInt32(read["Person_ID"])));
                }
            }

            Victims = results;
        }

        internal void AddVictim(Victim victim)
        {
            string sql = @"
                INSERT INTO Case_File (Case_ID, Person_ID)
                VALUES                (@CaseID, @PersonID)";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);
                cmd.Parameters.AddWithValue("PersonID", victim.PersonID);

                cmd.ExecuteNonQuery();
            }
        }

        internal void DeleteVictim(Victim victim)
        {
            string sql = @"
                DELETE FROM Case_File
                WHERE       Case_ID = @CaseID
                AND         Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);
                cmd.Parameters.AddWithValue("PersonID", victim.PersonID);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
        #region Offender Methods
        internal void GetOffenders()
        {
            string sql = @"
                SELECT      o.Person_ID
                FROM        Offender o
                INNER JOIN  Case_File cf ON o.Person_ID = cf.Person_ID
                WHERE       cf.Case_ID = @CaseID";
            List<Offender> results = new List<Offender>();

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(Offender.Get(Convert.ToInt32(read["Person_ID"])));
                }
            }

            Offenders = results;
        }

        internal void AddOffender(Offender offender)
        {
            string sql = @"
                INSERT INTO Case_File (Case_ID, Person_ID)
                VALUES                (@CaseID, @PersonID)";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);
                cmd.Parameters.AddWithValue("PersonID", offender.PersonID);

                cmd.ExecuteNonQuery();
            }
        }

        internal void DeleteOffender(Offender offender)
        {
            string sql = @"
                DELETE FROM Case_File
                WHERE       Case_ID = @CaseID
                AND         Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);
                cmd.Parameters.AddWithValue("PersonID", offender.PersonID);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
        #region Affiliate Methods
        internal void GetAffiliates()
        {
            string sql = @"
                SELECT      a.Person_ID
                FROM        Affiliate a
                INNER JOIN  Case_File cf ON a.Person_ID = cf.Person_ID
                WHERE       cf.Case_ID = @CaseID";
            List<Affiliate> results = new List<Affiliate>();

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(Affiliate.Get(Convert.ToInt32(read["Person_ID"])));
                }
            }

            Affiliates = results;
        }

        internal void AddAffiliate(Affiliate affiliate)
        {
            string sql = @"
                INSERT INTO Case_File (Case_ID, Person_ID)
                VALUES                (@CaseID, @PersonID)";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);
                cmd.Parameters.AddWithValue("PersonID", affiliate.PersonID);

                cmd.ExecuteNonQuery();
            }
        }

        internal void DeleteAffiliate(Affiliate affiliate)
        {
            string sql = @"
                DELETE FROM Case_File
                WHERE       Case_ID = @CaseID
                AND         Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);
                cmd.Parameters.AddWithValue("PersonID", affiliate.PersonID);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
        public static void AddPerson(int personID, int caseID)
        {
            string sql = "INSERT INTO Case_File (Person_ID, Case_ID) VALUES (@PersonID, @CaseID)";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);
                cmd.Parameters.AddWithValue("CaseID", caseID);

                cmd.ExecuteNonQuery();
            }
        }        
        #region Note Methods
        internal void GetNotes()
        {
            string sql = @"
                SELECT      n.Note_ID
                FROM        Note n
                INNER JOIN  Case_Note cn ON cn.Note_ID = n.Note_ID
                WHERE       cn.Case_ID = @CaseID";
            List<Note> results = new List<Note>();

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(Note.Get(Convert.ToInt32(read["Note_ID"])));
                }
            }

            Notes = results;
        }

        internal void AddNote(Note note)
        {
            string sql = @"
                INSERT INTO Case_Note (Case_ID, Note_ID)
                VALUES                (@CaseID, @NoteID)";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);
                cmd.Parameters.AddWithValue("NoteID", note.NoteID);

                cmd.ExecuteNonQuery();
            }
        }

        internal void DeleteNote(Note note)
        {
            string sql = @"
                DELETE FROM Case_Note
                WHERE       Case_ID = @CaseID
                AND         Note_ID = @NoteID";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);
                cmd.Parameters.AddWithValue("NoteID", note.NoteID);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
        #region Charge Methods
        internal void GetCharges()
        {
            string sql = @"
                SELECT      c.Charge_ID
                FROM        Charge c
                INNER JOIN  Case_Charge cc ON c.Charge_ID = cc.Charge_ID
                WHERE       cc.Case_ID = @CaseID";
            List<Charge> results = new List<Charge>();

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(Charge.Get(Convert.ToInt32(read["Charge_ID"])));
                }
            }

            Charges = results;
        }

        internal void AddCharge(Charge charge)
        {
            string sql = @"
                INSERT INTO Case_Charge (Case_ID, Charge_ID)
                VALUES                  (@CaseID, @ChargeID)";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);
                cmd.Parameters.AddWithValue("PersonID", charge.ChargeID);

                cmd.ExecuteNonQuery();
            }
        }

        internal void DeleteCharge(Charge charge)
        {
            string sql = @"
                DELETE FROM Case_Charge
                WHERE       Case_ID = @CaseID
                AND         Charge_ID = @Charge_ID";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);
                cmd.Parameters.AddWithValue("PersonID", charge.ChargeID);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
        #region Document Methods
        internal void GetDocuments()
        {
            string sql = @"
                SELECT      Document_ID
                FROM        Document
                WHERE       Case_ID = @CaseID";
            List<Document> results = new List<Document>();

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(Document.Get(Convert.ToInt32(read["Document_ID"])));
                }
            }

            Documents = results;
        }

        internal void AddDocument(Document doc)
        {
            Document.Add(doc.CaseID, doc.PersonWhoModified.PersonID, doc.FileLocation);
        }

        internal void DeleteDocument(Document doc)
        {
            doc.Delete();
        }
        #endregion
        #region Case Manager Methods
        internal void GetCaseManagers()
        {
            string sql = @"
                SELECT      c.Person_ID
                FROM        CASE_MANAGER c
                WHERE       c.Case_ID = @CaseID";
            List<InternalUser> results = new List<InternalUser>();

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(InternalUser.Get(Convert.ToInt32(read["Person_ID"])));
                }
            }

            CaseManagers = results;
        }

        internal void AddCaseManager(InternalUser manager)
        {
            string sql = @"
                INSERT INTO CASE_MANAGER (Case_ID, Person_ID)
                VALUES                (@CaseID, @PersonID)";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);
                cmd.Parameters.AddWithValue("PersonID", manager.PersonID);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        public static List<Case> GetCases()
        {
            List<Case> results = new List<Case>();
            string sql = "SELECT * FROM RJL_Case";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    Case newCase = new Case()
                    {
                        CaseID = Convert.ToInt32(read["Case_ID"]),
                        CourtID = default(int),
                        ReferralDate = read["Referral_Date"] as DateTime? ?? default(DateTime),
                        ReferralNumber = Convert.ToInt32(read["Referral_Number"]),
                        CourtDate = read["Court_Date"] as DateTime? ?? default(DateTime),
                        DateOfFinalConference = read["Final_Conference_Date"] as DateTime? ?? default(DateTime),
                        DateOfCompletion = read["Closure_Date"] as DateTime? ?? default(DateTime),
                        Status = read["Status"].ToString(),
                        District = Convert.ToInt32(read["District"])
                    };

                    newCase.GetOffenders();
                    newCase.GetVictims();
                    newCase.GetAffiliates();
                    newCase.GetNotes();
                    newCase.GetCharges();
                    newCase.GetDocuments();
                    newCase.GetCaseManagers();
                    results.Add(newCase);
                }
            }

            return results;
        }

        public static List<Case> GetCases(string status)
        {
            List<Case> results = new List<Case>();
            string sql = "SELECT * FROM RJL_Case WHERE Status LIKE @Status";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Status", status);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    Case newCase = new Case()
                    {
                        CaseID = Convert.ToInt32(read["Case_ID"]),
                        CourtID = default(int),
                        ReferralDate = read["Referral_Date"] as DateTime? ?? default(DateTime),
                        ReferralNumber = Convert.ToInt32(read["Referral_Number"]),
                        CourtDate = read["Court_Date"] as DateTime? ?? default(DateTime),
                        DateOfFinalConference = read["Final_Conference_Date"] as DateTime? ?? default(DateTime),
                        DateOfCompletion = read["Closure_Date"] as DateTime? ?? default(DateTime),
                        Status = read["Status"].ToString()
                    };

                    newCase.GetOffenders();
                    newCase.GetVictims();
                    newCase.GetAffiliates();
                    newCase.GetNotes();
                    newCase.GetCharges();
                    newCase.GetDocuments();
                    newCase.GetCaseManagers();
                    results.Add(newCase);
                }
            }

            return results;
        }

        public static List<Case> GetCases(bool getBasics)
        {
            if (getBasics)
            {
                List<Case> results = new List<Case>();
                string sql = "SELECT Case_ID, Status FROM RJL_Case";

                using (SqlConnection conn = new SqlConnection(Constants.DSN))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader read = cmd.ExecuteReader();

                    while (read.Read())
                    {
                        Case newCase = new Case()
                        {
                            CaseID = Convert.ToInt32(read["Case_ID"]),
                            Status = read["Status"].ToString()
                        };

                        newCase.GetOffenders();
                        newCase.GetVictims();

                        results.Add(newCase);
                    }
                }

                return results;
            }
            
            return null;
        }

        public static List<Case> GetCases(bool getBasics, int personID)
        {
            if (getBasics)
            {
                List<Case> results = new List<Case>();
                string sql = @"
                    SELECT      c.Case_ID, Status 
                    FROM        RJL_Case c
                    INNER JOIN  Case_Manager cm ON c.Case_ID = cm.Case_ID
                    WHERE       Person_ID = @PersonID";

                using (SqlConnection conn = new SqlConnection(Constants.DSN))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("PersonID", personID);

                    SqlDataReader read = cmd.ExecuteReader();

                    while (read.Read())
                    {
                        Case newCase = new Case()
                        {
                            CaseID = Convert.ToInt32(read["Case_ID"]),
                            Status = read["Status"].ToString()
                        };

                        newCase.GetOffenders();
                        newCase.GetVictims();

                        results.Add(newCase);
                    }
                }

                return results;
            }

            return null;
        }

        public static void Add(int courtID, DateTime refDate, int refNum, DateTime courtDate, string district,
            DateTime dateFinConf, DateTime dateComp, string status, List<Offender> offenders = null,
            List<Victim> victims = null, List<Affiliate> affiliates = null, List<Note> notes = null, 
            List<Charge> charges = null, List<Document> documents = null)
        {
            string sql = @"
                INSERT INTO RJL_Case 
                            (Court_ID, Referral_Date, Referral_Number, Court_Date, Final_Conference_Date, 
                                Status, Closure_Date, District)
                OUTPUT       INSERTED.Case_ID
                VALUES      (@CourtID, @ReferralDate, @ReferralNumber, @CourtDate, @FinalConferenceDate,
                                @Status, @ClosureDate, @District)";
            int caseID = -1;

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CourtID", courtID);
                cmd.Parameters.AddWithValue("ReferralDate", refDate);
                cmd.Parameters.AddWithValue("ReferralNumber", refNum);
                cmd.Parameters.AddWithValue("CourtDate", courtDate);
                cmd.Parameters.AddWithValue("FinalConferenceDate", dateFinConf);
                cmd.Parameters.AddWithValue("Status", status);
                cmd.Parameters.AddWithValue("ClosureDate", dateFinConf);
                cmd.Parameters.AddWithValue("District", district);

                caseID = Convert.ToInt32(cmd.ExecuteScalar());
            }

            Case thisCase = Case.Get(caseID);

            foreach (Offender offender in offenders)
            {
                thisCase.AddOffender(offender);
            }

            foreach (Victim victim in victims)
            {
                thisCase.AddVictim(victim);
            }

            foreach (Affiliate affiliate in affiliates)
            {
                thisCase.AddAffiliate(affiliate);
            }

            foreach (Note note in notes)
            {
                thisCase.AddNote(note);
            }

            foreach (Charge charge in charges)
            {
                thisCase.AddCharge(charge);
            }

            foreach (Document doc in documents)
            {
                thisCase.AddDocument(doc);
            }
        }

        internal void Delete()
        {
            string sql = "DELETE FROM RJL_Case WHERE Case_ID = @CaseID";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);

                cmd.ExecuteNonQuery();
            }

            Case thisCase = this;

            foreach (Offender offender in Offenders)
            {
                thisCase.DeleteOffender(offender);
            }

            foreach (Victim victim in Victims)
            {
                thisCase.DeleteVictim(victim);
            }

            foreach (Affiliate affiliate in Affiliates)
            {
                thisCase.DeleteAffiliate(affiliate);
            }

            foreach (Note note in Notes)
            {
                thisCase.DeleteNote(note);
            }

            foreach (Charge charge in Charges)
            {
                thisCase.DeleteCharge(charge);
            }

            foreach (Document doc in Documents)
            {
                thisCase.DeleteDocument(doc);
            }
        }

        internal void Update()
        {
            string sql = @"
                UPDATE  RJL_Case
                SET     Court_ID = @CourtID,
                        Referral_Date = @ReferralDate,
                        Referral_Number = @ReferralNumber,
                        Court_Date = @CourtDate,
                        Final_Conference_Date = @DateOfFinalConference,
                        Status = @Status,
                        Closure_Date = @DateOfCompletion,
                        District = @District
                WHERE   Case_ID = @CaseID";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", CaseID);
                cmd.Parameters.AddWithValue("CourtID", CourtID);
                if (ReferralDate == default(DateTime))
                    cmd.Parameters.AddWithValue("ReferralDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("ReferralDate", ReferralDate);
                cmd.Parameters.AddWithValue("ReferralNumber", ReferralNumber);
                if (CourtDate == default(DateTime))
                    cmd.Parameters.AddWithValue("CourtDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("CourtDate", CourtDate);
                if (DateOfFinalConference == default(DateTime))
                    cmd.Parameters.AddWithValue("DateOfFinalConference", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("DateOfFinalConference", DateOfFinalConference);
                cmd.Parameters.AddWithValue("Status", Status);
                if (DateOfCompletion == default(DateTime))
                    cmd.Parameters.AddWithValue("DateOfCompletion", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("DateOfCompletion", DateOfCompletion);
                cmd.Parameters.AddWithValue("District", District);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}