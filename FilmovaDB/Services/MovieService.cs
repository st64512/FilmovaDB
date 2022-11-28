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

        public MovieService()
        {}

        public async Task<List<Movie>> GetAll()
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Movie>(dataCollectionName);
            var movies = col.Query().ToList();
            
            return movies;
        }

        public async Task Insert(Movie movie) 
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Movie>(dataCollectionName);
            col.Insert(movie);
            col.EnsureIndex(x => x.Name);
        }

        public async Task<List<Movie>> GetMovieRepoSearch(string searchQeury)
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Movie>(dataCollectionName);
            var movies = col.Find(Query.Contains("Name", searchQeury)).ToList();
            
            return movies;
        }
    }
}
