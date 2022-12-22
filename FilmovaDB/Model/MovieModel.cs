using CommunityToolkit.Mvvm.ComponentModel;
using FilmovaDB.MovieEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.Model
{
    public class MovieModel {}

    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public List<Actor> Actors { get; set; } = new List<Actor>();
        public List<Director> Directors { get; set; } = new List<Director>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
