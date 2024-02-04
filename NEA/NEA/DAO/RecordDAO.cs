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
        public List<T> GetRecordHistory(Medicine selectedMedicine)
        {
            return FindByAttributeValue("MedicineID", selectedMedicine.GetID().ToString());
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
            return FindByAttributeValue("MedicineID", medicineID.ToString());
        }

        public abstract bool AddNewRecord(int MedicineID, int amount, DateTime date);
    }
}
