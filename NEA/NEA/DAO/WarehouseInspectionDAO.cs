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
        public WarehouseInspection GetByDateID(Medicine selectedMedicine, DateTime Date)
        {
            List<WarehouseInspection> FoundOnlyByDate = FindAll("WarehouseInspectionHistory", "Date", ConvertDateToString(Date));
            List<WarehouseInspection> FoundOnlyByMedicineID = FindAll("WarehouseInspectionHistory", "MedicineID", selectedMedicine.GetID().ToString());
            foreach(WarehouseInspection inspection in FoundOnlyByDate)
            {
                for(int i = 0; i < FoundOnlyByMedicineID.Count; i++)
                {
                    if (inspection == FoundOnlyByMedicineID[i])
                    {
                        return inspection;
                    }
                }
            }
            throw new DAOException("Particular Inspection by Date and ID was not found");
        }

        public List<WarehouseInspection> GetCountHistory(Medicine selectedMedicine)
        {
            return FindAll("WarehouseInspectionHistory", "MedicineID", selectedMedicine.GetID().ToString());
        }

        protected override WarehouseInspection SetValuesFromTableToObjectFields(NameValueCollection row)
        {
            DateTime dateOfInspection = ConvertStringToDate(row["Date"]);
            int amountOfMedicine = int.Parse(row["Amount"]);
            int inspectedMedicineID = int.Parse(row["MedicineID"]);
            Medicine inspectedMedicine = new MedicineDAO().FindById(inspectedMedicineID);
          
            return new WarehouseInspection(inspectedMedicine, amountOfMedicine, dateOfInspection);
        }
        private DateTime ConvertStringToDate(string date) 
        {
            string[] dateParts = date.Split('/');
            int day = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int year = int.Parse(dateParts[2]);
            return new DateTime(year, month, day);
        }
        private string ConvertDateToString(DateTime Date)
        {
            string[] dateParts = new string[3];
            dateParts[0] = Date.Day.ToString();
            dateParts[1] = Date.Month.ToString();
            dateParts[2] = Date.Year.ToString();
            return dateParts[0] + "/" + dateParts[1] + "/" + dateParts[2];
        }
    }
}
