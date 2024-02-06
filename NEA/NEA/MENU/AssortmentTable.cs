using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;
using NEA.DOMAIN;

namespace NEA.MENU
{
    internal class AssortmentTable : Table<Medicine>
    {
        private Stock stock = new Stock();
        private List<Medicine> selectedMedicines;
        protected override int spacesToDivider => 24;
        protected override Dictionary<ConsoleKey, string> attributesKeys => new Dictionary<ConsoleKey, string>
        {
            {ConsoleKey.D1, "ID" }, {ConsoleKey.D2, "Name"}, {ConsoleKey.D3, "Company Name"}, {ConsoleKey.D4, "Active Substance(ATC) Code"}
        };
        public AssortmentTable(int pageLength, ConsoleColor defaultFontColour) : base(pageLength, defaultFontColour) 
        {
            selectedMedicines = new List<Medicine>();
        }
        public List<Medicine> GetSample()
        {
            return selectedMedicines;
        }
        public override void PrintOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Press Left or Right arrow to navigate on the pages");
            Console.WriteLine("Press P to enter a medicine's id you want to select. ");
            Console.WriteLine("Press S to sort medicines");
            Console.WriteLine("Press F to apply filters");
            if(selectedMedicines.Count > 0) 
            {
                Console.WriteLine("Press A to move to the analysis table with selected medicine(s)");
            }
            Console.WriteLine("Press X to reset all filters and sortings");
            Console.WriteLine("Press V to reset medicine selection");
            Console.WriteLine("Press E to get back to the main menu");

        }
        public override void Select()
        {
            Console.WriteLine();
            Console.WriteLine("Enter Medicine's id to select and add it to your sample.");
            Console.WriteLine("Enter number from 1 to " + stock.GetBiggestID());
            string number = Console.ReadLine();
            try
            {
                int ID = int.Parse(number);
                if (ID <= stock.GetBiggestID() && ID > 0)
                {
                    var selectedMedicine = stock.FindByID(ID);
                    if (selectedMedicines.Contains(selectedMedicine) == false)
                    {
                        selectedMedicines.Add(selectedMedicine);
                    }
                }
                else
                {
                    throw new MenuException("Id was out of the range");
                }
            }
            catch (OverflowException)
            {
                throw new MenuException("Id was out of the range");
            }
            catch (FormatException)
            {
                throw new MenuException("Invalid id value");
            }
        }
        public override void OutputTable()
        {
            base.OutputTable();
            Console.WriteLine();
            if(selectedMedicines.Count > 0) 
            {
                Console.WriteLine();
                Console.WriteLine($"Selected medicines: ");
                for (int i = 0; i < selectedMedicines.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("ID:" + selectedMedicines[i].GetID() + ", name: " + selectedMedicines[i].GetName());
                    Console.ForegroundColor = defaultFontColour;
                }
            }
            PrintOptions();
        }
        public void FilterByID()
        {
            int biggestID = stock.GetBiggestID();
            Console.WriteLine();
            Console.WriteLine($"Enter minimum id value (In range from 1 to {biggestID}):");
            string ID = Console.ReadLine();
            try
            {
                int startRange = int.Parse(ID);
                int endRange;
                if (startRange < 1 || startRange > biggestID)
                {
                    throw new OverflowException();
                }
                else if (startRange + 1 == biggestID || startRange == biggestID)
                {
                    endRange = biggestID;
                }
                else
                {
                    Console.WriteLine($"Enter maximum id value (In range from {startRange + 1} to {biggestID}):");
                    ID = Console.ReadLine();
                    if (int.Parse(ID) > startRange && int.Parse(ID) <= biggestID)
                    {
                        endRange = int.Parse(ID);
                    }
                    else
                    {
                        throw new MenuException("You entered invalid maximum id value.");
                    }
                }
                items = stock.FilterByID(startRange, endRange);
            }
            catch (OverflowException)
            {
                throw new MenuException("Entered id was out of the range. ");
            }
            catch (FormatException)
            {
                throw new MenuException("Invalid id value. ");
            }
        }
        public void FilterRows()
        {
            try
            {
                string attributeToFilter = ReceiveAttributeFromUser();
                if (attributeToFilter == attributesKeys[ConsoleKey.D1])
                {
                    FilterByID();
                }
                else if(attributeToFilter == attributesKeys[ConsoleKey.D2] || attributeToFilter == attributesKeys[ConsoleKey.D3])
                {
                    FilterByName(attributeToFilter);
                }
                else if (attributeToFilter == attributesKeys[ConsoleKey.D4])
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter ATC code. Entering not a full code(7 digits) you will get all the codes within the subgroup you have just ended with");
                    items = stock.FilterByActiveSubstance(Console.ReadLine());
                }
                else
                    throw new MenuException("Invalid key was pressed");
            }
            catch (DomainException e)
            {
                throw new MenuException(e.Message);
            }
            catch (Exception) 
            {
                throw new MenuException("Invaild value was given");
            }
            
        }
        private void FilterByName(string attributeToFilter)
        {

            Console.WriteLine();
            Console.WriteLine(GetFilterInstructionForNames());
            string choice = Console.ReadLine();
            if (choice.Last() == '+')
            {
                choice = choice.Substring(0, choice.Length - 1);
                if (attributeToFilter == attributesKeys[ConsoleKey.D2])
                {
                    items = stock.FindByStartLettersName(choice);
                }
                else
                {
                    items = stock.FindByStartLettersCompanyName(choice);
                }
            }
            else
            {
                if (attributeToFilter == attributesKeys[ConsoleKey.D2])
                {
                    items = stock.FindByPartName(choice);
                }
                else
                    items = stock.FindByPartCompanyName(choice);
            }
        }
        private string GetFilterInstructionForNames()
        {
            return "You can enter only one part of the name."
                + "Entering only 'L' , followed by '+'(L +), you will get all the names starting with 'L' , " +
                "entreing 'Li+' you will get all the names starting with 'Li' and so on.";
        }
        public void ResetSelection()
        {
            selectedMedicines = new List<Medicine>();
        }
        public override void SortRows()
        {
            Console.WriteLine();
            string attribute = ReceiveAttributeFromUser();
            OrderBy order = ReceiveSortOrderFromUser();
            if (attribute == attributesKeys[ConsoleKey.D1])
            {
                items = stock.Sort(items, medicine => medicine.GetID(), order);
            }
            else if (attribute == attributesKeys[ConsoleKey.D2])
            {
                items = stock.Sort(items, medicine => medicine.GetName(), order);
            }
            else if (attribute == attributesKeys[ConsoleKey.D3])
            {
                items = stock.Sort(items, medicine => medicine.GetCompanyName(), order);
            }
            else if (attribute == attributesKeys[ConsoleKey.D4])
            {
                items = stock.Sort(items, medicine => medicine.GetActiveSubstance(), order);
            }
            else
            {
                throw new MenuException();
            }
        }
        public override void ResetFiltersAndSorts()
        {
            items = stock.GetAssortment();
        }

        protected override List<Medicine> GetItems()
        {
            return stock.GetAssortment();
        }
    }
}
