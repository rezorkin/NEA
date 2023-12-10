using NEA.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class AccountingAuditor
    {
        StockInspectionDAO inspectionDAO;
        PurchaseOrderDAO purchaseOrderDAO;
        public AccountingAuditor()
        {
            inspectionDAO = new StockInspectionDAO();
            purchaseOrderDAO = new PurchaseOrderDAO();
        }
        public List<PurchaseOrder> GetPurchaseOrderHistory(Medicine medicine)
        {
            try
            {
                return purchaseOrderDAO.GetRecordHistory(medicine);
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message, e);
            }
        }
        public List<StockInspection> GetStockInspectionHistory(Medicine medicine)
        {
            try
            {
                return inspectionDAO.GetRecordHistory(medicine);
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message, e);
            }
        }
        public List<SaleDate> GetSalesHistory(Medicine Medicine)
        {
            try
            {
                var purchases = purchaseOrderDAO.GetRecordHistory(Medicine);
                var inspections = inspectionDAO.GetRecordHistory(Medicine);
                return CalculateSalesHistory(purchases, inspections);
            }
            catch(DAOException e) 
            {
                throw new DomainException(e.Message);
            }
        }
        public List<SaleDate> GetSalesHistory(int medicineId)
        {
            try
            {
                var purchases = purchaseOrderDAO.GetRecordHistory(medicineId);
                var inspections = inspectionDAO.GetRecordHistory(medicineId);
                return CalculateSalesHistory(purchases, inspections);
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message);
            }
        }
        private List<SaleDate> CalculateSalesHistory(List<PurchaseOrder> purchaseOrders, List<StockInspection> inspections)
        {
            var result = new List<SaleDate>();
            foreach (StockInspection inspection in inspections)
            {
                var sale = new SaleDate();
                sale.Date = inspection.GetRecordDate();
                bool IsFirstPurchaseOrderInThisMonth = true;
                for (int i = 0; i < purchaseOrders.Count; i++)
                {
                    if (inspection.GetRecordDate() == purchaseOrders[i].GetRecordDate() && IsFirstPurchaseOrderInThisMonth == true)
                    {
                        sale.amount = purchaseOrders[i].getAmount() - inspection.getAmount();
                        IsFirstPurchaseOrderInThisMonth = false;
                    }
                    else if (inspection.GetRecordDate() == purchaseOrders[i].GetRecordDate() && IsFirstPurchaseOrderInThisMonth != true)
                    {
                        sale.amount += purchaseOrders[i].getAmount();
                    }
                }
                result.Add(sale);
            }
            return result;
        }
    }
    
}
