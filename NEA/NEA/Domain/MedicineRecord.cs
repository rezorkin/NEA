using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class MedicineRecord
    {
        private Medicine medicineInspected;
        private int amount;
        private DateTime recordDate;
        public MedicineRecord(Medicine medicineInspected, int amount, DateTime recordDate)
        {
            this.medicineInspected = medicineInspected;
            this.amount = amount;
            this.recordDate = recordDate;
        }
        public Medicine getMedicine() { return medicineInspected; }
        public int getAmount() { return amount; }
        public DateTime GetRecordDate() { return recordDate; }
        public override bool Equals(object obj)
        {
            return obj is MedicineRecord record &&
                   medicineInspected == record.medicineInspected &&
                   recordDate.Day == record.recordDate.Day &&
                   recordDate.Month == record.recordDate.Month &&
                   recordDate.Year == record.recordDate.Year;
        }
        public static bool operator ==(MedicineRecord left, MedicineRecord right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(MedicineRecord left, MedicineRecord right)
        {
            return !left.Equals(right);
        }
        public static bool operator >(MedicineRecord left, MedicineRecord right)
        {
            int i =  left.recordDate.CompareTo(right.recordDate);
            if(i > 0)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(MedicineRecord left, MedicineRecord right)
        {
            int i = left.recordDate.CompareTo(right.recordDate);
            if (i < 0)
            {
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Medicine ID: {medicineInspected.GetID()}, record date: {recordDate.Day}/{recordDate.Month}/{recordDate.Year}, amount: {amount}";
        }

        public override int GetHashCode()
        {
            return medicineInspected.GetHashCode() ^ amount.GetHashCode() ^ recordDate.GetHashCode();
        }
    }
}
