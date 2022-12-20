using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.MovieEnums
{
    public enum Genre
    {
        [Description("Komedie")]
        Comedy,
        [Description("Drama")]
        Drama,
        [Description("Akční")]
        Action,
        [Description("Dobrodružný")]
        Adventure,
        [Description("Horor")]
        Horror,
        [Description("Sci-fi")]
        Scifi,
        [Description("Western")]
        Western,
        [Description("Thriller")]
        Thriller, 
        [Description("Romantika")]
        Romance,
        [Description("Mysteriózní")]
        Mystery
    }
}
