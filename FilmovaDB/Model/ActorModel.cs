using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.Model
{
    public class ActorModel {}

    public class Actor : IEquatable<Actor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }

        public bool Equals(Actor? other)
        {
            if (other is null)
                return false;

            return this.Name == other.Name && this.Surname == other.Surname;
        }

        public override bool Equals(object obj) => Equals(obj as Actor);
        public override int GetHashCode() => (Name, Surname).GetHashCode();

    }
}
