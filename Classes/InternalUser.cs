using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RJLou.Classes
{
    public class InternalUser : Person
    {
        #region Private Variables
        private Role _role;
        private string _password;
        #endregion

        #region Public Properties
        public Role Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        #endregion

        #region Constructor
        public InternalUser() : base() { }
        #endregion

        #region Methods
        internal void GetRole(string role)
        {
            role = role.ToUpper();

            switch (role)
            {
                case "ADMIN":
                    Role = Classes.Role.ADMIN;
                    break;
                case "CASE MANAGER":
                    Role = Classes.Role.CASE_MANAGER;
                    break;
                case "FACILITATOR":
                    Role = Classes.Role.FACILITATOR;
                    break;
                case "VOLUNTEER":
                    Role = Classes.Role.VOLUNTEER;
                    break;
                default:
                    Role = Classes.Role.ADMIN;
                    break;
            }
        }

        public static InternalUser Get(int personID)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                SELECT      iu.Person_ID,
                            First_Name,
                            Last_Name,
                            Date_Of_Birth,
                            Gender,
                            Email,
                            Race,
                            Password,
                            Title
                FROM        Internal_User iu 
                INNER JOIN  Person p ON iu.Person_ID = p.Person_ID
                INNER JOIN  User_Type ut ON iu.User_Type_ID = ut.User_Type_ID
                WHERE       iu.Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    InternalUser result = new InternalUser()
                    {
                        PersonID = Convert.ToInt32(read["Person_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(read["Date_Of_Birth"]),
                        Gender = read["Gender"].ToString(),
                        Race = read["Race"].ToString(),
                        Password = read["Password"].ToString()
                    };

                    result.GetPhoneNumbers();
                    result.GetAddresses();
                    result.GetRole(read["Title"].ToString());

                    return result;
                }
            }

            return null;
        }

        public static InternalUser Get(string email, string password)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                SELECT      iu.Person_ID,
                            First_Name,
                            Last_Name,
                            Date_Of_Birth,
                            Gender,
                            Email,
                            Race,
                            Password,
                            Title
                FROM        Internal_User iu 
                INNER JOIN  Person p ON iu.Person_ID = p.Person_ID
                INNER JOIN  User_Type ut ON iu.User_Type_ID = ut.User_Type_ID
                WHERE       Email = @Email
                AND         Password = @Password";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Email", email);
                cmd.Parameters.AddWithValue("Password", password);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    InternalUser result = new InternalUser()
                    {
                        PersonID = Convert.ToInt32(read["Person_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(read["Date_Of_Birth"]),
                        Gender = read["Gender"].ToString(),
                        Race = read["Race"].ToString(),
                        Password = read["Password"].ToString()
                    };

                    result.GetPhoneNumbers();
                    result.GetAddresses();
                    result.GetRole(read["Title"].ToString());

                    return result;
                }
            }

            return null;
        }

        public static List<InternalUser> GetInternalUsers()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                SELECT      iu.Person_ID,
                            First_Name,
                            Last_Name,
                            Date_Of_Birth,
                            Gender,
                            Email,
                            Race,
                            Password,
                            Title
                FROM        Internal_User iu 
                INNER JOIN  Person p ON iu.Person_ID = p.Person_ID
                INNER JOIN  User_Type ut ON iu.User_Type_ID = ut.User_Type_ID";
            List<InternalUser> results = new List<InternalUser>();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    InternalUser newUser = new InternalUser()
                    {
                        PersonID = Convert.ToInt32(read["Person_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(read["Date_Of_Birth"]),
                        Gender = read["Gender"].ToString(),
                        Race = read["Race"].ToString(),
                        Password = read["Password"].ToString()
                    };

                    newUser.GetPhoneNumbers();
                    newUser.GetAddresses();
                    newUser.GetRole(read["Title"].ToString());

                    results.Add(newUser);
                }
            }

            return results;
        }

        public static List<InternalUser> GetInternalUsers(Role role)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                SELECT      iu.Person_ID,
                            First_Name,
                            Last_Name,
                            Date_Of_Birth,
                            Gender,
                            Email,
                            Race,
                            Password,
                            Title
                FROM        Internal_User iu 
                INNER JOIN  Person p ON iu.Person_ID = p.Person_ID
                INNER JOIN  User_Type ut ON iu.User_Type_ID = ut.User_Type_ID
                WHERE       Title LIKE @Title";
            List<InternalUser> results = new List<InternalUser>();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Title", role.ToString());

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    InternalUser newUser = new InternalUser()
                    {
                        PersonID = Convert.ToInt32(read["Person_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(read["Date_Of_Birth"]),
                        Gender = read["Gender"].ToString(),
                        Race = read["Race"].ToString(),
                        Password = read["Password"].ToString()
                    };

                    newUser.GetPhoneNumbers();
                    newUser.GetAddresses();
                    newUser.GetRole(read["Title"].ToString());

                    results.Add(newUser);
                }
            }

            return results;
        }

        public static void Add(string fname, string lname, DateTime dob, string gender, string email,
            string race, List<PhoneNumber> numbers, List<Address> addresses, Role role, string password)
        {
            int personID = Person.Add(fname, lname, dob, gender, email, race, numbers, addresses);

            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                INSERT INTO Internal_User (Person_ID, User_Type_ID, Password)
                VALUES      (@PersonID, (SELECT User_Type_ID FROM User_Type WHERE Title LIKE @Role), @Password)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);
                cmd.Parameters.AddWithValue("Role", role);
                cmd.Parameters.AddWithValue("Password", password);

                personID = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        internal override void Delete()
        {
            base.Delete();

            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM Internal_User WHERE Person_ID = @PersonID";

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
                UPDATE  Internal_User 
                SET     User_Type_ID = (SELECT User_Type_ID FROM User_Type WHERE Title LIKE @Role),
                        Password = @Password
                WHERE   Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);
                cmd.Parameters.AddWithValue("Role", Role);
                cmd.Parameters.AddWithValue("Password", Password);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }

    public enum Role
    {
        ADMIN,
        CASE_MANAGER,
        FACILITATOR,
        VOLUNTEER
    }
}
