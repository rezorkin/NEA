using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using NEA.DOMAIN;

namespace NEA.DAO
{
    internal interface IMedicineDAO
    {
        List<Medicine> GetAll();
        List<Medicine> GetSortedByID(OrderBy order);
        List<Medicine> GetSortedByName(OrderBy order);
        List<Medicine> GetSortedByCompanyName(OrderBy order);
        List<Medicine> GetSortedByActiveSubstance(OrderBy order);
        List<Medicine> FindAllByName(string name, bool IsCompleteName);
        List<Medicine> FindAllByCompanyName(string companyName, bool IsCompleteName);
        List<Medicine> FindAllByActiveSubstance(string activeSubstance);
        Medicine FindByID(int id);
        List<Medicine> FindInIDRange(int startRange, int endRange);
       bool AddNewMedicine(Medicine m);

    }
}
