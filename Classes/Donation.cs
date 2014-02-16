using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public class Donation
    {
        #region Private Variables
        private int _donationID;
        private int _donorID;
        private double _amount;
        private int _eventID;
        #endregion

        #region Public Properties
        public int DonationID
        {
            get
            {
                return _donationID;
            }

            set
            {
                _donationID = value;
            }
        }

        public int DonorID
        {
            get
            {
                return _donorID;
            }

            set
            {
                _donorID = value;
            }
        }

        public double Amount
        {
            get
            {
                return _amount;
            }

            set
            {
                _amount = value;
            }
        }

        public int EventID
        {
            get
            {
                return _eventID;
            }

            set
            {
                _eventID = value;
            }
        }
        #endregion

        #region Constructors
        public Donation() { }
        #endregion

        #region Methods
        public static Donation Get(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "SELECT * FROM Donation WHERE Donation_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", id);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Donation result = new Donation()
                    {
                        DonationID = Convert.ToInt32(read["Donation_ID"]),
                        DonorID = Convert.ToInt32(read["Donor_ID"]),
                        Amount = Convert.ToDouble(read["Amount"]),
                        EventID = Convert.ToInt32(read["Event_ID"])
                    };

                    return result;
                }
            }

            return null;
        }

        public static List<Donation> GetDonations()
        {
            string sql = "SELECT * FROM Donation";
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<Donation> results = new List<Donation>();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(new Donation()
                        {
                            DonationID = Convert.ToInt32(read["Donation_ID"]),
                            DonorID = Convert.ToInt32(read["Donor_ID"]),
                            Amount = Convert.ToDouble(read["Amount"]),
                            EventID = Convert.ToInt32(read["Event_ID"])
                        });
                }
            }

            return results;
        }

        public static List<Donation> GetDonationsByDonor(int donorID)
        {
            string sql = "SELECT * FROM Donation WHERE Donor_ID = @DonorID";
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<Donation> results = new List<Donation>();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("DonorID", donorID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(new Donation()
                    {
                        DonationID = Convert.ToInt32(read["Donation_ID"]),
                        DonorID = Convert.ToInt32(read["Donor_ID"]),
                        Amount = Convert.ToDouble(read["Amount"]),
                        EventID = Convert.ToInt32(read["Event_ID"])
                    });
                }
            }

            return results;
        }

        public static List<Donation> GetDonationsByEvent(int eventID)
        {
            string sql = "SELECT * FROM Donation WHERE Event_ID = @EventID";
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<Donation> results = new List<Donation>();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("EventID", eventID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(new Donation()
                    {
                        DonationID = Convert.ToInt32(read["Donation_ID"]),
                        DonorID = Convert.ToInt32(read["Donor_ID"]),
                        Amount = Convert.ToDouble(read["Amount"]),
                        EventID = Convert.ToInt32(read["Event_ID"])
                    });
                }
            }

            return results;
        }

        public static void Add(int donorID, double amount, int eventID)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "INSERT INTO Dontion (Donor_ID, Amount, Event_ID) VALUES (@DonorID, @Amount, @EventID)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("DonorID", donorID);
                cmd.Parameters.AddWithValue("Amount", amount);
                cmd.Parameters.AddWithValue("EventID", eventID);

                cmd.ExecuteNonQuery();
            }
        }

        internal void Delete()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM Donation WHERE Donation_ID = @DonationID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", DonationID);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}