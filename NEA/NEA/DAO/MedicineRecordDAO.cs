using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Domain;

namespace NEA.DAO
{
    internal abstract class MedicineRecordDAO<T> : DAO<T> where T : MedicineRecord
    {
        protected readonly string tableNameDB;
        protected abstract string setTableName();
        public MedicineRecordDAO() : base() 
        {
            tableNameDB = setTableName();
        }
        public T GetByDateID(Medicine selectedMedicine, DateTime Date)
        {
            List<T> foundByDate = FindByAttributeValue(tableNameDB, "Date", ConvertDateToString(Date));
            List<T> foundByID = FindByAttributeValue(tableNameDB, "MedicineID", selectedMedicine.GetID().ToString()); 
            foreach (T record in foundByDate)
            {
                for (int i = 0; i < foundByID.Count; i++)
                {
                    if (record == foundByID[i])
                    {
                        return record;
                    }
                }
            }
            throw new DAOException("Particular record by Date and ID was not found");
        }

        public List<T> GetRecordHistory(Medicine selectedMedicine)
        {
            return FindByAttributeValue(tableNameDB, "MedicineID", selectedMedicine.GetID().ToString());
        }

        protected abstract override T SetValuesFromTableToObjectFields(NameValueCollection row);     
        protected DateTime ConvertStringToDate(string date)
        {
            string[] dateParts = date.Split('/');
            int day = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int year = int.Parse(dateParts[2]);
            return new DateTime(year, month, day);
        }
        protected string ConvertDateToString(DateTime Date)
        {
            string[] dateParts = new string[3];
            dateParts[0] = Date.Day.ToString();
            dateParts[1] = Date.Month.ToString();
            dateParts[2] = Date.Year.ToString();
            return dateParts[0] + "/" + dateParts[1] + "/" + dateParts[2];
        }
    }
}
