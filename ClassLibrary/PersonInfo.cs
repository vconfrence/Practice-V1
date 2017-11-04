using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class PersonInfo
    {
        // class members
        private string fname;
        private string lname;
        private string gendr;
        private string color;
        private DateTime dob;

        // Getter & Setter methods for class members
        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public string Gendr { get => gendr; set => gendr = value; }
        public string Color { get => color; set => color = value; }
        public DateTime Dob { get => dob; set => dob = value; }
    }
}
