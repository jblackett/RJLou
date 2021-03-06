﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace RJLou.Classes
{
    public class Address
    {
        #region Private Variables
        private int _id;
        private string _streetAddress;
        private string _addressType;
        private string _city;
        private string _state;
        private int _zip;
        #endregion
        #region Public Properties
        public int AddressID
        {
            get
            { return _id; }
            set
            { _id = value; }
        }
        public string streetAddress
        {
            get
            { return _streetAddress; }
            set
            {_streetAddress=value;}
        }
        public string type
        {
            get
            { return _addressType; }
            set
            { _addressType = value; }
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
            string sql = "SELECT * FROM Address_List WHERE Address_ID = @ID";

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
                        AddressID = Convert.ToInt32(read["Address_ID"]),
                        streetAddress = read["Street_Address"].ToString(),
                        city= read["City"].ToString(),
                        state= read["State"].ToString(),
                        zip= Convert.ToInt32(read["Zip"]),
                        type = read["Address_Type"].ToString()
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
                        AddressID = Convert.ToInt32(read["Address_ID"]),
                        streetAddress = read["Street_Address"].ToString(),
                        city = read["City"].ToString(),
                        state = read["State"].ToString(),
                        zip = Convert.ToInt32(read["Zip"]),
                        type = read["Address_Type"].ToString()
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
                        AddressID = Convert.ToInt32(read["Address_ID"]),
                        streetAddress = read["Street_Address"].ToString(),
                        city = read["City"].ToString(),
                        state = read["State"].ToString(),
                        zip = Convert.ToInt32(read["Zip"]),
                        type = read["Address_Type"].ToString()
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
                        AddressID = Convert.ToInt32(read["Address_ID"]),
                        streetAddress = read["Street_Address"].ToString(),
                        city = read["City"].ToString(),
                        state = read["State"].ToString(),
                        zip = Convert.ToInt32(read["Zip"]),
                        type = read["Address_Type"].ToString()
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
                        AddressID = Convert.ToInt32(read["Address_ID"]),
                        streetAddress = read["Street_Address"].ToString(),
                        city = read["City"].ToString(),
                        state = read["State"].ToString(),
                        zip = Convert.ToInt32(read["Zip"]),
                        type = read["Address_Type"].ToString()
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
                        AddressID = Convert.ToInt32(read["Address_ID"]),
                        streetAddress = read["Street_Address"].ToString(),
                        city = read["City"].ToString(),
                        state = read["State"].ToString(),
                        zip = Convert.ToInt32(read["Zip"]),
                        type = read["Address_Type"].ToString()
                    });
                }
            }

            return results;
        }
        public static void Add(int personID, string streetAddress, string city, string state, int zip, string type = null)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "INSERT INTO Address_List (Person_ID, Street_Address, City, State, Zip, Address_Type) VALUES (@PersonID, @Street_Address, @City, @State, @Zip, @Type)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("PersonID", personID);
                cmd.Parameters.AddWithValue("Street_Address", streetAddress);
                cmd.Parameters.AddWithValue("City", city);
                cmd.Parameters.AddWithValue("State", state);
                cmd.Parameters.AddWithValue("Zip", zip);
                cmd.Parameters.AddWithValue("Type", type);

                cmd.ExecuteNonQuery();
            }
        }

        internal void Delete()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM Address_List WHERE Address_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", AddressID);

                cmd.ExecuteNonQuery();
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(streetAddress);
            result.Append(", ");
            result.Append(city);
            result.Append(", ");
            result.Append(state);
            result.Append(" ");
            result.Append(zip);

            return result.ToString();
        }

        #endregion
    }
}

