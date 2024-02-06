using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DAO;

namespace NEA.DOMAIN
{
    internal class Stock : ISortable<Medicine>
    {
        MedicineDAO medicineDAO;
        
        public Stock() 
        {
            medicineDAO = new MedicineDAO();
        }
        public int GetBiggestID()
        {
            List<Medicine> medicines = Sort(medicineDAO.GetAll(), medicine => medicine.GetID(), OrderBy.DESC);            
            return medicines.First().GetID();
        }
        public List<Medicine> GetAssortment() 
        {
            try
            {
                return Sort(medicineDAO.GetAll(), medicine => medicine.GetID(), OrderBy.ASC);
            }
            catch (DAOException)
            {
                return new List<Medicine>();
            }
        }
        public List<Medicine> FindByStartLettersName(string name)
        {
            try
            {
                return medicineDAO.FindByStartLettersName(name); ;
            }
            catch (DAOException)
            {
                throw new DomainException("Nothing was found");
            }
        }
        public List<Medicine> FindByPartName(string name)
        {
            try
            {
                return medicineDAO.FindByPartName(name); ;
            }
            catch (DAOException)
            {
                throw new DomainException("Nothing was found");
            }
        }
        public List<Medicine> FindByStartLettersCompanyName(string name)
        {
            try
            {
                return medicineDAO.FindByStartCompanyName(name);
            }
            catch (DAOException)
            {
                throw new DomainException("Nothing was found");
            }
        }
        public List<Medicine> FindByPartCompanyName(string name)
        {
            try
            {
                return medicineDAO.FindByPartCompanyName(name);
            }
            catch (DAOException)
            {
                throw new DomainException("Nothing was found");
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
        public List<Medicine> FilterByID(int startRange, int endRange)
        {
            try
            {
                return medicineDAO.FindInIDRange(startRange, endRange);
            }
            catch (DAOException e) 
            {
                throw new DomainException(e.Message);
            }
        }
        public List<Medicine> FilterByActiveSubstance(string ActiveSubstance)
        {
            try
            {
                return medicineDAO.FindByActiveSubstance(ActiveSubstance);
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message);
            }
        }
        public List<Medicine> Sort<TKey>(List<Medicine> medicines, Func<Medicine, TKey> attributeToSortBy, OrderBy order)
        {
            if(order == OrderBy.ASC)
            {
                return medicines.OrderBy(attributeToSortBy).ToList();
            }
            return medicines.OrderByDescending(attributeToSortBy).ToList();
        }

    }
}
