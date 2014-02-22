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
                        CourtID = default(int),
                        ReferralDate = Convert.ToDateTime(read["Referral_Date"]),
                        ReferralNumber = Convert.ToInt32(read["Referral_Number"]),
                        CourtDate = Convert.ToDateTime(read["Court_Date"]),
                        DateOfFinalConference = Convert.ToDateTime(read["Final_Conference_Date"]),
                        DateOfCompletion = Convert.ToDateTime(read["Closure_Date"]),
                        Status = read["Status"].ToString()
                    };

                    result.GetOffenders();
                    result.GetVictims();
                    result.GetAffiliates();
                    result.GetNotes();
                    result.GetCharges();
                    result.GetDocuments();

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

        }
        #endregion
        #region Note Methods
        internal void GetNotes()
        {

        }
        #endregion
        #region Charge Methods
        internal void GetCharges()
        {

        }
        #endregion
        #region Document Methods
        internal void GetDocuments()
        {

        }
        #endregion
        #region Case Manager Methods
        internal void GetCaseManagers()
        {

        }
        #endregion

        public static List<Case> GetCases()
        {
            List<Case> results = new List<Case>();
            string sql = "";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Case newCase = new Case()
                    {
                        CaseID = Convert.ToInt32(read["Case_ID"]),
                        CourtID = default(int),
                        ReferralDate = Convert.ToDateTime(read["Referral_Date"]),
                        ReferralNumber = Convert.ToInt32(read["Referral_Number"]),
                        CourtDate = Convert.ToDateTime(read["Court_Date"]),
                        DateOfFinalConference = Convert.ToDateTime(read["Final_Conference_Date"]),
                        DateOfCompletion = Convert.ToDateTime(read["Closure_Date"]),
                        Status = read["Status"].ToString()
                    };

                    newCase.GetOffenders();
                    newCase.GetVictims();
                    newCase.GetAffiliates();
                    newCase.GetNotes();
                    newCase.GetCharges();
                    newCase.GetDocuments();


                }
            }

            return results;
        }
        #endregion
    }
}