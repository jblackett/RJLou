using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    class Document
    {
        #region Private Variables
        private int _CaseID;
        private string _FirstName;
        private string _LastName;
        #endregion

        #region Public Properties
        public int CaseID
        {
            get
            {
                return _CaseID;
            }
            set
            {
                _CaseID = value;
            }
        }
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
            }
        }
        #endregion

        #region Constructors
        public Document() { }

        public Document(int CID, string FName, string LName)
        {
            CaseID = CID;
            FirstName = FName;
            LastName = LName;
        }
        #endregion

        #region Methods
        public static Document Get(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();

            string sql = "SELECT * FROM DOCUMENT WHERE Document_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", id);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Document result = new Document()
                    {
                        CaseID = Convert.ToInt32(read["Case_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString()
                    };

                    return result;
                }
            }

            return null;

        }
        public static List<Document> GetDocuments()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<Document> results = new List<Document>();
            string sql = "SELECT * FROM DOCUMENT";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(new Document()
                    {
                        CaseID = Convert.ToInt32(read["Case_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString()
                    });
                }
            }

            return results;
        }

        public static List<Document> GetDocuments(int caseID)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<Document> results = new List<Document>();
            string sql = "SELECT * FROM DOCUMENT WHERE Case_ID = @caseID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Case_ID", caseID);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(new Document()
                    {
                        CaseID = Convert.ToInt32(read["Case_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString()
                    });
                }
            }

            return results;
        }

        public static List<Document> GetDocuments(string firstName)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<Document> results = new List<Document>();
            string sql = "SELECT * FROM DOCUMENT WHERE First_Name = @firstName";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("First_Name", firstName);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(new Document()
                    {
                        CaseID = Convert.ToInt32(read["Case_ID"]),
                        FirstName = read["First_Name"].ToString(),
                        LastName = read["Last_Name"].ToString()
                    });
                }
            }

            return results;
        }
        //public static List<Document> GetDocuments(string lastName)
        //{
        //    string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
        //    List<Document> results = new List<Document>();
        //    string sql = "SELECT * FROM DOCUMENT WHERE First_Name = @lastName";

        //    using (SqlConnection conn = new SqlConnection(dsn))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Parameters.AddWithValue("Last_Name", lastName);

        //        SqlDataReader read = cmd.ExecuteReader();

        //        while (read.Read())
        //        {
        //            results.Add(new Document()
        //            {
        //                CaseID = Convert.ToInt32(read["Case_ID"]),
        //                FirstName = read["First_Name"].ToString(),
        //                LastName = read["Last_Name"].ToString()
        //            });
        //        }
        //    }

        //    return results;
        //}

        public static void Add(int caseID, string firstName, string lastName)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "INSERT INTO DOCUMENT (Case_ID, First_Name, Last_Name) VALUES (@CaseID, @FirstName, @LastName)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", caseID);
                cmd.Parameters.AddWithValue("FirstName", firstName);
                cmd.Parameters.AddWithValue("LastName", lastName);

                cmd.ExecuteNonQuery();
            }
        }
        internal void Delete(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM DOCUMENT WHERE Document_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("Document", id);
                cmd.Parameters.AddWithValue("ID", id);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}