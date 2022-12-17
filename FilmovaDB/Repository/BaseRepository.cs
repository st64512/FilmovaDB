using FilmovaDB.Interface;
using FilmovaDB.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.Repository
{
    public abstract class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        protected const string path = @".\movies.db";
        protected string dataCollectionName;

        public BaseRepository(string dataCollectionName)
        {
            this.dataCollectionName = dataCollectionName;
        }

        public void Delete(int id)
        {
            using var db = new LiteDatabase(path);

            var col = db.GetCollection<T>(dataCollectionName);
            col.Delete(id);
        }

        public List<T> GetAll()
        {
            using var db = new LiteDatabase(path);

            var col = db.GetCollection<T>(dataCollectionName);
            var list = col.Query().ToList();
            return list;
        }

        public T GetById(int id)
        {
            using var db = new LiteDatabase(path);

            var col = db.GetCollection<T>(dataCollectionName);
            return col.FindById(id);
        }

        public void Insert(T entity)
        {
            using var db = new LiteDatabase(path);

            var col = db.GetCollection<T>(dataCollectionName);
            col.Insert(entity);
            this.EnsureIndex(col);
        }

        public void Update(T entity)
        {
            using var db = new LiteDatabase(path);

            var col = db.GetCollection<T>(dataCollectionName);
            col.Update(entity);
            this.EnsureIndex(col);
        }

        public List<T> GetBySearch(string query)
        {
            using var db = new LiteDatabase(path);

            var col = db.GetCollection<T>(dataCollectionName);
            var list = col.Find(Query.Contains("Name", query)).ToList();
            return list;
        }

        public abstract void EnsureIndex(ILiteCollection<T> col);
    }
}
