using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class PurchaseOrder : MedicineRecord
    {
        public PurchaseOrder(Medicine purchasedMedicine, int purchasedAmount, DateTime purchasedDate) 
            : base(purchasedMedicine, purchasedAmount, purchasedDate){ }
        public override string ToString()
        {
            return $"Medicine ID: {getMedicine().GetID()}, purchase date: {GetRecordDate()}, amount purchased: {getAmount()}";
        }
    }
}
