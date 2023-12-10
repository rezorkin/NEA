using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DOMAIN;

namespace NEA.MENU
{
    internal class AnalysisTable : Table
    {
        private AccountingAuditor auditor;
        private List<MedicineStatistics> statisticRecords;
        private readonly List<Medicine> sample;
        public AnalysisTable(int pageLength, int spacesToDivider, List<Medicine> sample) : base(pageLength, spacesToDivider)
        {
            this.sample = sample;
            auditor = new AccountingAuditor();
            SetRecordsFromSample();
            UpdatePages(RecordsToStrings());
        }
        protected override string[] attributes => new string[] {"ID", "Name", "Mean", "Median", "Mode", "Standard deviation" };
        public override void FilterRows(string attribute)
        {
            throw new NotImplementedException();
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

        public override void Select()
        {
            Console.WriteLine();
            Console.WriteLine("Enter Medicine's id:");
            string input = Console.ReadLine();
            bool doesMatchesAnySampleID = false;
            foreach (var record in statisticRecords)
            {
                if (input == record.id.ToString())
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
            SetRecordsFromSample();
            UpdatePages(RecordsToStrings());

        }
        private void SetRecordsFromSample()
        {
            this.statisticRecords = new List<MedicineStatistics>();
            foreach (Medicine medicine in sample)
            {
                try
                {
                    var saleshistory = auditor.GetSalesHistory(medicine);
                    SalesAnalyser analyser = new SalesAnalyser(saleshistory);
                    var analysedMedicine = new MedicineStatistics
                    {
                        id = medicine.GetID(),
                        name = medicine.GetName(),
                        mean = analyser.getMeanTo1dp(),
                        median = analyser.CalculateMedian(),
                        modes = analyser.CalculateModes(),
                        standrardDeviation = analyser.CalculateStandartDeviation(),
                    };
                    this.statisticRecords.Add(analysedMedicine);
                }
                catch (DomainException)
                {
                    var analysedMedicine = new MedicineStatistics
                    {
                        id = medicine.GetID(),
                        name = medicine.GetName(),
                        mean = -1,
                        median = -1,
                        modes = new Dictionary<int, int>(),
                        standrardDeviation = -1
                    };
                    this.statisticRecords.Add(analysedMedicine);
                }
               
            }
           

        }
    }
}
