using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal interface ISortable<T>
    {
        List<T> Sort(SortOption attribute, OrderBy order, List<T> medicines);
        List<T> Sort<TKey>(List<T> medicines, Func<T, TKey> sorter, OrderBy order);
       
    }
}
