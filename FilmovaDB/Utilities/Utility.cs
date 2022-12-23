using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.Utilities
{
    public static class Utility<T> where T : class
    {
        public static List<T> utilityExcept(List<T> list1, List<T> list2)
        {
            if (list2.Count == 0)
                return list1;

            List<T> rList = new List<T>();
            foreach (T item in list1)
            {
                if (!list2.Contains(item))
                    rList.Add(item);
            }
            return rList;
        }
    }
}
