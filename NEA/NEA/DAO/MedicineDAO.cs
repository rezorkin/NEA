using Prototype.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Prototype.DAO
{
    internal class MedicineDAO : DAO<Medicine>, IMedicineDAO
    {
        public MedicineDAO() : base() {}

       
        public List<Medicine> FindAllByActiveSubstance(string activeSubstance)
        {
            return FindAll("AssortmentOfTheMedicalSupplies" , "ActiveSubstance", activeSubstance);
        }
        public List<Medicine> FindAllByCompanyName(string companyName)
        {
            return FindAll("AssortmentOfTheMedicalSupplies", "CompanyName", companyName);
        }

        public List<Medicine> FindAllByName(string name)
        {
            return FindAll("AssortmentOfTheMedicalSupplies", "ProductName", name);
        }

        public Medicine FindById(int id)
        {
            List<Medicine> result = FindAll("AssortmentOfTheMedicalSupplies", "ProductID", id.ToString());
            if (result.Count > 1)
            {
                throw new DAOException("Was found more than one medicine with following ID, howewer the value must be unique");
            }
            else
            {
                return result[0];
            } 
        }

        public List<Medicine> GetAll()
        {
            return FindAll("AssortmentOfTheMedicalSupplies");
        }

        protected override Medicine SetValuesFromTableToObjectFields(NameValueCollection row)
        {
            int medicineID = int.Parse(row["ProductID"]);
            string medicineName = row["ProductName"];
            string medicineCompanyName = row["CompanyName"];
            string medicineActiveSubstance = row["ActiveSubstance"];
            return new Medicine(medicineID, medicineName, medicineCompanyName, medicineActiveSubstance);
        }
    }
}
