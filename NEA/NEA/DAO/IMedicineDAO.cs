using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using NEA.Domain;

namespace NEA.DAO
{
    internal interface IMedicineDAO
    {
        List<Medicine> GetAll();
        List<Medicine> FindAllByName(string name);
        List<Medicine> FindAllByCompanyName(string companyName);
        List<Medicine> FindAllByActiveSubstance(string activeSubstance);
        Medicine FindByID(int id);

    }
}
