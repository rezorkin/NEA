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
            assortment = medicineDAO.GetAll();
            foreach (Medicine medicine in assortment)
            {
                List<WarehouseInspection> inspectionHistory = inspectionDAO.GetCountHistory(medicine);
                for(int i = 0; i < inspectionHistory.Count; i++)
                {
                    medicine.AddNewInspection(inspectionHistory[i]);
                }
            }
        }
        public string[] GetMedicinesToStrings()
        {
            string[] medicinesTostrings = new string[assortment.Count];
            for(int i = 0;i < assortment.Count;i++)
            {
                medicinesTostrings[i] = assortment[i].ToString();
            }
            return medicinesTostrings;
        }



    }
}
