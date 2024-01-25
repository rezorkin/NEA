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
    internal class AssortmentTable : Table
    {
        private Stock stock;
        private List<Medicine> selectedMedicines;
        private List<Medicine> assortment;
        protected override int spacesToDivider => 24;

        protected override string[] attributes => new string[] { "ID", "Name", "Company Name", "Active Substance(ATC) Code" };
        public AssortmentTable(int pageLength, ConsoleColor defaultFontColour) : base(pageLength, defaultFontColour) 
        {
            stock = new Stock();
            assortment = stock.GetAssortment();
            UpdatePages(AssortmentToStrings());
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
        public void FilterRows(string attribute)
        {
            string attributeToFilter = attribute;
            int biggestID = stock.GetBiggestID();
            if (attributeToFilter == attributes[0])
            {
                Console.WriteLine();
                Console.WriteLine($"Enter minimum id value (In range from 1 to {biggestID}):");
                string ID = Console.ReadLine();
                try 
                {
                    int startRange = int.Parse(ID) - 1;
                    int endRange;
                    if (startRange + 1 == 0 || startRange + 1 > biggestID)
                    {
                        throw new OverflowException();
                    }
                    else if (startRange + 1 == biggestID)
                    {
                        endRange = 0;
                        FilterByID(startRange, endRange);
                    }
                    else if (startRange + 2 == biggestID)
                    {
                        endRange = biggestID + 1;
                        FilterByID(startRange, endRange);
                    }
                    else
                    {
                        Console.WriteLine($"Enter maximum id value (In range from {startRange + 2} to {biggestID}):");
                        ID = Console.ReadLine();
                        if (int.Parse(ID) + 1 > startRange)
                        {
                            endRange = int.Parse(ID) + 1;
                            FilterByID(startRange, endRange);
                        }
                        else
                        {
                            throw new MenuException("You entered invalid maximum id value.");
                        }
                    }
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
            else if (attributeToFilter == attributes[1])
            {
                Console.WriteLine();
                Console.WriteLine("Enter name of the medicine. You can enter only one part of the name if the name is like 'Misinopril Ultra'.");
                Console.WriteLine("Entering only 'L' , followed by '+' (L+), you will get all the statisticRecords starting with 'L' , entreing 'Li+' you will get all the statisticRecords starting with 'Li' and so on.");
                string choice = Console.ReadLine();
                try
                {
                    if(choice.Last() == '+')
                    {
                        int length = choice.Length - 1;
                        choice = choice.Substring(0,length);
                        assortment = stock.FindByName(choice, true, false);
                    }
                    else
                    {
                        assortment = stock.FindByName(choice, true, true);
                    }
                    UpdatePages(AssortmentToStrings());
                }
                catch (DomainException e) 
                {
                    throw new MenuException(e.Message);
                }
            }
            else if (attributeToFilter == attributes[2])
            {
                Console.WriteLine();
                Console.WriteLine("Enter name of the company. You can enter only one part of the name if the name is like 'Viabes Collum'.");
                Console.WriteLine("Entering only 'L' , followed by '+'(L +), you will get all the statisticRecords starting with 'L' , entreing 'Li+' you will get all the statisticRecords starting with 'Li' and so on.");
                string choice = Console.ReadLine();
                try
                {
                    if (choice.Last() == '+')
                    {
                        int length = choice.Length - 1;
                        choice = choice.Substring(0, length);
                        assortment = stock.FindByName(choice, false, false);
                    }
                    else
                    {
                        assortment = stock.FindByName(choice, false, true);
                    }
                    UpdatePages(AssortmentToStrings());
                }
                catch (DomainException e)
                {
                    throw new MenuException(e.Message);
                }
            }
            else if (attributeToFilter == attributes[3])
            {
                Console.WriteLine();
                Console.WriteLine("Enter ATC code. Entering not a full code(7 digits) you will get all the statisticRecords within the subgroup you have just ended with");
                string choice = Console.ReadLine();
                try
                {
                    assortment = stock.FilterByActiveSubstance(choice);
                    UpdatePages(AssortmentToStrings());
                }
                catch (DomainException e)
                {
                    throw new MenuException(e.Message);
                }
            }
            else
                throw new MenuException();
        }
        public void ResetSelection()
        {
            selectedMedicines = new List<Medicine>();
        }
        public override void SortRows(string attribute, OrderBy order)
        {
            Console.WriteLine();

            if (attribute == attributes[0])
            {
                assortment = stock.Sort(assortment, medicine => medicine.GetID(), order);
            }
            else if (attribute == attributes[1])
            {
                assortment = stock.Sort(assortment, medicine => medicine.GetName(), order);
            }
            else if (attribute == attributes[2])
            {
                assortment = stock.Sort(assortment, medicine => medicine.GetCompanyName(), order);
            }
            else if (attribute == attributes[3])
            {
                assortment = stock.Sort(assortment, medicine => medicine.GetActiveSubstance(), order);
            }
            else
            {
                throw new MenuException();
            }
            
            var tableRows = AssortmentToStrings();
            UpdatePages(tableRows);
        }
        public override void ResetToInitialTable()
        {
            assortment = stock.GetAssortment();
            UpdatePages(AssortmentToStrings());
        }
        protected List<string> AssortmentToStrings()
        {
            var result = new List<string>();
            foreach (var medicine in assortment)
            {
                result.Add(medicine.ToString());
            }
            return result;
        }
        private void FilterByID(int startRange, int endRange)
        {
            try
            {
                assortment = stock.FilterByID(startRange, endRange);
                UpdatePages(AssortmentToStrings());
            }
            catch (DomainException e)
            {
                throw new MenuException(e.Message);
            }
        }
    }
}
