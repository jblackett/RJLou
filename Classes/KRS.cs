using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RJLou.Classes
{
    public class KRS
    {
        #region Private Variables
        private string _krsCode;
        #endregion

        #region Public Properties
        public string KRSCode
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
        #endregion

        #region Constructors
        public KRS() { }

        public KRS(string krscode)
        {
            KRSCode = krscode;
        }
        #endregion

        #region Methods
        public static KRS Get(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "SELECT * FROM KRS WHERE KRS_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", id);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    KRS result = new KRS()
                    {
                        KRSCode = read["KRS_Code"].ToString()
                    };

                    return result;
                }
            }

            return null;
        }

        public static void Add(int krsCode)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "INSERT INTO KRS (KRS_Code) VALUES (@KRS_Code)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("KRS_Code", krsCode);

                cmd.ExecuteNonQuery();
            }
        }

        internal void Delete(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM KRS WHERE KRS_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("KRS_ID", id);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}