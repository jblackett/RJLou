using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RJLou.Classes
{
    public class Note
    {
        #region Private Variables
        private int _noteID;
        private DateTime _createDate;
        private DateTime? _editDate;
        private InternalUser _author;
        private string _noteText;
        //private string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
        #endregion

        #region Public Properties
        public int NoteID
        {
            get
            {
                return _noteID;
            }
            set
            {
                _noteID = value;
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
            }
        }

        public DateTime? EditDate
        {
            get
            {
                return _editDate;
            }
            set
            {
                _editDate = value;
            }
        } 

        public InternalUser Author
        {
            get
            {
                return _author;
            }
            set
            {
                _author = value;
            }
        }

        public string NoteText
        {
            get
            {
                return _noteText;
            }
            set
            {
                _noteText = value;
            }
        }
        #endregion

        #region Constructors
        public Note() { }

        public Note(int noteID, DateTime createDate, InternalUser author, string noteText)
        {
            NoteID = noteID;
            CreateDate = createDate;
            Author = author;
            NoteText = noteText;
            EditDate = null;
        }

        public Note(int noteID, DateTime createDate, InternalUser author, string noteText, DateTime editDate)
        {
            NoteID = noteID;
            CreateDate = createDate;
            Author = author;
            NoteText = noteText;
            EditDate = editDate;
        }
        #endregion

        #region Methods
        public static Note Get(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = @"SELECT * FROM NOTE WHERE Note_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", id);

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    Note result = new Note()
                    {
                        NoteID = Convert.ToInt32(read["Note_ID"]),
                        CreateDate = Convert.ToDateTime(read["CreateDate"]),
                        EditDate = Convert.ToDateTime(read["EditDate"]),
                        Author = (InternalUser) read["Author"],
                        NoteText = read["NoteText"].ToString()
                    };

                    return result;
                }
            }

            return null;
        }

        public static List<Note> GetNotes()
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            List<Note> results = new List<Note>();
            string sql = "SELECT * FROM NOTE";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    results.Add(new Note()
                        {
                            NoteID = Convert.ToInt32(read["Note_ID"]),
                            CreateDate = Convert.ToDateTime(read["CreateDate"]),
                            EditDate = Convert.ToDateTime(read["EditDate"]),
                            Author = (InternalUser)read["Author"],
                            NoteText = read["NoteText"].ToString()
                        });
                }
            }
            
            return results;
        }
        
        #endregion
    }
}