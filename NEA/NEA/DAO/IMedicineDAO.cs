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
        int GetLastID();
        List<Medicine> FindByName(string name);
        List<Medicine> FindByPartName(string name);
        List<Medicine> FindByCompanyName(string companyName);
        List<Medicine> FindByPartCompanyName(string name);
        List<Medicine> FindByActiveSubstance(string activeSubstance);
        Medicine FindByID(int id);
        List<Medicine> FindInIDRange(int startRange, int endRange);
        bool AddNewMedicine(Medicine m);

    }
}
