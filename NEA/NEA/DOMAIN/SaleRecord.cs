using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class SaleRecord : Record
    {
        public SaleRecord(Medicine medicineInspected, int amount, DateTime recordDate) : base(medicineInspected, amount, recordDate) { }
        public SaleRecord(DateTime recordDate) : base(recordDate) { }
        public override bool Equals(object obj)
        {
            return obj is SaleRecord record &&
                   GetRecordDate().Month == record.GetRecordDate().Month &&
                   GetRecordDate().Year == record.GetRecordDate().Year;
        }
        public static bool operator ==(SaleRecord left, SaleRecord right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(SaleRecord left, SaleRecord right)
        {
            return !left.Equals(right);
        }
    }
}
