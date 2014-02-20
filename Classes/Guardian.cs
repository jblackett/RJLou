using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public class Guardian : Person
    {
        #region Private Variables
        private string _relationship;
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
                        Relationship = read["Relationship"].ToString(),
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
                        Relationship = read["Relationship"].ToString()
                    };

                    newGuardian.GetPhoneNumbers();
                    newGuardian.GetAddresses();

                    results.Add(newGuardian);
                }
            }

            return results;
        }

        public static void Add(string fname, string lname, DateTime dob, string gender, string email,
            string race, List<PhoneNumber> numbers, List<Address> addresses, string relationship)
        {
            int personID = Person.Add(fname, lname, dob, gender, email, race, numbers, addresses, relationship);

            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                INSERT INTO Guardian (Person_ID)
                VALUES      (@PersonID, @Relationship)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);
                cmd.Parameters.AddWithValue("Relationship", relationship);

                personID = Convert.ToInt32(cmd.ExecuteScalar());
            }
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