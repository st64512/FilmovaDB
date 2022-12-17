using FilmovaDB.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.Repository
{
    public class DirectorRepository : BaseRepository<Director>
    {
        public DirectorRepository(string dataCollectionName = "directors") : base(dataCollectionName) {}

        public override void EnsureIndex(ILiteCollection<Director> col)
        {
            col.EnsureIndex(x => x.Name);
        }
    }
}
