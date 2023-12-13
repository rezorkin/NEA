using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DOMAIN;

namespace NEA.DAO
{
    internal abstract class RecordDAO<T> : DAO<T>, IRecordDAO<T> where T : Record
    {
        protected readonly string tableName;
        protected abstract string setTableName();
        public RecordDAO() : base() 
        {
            tableName = setTableName();
        }
        public T GetByDateID(Medicine selectedMedicine, DateTime Date)
        {
            List<T> foundByDate = FindByAttributeValue(tableName, "Date", ConvertDateToString(Date));
            List<T> foundByID = FindByAttributeValue(tableName, "MedicineID", selectedMedicine.GetID().ToString()); 
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
            return FindByAttributeValue(tableName, "MedicineID", selectedMedicine.GetID().ToString());
        }
        public List<T> GetRecordHistory(Medicine selectedMedicine, OrderBy order)
        {
            return FindByAttributeValue(tableName, "MedicineID", selectedMedicine.GetID().ToString(), "Date", order);
        }

        protected abstract override T MapDBRowToItemFields(NameValueCollection row);     
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

        public List<T> GetRecordHistory(int medicineID)
        {
            return FindByAttributeValue(tableName, "MedicineID", medicineID.ToString());
        }
    }
}
