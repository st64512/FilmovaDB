using FilmovaDB.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.Repository
{
    public class MovieService
    {
        private const string _path = @".\movies.db";
        private const string dataCollectionName = "movies";

        public List<Movie> GetAll()
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Movie>(dataCollectionName);
            var movies = col.Query().ToList();
            return movies;
        }

        public Movie GetById(int id) 
        {
            using var db = new LiteDatabase(_path);
            var col = db.GetCollection<Movie>(dataCollectionName);
            return col.FindById(id);
        }

        public List<Movie> GetBySearch(string searchQuery)
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Movie>(dataCollectionName);
            var movies = col.Find(Query.Contains("Name", searchQuery)).ToList();
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
