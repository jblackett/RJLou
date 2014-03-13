using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public class Affiliate : Person
    {
        #region Private Variables
        //none yet
        #endregion

        #region Public Properties
        //none yet
        #endregion

        #region Constructors
        public Affiliate() { }
        #endregion

        #region Methods
        public static Affiliate Get(int personID)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                SELECT      a.Person_ID,
                            First_Name,
                            Last_Name,
                            Date_Of_Birth,
                            Gender,
                            Email,
                            Race
                FROM        Affiliate a 
                INNER JOIN  Person p ON a.Person_ID = p.Person_ID
                WHERE       a.Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Affiliate result = new Affiliate()
                    {
                        PersonID = Convert.ToInt32(read["Person_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(read["Date_Of_Birth"]),
                        Gender = read["Gender"].ToString(),
                        Email = read["Email"].ToString(),
                        Race = read["Race"].ToString()
                    };

                    result.GetPhoneNumbers();
                    result.GetAddresses();

                    return result;
                }
            }

            return null;
        }

        public static List<Affiliate> GetAffiliates()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                SELECT      a.Person_ID,
                            First_Name,
                            Last_Name,
                            Date_Of_Birth,
                            Gender,
                            Email,
                            Race
                FROM        Affiliate a 
                INNER JOIN  Person p ON a.Person_ID = p.Person_ID";
            List<Affiliate> results = new List<Affiliate>();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    Affiliate newAffiliate = new Affiliate()
                    {
                        PersonID = Convert.ToInt32(read["Person_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(read["Date_Of_Birth"]),
                        Gender = read["Gender"].ToString(),
                        Email = read["Email"].ToString(),
                        Race = read["Race"].ToString()
                    };

                    newAffiliate.GetPhoneNumbers();
                    newAffiliate.GetAddresses();

                    results.Add(newAffiliate);
                }
            }

            return results;
        }

        public static void Add(string fname, string lname, DateTime dob, string gender, string email,
            string race, List<PhoneNumber> numbers, List<Address> addresses)
        {
            int personID = Person.Add(fname, lname, dob, gender, email, race, numbers, addresses);

            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                INSERT INTO Affiliate (Person_ID)
                VALUES      (@PersonID)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);

                personID = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        internal override void Delete()
        {
            base.Delete();

            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM Affiliate WHERE Person_ID = @PersonID";

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
                UPDATE  Affiliate 
                SET     Relationship = @Relationship
                WHERE   Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}