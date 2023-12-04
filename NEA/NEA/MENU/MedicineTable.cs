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
        private List<Medicine> selectedMedicines;

        public MedicineTable(int pageLength):base(pageLength) 
        {
            stock = new Stock();
            assortment = stock.GetCurrentAssortment();
            selectedMedicines = new List<Medicine>();
        } 
        public List<Medicine> GetSample()
        {
            return selectedMedicines;
        }
        public override MenuAction MakeChoice()
        {

            Console.WriteLine("Press Left or Right arrow to navigate on the pages");
            Console.WriteLine("Press V to 'View All Commands' ");
            if (Console.ReadKey(false).Key == ConsoleKey.RightArrow)
            {
                return MenuAction.GoToNextPage;
            }
            else if (Console.ReadKey(false).Key == ConsoleKey.LeftArrow)
            {
                return MenuAction.GoToPreviousPage;
            }
            else if(Console.ReadKey(false).Key == ConsoleKey.V)
            {
                return MenuAction.ViewAllCommands;
            }
            else
                return MenuAction.Default;
           
        }

        public override void Sort(string attribute, Order order)
        {
            throw new NotImplementedException();
        }
        protected override MenuAction Select()
        {
            Console.WriteLine("Enter ID number to select medicine or press = to proceed further calculations with selected medicines");
            Console.WriteLine("Selected medicines: " + selectedMedicines.Count);
            int enteredID = int.Parse(Console.ReadLine());
            selectedMedicines.Add(stock.FindByID(enteredID));
            if(Console.ReadKey(false).Key == ConsoleKey.UpArrow)
            {
                if(selectedMedicines.Count > 0)
                {
                    return MenuAction.GoToAnalysisTable;
                }
                else { Console.WriteLine("You have not select any medecine"); }
            }
            throw new MenuException();
        }
        public override void ViewAllCommands()
        {
           Console.WriteLine("Press S to enter fwejfkwejfwnekfw");
           Console.WriteLine("Press M to enter medicine name ");
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
