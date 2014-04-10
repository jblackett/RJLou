using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public class Guardian : Person
    {
        #region Private Variables
        private string _relationship;
        private int _guardianID;
        #endregion

        #region Properties
        public string Relationship
        {
            get
            {
                return _relationship;
            }
            set
            {
                _relationship = value;
            }
        }        

        public int GuardianID
        {
            get
            {
                return _guardianID;
            }
            set
            {
                _guardianID = value;
            }
        }
        #endregion

        #region Constructors

        public Guardian() : base(){}

        #endregion

        #region Methods
        public static Guardian Get(int personID)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                SELECT      g.Person_ID,
                            Guardian_ID,
                            First_Name,
                            Last_Name,
                            Date_Of_Birth,
                            Gender,
                            Email,
                            Race,
                            Relationship
                FROM        Guardian g 
                INNER JOIN  Person p ON g.Person_ID = p.Person_ID
                WHERE       g.Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Guardian result = new Guardian()
                    {
                        PersonID = Convert.ToInt32(read["Person_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(read["Date_Of_Birth"]),
                        Gender = read["Gender"].ToString(),
                        Race = read["Race"].ToString(),
                        Email = read["Email"].ToString(),
                        Relationship = read["Relationship"].ToString(),
                        GuardianID = Convert.ToInt32(read["Guardian_ID"])
                    };

                    result.GetPhoneNumbers();
                    result.GetAddresses();

                    return result;
                }
            }

            return null;
        }

        public static Guardian GetByGID(int guardianID)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                SELECT      g.Person_ID,
                            Guardian_ID,
                            First_Name,
                            Last_Name,
                            Date_Of_Birth,
                            Gender,
                            Email,
                            Race,
                            Relationship
                FROM        Guardian g 
                INNER JOIN  Person p ON g.Person_ID = p.Person_ID
                WHERE       g.Guardian_ID = @GuardianID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("GuardianID", guardianID);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Guardian result = new Guardian()
                    {
                        PersonID = Convert.ToInt32(read["Person_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(read["Date_Of_Birth"]),
                        Gender = read["Gender"].ToString(),
                        Race = read["Race"].ToString(),
                        Email = read["Email"].ToString(),
                        Relationship = read["Relationship"].ToString(),
                        GuardianID = Convert.ToInt32(read["Guardian_ID"])
                    };

                    result.GetPhoneNumbers();
                    result.GetAddresses();

                    return result;
                }
            }

            return null;
        }

        public static List<Guardian> GetGuardians()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                SELECT      g.Person_ID,
                            Guardian_ID,
                            First_Name,
                            Last_Name,
                            Date_Of_Birth,
                            Gender,
                            Email,
                            Race,
                            Relationship
                FROM        Guardian g 
                INNER JOIN  Person p ON g.Person_ID = p.Person_ID";
            List<Guardian> results = new List<Guardian>();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    Guardian newGuardian = new Guardian()
                    {
                        PersonID = Convert.ToInt32(read["Person_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(read["Date_Of_Birth"]),
                        Gender = read["Gender"].ToString(),
                        Race = read["Race"].ToString(),
                        Email = read["Email"].ToString(),
                        Relationship = read["Relationship"].ToString(),
                        GuardianID = Convert.ToInt32(read["Guardian_ID"])
                    };

                    newGuardian.GetPhoneNumbers();
                    newGuardian.GetAddresses();

                    results.Add(newGuardian);
                }
            }

            return results;
        }

        public static int Add(string fname, string lname, DateTime dob, string gender, string email,
            string race, List<PhoneNumber> numbers, List<Address> addresses, string relationship)
        {
            int personID = Person.Add(fname, lname, dob, gender, email, race, numbers, addresses);

            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                INSERT INTO Guardian (Person_ID, Relationship)
                VALUES      (@PersonID, @Relationship)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);
                cmd.Parameters.AddWithValue("Relationship", relationship);

                cmd.ExecuteNonQuery();
            }

            return personID;
        }

        internal override void Delete()
        {
            base.Delete();

            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM Guardian WHERE Person_ID = @PersonID";

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
                UPDATE  Guardian 
                SET     Relationship = @Relationship
                WHERE   Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);
                cmd.Parameters.AddWithValue("Relationship", Relationship);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

    }
}