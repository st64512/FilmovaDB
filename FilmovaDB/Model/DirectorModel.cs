using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.Model
{
    public class DirectorModel {}
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }
    }
}
