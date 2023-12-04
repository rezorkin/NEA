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
                foundMedicine.SetNewInspectionHistory(inspectionDAO.GetRecordHistory(foundMedicine));
                foundMedicine.SetNewPurchaseOrders(purchaseOrderDAO.GetRecordHistory(foundMedicine));
                return foundMedicine;
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message, e);
            }
        }

    }
}
