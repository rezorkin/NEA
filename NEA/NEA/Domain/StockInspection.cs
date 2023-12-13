using PharmacySalesAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NEA.DOMAIN
{
    internal class StockInspection : Record
    {
        public StockInspection(Medicine medicineInspected, int amountLeft, DateTime inspectionDate) : base(medicineInspected, amountLeft, inspectionDate)
        {
        }
        public override string ToString()
        {
            return $"Medicine ID: {GetMedicine().GetID()}, last inspection date: {GetRecordDate()}, amount in the stock: {GetAmount()}";
        }
    }
}
