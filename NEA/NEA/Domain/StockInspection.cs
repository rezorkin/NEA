using PharmacySalesAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NEA.Domain
{
    internal class StockInspection : MedicineRecord
    {
        public StockInspection(Medicine medicineInspected, int amountLeft, DateTime inspectionDate) : base(medicineInspected, amountLeft, inspectionDate)
        {
        }
        public override string ToString()
        {
            return $"Medicine ID: {getMedicine().GetID()}, last inspection date: {getRecordDate()}, amount in the stock: {getAmount()}";
        }
    }
}
