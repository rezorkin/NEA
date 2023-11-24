using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Domain
{
    internal class MedicineRecord
    {
        private Medicine medicineInspected;
        private int amount;
        private DateTime inspectionDate;
        public MedicineRecord(Medicine medicineInspected, int amount, DateTime recordDate)
        {
            this.medicineInspected = medicineInspected;
            this.amount = amount;
            this.inspectionDate = recordDate;
        }
        public Medicine getMedicine() { return medicineInspected; }
        public int getAmount() { return amount; }
        public DateTime getRecordDate() { return inspectionDate; }
        public override bool Equals(object obj)
        {
            return obj is MedicineRecord record &&
                   medicineInspected == record.medicineInspected &&
                   amount == record.amount &&
                   inspectionDate.Day == record.inspectionDate.Day &&
                   inspectionDate.Month == record.inspectionDate.Month &&
                   inspectionDate.Year == record.inspectionDate.Year;
        }
        public static bool operator ==(MedicineRecord left, MedicineRecord right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(MedicineRecord left, MedicineRecord right)
        {
            return !left.Equals(right);
        }
        public override string ToString()
        {
            return $"Medicine ID: {medicineInspected.GetID()}, record date: {inspectionDate.Day}/{inspectionDate.Month}/{inspectionDate.Year}, amount: {amount}";
        }

        public override int GetHashCode()
        {
            return medicineInspected.GetHashCode() ^ amount.GetHashCode() ^ inspectionDate.GetHashCode();
        }
    }
}
