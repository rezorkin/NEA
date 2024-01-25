using NEA.DAO;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class PurchaseOrder : Record
    {
        private int orderNumber;
        public PurchaseOrder(int orderNumber, Medicine purchasedMedicine, int purchasedAmount, DateTime purchasedDate) 
            : base(purchasedMedicine, purchasedAmount, purchasedDate)
        {
            this.orderNumber = orderNumber;
        }
        public override bool Equals(object obj)
        {
            return obj is PurchaseOrder order &&
                   orderNumber == order.GetOrderNumber();
        }
                   
        public static bool operator ==(PurchaseOrder left, PurchaseOrder right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(PurchaseOrder left, PurchaseOrder right)
        {
            return !left.Equals(right);
        }
        public int GetOrderNumber()
        {
            return orderNumber;
        }
        public override string ToString()
        {
            return $"Medicine ID: {GetMedicine().GetID()}, purchase date: {GetRecordDate()}, amount purchased: {GetAmount()}";
        }
    }
}
