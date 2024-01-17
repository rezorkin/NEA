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
            return medicineDAO.GetSortedByID(OrderBy.ASC).Last().GetID();
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
        public List<Medicine> Sort(SortOption attribute, OrderBy order, List<Medicine> assortment)
        {
            bool IsAssortmentfiltered = assortment.Count < GetAssortment().Count;
            if (IsAssortmentfiltered == true)
            {
                return GetSortedSample(attribute, order, assortment);
            }
            return GetAssortment(attribute, order);
        }
        private List<Medicine> GetAssortment(SortOption attribute, OrderBy order) 
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
        private List<Medicine> GetSortedSample(SortOption attribute, OrderBy order, List<Medicine> sample)
        {
            if (attribute == SortOption.ID)
                return Sort(sample, medicine => medicine.GetID(), order);
            else if (attribute == SortOption.Name)
                return Sort(sample, medicine => medicine.GetName(), order);
            else if (attribute == SortOption.CompanyName)
                return Sort(sample, medicine => medicine.GetCompanyName(), order);
            else if (attribute == SortOption.ActiveSubstance)
                return Sort(sample, medicine => medicine.GetActiveSubstance(), order);
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
       
        public List<Medicine> Sort<TKey>(List<Medicine> medicines, Func<Medicine, TKey> sorter, OrderBy order)
        {
            if(order == OrderBy.ASC)
            {
                return medicines.OrderBy(sorter).ToList();
            }
            return medicines.OrderByDescending(sorter).ToList();
        }

    }
}
