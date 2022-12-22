using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.Model
{
    public class DirectorModel {}
    public class Director : IEquatable<Director>
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

        public bool Equals(Director? other)
        {
            if (other is null)
                return false;

            return this.Name == other.Name && this.Surname == other.Surname;
        }

        public override bool Equals(object obj) => Equals(obj as Director);
        public override int GetHashCode() => (Name, Surname).GetHashCode();
    }
}
