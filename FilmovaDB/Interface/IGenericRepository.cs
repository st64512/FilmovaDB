using FilmovaDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        // List<T> GetBySearch(string query);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
