using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public class Offender : Person
    {
        #region Private Variables
        private List<Guardian> _guardians;
        private string _courtID;
        #endregion

        #region Properties
        public List<Guardian> Guardians
        {
            get
            {
                return _guardians;
            }
            set
            {
                _guardians = value;
            }
        }

        public string CourtID
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
       
        #endregion

        #region Constructors

        public Offender() : base(){}

        #endregion

        #region Methods
        public static Offender Get(int personID)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                SELECT      o.Person_ID,
                            First_Name,
                            Last_Name,
                            Date_Of_Birth,
                            Gender,
                            Email,
                            Race,
                            Offender_Number
                FROM        Offender o 
                INNER JOIN  Person p ON o.Person_ID = p.Person_ID
                WHERE       o.Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Offender result = new Offender()
                    {
                        PersonID = Convert.ToInt32(read["Person_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(read["Date_Of_Birth"]),
                        Gender = read["Gender"].ToString(),
                        Race = read["Race"].ToString(),
                        Email = read["Email"].ToString(),
                        CourtID = read["Offender_Number"].ToString()
                    };

                    result.GetPhoneNumbers();
                    result.GetAddresses();
                    result.GetGuardians();

                    return result;
                }
            }

            return null;
        }

        public static List<Offender> GetOffenders()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                SELECT      o.Person_ID,
                            First_Name,
                            Last_Name,
                            Date_Of_Birth,
                            Gender,
                            Email,
                            Race,
                            Offender_Number
                FROM        Offender o 
                INNER JOIN  Person p ON o.Person_ID = p.Person_ID";
            List<Offender> results = new List<Offender>();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    Offender newOffender = new Offender()
                    {
                        PersonID = Convert.ToInt32(read["Person_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(read["Date_Of_Birth"]),
                        Gender = read["Gender"].ToString(),
                        Race = read["Race"].ToString(),
                        Email = read["Email"].ToString(),
                        CourtID = read["Offender_Number"].ToString()
                    };

                    newOffender.GetPhoneNumbers();
                    newOffender.GetAddresses();
                    newOffender.GetGuardians();

                    results.Add(newOffender);
                }
            }

            return results;
        }

        public static int Add(string fname, string lname, DateTime dob, string gender, string email,
            string race, List<PhoneNumber> numbers, List<Address> addresses, int courtID)
        {
            int personID = Person.Add(fname, lname, dob, gender, email, race, numbers, addresses);

            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                INSERT INTO Offender (Person_ID, Offender_Number)
                VALUES      (@PersonID, @CourtID)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);
                cmd.Parameters.AddWithValue("CourtID", courtID);

                cmd.ExecuteNonQuery();
            }

            return personID;
        }

        internal override void Delete()
        {
            base.Delete();

            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM Offender WHERE Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);

                cmd.ExecuteNonQuery();
            }
        }

        
        internal override void Update()
        {
            base.Update();

            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                UPDATE  Offender 
                SET     Offender_Number = @CourtID
                WHERE   Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);
                cmd.Parameters.AddWithValue("CourtID", CourtID);

                cmd.ExecuteNonQuery();
            }
        }

        internal void GetGuardians()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();

            string sql = @"
                SELECT      g.* 
                FROM        Guardian g
                INNER JOIN  Guardian_List gl ON g.Guardian_ID = gl.Guardian_ID
                WHERE       gl.Person_ID = @PersonID";
            List<Guardian> results = new List<Guardian>();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(Guardian.GetByGID(Convert.ToInt32(read["Guardian_ID"])));
                }
            }

            Guardians = results;
        }

        internal void AddGuardian(Guardian newGuardian)
        {
            string sql = "INSERT INTO Guardian_List (Person_ID, Guardian_ID) VALUES (@PersonID, @GuardianID)";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);
                cmd.Parameters.AddWithValue("GuardianID", newGuardian.GuardianID);

                cmd.ExecuteNonQuery();
            }
        }

        internal void DeleteGuardian(Guardian oldGuardian)
        {
            string sql = "DELETE FROM Guardian_List WHERE Person_ID = @PersonID AND Guardian_ID = @GuardianID";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);
                cmd.Parameters.AddWithValue("GuardianID", oldGuardian.GuardianID);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}