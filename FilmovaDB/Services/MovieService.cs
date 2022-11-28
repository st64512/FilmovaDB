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
    public class MovieService
    {
        private const string _path = @".\movies.db";
        private const string dataCollectionName = "movies";

        public MovieService() 
        {
            BsonMapper.Global.Entity<Movie>().DbRef(x => x.Actors, "actors");
            BsonMapper.Global.Entity<Movie>().DbRef(x => x.Directors, "directors");
        }
        public List<Movie> GetAll()
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Movie>(dataCollectionName);
            var movies = col.Query()
                .Include(x => x.Actors)
                .Include(x => x.Directors)
                .ToList();
            return movies;
        }

        public Movie GetById(int id) 
        {
            using var db = new LiteDatabase(_path);
            var col = db.GetCollection<Movie>(dataCollectionName);
            return col
                .Include(x => x.Actors)
                .Include(x => x.Directors)
                .FindById(id);
        }

        public List<Movie> GetBySearch(string searchQuery)
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Movie>(dataCollectionName);
            var movies = col
                .Include(x => x.Actors)
                .Include(x => x.Directors)
                .Find(Query.Contains("Name", searchQuery)).ToList();
            return movies;
        }

        public void Insert(Movie movie)
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Movie>(dataCollectionName);
            col.Insert(movie);
            col.EnsureIndex(x => x.Name);
        }

        public void Delete(int id) 
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Movie>(dataCollectionName);
            col.Delete(id);
        }

        public void Update(Movie movie)
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Movie>(dataCollectionName);
            col.Update(movie);
            col.EnsureIndex(x => x.Name);
        }
    }
}
