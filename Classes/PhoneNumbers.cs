using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace RJLou.Classes
{
    class PhoneNumber
    {
        #region Private Variables
        private string _pType;
        private int _number;
        private string dsn = ConfigurationManager.ConnectionStrings["RJLouEntities"].ToString();
        #endregion

        #region Public Properties
        public PhoneNumber(string type, int number)
        {
            PType = type;
            Number = number; 
        }

        public string PType
        {
            get
            {
                return _pType;
            }
            set
            {
                _pType = value;
            }
        }

        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
            }
        }
        #endregion

        #region Methods
        public static PhoneNumber Get(int phoneNumber)
        {
            PhoneNumber result;
            string sql = "SELECT * FROM PhoneList HWERE Phone_Number = @PhoneNumber";

            using (SqlConnection conn = new SqlConnection(dsn)) ;
        }

        public override string ToString()
        {
            string number_string = Number.ToString();
            string area_code;
            string first_three;
            string last_four;
            area_code=number_string.Substring(0,3);
            first_three=number_string.Substring(3,3);
            last_four=number_string.Substring(6,4);
            return String.Format("({0}){1}-{2}", area_code, first_three, last_four);
        }
        #endregion
    }
}
