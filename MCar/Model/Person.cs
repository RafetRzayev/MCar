using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCar.Model
{
    public class Person
    {
        public string Seriya { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }
        public string MobilNumber { get; set; }
        //public string HomeNumber { get; set; }
        //public string Address { get; set; }

        public override string ToString()
        {
            return FullName;
        }
    }
}
