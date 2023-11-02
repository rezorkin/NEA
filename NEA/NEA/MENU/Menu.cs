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
        public static void Start()
        {
            var assortment = new AssortmentOfMedicine();
            string[] medicines = assortment.GetMedicinesToStrings();
            foreach (var medicine in medicines)
            {
                Console.WriteLine(medicine);
            }
            Console.ReadKey();
        }
    }
}
