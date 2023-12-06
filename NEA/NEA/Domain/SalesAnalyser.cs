using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Domain
{
    internal class SalesAnalyser
    {
        private struct SaleDate
        {
            public int amount;
            public DateTime Date;
        }
        List<PurchaseOrder> purchaseOrders;
        List<StockInspection> inspections;
        List<SaleDate> salesHistory;

        public SalesAnalyser(Medicine medicine) 
        {
            var stock = new Stock();
            purchaseOrders = stock.GetPurchaseOrderHistory(medicine);
            inspections = stock.GetStockInspectionHistory(medicine);
            salesHistory = CalculateSalesHistory();
        }
        public double getMedian()
        {
            int[] salesValues = getSalesValues();
            salesValues = SortInAscendingOrder(salesValues);
            if (salesValues.Length % 2 == 0)
            {
                int leftMedianIndex = salesValues.Length / 2 - 1;
                int rightMedianINdex = salesValues.Length / 2;
                double median = (salesValues[leftMedianIndex] + salesValues[rightMedianINdex]) / 2;
                return Math.Round(median, 3);
                 
            }
            else
            {
                int medianIndex = salesValues.Length / 2;
                return (salesValues[medianIndex]);
            }
        }
        public double getMeanTo1dp()
        {
            return Math.Round(getMean(),1);
        }
        public Dictionary<int,int> getModes() 
        {
            int[] salesValues = getSalesValues();
            Dictionary<int,int> salesOccurrences = new Dictionary<int,int>();
            for(int g = 0; g < salesValues.Length; g++)
            {
                int numberOfOccurrences = 0;
                for(int i = 0; i < salesValues.Length; i++)
                {
                    if (salesValues[g] == salesValues[i])
                    {
                        numberOfOccurrences++;
                    }
                }
                if(numberOfOccurrences != 1)
                {
                    salesOccurrences.Add(salesValues[g], numberOfOccurrences);
                }
            }
            if(salesOccurrences.Count == 0)
            {
                throw new DomainException("There are no values occurrenced more then once");
            }
            else
            {
                Dictionary<int,int> modes = new Dictionary<int,int>();
                KeyValuePair<int,int> max = salesOccurrences.Max();
                foreach(KeyValuePair<int,int> saleOccurrence in salesOccurrences)
                {
                    if (saleOccurrence.Value == max.Value)
                    {
                        modes.Add(saleOccurrence.Key, saleOccurrence.Value);
                    }
                }
                return modes;
            }

        }
        public double getStandrardDeviation()
        {
            return Math.Sqrt(getVariance());
        }
        private double getVariance()
        {
            double mean = getMean();
            double sumOfSquareDistances = 0;
            int[] salesValues = getSalesValues();
            foreach (int saleValue in salesValues)
            {
                double squareDistance = Math.Pow((saleValue - mean), 2);
                sumOfSquareDistances += squareDistance;
            }
            double variance = sumOfSquareDistances / salesValues.Length;
            return variance;
        }
        private double getMean()
        {
            int[] salesValues = getSalesValues();
            int totalSum = 0;
            foreach (int salesValue in salesValues)
            {
                totalSum += salesValue;
            }
            return totalSum / salesValues.Length;
        }
        private List<SaleDate> CalculateSalesHistory()
        {
            var result = new List<SaleDate>();
            foreach(StockInspection inspection in inspections) 
            {
                var sale = new SaleDate();
                sale.Date = inspection.getRecordDate();
                bool IsFirstPurchaseOrderInThisMonth = true;
                for(int i = 0; i < purchaseOrders.Count; i++)
                {
                    if(inspection.getRecordDate() == purchaseOrders[i].getRecordDate() && IsFirstPurchaseOrderInThisMonth == true)
                    {
                        sale.amount = purchaseOrders[i].getAmount() - inspection.getAmount();
                        IsFirstPurchaseOrderInThisMonth = false;
                    }
                    else if(inspection.getRecordDate() == purchaseOrders[i].getRecordDate() && IsFirstPurchaseOrderInThisMonth != true)
                    {
                        sale.amount += purchaseOrders[i].getAmount();
                    }
                }
                result.Add(sale);
            }
            return result;
        }
        private int[] getSalesValues()
        {
            int[] saleValues = new int[salesHistory.Count];
            for (int i = 0; i < salesHistory.Count; i++)
            {
                saleValues[i] = salesHistory[i].amount;
            }
            return saleValues;
        }
        private int[] SortInAscendingOrder(int[] salesValues)
        {
            int i = 0;
            int next = 1;
            while (next < salesValues.Length)
            {
                int nextNum = salesValues[next];
                int currNum = salesValues[i];
                if (nextNum < currNum)
                {
                    salesValues[i] = nextNum;
                    salesValues[next] = currNum;
                    return SortInAscendingOrder(salesValues);
                }
                i++;
                next++;
            }
            return salesValues;
        }

    }
}
