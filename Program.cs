using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Cpract
{
    class Program
    {
        static void Main(string[] args)
        {
            //Array intarry = Array.CreateInstance(typeof(int),5);

            //for(int i=0;i<5;i++)
            //{
            //    intarry.SetValue(33+i, i);
            //}

            //for(int i=0;i<5;i++)
            //{
            //    Console.WriteLine(intarry.GetValue(i));
            //}
            Person[] p = { new Person { FN="bibekanand",LN="Panigrahi"},

                 new Person { FN="Miracle",LN="Apply"},
                 new Person { FN="Other",LN="List"}
            };
            Array.Sort(p,new PersonCompare(PersonCompareType.lastName));

            foreach(var pers in p)
            {
                Console.WriteLine(pers.FN+"     "+pers.LN);
            }

            Console.Read();
        }
    }
   
    public class Person : IComparable<Person>
    {
        public string FN { set; get; } = "";
        public string LN { set; get; } = "";
        public int CompareTo(Person other)
        {
            if (other == null) { return 1; }
            int result = string.Compare(this.LN, other.LN);
            if(result==0)
            {
                result = string.Compare(this.FN, other.FN);
            }
            return result;
        }
    }
    public enum PersonCompareType
    {
       firstName,lastName
    }
    public class PersonCompare : IComparer<Person>
    {
        private PersonCompareType _compareType;
        public int Compare(Person x, Person y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1; 
            switch(_compareType)
            {
                case PersonCompareType.firstName:
                    return string.Compare(x.FN, y.FN);
                case PersonCompareType.lastName:
                    return string.Compare(x.LN, y.LN);
                default:
                    throw new ArgumentException("Unexpected Compar Type");
                       
                   
            }
        }
        public PersonCompare(PersonCompareType compareType)
        {
            _compareType = compareType;

        }
    }
}
