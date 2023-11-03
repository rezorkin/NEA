using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.DAO;

namespace Prototype.Domain
{
    internal class AssortmentOfMedicine
    {
        private List<Medicine> assortment;
        MedicineDAO medicineDAO;
        WarehouseInspectionDAO inspectionDAO;
        public AssortmentOfMedicine() 
        {
            medicineDAO = new MedicineDAO();
            inspectionDAO = new WarehouseInspectionDAO();
        }
        public Medicine findByID(int id)
        {
            try
            {
                Medicine foundMedicine = medicineDAO.FindById(id);
                foundMedicine.SetNewInspectionHistory(inspectionDAO.GetCountHistory(foundMedicine));
                return foundMedicine;
            }
            catch(DAOException e)
            { 
                throw new DomainException("Particular medicine was not found", e); 
            }
        }
        public List<Medicine> GetAll() 
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
