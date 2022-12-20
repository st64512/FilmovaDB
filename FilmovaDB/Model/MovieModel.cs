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
        public List<Actor> Actors { get; set; }
        public List<Director> Directors { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
