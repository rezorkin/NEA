using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DAO;

namespace NEA.Domain
{
    internal class Stock
    {
        MedicineDAO medicineDAO;
        StockInspectionDAO inspectionDAO;
        PurchaseOrderDAO purchaseOrderDAO;
        public Stock() 
        {
            medicineDAO = new MedicineDAO();
            inspectionDAO = new StockInspectionDAO();
            purchaseOrderDAO = new PurchaseOrderDAO();
        }
        public List<Medicine> GetCurrentAssortment() 
        {
            try
            {
                List<Medicine> allMedicines = medicineDAO.GetAll();
                return allMedicines;
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message);
            }
        }
        public Medicine FindByID(int id)
        {
            try
            {
                Medicine foundMedicine = medicineDAO.FindByID(id);
                return foundMedicine;
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message, e);
            }
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

    }
}
