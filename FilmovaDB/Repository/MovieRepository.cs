using FilmovaDB.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FilmovaDB.Repository
{
    public class MovieRepository : BaseRepository<Movie>
    {

        public MovieRepository(string dataCollectionName = "movies") : base(dataCollectionName)
        {
            BsonMapper.Global.Entity<Movie>().DbRef(x => x.Actors, "actors");
            BsonMapper.Global.Entity<Movie>().DbRef(x => x.Directors, "directors");
        }
        new public List<Movie> GetAll()
        {
            using var db = new LiteDatabase(path);

            var col = db.GetCollection<Movie>(dataCollectionName);
            var movies = col.Query()
                .Include(x => x.Actors)
                .Include(x => x.Directors)
                .ToList();
            return movies;
        }

        new public Movie GetById(int id) 
        {
            using var db = new LiteDatabase(path);
            var col = db.GetCollection<Movie>(dataCollectionName);
            return col
                .Include(x => x.Actors)
                .Include(x => x.Directors)
                .FindById(id);
        }

        new public List<Movie> GetBySearch(string searchQuery)
        {
            using var db = new LiteDatabase(path);

            var col = db.GetCollection<Movie>(dataCollectionName);
            var movies = col
                .Include(x => x.Actors)
                .Include(x => x.Directors)
                .Find(Query.Contains("Name", searchQuery)).ToList();
            return movies;
        }

        public override void EnsureIndex(ILiteCollection<Movie> col)
        {
            col.EnsureIndex(x => x.Name);
        }
    }
}
