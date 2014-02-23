using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public class Document
    {
        #region Private Variables
        private int _documentID;
        private int _caseID;
        private InternalUser _personWhoModified;
        private string _fileLocation;
        #endregion

        #region Public Properties
        public int DocumentID
        {
            get
            {
                return _documentID;
            }
            set
            {
                _documentID = value;
            }
        }
        public int CaseID
        {
            get
            {
                return _caseID;
            }
            set
            {
                _caseID = value;
            }
        }
        public InternalUser PersonWhoModified
        {
            get
            {
                return _personWhoModified;
            }
            set
            {
                _personWhoModified = value;
            }
        }
        public string FileLocation
        {
            get
            {
                return _fileLocation;
            }
            set
            {
                _fileLocation = value;
            }
        }
        #endregion

        #region Constructors
        public Document() { }
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
                        DocumentID = Convert.ToInt32(read["Document_ID"]),
                        CaseID = Convert.ToInt32(read["Case_ID"]),
                        PersonWhoModified = InternalUser.Get(Convert.ToInt32(read["Person_ID"])),
                        FileLocation = read["File_Location"].ToString()
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
                        DocumentID = Convert.ToInt32(read["Document_ID"]),
                        CaseID = Convert.ToInt32(read["Case_ID"]),
                        PersonWhoModified = InternalUser.Get(Convert.ToInt32(read["Person_ID"])),
                        FileLocation = read["File_Location"].ToString()
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
                        DocumentID = Convert.ToInt32(read["Document_ID"]),
                        CaseID = Convert.ToInt32(read["Case_ID"]),
                        PersonWhoModified = InternalUser.Get(Convert.ToInt32(read["Person_ID"])),
                        FileLocation = read["File_Location"].ToString()
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
                        DocumentID = Convert.ToInt32(read["Document_ID"]),
                        CaseID = Convert.ToInt32(read["Case_ID"]),
                        PersonWhoModified = InternalUser.Get(Convert.ToInt32(read["Person_ID"])),
                        FileLocation = read["File_Location"].ToString()
                    });
                }
            }

            return results;
        }

        public static void Add(int caseID, int personID, string fileLocation)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"
                INSERT INTO DOCUMENT 
                            (Case_ID, Person_ID, File_Location) 
                VALUES      (@CaseID, @PersonID, @FileLocation)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CaseID", caseID);
                cmd.Parameters.AddWithValue("PersonID", personID);
                cmd.Parameters.AddWithValue("FileLocation", fileLocation);

                cmd.ExecuteNonQuery();
            }
        }
        internal void Delete()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM DOCUMENT WHERE Document_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", DocumentID);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}