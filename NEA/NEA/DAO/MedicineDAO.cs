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

        private List<Medicine> FindAllByAttribute(string attributeName, string value)
        {
            List<Medicine> result = new List<Medicine>();
            List<NameValueCollection> undecodedResultSet = GetMatchedRows("AssortmentOfTheMedicalSupplies", attributeName, value);
            foreach (NameValueCollection row in undecodedResultSet)
            {
                result.Add(SetRetrievedValuesFromDBRow(row));
            }
            return result;
        }
        public List<Medicine> FindAllByActiveSubstance(string activeSubstance)
        {
            return FindAllByAttribute("ActiveSubstance", activeSubstance);
        }
        public List<Medicine> FindAllByCompanyName(string companyName)
        {
            return FindAllByAttribute("CompanyName", companyName);
        }

        public List<Medicine> FindAllByName(string name)
        {
            return FindAllByAttribute("ProductName", name);
        }

        public Medicine FindById(int id)
        {
            List<Medicine> result = FindAllByAttribute("ProductID", id.ToString());
            if (result.Count > 1)
            {
                throw new DAOException("Was found more than one medicine with following ID, howewer the value must be unique");
            }
            else
            {
                return result[0];
            } 
        }

        public override List<Medicine> GetAll() 
        {
          List<Medicine> result = new List<Medicine>();
          foreach(NameValueCollection row in GetMatchedRows("AssortmentOfTheMedicalSupplies")
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
