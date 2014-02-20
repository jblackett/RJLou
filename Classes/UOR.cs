using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RJLou.Classes
{
    public class UOR
    {
        #region Private Variables
        private string _uorCode;
        private string _uorDescription;
        #endregion

        #region Public Properties
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

        public string UORDescription
        {
            get
            {
                return _uorDescription;
            }
            set
            {
                _uorDescription = value;
            }
        }
        #endregion

        #region Constructors
        public UOR() { }

        public UOR(string uorcode, string uordescription)
        {
            UORCode = uorcode;
            UORDescription = uordescription;
        }
        #endregion

        #region Methods
        public static UOR Get(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "SELECT * FROM UOR HWERE UOR_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", id);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    UOR result = new UOR()
                    {
                        UORCode = read["UOR_Code"].ToString(),
                        UORDescription = read["UOR_Description"].ToString()
                    };

                    return result;
                }
            }

            return null;
        }

        public static void Add(int uorCode, int uorDescription)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "INSERT INTO UOR (UOR_Code, UOR_Description) VALUES (@UOR_Code, @UOR_Description)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("UOR_Code", uorCode);
                cmd.Parameters.AddWithValue("UOR_Description", uorDescription);

                cmd.ExecuteNonQuery();
            }
        }

        internal void Delete(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM UOR WHERE UOR_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("UOR_ID", id);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}