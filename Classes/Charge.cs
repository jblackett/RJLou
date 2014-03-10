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
        private int _krsCode;
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
        public int KRSCode
        {
            get
            {
                return _krsCode;
            }
            set
            {
                _krsCode = value;
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
            string sql = @"
                SELECT      c.*,
                            k.KRS_Code
                FROM        Charge c
                INNER JOIN  KRS k ON c.KRS_ID = k.KRS_ID
                WHERE       Charge_ID = @ID";

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
                        KRSCode = Convert.ToInt32(read["KRS_Code"]),
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
            string sql = @"
                SELECT      c.*,
                            k.KRS_Code
                FROM        Charge c
                INNER JOIN  KRS k ON c.KRS_ID = k.KRS_ID";
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
                        KRSCode = Convert.ToInt32(read["KRS_Code"]),
                        UORCode = read["UOR_Code"].ToString(),
                        Description = read["Description"].ToString()
                    });
                }
            }

            return results;
        }

        public static void Add(int krsCode, string uorCode, string description)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
INSERT INTO Charge  (KRS_ID, UOR_Code, Description) 
VALUES              ((SELECT KRS_ID FROM KRS WHERE KRS_Code = @KRSCode)
                    , @UORCode, @Description)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("KRSCode", krsCode);
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

        internal void UpdateUOR()
        {
            string sql = @"
                UPDATE  CHARGE
                SET     UOR_Code = @UORCode,
                        Description = @Description,
                WHERE   Charge_ID = @ChargeID";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", ChargeID);
                cmd.Parameters.AddWithValue("UOR_Code", UORCode);
                cmd.Parameters.AddWithValue("Description", Description);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}