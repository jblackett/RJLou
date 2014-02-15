using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RJLou.Classes
{
    class PhoneNumbers
    {
        private string _pType;
        private int _number;

        public PhoneNumbers(string type, int number)
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
    }
}
