using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public class Victim
    {
        #region Private variables
        private List<Guardian> _guardians;
        #endregion

        #region Public properties
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
        #endregion

        #region Constructors
        public Victim() : base() { }
        #endregion

        #region Methods
        public static Victim Get(int personID)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                SELECT      v.Person_ID,
                            First_Name,
                            Last_Name,
                            Date_Of_Birth,
                            Gender,
                            Email,
                            Race
                FROM        Victim v
                INNER JOIN  Person p ON v.Person_ID = p.Person_ID
                WHERE       v.Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Victim result = new Victim()
                    {
                        PersonID = Convert.ToInt32(read["Person_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(read["Date_Of_Birth"]),
                        Gender = read["Gender"].ToString(),
                        Race = read["Race"].ToString()
                    };

                    result.GetPhoneNumbers();
                    result.GetAddresses();
                    result.GetGuardians();


                    return result;
                }
            }

            return null;
        }

        public static List<Victim> GetVictims()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                SELECT      v.Person_ID,
                            First_Name,
                            Last_Name,
                            Date_Of_Birth,
                            Gender,
                            Email,
                            Race
                FROM        Victim v 
                INNER JOIN  Person p ON v.Person_ID = p.Person_ID
                WHERE       v.Person_ID = @PersonID";
            List<Victim> results = new List<Victim>();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    Victim newVictim = new Victim()
                    {
                        PersonID = Convert.ToInt32(read["Person_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(read["Date_Of_Birth"]),
                        Gender = read["Gender"].ToString(),
                        Race = read["Race"].ToString()
                    };

                    newVictim.GetPhoneNumbers();
                    newVictim.GetAddresses();
                    newVictim.GetGuardians();

                    results.Add(newVictim);
                }
            }

            return results;
        }

        public static void Add(string fname, string lname, DateTime dob, string gender, string email,
            string race, List<PhoneNumber> numbers, List<Address> addresses, List<Guardian> guardian)
        {
            int personID = Person.Add(fname, lname, dob, gender, email, race, numbers, addresses, guardian);

            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                INSERT INTO Victim (Person_ID)
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
            string sql = "DELETE FROM Victim WHERE Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);

                cmd.ExecuteNonQuery();
            }
        }

        //Does victim/offender even need a unique update method if it just inherits from person?
        //updating guardian list?
        //internal override void Update()

        //Should this part be getting a list of guardians instead?
        //what about the results statement?
       /* internal void GetGuardians()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();

            string sql = "SELECT * FROM Guardian_List WHERE Person_ID = @PersonID";
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
                    results.Add(new Guardian()
                    {
                        //what goes here? -joe  
                    });
                }
            }

            PhoneNumbers = results;
        }*/
        #endregion
    }
}