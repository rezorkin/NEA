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
    internal class AnalysisTable : MedicineTable
    {
        private AccountingAuditor auditor;
        private List<MedicineStatistics> statisticRecords;
        private readonly List<Medicine> sample;
        private readonly int roundingLength;
        public AnalysisTable(int pageLength, int spacesToDivider, ConsoleColor defaultFontColour, List<Medicine> sample, int roundingLength) : base(pageLength, spacesToDivider, defaultFontColour)
        {
            this.sample = sample;
            auditor = new AccountingAuditor(roundingLength);
            statisticRecords = auditor.GetStatisticRecords(sample);
            UpdatePages(RecordsToStrings());
            this.roundingLength = roundingLength;
        }
        protected override string[] attributes => new string[] {"ID", "Name", "Mean", "Median", "Mode", "Standard deviation" };
        public void ApplyDateBoundaries()
        {
            Console.WriteLine();
            try
            {
                int year, month;
                DateTime userDate, dateAfter1Month;
                SaleRecord startDate, endDate;
                var earliestSaleRecord = auditor.GetEarlisetSaleRecordFromTheSample(sample);
                var latestSaleRecord = auditor.GetLatestSaleRecordFromTheSample(sample);

                Console.WriteLine();
                Console.WriteLine($"Enter start date (In range from {earliestSaleRecord.GetRecordDate().Month}" +
                    $"/{earliestSaleRecord.GetRecordDate().Year} " +
                    $"to {latestSaleRecord.GetRecordDate().Month}/{latestSaleRecord.GetRecordDate().Year}):");
                Console.WriteLine("Enter year:");
                year = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter month(from 1 to 12):");
                month = int.Parse(Console.ReadLine());
                userDate = new DateTime(year, month, 1);
                startDate = new SaleRecord(userDate);
                Console.WriteLine("Start date: " + userDate.Month + "/" + userDate.Year);
                dateAfter1Month = startDate.GetRecordDate().AddMonths(1);
                SaleRecord recordAfter1Month = new SaleRecord(dateAfter1Month);
                if (startDate < earliestSaleRecord || startDate > latestSaleRecord)
                {
                    throw new OverflowException();
                }
                else if(recordAfter1Month == latestSaleRecord)
                {
                    endDate = new SaleRecord(dateAfter1Month);
                    statisticRecords = auditor.GetStatisticRecords(sample, startDate, endDate);
                    UpdatePages(RecordsToStrings());
                    throw new MenuException("Date boundaries were applied successfully");
                }
                else if(startDate == latestSaleRecord)
                {
                    throw new MenuException("Date boundaries have to be in a range of at least 2 monthes");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Enter end date (In range from {dateAfter1Month.Month}/{dateAfter1Month.Year} " +
                        $"to {latestSaleRecord.GetRecordDate().Month}/{latestSaleRecord.GetRecordDate().Year}):");
                    Console.WriteLine("Enter year:");
                    year = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter month(from 1 to 12):");
                    month = int.Parse(Console.ReadLine());
                    userDate = new DateTime(year, month, 1);
                    endDate = new SaleRecord(userDate);
                    Console.WriteLine("End date: " + userDate.Month + "/" + userDate.Year + ". Press any key to move on");
                    Console.ReadKey();
                    if (startDate < endDate && (endDate < latestSaleRecord || endDate == latestSaleRecord))
                    {
                        statisticRecords = auditor.GetStatisticRecords(sample, startDate, endDate);
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
        protected override void PrintOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Press Left or Right arrow to navigate on the pages");
            Console.WriteLine("Press P to enter a medicine's id to see its sales history");
            Console.WriteLine("Press S to sort medicines");
            Console.WriteLine("Press F to apply time boundaries");
            Console.WriteLine("Press BackSpace to return to the assortment table");
            Console.WriteLine("Press X to reset all time boundaries and sortings");
        }

        public override void SortRows(string attribute, Order order)
        {
            Console.WriteLine();
            if (attribute == attributes[0])
            {
                statisticRecords = MergeSort.MergeSortByID(statisticRecords, order);
                var tableRows = RecordsToStrings();
                UpdatePages(tableRows);
            }
            else if (attribute == attributes[1])
            {
                statisticRecords = MergeSort.MergeSortByName(statisticRecords, order);
                var tableRows = RecordsToStrings();
                UpdatePages(tableRows);
            }
            else if (attribute == attributes[2])
            {
                statisticRecords = MergeSort.MergeSortByMean(statisticRecords, order);
                var tableRows = RecordsToStrings();
                UpdatePages(tableRows);
            }
            else if (attribute == attributes[3])
            {
                statisticRecords = MergeSort.MergeSortByMedian(statisticRecords, order);
                var tableRows = RecordsToStrings();
                UpdatePages(tableRows);
            }
            else if(attribute == attributes[4])
            {
                statisticRecords = MergeSort.MergeSortByModes(statisticRecords, order);
                var tableRows = RecordsToStrings();
                UpdatePages(tableRows);
            }
            else if(attribute == attributes[5])
            {
                statisticRecords = MergeSort.MergeSortByDeviation(statisticRecords, order);
                var tableRows = RecordsToStrings();
                UpdatePages(tableRows);
            }
            else
                throw new MenuException();
        }
        public override void OutputPage()
        {
            base.OutputPage();
            PrintOptions();
        }
        public override void Select()
        {
            Console.WriteLine();
            Console.WriteLine("Enter Medicine's id:");
            string input = Console.ReadLine();
            bool doesMatchesAnySampleID = false;
            foreach (var record in statisticRecords)
            {
                if (input == record.GetID().ToString())
                {
                    doesMatchesAnySampleID = true;
                    int ID = int.Parse(input);
                    var salesHistory = auditor.GetSalesHistory(ID);
                    foreach(var sales in salesHistory) 
                    {
                        Console.WriteLine(sales.ToString());
                    }
                    Console.WriteLine();
                    Console.WriteLine("Press any key to move on");
                    Console.ReadKey();
                }
            }
            if(doesMatchesAnySampleID == false)
            {
                throw new MenuException("Invalid id value");
            }

        }
        protected List<string> RecordsToStrings()
        {
            var result = new List<string>();    
            foreach(MedicineStatistics medicine in statisticRecords)
            {
                result.Add(medicine.ToString());
            }
            return result;
        }

        public override void ResetToInitialTable()
        {
            statisticRecords = auditor.GetStatisticRecords(sample);
            UpdatePages(RecordsToStrings());

        }
        
    }
}
