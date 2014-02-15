using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RJLou.Classes
{
    public class PhoneList
    {
        internal List<PhoneNumber> numbers;
        public PhoneList()
        { 
        }

        public void AddNumber(PhoneNumber newNumber)
        {
            numbers.Add(newNumber);
        }

        public void RemoveNumber(PhoneNumber oldNumber)
        {
            for(int i=0;i<numbers.Count();i++)
            {
                if(oldNumber.Number==numbers[i].Number)
                {
                    numbers.Remove(numbers[i]);//remember this for later if it breaks
                }
            }
        }


        public override string ToString()
        {
            string numberList = "";
            for(int i=0;i<numbers.Count();i++)
            {
                numberList += i.ToString();
            }
            return numberList;
        }
    }
}