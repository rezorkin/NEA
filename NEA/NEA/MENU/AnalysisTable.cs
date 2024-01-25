using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NEA.DOMAIN;

namespace NEA.MENU
{
    internal class AnalysisTable : Table
    {
        private AccountingAuditor auditor;
        private List<SalesStatistic> statistics;
        protected override int spacesToDivider => 9;
        public AnalysisTable(int pageLength, ConsoleColor defaultFontColour, List<Medicine> sample, int roundingLength) : base(pageLength, defaultFontColour)
        {
            auditor = new AccountingAuditor(roundingLength);
            statistics = auditor.GetStatisticRecords(sample);
            UpdatePages(RecordsToStrings());
        }
        protected override string[] attributes => new string[] {"ID", "Name", "Mean", "Median", "Minimum", "Maximum", "Standard deviation" };
        public void ApplyDateBoundaries()
        {
            Console.WriteLine();
            try
            {
                int year, month;
                DateTime userDate, dateAfter1Month;
                SaleRecord startDate, endDate;
                var earliestSaleRecord = auditor.GetFirstSalesRecordFromTheSample(statistics, OrderBy.ASC);
                var latestSaleRecord = auditor.GetFirstSalesRecordFromTheSample(statistics, OrderBy.DESC);

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
                    statistics = auditor.UpdateStatisticRecords(statistics, startDate, startDate);
                    UpdatePages(RecordsToStrings());
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
                        statistics = auditor.UpdateStatisticRecords(statistics, startDate, endDate);
                        UpdatePages(RecordsToStrings());
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

        public override void SortRows(string attribute, OrderBy order)
        {
             Console.WriteLine();
             if (attribute == attributes[0])
             {
                statistics = auditor.Sort(statistics, stat => stat.GetMedicineID(), order);
             }
             else if (attribute == attributes[1])
             {
                statistics = auditor.Sort(statistics, stat => stat.GetMedicineName(), order);
             }
             else if (attribute == attributes[2])
             {
                statistics = auditor.Sort(statistics, stat => stat.GetMean(), order);
             }
             else if (attribute == attributes[3])
             {
                statistics = auditor.Sort(statistics, stat => stat.GetMedian(), order);
             }
             else if(attribute == attributes[4])
             {
                statistics = auditor.Sort(statistics, stat => stat.GetSalesExtremums().Item1, order);
             }
            else if (attribute == attributes[5])
            {
                statistics = auditor.Sort(statistics, stat => stat.GetSalesExtremums().Item2, order);
            }
            else if(attribute == attributes[6])
             {
                statistics = auditor.Sort(statistics, stat => stat.GetDeviation(), order);
             }
             else
             { 
                throw new MenuException();
             }
             UpdatePages(RecordsToStrings());
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
        protected List<string> RecordsToStrings()
        {
            var result = new List<string>();    
            foreach(SalesStatistic medicine in statistics)
            {
                result.Add(medicine.ToString());
            }
            return result;
        }

        public override void ResetToInitialTable()
        {
            statistics = auditor.UpdateStatisticRecords(statistics);
            UpdatePages(RecordsToStrings());

        }
        
    }
}
