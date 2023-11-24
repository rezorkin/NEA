using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.Domain;

namespace Prototype.MENU
{
    static internal class Menu
    {
        static Stock assortment;
        public static void Start()
        {
            Menu.assortment = new Stock();
            string[] medicines = GetMedicinesToStrings(Menu.assortment.GetCurrentAssortment());
            foreach (var medicine in medicines)
            {
                Console.WriteLine(medicine);
            }
            MakeChoice();
            Console.ReadKey();

        }
        private static void MakeChoice()
        {
            Console.WriteLine("Enter ID number to select medicine");
            int enteredID = int.Parse(Console.ReadLine());
            Medicine selectedMedicine = assortment.findByID(enteredID);
            Console.WriteLine();
            Console.WriteLine("Medicine selected:" + selectedMedicine.ToString());
            ShowMedicineCountHistory(selectedMedicine);
        }
        private static void ShowMedicineCountHistory(Medicine medicine)
        {
            List<WarehouseInspection> countHistory = medicine.GetInspectionHistory();
            foreach(WarehouseInspection inspection in  countHistory) 
            {
                Console.WriteLine(inspection.ToString());
            }
        }
        public static string[] GetMedicinesToStrings(List<Medicine> medicines)
        {
            string[] medicinesTostrings = new string[medicines.Count];
            for (int i = 0; i < medicines.Count; i++)
            {
                medicinesTostrings[i] = medicines[i].ToString();
            }
            return medicinesTostrings;
        }
    }
}
