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
        public Stock() 
        {
            medicineDAO = new MedicineDAO();
            inspectionDAO = new StockInspectionDAO();
        }
        public Medicine findByID(int id)
        {
            try
            {
                Medicine foundMedicine = medicineDAO.FindById(id);
                foundMedicine.SetNewInspectionHistory(inspectionDAO.GetRecordHistory(foundMedicine));
                return foundMedicine;
            }
            catch(DAOException e)
            { 
                throw new DomainException("Particular medicine was not found", e); 
            }
        }
        public List<Medicine> GetCurrentAssortment() 
        {
            try
            {
                List<Medicine> allMedicines = medicineDAO.GetAll();
                return allMedicines;
            }
            catch (DAOException)
            {
                throw new DomainException();
            }
        }
        
    }
}
