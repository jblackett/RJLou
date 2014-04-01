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
                        CreateDate = read["CreateDate"] as DateTime? ?? default(DateTime),
                        EditDate = read["EditDate"] as DateTime? ?? default(DateTime),
                        NoteText = read["Note_Text"] as string
                    };

                    var author = read["Author"] as int? ?? default(int);

                    result.Author = InternalUser.Get(author);

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
                    Note result = new Note()
                    {
                        NoteID = Convert.ToInt32(read["Note_ID"]),
                        Author = InternalUser.Get(Convert.ToInt32(read["Author"])),
                        NoteText = read["NoteText"].ToString()
                    };

                    var readCreateDate = read["CreateDate"];
                    if (readCreateDate is DBNull)
                        result.CreateDate = default(DateTime);
                    else
                        result.CreateDate = Convert.ToDateTime(read["CreateDate"]);

                    var readEditDate = read["EditDate"];
                    if (readEditDate is DBNull)
                        result.EditDate = default(DateTime);
                    else
                        result.EditDate = Convert.ToDateTime(read["EditDate"]);

                    results.Add(result);
                }
            }
            
            return results;
        }

        public static int Add(DateTime createDate, InternalUser author, string noteText)
        {
            int result = -1;
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "INSERT INTO NOTE (CreateDate, Author, Note_Text) OUTPUT INSERTED.Note_ID VALUES (@CreateDate, @Author, @NoteText)";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("CreateDate", createDate);
                cmd.Parameters.AddWithValue("Author", author.PersonID);
                cmd.Parameters.AddWithValue("NoteText", noteText);

                result = (int)cmd.ExecuteScalar();
            }

            return result;
        }

        internal void Delete(int id)
        {
            string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
            string sql = "DELETE FROM NOTE WHERE Note_ID = @ID";

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", id);

                cmd.ExecuteNonQuery();
            }
        }

        internal void Update()
        {
            string sql = @"
                UPDATE  Note
                SET     Note_Text = @NoteText,
                        Author = @Author,
                        EditDate = @EditDate
                WHERE   Note_ID = @NoteID";

            using (SqlConnection conn = new SqlConnection(Constants.DSN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("NoteID", NoteID);
                cmd.Parameters.AddWithValue("NoteText", NoteText);
                cmd.Parameters.AddWithValue("Author", Author.PersonID);
                cmd.Parameters.AddWithValue("EditDate", EditDate);

                cmd.ExecuteNonQuery();
            }
        }

        public override string ToString()
        {
            string author_string = Author.ToString();
            string createDate_string = CreateDate.ToString();
            string editDate_string;
            string text_string = NoteText;
            if (EditDate != null)
            {
                editDate_string = EditDate.ToString();

                return String.Format("Author: {0}" + System.Environment.NewLine +
                    "Date Created: {1}" + System.Environment.NewLine +
                    "Last Edited: {2}" + System.Environment.NewLine +
                    "{3}", author_string, createDate_string, editDate_string, text_string);
            }
            else
            {
                editDate_string = null;

                return String.Format("Author: {0}" + System.Environment.NewLine +
                    "Date Created: {1}" + System.Environment.NewLine +
                    "{2}", author_string, createDate_string, text_string);
            }

            
        }
        
        #endregion
    }
}