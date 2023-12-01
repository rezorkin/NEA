using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Domain;

namespace NEA.MENU
{
    internal class MedicineTable : Table
    {
        private List<Medicine> assortment;
        private Stock stock;

        public MedicineTable(int pageLength):base(pageLength) 
        {
            stock = new Stock();
            assortment = stock.GetCurrentAssortment();
        } 
        public override void MakeChoice()
        {
            Console.WriteLine("Press Left or Right arrow to navigate on the pages");
            if(Console.ReadKey(false).Key == ConsoleKey.RightArrow)
            {
                GoToNextPage();
            }
            else if (Console.ReadKey(false).Key == ConsoleKey.LeftArrow)
            {
                GoToPreviousPage();
            }
           
        }

        public override void Sort(string attribute, Order order)
        {
            throw new NotImplementedException();
        }
        protected override void Select()
        {
            Console.WriteLine("Enter ID number to select medicine");
            int enteredID = int.Parse(Console.ReadLine());
            Medicine selectedMedicine = stock.findByID(enteredID);
            Console.WriteLine();
            Console.WriteLine("Medicine selected:" + selectedMedicine.ToString());
            ShowMedicineCountHistory(selectedMedicine);
        }
        public override List<string> ViewAllCommands()
        {
            throw new NotImplementedException();
        }

        protected override List<string> getAttributes()
        {
            List<string> result = new List<string>
            {
                "ID",
                "Name",
                "CompanyName",
                "Active Substance"
            };
            return result;
        }

        protected override List<string> getRowSet()
        {
            var result = new List<string>();
            if(assortment == null) 
            {
                stock = new Stock();
                assortment = stock.GetCurrentAssortment();
            }
            foreach (var item in assortment) 
            {
                result.Add(item.ToString());
            }
            return result;
        }
        private void ShowMedicineCountHistory(Medicine medicine)
        {
            List<StockInspection> countHistory = medicine.GetInspectionHistory();
            foreach (StockInspection inspection in countHistory)
            {
                Console.WriteLine(inspection.ToString());
            }
        }
    }
}
