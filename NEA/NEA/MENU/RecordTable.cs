using NEA.DOMAIN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal class RecordTable : AssortmentTable
    {
        AccountingAuditor auditor;
        RecordWriter writer;
        public RecordTable(int pageLength, ConsoleColor defaultFontColour) : base(pageLength, defaultFontColour)
        {
            auditor = new AccountingAuditor();
            writer = new RecordWriter();
        }
        public override void PrintOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Press Left or Right arrow to navigate on the pages");
            Console.WriteLine("Press P to enter a medicine's id for which you want to insert a new record. ");
            Console.WriteLine("Press B to add a new medicine.");
            Console.WriteLine("Press S to sort medicines");
            Console.WriteLine("Press F to apply filters");
            Console.WriteLine("Press X to reset all filters and sortings");
            Console.WriteLine("Press E to get back to the main menu");
        }
        public override void Select()
        {
            Console.WriteLine();
            if(GetItems().Count == 0) 
            {
                throw new MenuException("In the database there are no medicines yet.");
            }
            Console.WriteLine("Enter Medicine's id to select and add it to your sample.");
            Console.WriteLine($"Enter number from {GetItems().First().GetID()} to " + GetItems().Last().GetID());
            string number = Console.ReadLine();
            try
            {
                int ID = int.Parse(number);
                if (ID <= GetItems().Last().GetID() && ID > 0)
                {

                    Console.WriteLine("Enter 1 to write a new stock inspection.");
                    Console.WriteLine("Enter 2 to write a new purchase order");
                    string choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        AddStockInspection(ID);
                    }
                    else if (choice == "2")
                    {
                        AddPurchaseOrder(ID);
                    }
                    else
                    {
                        throw new MenuException("Invalid input");
                    }
                }
                else
                {
                    throw new MenuException("Id was out of the range");
                }
            }
            catch (DomainException e)
            {
                throw new MenuException(e.Message);
            }
            catch (OverflowException)
            {
                throw new MenuException("Too long value");
            }
            catch(FormatException)  
            {
                throw new MenuException("Invalid value");
            }
        }
        public void AddPurchaseOrder(int ID)
        {
            try
            {
                (int, DateTime) recordDetails = GetAmountAndDateFromUser(ID);
                writer.AddNewPurchaseOrder(ID, recordDetails.Item1, recordDetails.Item2);
                Console.WriteLine();
                Console.WriteLine("Order was sucessfully recorded");
                Console.WriteLine("Press any key to move on");
                Console.ReadKey();
            }
            catch(DomainException e) 
            {
                throw new MenuException(e.Message);
            }
        }
        public void AddStockInspection(int ID)
        {
            try
            {
                (int, DateTime) recordDetails = GetAmountAndDateFromUser(ID);
                writer.AddNewStockInspection(ID, recordDetails.Item1, recordDetails.Item2);
                Console.WriteLine();
                Console.WriteLine("Inspection was sucessfully recorded");
                Console.WriteLine("Press any key to move on");
                Console.ReadKey();
            }
            catch (DomainException e)
            {
                throw new MenuException(e.Message);
            }
        }
        private (int amount, DateTime date) GetAmountAndDateFromUser(int ID)
        {
            try
            {
                Console.WriteLine("Enter year:");
                int year = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter month, without 0 if single digit");
                int month = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter day, without 0 if single digit");
                int day = int.Parse(Console.ReadLine());
                DateTime date = new DateTime(year, month, day);
                Console.WriteLine("Enter amount:");
                int amount = int.Parse(Console.ReadLine());
                return (amount, date);
            }
            catch(Exception) 
            {
                throw new MenuException("Date part is out of range");
            }
        }
        public void AddMedicineToAssortment()
        {
            try
            {
                Console.WriteLine("Enter medicine name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter company name:");
                string companyName = Console.ReadLine();
                Console.WriteLine("Enter active substance code(ATC): ");
                string ATC = Console.ReadLine();
                if(name.Length == 0 || companyName.Length == 0 || ATC.Length == 0)
                {
                    throw new MenuException("Empty fields");
                }
                writer.AddNewMedicine(name, companyName, ATC);
                Console.WriteLine("Medicine was sucessfully added to the assortment.");
                items = GetItems();
                Console.ReadKey();
            }
            catch (DomainException e)
            { 
                throw new MenuException(e.Message); 
            } 
            catch (OverflowException) 
            {  
                throw new MenuException("Invalid value"); 
            }
        }
    }
}
