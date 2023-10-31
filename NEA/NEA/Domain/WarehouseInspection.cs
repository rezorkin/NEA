using PharmacySalesAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Domain
{
    internal class WarehouseInspection
    {
        private Medicine medicineInspected;
        private int amountLeft;
        private DateTime inspectionDate;
        public WarehouseInspection() { }
        public WarehouseInspection(Medicine medicineInspected, int amountLeft, DateTime inspectionDate)
        {
            this.medicineInspected = medicineInspected;
            this.amountLeft = amountLeft;
            this.inspectionDate = inspectionDate;
        }
        public Medicine getMedicine() {  return medicineInspected; }
        public int getAmountLeft() {  return amountLeft; }
        public DateTime getInspectionDate() {  return inspectionDate; }
    }
}
