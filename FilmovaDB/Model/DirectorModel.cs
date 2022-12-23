using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FilmovaDB.Model
{
    public class DirectorModel {}
    public class Director : ObservableObject
    {
        private string name;
        private string surname;
        public int Id { get; set; }
        public string Name
        {
            get => name;
            set
            {
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

        public bool Equals(Director? other)
        {
            if (other is null)
                return false;

            return this.Name == other.Name && this.Surname == other.Surname;
        }

        public override bool Equals(object obj) => Equals(obj as Director);
    }
}
