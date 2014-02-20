using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public abstract class Person
    {
        #region Private Variables
        private int _pID;
        private string _fname;
        private string _lname;
        private DateTime _dob;
        private string _gender;
        private string _email;
        private string _race;
        private List<PhoneNumber> _phoneNums;
        private List<Address> _addresses;
        private int _ssn;
        #endregion

        #region Public Properties
        public int PersonID
        {
            get
            {
                return _pID;
            }
            set
            {
                _pID = value;
            }
        }
        public string FirstName
        {
            get
            {
                return _fname;
            }
            set
            {
                _fname = value;
            }
        }
        public string LastName
        {
            get
            {
                return _lname;
            }
            set
            {
                _lname = value;
            }
        }
        public DateTime DateOfBirth
        {
            get
            {
                return _dob;
            }
            set
            {
                _dob = value;
            }
        }
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }
        public string Race
        {
            get
            {
                return _race;
            }
            set
            {
                _race = value;
            }
        }
        public List<PhoneNumber> PhoneNumbers
        {
            get
            {
                return _phoneNums;
            }
            set
            {
                _phoneNums = value;
            }
        }
        public List<Address> Addresses
        {
            get
            {
                return _addresses;
            }
            set
            {
                _addresses = value;
            }
        }
        public int SocialSecurityNumber
        {
            get
            {
                return _ssn;
            }
            set
            {
                _ssn = value;
            }
        }
        #endregion

        #region Constructors
        public Person() { }
        #endregion

        #region Methods
        internal void GetPhoneNumbers()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "SELECT * FROM Phone_List WHERE Person_ID = @PersonID";
            List<PhoneNumber> results = new List<PhoneNumber>();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(new PhoneNumber()
                    {
                        Number = Convert.ToInt32(read["Phone_Number"]),
                        PType = read["Phone_Type"].ToString()
                    });
                }
            }

            PhoneNumbers = results;
        }

        internal void GetAddresses()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "SELECT * FROM Address_List WHERE Person_ID = @PersonID";
            List<Address> results = new List<Address>();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(new Address()
                        {
                            streetAddress = read["Street_Address"].ToString(),
                            city = read["City"].ToString(),
                            state = read["State"].ToString(),
                            zip = Convert.ToInt32(read["Zip"])
                        });
                }
            }

            Addresses = results;
        }

        public static void Add(string fname, string lname, DateTime dob, string gender, string email,
            string race, List<PhoneNumber> numbers, List<Address> addresses)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                INSERT INTO Person  (First_Name, Last_Name, Date_Of_Birth, Gender, Email, Race)
                VALUES              (@FirstName, @LastName, @DOB, @Gender, @Email, @Race)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("FirstName", fname);
                cmd.Parameters.AddWithValue("LastName", lname);
                cmd.Parameters.AddWithValue("DOB", dob);
                cmd.Parameters.AddWithValue("Gender", gender);
                cmd.Parameters.AddWithValue("Email", email);
                cmd.Parameters.AddWithValue("Race", race);

                cmd.ExecuteNonQuery();
            }

            int ID = Person.GetPersonID(email);

            if (ID <= 0)
                return;

            foreach (PhoneNumber number in numbers)
            {
                sql = "INSERT INTO Phone_List (Person_ID, Phone_Type, Phone_Number) VALUES (@PID, @PType, @Number)";
                using (SqlConnection conn = new SqlConnection(dsn))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("PID", ID);
                    cmd.Parameters.AddWithValue("PType", number.PType);
                    cmd.Parameters.AddWithValue("Number", number.Number);

                    cmd.ExecuteNonQuery();
                }
            }

            foreach (Address address in addresses)
            {
                sql = @"
                    INSERT INTO Address_List
                                (Person_ID, Address_Type, Street_Address, City, State, Zip)
                    VALUES      (@PID, @AddressType, @Address, @City, @State, @Zip)";
                using (SqlConnection conn = new SqlConnection(dsn))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("PID", ID);
                    cmd.Parameters.AddWithValue("AddressType", null);
                    cmd.Parameters.AddWithValue("Address", address.streetAddress);
                    cmd.Parameters.AddWithValue("City", address.city);
                    cmd.Parameters.AddWithValue("State", address.state);
                    cmd.Parameters.AddWithValue("Zip", address.zip);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal virtual void Delete()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM Person WHERE Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);

                cmd.ExecuteNonQuery();
            }

            sql = "DELETE FROM Phone_List WHERE Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);

                cmd.ExecuteNonQuery();
            }

            sql = "DELETE FROM Address_List WHERE Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", PersonID);

                cmd.ExecuteNonQuery();
            }
        }

        internal virtual void Update()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                UPDATE  Person
                SET     First_Name = @FName,
                        Last_Name = @LName,
                        Date_Of_Birth = @DOB,
                        Gender = @Gender,
                        Email = @Email,
                        Race = @Race
                WHERE   Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("FName", FirstName);
                cmd.Parameters.AddWithValue("LName", LastName);
                cmd.Parameters.AddWithValue("DOB", DateOfBirth);
                cmd.Parameters.AddWithValue("Gender", Gender);
                cmd.Parameters.AddWithValue("Email", Email);
                cmd.Parameters.AddWithValue("Race", Race);
                cmd.Parameters.AddWithValue("PersonID", PersonID);

                cmd.ExecuteNonQuery();
            }
        }

        public static int GetPersonID(string email)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "SELECT Person_ID FROM Person WHERE Email = @Email";
            int result = -1;

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Email", email);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    result = Convert.ToInt32(read["Person_ID"]);
                }
            }

            return result;
        }
        #endregion
    }
}