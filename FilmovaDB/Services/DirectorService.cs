using FilmovaDB.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.Services
{
    public class DirectorService
    {
        private const string _path = @".\movies.db";
        private const string dataCollectionName = "directors";

        public List<Director> GetAll()
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Director>(dataCollectionName);
            var directors = col.Query().ToList();
            return directors;
        }

        public Director GetById(int id)
        {
            using var db = new LiteDatabase(_path);
            var col = db.GetCollection<Director>(dataCollectionName);
            return col.FindById(id);
        }

        public List<Director> GetBySearch(string searchQuery)
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Director>(dataCollectionName);
            var directors = col.Find(Query.Contains("Name", searchQuery)).ToList();
            return directors;
        }

        public void Insert(Director director)
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Director>(dataCollectionName);
            col.Insert(director);
            col.EnsureIndex(x => x.Name);
        }

        public void Delete(int id)
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Director>(dataCollectionName);
            col.Delete(id);
        }

        public void Update(Director director)
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Director>(dataCollectionName);
            col.Update(director);
            col.EnsureIndex(x => x.Name);
        }
    }
}
