using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public abstract class Person
    {
        #region Private Variables
        private int _pID;
        private string _fname;
        private string _lname;
        private DateTime _dob;
        private string _gender;
        private string _race;
        private List<PhoneNumber> _phoneNums;
        private List<Address> _addresses;
        private int _ssn;
        #endregion

        #region Public Properties
        public int PersonID
        {
            get
            {
                return _pID;
            }
            set
            {
                _pID = value;
            }
        }
        public string FirstName
        {
            get
            {
                return _fname;
            }
            set
            {
                _fname = value;
            }
        }
        public string LastName
        {
            get
            {
                return _lname;
            }
            set
            {
                _lname = value;
            }
        }
        public DateTime DateOfBirth
        {
            get
            {
                return _dob;
            }
            set
            {
                _dob = value;
            }
        }
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }
        public string Race
        {
            get
            {
                return _race;
            }
            set
            {
                _race = value;
            }
        }
        public List<PhoneNumber> PhoneNumbers
        {
            get
            {
                return _phoneNums;
            }
            set
            {
                _phoneNums = value;
            }
        }
        public List<Address> Addresses
        {
            get
            {
                return _addresses;
            }
            set
            {
                _addresses = value;
            }
        }
        public int SocialSecurityNumber
        {
            get
            {
                return _ssn;
            }
            set
            {
                _ssn = value;
            }
        }
        #endregion

        #region Constructors
        public Person() { }
        #endregion

        #region Methods
        public abstract Person Get(int personID);

        public abstract List<Person> GetPersons();

        public abstract void Add(string fName, string lName, DateTime dob, string gender,
            string race, List<PhoneNumber> phoneNums, List<Address> addresses, int ssn);

        internal abstract void Delete();
        #endregion
    }
}