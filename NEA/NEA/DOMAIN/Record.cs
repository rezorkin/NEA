using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class Record
    {
        private Medicine medicineInspected;
        private int amount;
        private DateTime recordDate;
        public Record(Medicine medicineInspected, int amount, DateTime recordDate)
        {
            this.medicineInspected = medicineInspected;
            this.amount = amount;
            this.recordDate = recordDate;
        }
        public Record(DateTime recordDate)
        {
            medicineInspected = null;
            amount = -1;
            this.recordDate = recordDate;
        }
        public Medicine GetMedicine() 
        {
           if(medicineInspected != null)
           {
                return medicineInspected;
           }
           throw new DomainException("It is a basic record to compare dates, not storing any values except date");
        }
        public int GetAmount() 
        {
            if(amount == -1)
            {
                throw new DomainException("It is a basic record to compare dates, not storing any values except date");
            }
            return amount; 
        }
        public DateTime GetRecordDate() 
        {
            return recordDate; 
        }
        public override bool Equals(object obj)
        {
            return obj is Record record &&
                   medicineInspected == record.medicineInspected &&
                   recordDate.Month == record.recordDate.Month &&
                   recordDate.Year == record.recordDate.Year;
        }
        public static bool operator ==(Record left, Record right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Record left, Record right)
        {
            return !left.Equals(right);
        }
        public static bool operator >(Record left, Record right)
        {
            List<Record> parameters = new List<Record>() { left, right};
            DateTime[] dateTimes = new DateTime[parameters.Count];
            for(int g = 0; g < 2; g++)
            {
                int year = parameters[g].GetRecordDate().Year;
                int month = parameters[g].GetRecordDate().Month;
                dateTimes[g] = new DateTime(year, month, 1);
            }
            int i = dateTimes[0].CompareTo(dateTimes[1]);
            if(i > 0)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Record left, Record right)
        {
            List<Record> parameters = new List<Record>() { left, right };
            DateTime[] dateTimes = new DateTime[parameters.Count];
            for (int g = 0; g < 2; g++)
            {
                int year = parameters[g].GetRecordDate().Year;
                int month = parameters[g].GetRecordDate().Month;
                dateTimes[g] = new DateTime(year, month, 1);
            }
            int i = dateTimes[0].CompareTo(dateTimes[1]);
            if (i < 0)
            {
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Record date: {recordDate.Month}/{recordDate.Year}, amount: {amount}";
        }

        public override int GetHashCode()
        {
            return medicineInspected.GetHashCode() ^ amount.GetHashCode() ^ recordDate.GetHashCode();
        }
    }
}
