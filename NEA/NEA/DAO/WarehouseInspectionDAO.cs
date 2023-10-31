using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.Domain;

namespace Prototype.DAO
{
    internal class WarehouseInspectionDAO : DAO<WarehouseInspection>, IWarehouseInspectionDAO
    {
        public override List<WarehouseInspection> FindAll()
        {
            return FindAllByAttribute("WarehouseInspectionHistory", "MedicineID", selectedMedicine.GetID().ToString());
        }

        public List<WarehouseInspection> GetCountHistory(Medicine selectedMedicine)
        {
            return FindAllByAttribute("WarehouseInspectionHistory", "MedicineID", selectedMedicine.GetID().ToString());
        }

        public override int Insert(WarehouseInspection entity)
        {
            throw new NotImplementedException();
        }

        protected override WarehouseInspection SetRetrievedValuesFromDBRow(NameValueCollection row)
        {
            DateTime dateOfInspection = ConvertStringDate(row["Date"]);
            int amountOfMedicine = int.Parse(row["Amount"]);
            int inspectedMedicineID = int.Parse(row["MedicineID"]);
            Medicine inspectedMedicine = new MedicineDAO().FindById(inspectedMedicineID);
          
            return new WarehouseInspection(inspectedMedicine, amountOfMedicine, dateOfInspection);
        }
        private DateTime ConvertStringDate(string date) 
        {
            string[] dateParts = date.Split('/');
            int day = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int year = int.Parse(dateParts[2]);
            return new DateTime(year, month, day);
        }
    }
}
