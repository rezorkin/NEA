using NEA.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class RecordWriter
    {
        private MedicineDAO medicineDAO;
        private PurchaseOrderDAO purchaseOrderDAO;
        private StockInspectionDAO stockInspectionDAO;
        public RecordWriter()
        {
            medicineDAO = new MedicineDAO();
            purchaseOrderDAO = new PurchaseOrderDAO();
            stockInspectionDAO = new StockInspectionDAO();
        }
        public void AddNewStockInspection(int ID, int amount, DateTime date)
        {
            bool IsSucessful = stockInspectionDAO.AddNewRecord(ID, amount, date);
            if (IsSucessful == false) 
            {
                throw new DomainException("Inspcetion was not recorded in the database");
            }
        }
        public void AddNewPurchaseOrder(int ID, int amount, DateTime date)
        {
            bool IsSucessful = purchaseOrderDAO.AddNewRecord(ID, amount, date);
            if (IsSucessful == false)
            {
                throw new DomainException("Order was not recorded in the database");
            }
        }
        public void AddNewMedicine(string name, string companyName, string activeSubstance)
        {
            try
            {
                int ID = medicineDAO.GetLastID() + 1;
                Medicine medicine = new Medicine(ID, name, companyName, activeSubstance.ToUpper());
                bool IsSucessful = medicineDAO.AddNewMedicine(medicine);
                if (IsSucessful == false)
                {
                    throw new DomainException("Medicine was not recorded in the database");
                }
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message);
            }
        }
    }
}
