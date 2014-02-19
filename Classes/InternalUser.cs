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
        private Role _role;
        private string _password;

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

        public InternalUser() : base() { }

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
                        Password = read["Password"].ToString(),
                        Role = (Role)read["Title"]
                    };

                    result.GetPhoneNumbers();
                    result.GetAddresses();

                    return result;
                }
            }

            return null;
        }
    }

    public enum Role
    {
        ADMIN,
        CASE_MANAGER,
        FACILITATOR,
        VOLUNTEER
    }
}
