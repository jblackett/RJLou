using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RJLou.Classes
{
    class PhoneNumber
    {
        #region Private Variables
        private string _pType;
        private int _number;
        #endregion

        #region Public Properties
        public string PType
        {
            get
            {
                return _pType;
            }
            set
            {
                _pType = value;
            }
        }

        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
            }
        }
        #endregion

        #region Constructors
        public PhoneNumber() { }

        public PhoneNumber(string type, int number)
        {
            PType = type;
            Number = number;
        }
        #endregion

        #region Methods
        public static PhoneNumber Get(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "SELECT * FROM Phone_List WHERE Phone_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", id);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    PhoneNumber result = new PhoneNumber()
                    {
                        PType = read["Phone_Type"].ToString(),
                        Number = Convert.ToInt32(read["Phone_Number"])
                    };

                    return result;
                }
            }

            return null;
        }

        public static List<PhoneNumber> GetPhoneNumbers()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<PhoneNumber> results = new List<PhoneNumber>();
            string sql = "SELECT * FROM Phone_List";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(new PhoneNumber()
                    {
                        PType = read["Phone_Type"].ToString(),
                        Number = Convert.ToInt32(read["Phone_Number"])
                    });
                }
            }

            return results;
        }

        public static List<PhoneNumber> GetPhoneNumbers(int personID)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<PhoneNumber> results = new List<PhoneNumber>();
            string sql = "SELECT * FROM Phone_List WHERE Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(new PhoneNumber()
                    {
                        PType = read["Phone_Type"].ToString(),
                        Number = Convert.ToInt32(read["Phone_Number"])
                    });
                }
            }

            return results;
        }

        public static List<PhoneNumber> GetPhoneNumbers(string type)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<PhoneNumber> results = new List<PhoneNumber>();
            string sql = "SELECT * FROM Phone_List WHERE Phone_Type = @Type";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Type", type);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(new PhoneNumber()
                    {
                        PType = read["Phone_Type"].ToString(),
                        Number = Convert.ToInt32(read["Phone_Number"])
                    });
                }
            }

            return results;
        }

        public static void Add(int phoneNumber, int personID, string type = null)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "INSERT INTO Phone_List (Phone_Number, Person_ID, Phone_Type) VALUES (@PhoneNumber, @PersonID, @Type)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PhoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("PersonID", personID);
                cmd.Parameters.AddWithValue("Type", type);

                cmd.ExecuteNonQuery();
            }
        }

        internal void Delete(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM Phone_List WHERE Phone_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", id);

                cmd.ExecuteNonQuery();
            }
        }

        public override string ToString()
        {
            string number_string = Number.ToString();
            string area_code;
            string first_three;
            string last_four;
            area_code=number_string.Substring(0,3);
            first_three=number_string.Substring(3,3);
            last_four=number_string.Substring(6,4);
            return String.Format("({0}){1}-{2}", area_code, first_three, last_four);
        }
        #endregion
    }
}
