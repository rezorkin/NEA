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
            return FindAllByAttribute("AssortmentOfTheMedicalSupplies" , "ActiveSubstance", activeSubstance);
        }
        public List<Medicine> FindAllByCompanyName(string companyName)
        {
            return FindAllByAttribute("AssortmentOfTheMedicalSupplies", "CompanyName", companyName);
        }

        public List<Medicine> FindAllByName(string name)
        {
            return FindAllByAttribute("AssortmentOfTheMedicalSupplies", "ProductName", name);
        }

        public Medicine FindById(int id)
        {
            List<Medicine> result = FindAllByAttribute("AssortmentOfTheMedicalSupplies", "ProductID", id.ToString());
            if (result.Count > 1)
            {
                throw new DAOException("Was found more than one medicine with following ID, howewer the value must be unique");
            }
            else
            {
                return result[0];
            } 
        }

        public override List<Medicine> FindAll() 
        {
          List<Medicine> result = new List<Medicine>();
          foreach(NameValueCollection row in GetMatchedRows("AssortmentOfTheMedicalSupplies"))
          {
                result.Add(SetRetrievedValuesFromDBRow(row));
          }
          return result;
        }
        public override int Insert(Medicine entity)
        {
            throw new NotImplementedException();
        }

        protected override Medicine SetRetrievedValuesFromDBRow(NameValueCollection row)
        {
            int medicineID = int.Parse(row["ProductID"]);
            string medicineName = row["ProductName"];
            string medicineCompanyName = row["CompanyName"];
            string medicineActiveSubstance = row["ActiveSubstance"];
            return new Medicine(medicineID, medicineName, medicineCompanyName, medicineActiveSubstance);
        }
    }
}
