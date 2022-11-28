using FilmovaDB.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.Services
{
    public class ActorService
    {
        private const string _path = @".\movies.db";
        private const string dataCollectionName = "actors";

        public List<Actor> GetAll()
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Actor>(dataCollectionName);
            var actors = col.Query().ToList();
            return actors;
        }

        public Actor GetById(int id)
        {
            using var db = new LiteDatabase(_path);
            var col = db.GetCollection<Actor>(dataCollectionName);
            return col.FindById(id);
        }

        public List<Actor> GetBySearch(string searchQuery)
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Actor>(dataCollectionName);
            var actors = col.Find(Query.Contains("Name", searchQuery)).ToList();
            return actors;
        }

        public void Insert(Actor actor)
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Actor>(dataCollectionName);
            col.Insert(actor);
            col.EnsureIndex(x => x.Name);
        }

        public void Delete(int id)
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Actor>(dataCollectionName);
            col.Delete(id);
        }

        public void Update(Actor actor)
        {
            using var db = new LiteDatabase(_path);

            var col = db.GetCollection<Actor>(dataCollectionName);
            col.Update(actor);
            col.EnsureIndex(x => x.Name);
        }
    }
}
