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
        static AssortmentOfMedicine assortment;
        public static void Start()
        {
            Menu.assortment = new AssortmentOfMedicine();
            string[] medicines = GetMedicinesToStrings(Menu.assortment.GetAll());
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
            var countHistory = new List<WarehouseInspection>();
            countHistory = medicine.GetInspectionHistory();
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
