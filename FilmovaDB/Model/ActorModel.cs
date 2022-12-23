using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.Model
{
    public class ActorModel {}

    public class Actor : ObservableObject
    {
        private string name;
        private string surname;

        public int Id { get; set; }
        public string Name {
            get => name;
            set {
                if (name != value)
                {
                    SetProperty(ref name, value);
                    OnPropertyChanged("FullName");
                }
            }
        }
        public string Surname
        {
            get => surname;
            set
            {
                if (surname != value)
                {
                    SetProperty(ref surname, value);
                    OnPropertyChanged("FullName");
                }
            }
        }
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
    }
}
