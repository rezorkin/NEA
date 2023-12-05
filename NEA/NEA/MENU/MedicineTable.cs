using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public override void MakeChoice()
        {

            Console.WriteLine("Press Left or Right arrow to navigate on the pages");
            Console.WriteLine("Press V to 'View All Commands' ");
            
        }

        public override void Sort(string command)
        {
            
            
            Console.ReadKey();
        }
        protected override void Select()
        {
            Console.WriteLine("Enter ID number to select medicine or press = to proceed further calculations with selected medicines");
            Console.WriteLine("Selected medicines: " + selectedMedicines.Count);
            int enteredID = int.Parse(Console.ReadLine());
            selectedMedicines.Add(stock.FindByID(enteredID));
            if(Console.ReadKey(false).Key == ConsoleKey.UpArrow)
            {
                if(selectedMedicines.Count > 0)
                {
                   
                }
                else { Console.WriteLine("You have not select any medecine"); }
            }
            throw new MenuException();
        }
        public override void ViewAllCommands()
        {
           Console.WriteLine();
           Console.WriteLine("Commands:");
           Console.WriteLine("Press S to see the sort command explanation");
           Console.WriteLine("Press M to see the search command explanation");
           Console.WriteLine();

        }
        public void SortCommandExplanation()
        {
            var attributes = getAttributes();
            Console.Write("You can sort the list by: ");
            foreach (var attribute in attributes)
            {
                Console.Write(attribute + ", ");
            }
            Console.Write("and do it either in ascending or descending order.");
            Console.WriteLine("To do that you need to enter an attribute followed by 'asc' or 'desc' without spaces, for ascending and descending respectively.");
            Console.WriteLine("You can enter 'compname' for 'CompanyName' and 'sub' for 'Active substance'. Examples: compnameasc, namedesc, idasc");
            Console.WriteLine();
        }
        public void SearchCommandExplanation()
        {
            var attributes = getAttributes();
            Console.Write("You can search for a medicine by: ");
            foreach (var attribute in attributes)
            {
                Console.Write(attribute + ", ");
            }
            Console.WriteLine("Enter attribute followed by ':' and a value without spaces between semicolumn and the value(you can use spaces in value if it is a name)");
            Console.WriteLine("You can enter 'compname' for 'CompanyName' and 'sub' for 'Active substance'. Examples: compname:NHS, name:Lisinopril, id:4");
            Console.WriteLine();
        }
        public bool IsSortCommand(string command)
        {
            Regex allowedSortCommands = new Regex("(id|sub|name|compname)(asc|desc)");
            return allowedSortCommands.IsMatch(command);
        }
        public bool IsSearchCommand(string command)
        {
            Regex allowedsearchCommmands = new Regex("(id|sub|name|compname):\\w+.*");
            return allowedsearchCommmands.IsMatch(command);
        }
        protected override List<string> getAttributes()
        {
            List<string> result = new List<string>
            {
                "ID",
                "Name",
                "Company Name",
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
