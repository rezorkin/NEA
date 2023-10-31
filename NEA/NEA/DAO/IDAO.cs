using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Prototype.DAO
{
    internal interface IDAO<T>
    {   
        List<T> FindAll();
       
        int Insert(T entity);
    }
}
