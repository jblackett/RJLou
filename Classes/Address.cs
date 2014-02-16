using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public class Address
    {
        #region Private Variables
        private string _streetAddress;
        private string _city;
        private string _state;
        private int _zip;
        #endregion
        #region Public Properties
        public string streetAddress
        {
            get
            { return _streetAddress; }
            set
            {_streetAddress=value;}
        }
        public string city
        {
            get
            { return _city; }
            set 
            {_city =value;}
        }
        public string state
        {
            get { return _state; }
            set{_state=value;}
        }
        public int zip
        {
            get { return _zip; }
            set{_zip=value;}
        }
        #endregion
        #region Constructors
        public Address() { }
        public Address(string StAddress, string acity, string astate, int azip)
        { 
        streetAddress = StAddress;
        city = acity;
        state = astate;
        zip = azip;
        }
        #endregion
        #region Methods
        public static Address Get(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "SELECT * FROM Address WERE Address_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", id);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Address result = new Address()
                    {
                        streetAddress = read["Street_Address"].ToString(),
                        city= read["City"].ToString(),
                        state= read["State"].ToString(),
                        zip= Convert.ToInt32(read["Zip"])
                    };
                    return result;
            }
        }
                
           

            return null;
        }
        
    public static List<Address> GetAddresses()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<Address> results = new List<Address>();
            string sql = "SELECT * FROM Address_List";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

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

            return results;
        }

        public static List<Address> GetAddresses(int personID)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<Address> results = new List<Address>();
            string sql = "SELECT * FROM Address_List WHERE Person_ID = @PersonID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);

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

            return results;
        }

        public static List<Address> GetAddreses(string city)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<Address> results = new List<Address>();
            string sql = "SELECT * FROM Address_List WHERE City = @city";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("city", city);

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

            return results;
        }
public static List<Address> GetAddresesByState(string state)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<Address> results = new List<Address>();
            string sql = "SELECT * FROM Address_List WHERE State = @state";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("state", state);

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

            return results;
        }
public static List<Address> GetAddresesByZip(string zip)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<Address> results = new List<Address>();
            string sql = "SELECT * FROM Address_List WHERE Zip = @zip";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("zip", zip);

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

            return results;
        }
        public static void Add(string streetAddress, string city, string state, int zip)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "INSERT INTO Address_List (Street_Address, City, State, Zip) VALUES (@Street_Address, @City, @State, @Zip)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Street_Address", streetAddress);
                cmd.Parameters.AddWithValue("City", city);
                cmd.Parameters.AddWithValue("State", state);
                cmd.Parameters.AddWithValue("Zip", zip);

                cmd.ExecuteNonQuery();
            }
        }

        internal void Delete(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM Address_List WHERE Address_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", id);

                cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}

