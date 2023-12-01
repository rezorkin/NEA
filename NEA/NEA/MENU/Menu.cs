using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Domain;

namespace NEA.MENU
{
    static internal class Menu
    {
        public static void Start()
        {

            OpenAssortmentOfMedicine();
            Console.ReadKey();

        }
        private static void OpenAssortmentOfMedicine()
        {
            MedicineTable table = new MedicineTable(10);
            table.OutputPage();

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
