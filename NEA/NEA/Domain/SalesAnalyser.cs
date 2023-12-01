using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Domain
{
    internal class SalesAnalyser
    {
        private struct SaleDate
        {
            public int amount;
            public DateTime Date;
        }
        List<PurchaseOrder> purchaseOrderHistory;
        List<StockInspection> inspectionHistory;
        List<SaleDate> salesHistory;

        public SalesAnalyser(List<PurchaseOrder> purchaseOrderHistory, List<StockInspection> inspectionHistory)
        {
            this.purchaseOrderHistory = purchaseOrderHistory;
            this.inspectionHistory = inspectionHistory;
            salesHistory = new List<SaleDate>();
        }
    }
}
