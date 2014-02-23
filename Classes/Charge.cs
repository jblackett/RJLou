using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public class Charge
    {
        #region Private Variables
        private int _chargeID;
        private int _krsID;
        private string _uorCode;
        private string _description;
        #endregion

        #region Public Properties
        public int ChargeID
        {
            get
            {
                return _chargeID;
            }
            set
            {
                _chargeID = value;
            }
        }
        public int KRSID
        {
            get
            {
                return _krsID;
            }
            set
            {
                _krsID = value;
            }
        }
        public string UORCode
        {
            get
            {
                return _uorCode;
            }
            set
            {
                _uorCode = value;
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        #endregion

        #region Constructors
        public Charge() { }
        #endregion

        #region Methods
        public static Charge Get(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "SELECT * FROM Charge WHERE Charge_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", id);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Charge result = new Charge()
                    {
                        ChargeID = Convert.ToInt32(read["Charge_ID"]),
                        KRSID = Convert.ToInt32(read["KRS_ID"]),
                        UORCode = read["UOR_Code"].ToString(),
                        Description = read["Description"].ToString()
                    };

                    return result;
                }
            }

            return null;
        }

        public static List<Charge> GetCharges()
        {
            string sql = "SELECT * FROM Charge";
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<Charge> results = new List<Charge>();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(new Charge()
                    {
                        ChargeID = Convert.ToInt32(read["Charge_ID"]),
                        KRSID = Convert.ToInt32(read["KRS_ID"]),
                        UORCode = read["UOR_Code"].ToString(),
                        Description = read["Description"].ToString()
                    });
                }
            }

            return results;
        }

        public static void Add(int krsID, string uorCode, string description)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "INSERT INTO Charge (KRS_ID, UOR_Code, Description) VALUES (@KRSID, @UORCode, @Description)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("KRSID", krsID);
                cmd.Parameters.AddWithValue("UORCode", uorCode);
                cmd.Parameters.AddWithValue("Description", description);

                cmd.ExecuteNonQuery();
            }
        }

        internal void Delete()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM Charge WHERE Charge_ID = @ChargeID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ChargeID", ChargeID);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}