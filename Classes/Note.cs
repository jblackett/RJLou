using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public class Note
    {
        #region Private Variables
        private int _noteID;
        private DateTime _createDate;
        private DateTime _editDate;
        private InternalUser _author;
        private string _noteText;
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

        public DateTime EditDate
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

    }
}