using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NEA.DOMAIN
{
    internal class SalesAnalyser
    {
        private List<SaleRecord> salesHistory;
        public SalesAnalyser(List<SaleRecord> salesHistory) 
        {
            this.salesHistory = salesHistory;
        }
        public double CalculateMedian()
        {
            int[] salesValues = GetSales();
            salesValues = MergeSort.MergeSortArray(salesValues, Order.ASC);
            if (salesValues.Length % 2 == 0)
            {
                int leftMedianIndex = salesValues.Length / 2 - 1;
                int rightMedianIndex = salesValues.Length / 2;
                double median = (salesValues[leftMedianIndex] + salesValues[rightMedianIndex]) / 2;
                return median;
                 
            }
            else
            {
                int medianIndex = salesValues.Length / 2;
                return (salesValues[medianIndex]);
            }
        }
        public double CalculateMean()
        {
            int[] salesValues = GetSales();
            int totalSum = 0;
            foreach (int salesValue in salesValues)
            {
                totalSum += salesValue;
            }
            return totalSum / salesValues.Length; ;
        }
        public Dictionary<int,int> CalculateModes() 
        {
            int[] salesValues = GetSales();
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
                return new Dictionary<int, int>();
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
        public double CalculateStandartDeviation()
        {
            double deviation = Math.Sqrt(CalculateVariance(false));
            return deviation;
        }
        public double CalculateSampleDeviation()
        {
            double deviation = Math.Sqrt(CalculateVariance(true));
            return deviation;
        }
        private double CalculateVariance(bool isSampleVariance)
        {
            double mean = CalculateMean();
            double sumOfSquareDistances = 0;
            int[] salesValues = GetSales();
            foreach (int saleValue in salesValues)
            {
                double squareDistance = Math.Pow((saleValue - mean), 2);
                sumOfSquareDistances += squareDistance;
            }
            if(isSampleVariance == false)
            {
                double variance = sumOfSquareDistances / salesValues.Length;
                return variance;
            }
            else
            {
                double variance = sumOfSquareDistances / (salesValues.Length - 1);
                return variance;
            }
            
        }
        private int[] GetSales()
        {
            int[] Sales = new int[salesHistory.Count];
            for (int i = 0; i < salesHistory.Count; i++)
            {
                Sales[i] = salesHistory[i].getAmount();
            }
            return Sales;
        }

    }
}
