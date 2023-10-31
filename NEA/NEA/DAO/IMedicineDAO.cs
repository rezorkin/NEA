using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Prototype.Domain;

namespace Prototype.DAO
{
    internal interface IMedicineDAO : IDAO<Medicine>
    {
        List<Medicine> FindAllByName(string name);
        List<Medicine> FindAllByCompanyName(string companyName);
        List<Medicine> FindAllByActiveSubstance(string activeSubstance);
        Medicine FindById(int id);

    }
}
