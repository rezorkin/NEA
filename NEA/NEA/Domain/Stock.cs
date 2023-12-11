using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DAO;

namespace NEA.DOMAIN
{
    internal class Stock
    {
        MedicineDAO medicineDAO;
        
        public Stock() 
        {
            medicineDAO = new MedicineDAO();
        }
        public int GetBiggestID()
        {
            return medicineDAO.GetSortedByID(Order.ASC).Last().GetID();
        }
        public List<Medicine> GetAssortment() 
        {
            try
            {
                List<Medicine> allMedicines = medicineDAO.GetAll();
                return allMedicines;
            }
            catch (DAOException)
            {
                return new List<Medicine>();
            }
        }
        public List<Medicine> Sort(SortOption attribute, Order order, List<Medicine> sample)
        {
            if (sample.Count < GetAssortment().Count)
            {
                return SortedSample(attribute, order, sample);
            }
            else
                return SortedAssortment(attribute, order);
        }
        private List<Medicine> SortedAssortment(SortOption attribute, Order order) 
        {
            if (attribute == SortOption.ID)
                return medicineDAO.GetSortedByID(order);
            else if (attribute == SortOption.Name)
                return medicineDAO.GetSortedByName(order);
            else if (attribute == SortOption.CompanyName)
                return medicineDAO.GetSortedByCompanyName(order);
            else if (attribute == SortOption.ActiveSubstance)
                return medicineDAO.GetSortedByActiveSubstance(order);
            else
                throw new DomainException("Invalid sort option");
        }
        private List<Medicine> SortedSample(SortOption attribute, Order order, List<Medicine> sample)
        {
            if (attribute == SortOption.ID)
                return MergeSort.MergeSortByID(sample, order);
            else if (attribute == SortOption.Name)
                return MergeSort.MergeSortByName(sample, order);
            else if (attribute == SortOption.CompanyName)
                return MergeSort.MergeSortByCompName(sample, order);
            else if (attribute == SortOption.ActiveSubstance)
                return MergeSort.MergeSortByActiveSubstance(sample, order);
            else
                throw new DomainException("Invalid sort option");
        }
        public List<Medicine> FindByName(string name, bool IsProductName, bool IsCompleteName)
        {
            try
            {
                List<Medicine> medicines;
                if (IsProductName == true)
                {
                    medicines = medicineDAO.FindAllByName(name, IsCompleteName);
                }
                else
                {
                    medicines = medicineDAO.FindAllByCompanyName(name, IsCompleteName);
                }
                return medicines;
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
                return medicineDAO.FindAllByActiveSubstance(ActiveSubstance);
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message);
            }
        }

    }
}
