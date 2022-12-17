using FilmovaDB.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.Repository
{
    public class ActorRepository : BaseRepository<Actor>
    {
        public ActorRepository(string dataCollectionName = "actors") : base(dataCollectionName) {}

        public override void EnsureIndex(ILiteCollection<Actor> col)
        {
            col.EnsureIndex(x => x.Name);
        }
    }
}
