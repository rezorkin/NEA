using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NEA.DOMAIN;

namespace NEA.MENU
{
    internal class AnalysisTable : Table<SalesStatistic>
    {
        private AccountingAuditor auditor;
        private List<Medicine> sample;
        protected override int spacesToDivider => 9;
        public AnalysisTable(int pageLength, ConsoleColor defaultFontColour, List<Medicine> sample, int roundingLength) : base(pageLength, defaultFontColour)
        {
            auditor = new AccountingAuditor(roundingLength);
            this.sample = sample;
        }
        protected override Dictionary<ConsoleKey, string> attributesKeys => new Dictionary<ConsoleKey, string>
        {
            {ConsoleKey.D1, "ID"} , {ConsoleKey.D2, "Name"} , {ConsoleKey.D3, "Mean"}, {ConsoleKey.D4, "Median"},
            {ConsoleKey.D5, "Minimum"}, {ConsoleKey.D6, "Maximum"}, {ConsoleKey.D7, "Standard deviation"}
        };
        public void ApplyDateBoundaries()
        {
            Console.WriteLine();
            try
            {
                int year, month;
                DateTime userDate, dateAfter1Month;
                SaleRecord startDate, endDate;
                var earliestSaleRecord = auditor.GetFirstSalesRecordFromTheSample(items, OrderBy.ASC);
                var latestSaleRecord = auditor.GetFirstSalesRecordFromTheSample(items, OrderBy.DESC);

                Console.WriteLine();
                Console.WriteLine($"Enter start date (In range from {earliestSaleRecord.GetRecordDate().Month}" +
                    $"/{earliestSaleRecord.GetRecordDate().Year} " +
                    $"to {latestSaleRecord.GetRecordDate().Month}/{latestSaleRecord.GetRecordDate().Year}):");
                Console.WriteLine("Enter year:");
                year = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter month:");
                month = int.Parse(Console.ReadLine());
                userDate = new DateTime(year, month, 1);
                startDate = new SaleRecord(userDate);
                Console.WriteLine("Start date: " + userDate.Month + "/" + userDate.Year);
                dateAfter1Month = startDate.GetRecordDate().AddMonths(1);
                if (startDate < earliestSaleRecord || startDate > latestSaleRecord)
                {
                    throw new OverflowException();
                }
                else if(startDate == latestSaleRecord)
                {
                    items = auditor.UpdateStatisticRecords(items, startDate, startDate);
                    Console.WriteLine("Date boundaries were applied successfully");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Enter end date (In range from {dateAfter1Month.Month}/{dateAfter1Month.Year} " +
                        $"to {latestSaleRecord.GetRecordDate().Month}/{latestSaleRecord.GetRecordDate().Year}):");
                    Console.WriteLine("Enter year:");
                    year = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter month:");
                    month = int.Parse(Console.ReadLine());
                    userDate = new DateTime(year, month, 1);
                    endDate = new SaleRecord(userDate);
                    Console.WriteLine("End date: " + userDate.Month + "/" + userDate.Year + ". Press any key to move on");
                    Console.ReadKey();
                    if (startDate < endDate && (endDate < latestSaleRecord || endDate == latestSaleRecord))
                    {
                        items = auditor.UpdateStatisticRecords(items, startDate, endDate);
                    }
                    else
                    {
                        throw new MenuException("You entered invalid ending date.");
                    }
                }
            }
            catch (DomainException e)
            {
                throw new MenuException(e.Message);
            }
            catch(ArgumentOutOfRangeException)
            {
                throw new MenuException("Entered date was out of the range. ");
            }
            catch (OverflowException)
            {
                throw new MenuException("Entered date was out of the range. ");
            }
            catch (FormatException)
            {
                throw new MenuException("Invalid date. ");
            }
        }
        public override void PrintOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Press Left or Right arrow to navigate on the pages");
            Console.WriteLine("Press P to enter a medicine's id to see its sales history");
            Console.WriteLine("Press S to sort medicines");
            Console.WriteLine("Press F to apply time boundaries");
            Console.WriteLine("Press BackSpace to return to the assortment table");
            Console.WriteLine("Press X to reset all time boundaries and sortings");
        }

        public override void SortRows()
        {
             Console.WriteLine();
             string attribute = ReceiveAttributeFromUser();
             OrderBy order = ReceiveSortOrderFromUser();
             if (attribute == attributesKeys[ConsoleKey.D1])
             {
                items = auditor.Sort(items, stat => stat.GetMedicineID(), order);
             }
             else if (attribute == attributesKeys[ConsoleKey.D2])
             {
                items = auditor.Sort(items, stat => stat.GetMedicineName(), order);
             }
             else if (attribute == attributesKeys[ConsoleKey.D3])
             {
                items = auditor.Sort(items, stat => stat.GetMean(), order);
             }
             else if (attribute == attributesKeys[ConsoleKey.D4])
             {
                items = auditor.Sort(items, stat => stat.GetMedian(), order);
             }
             else if(attribute == attributesKeys[ConsoleKey.D5])
             {
                items = auditor.Sort(items, stat => stat.GetSalesExtremums().Item1, order);
             }
            else if (attribute == attributesKeys[ConsoleKey.D6])
            {
                items = auditor.Sort(items, stat => stat.GetSalesExtremums().Item2, order);
            }
            else if(attribute == attributesKeys[ConsoleKey.D7])
             {
                items = auditor.Sort(items, stat => stat.GetDeviation(), order);
             }
             else
             { 
                throw new MenuException();
             }
        }
        public override void OutputTable()
        {
            base.OutputTable();
            PrintOptions();
        }
        public override void Select()
        {
            Console.WriteLine();
            Console.WriteLine("Enter Medicine's id:");
            string input = Console.ReadLine();
            try
            {
                int ID = int.Parse(input);
                var salesHistory = auditor.GetSalesHistory(ID);
                foreach (var sales in salesHistory)
                {
                    Console.WriteLine(sales.ToString());
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to move on");
                Console.ReadKey();
            }
            catch(FormatException)
            {
                throw new MenuException("Invalid input type");
            }
            catch(DomainException)
            {
                throw new MenuException("Invalid id value");
            }

        }
        public override void ResetFiltersAndSorts()
        {
            items = auditor.GetStatisticRecords(sample);
        }

        protected override List<SalesStatistic> GetItems()
        {
            return auditor.GetStatisticRecords(sample);
        }
    }
}
