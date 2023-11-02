using PharmacySalesAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Prototype.Domain
{
    internal class WarehouseInspection
    {
        private Medicine medicineInspected;
        private int amountLeft;
        private DateTime inspectionDate;
        public WarehouseInspection(Medicine medicineInspected, int amountLeft, DateTime inspectionDate)
        {
            this.medicineInspected = medicineInspected;
            this.amountLeft = amountLeft;
            this.inspectionDate = inspectionDate;
        }
        public Medicine getMedicine() {  return medicineInspected; }
        public int getAmountLeft() {  return amountLeft; }
        public DateTime getInspectionDate() {  return inspectionDate; }
        public override bool Equals(object obj)
        {
            return obj is WarehouseInspection inspection &&
                   medicineInspected == inspection.medicineInspected &&
                   amountLeft == inspection.amountLeft &&
                   inspectionDate.Day == inspection.inspectionDate.Day &&
                   inspectionDate.Month == inspection.inspectionDate.Month &&
                   inspectionDate.Year == inspection.inspectionDate.Year;
        }
        public static bool operator ==(WarehouseInspection left, WarehouseInspection right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(WarehouseInspection left, WarehouseInspection right)
        {
            return !left.Equals(right);
        }
        public override string ToString()
        {
            return $"Medicine ID: {medicineInspected.GetID()}, inspection date: {inspectionDate.Day}/{inspectionDate.Month}/{inspectionDate.Year}, amount in the stock: {amountLeft}";
        }

        public override int GetHashCode()
        {
            return medicineInspected.GetHashCode() ^ amountLeft.GetHashCode() ^ inspectionDate.GetHashCode();
        }
    }
}
