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
        private int roundingLength;
        private Medicine inspectedMedicine;
        public SalesAnalyser(Medicine inspectedMedicine, List<SaleRecord> salesHistory, int roundingLength) 
        {
            this.salesHistory = salesHistory;
            this.roundingLength = roundingLength;
            this.inspectedMedicine = inspectedMedicine;
        }
        public SalesStatistic GetStatistics() 
        {
            if(salesHistory.Count > 0) 
            {
                double mean = CalculateMean();
                double median = CalculateMedian();
                var saleExtremums = GetSaleExtremums();
                double standrardDeviation = CalculateStandartDeviation();
                return new SalesStatistic(mean, median, saleExtremums, standrardDeviation, inspectedMedicine);
            }
            return new SalesStatistic(-1, -1, (-1,-1), -1, inspectedMedicine);

        }
        public double CalculateMedian()
        {
            var sales = GetSales().ToList();
            sales.Sort();
            int[] salesValues = sales.ToArray();
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
            return RoundValue(totalSum / salesValues.Length);
        }
        public (int minimum, int maximum) GetSaleExtremums()
        {
            var sales = GetSales().ToList();
            sales.Sort();
            int minimum = sales[0];
            int maximum = sales.Last();
            return (minimum, maximum);
        }
        public double CalculateStandartDeviation()
        {
            double deviation = Math.Sqrt(CalculateVariance());
            return RoundValue(deviation);
        }
        public double CalculateSampleDeviation()
        {
            double deviation = Math.Sqrt(CalculateSampleVariance());
            return RoundValue(deviation);
        }
        private double CalculateVariance()
        {
            double mean = CalculateMean();
            double sumOfSquareDistances = 0;
            int[] salesValues = GetSales();
            foreach (int saleValue in salesValues)
            {
                double squareDistance = Math.Pow((saleValue - mean), 2);
                sumOfSquareDistances += squareDistance;
            }
            double variance = sumOfSquareDistances / salesValues.Length;
            return variance;
        }
        private double CalculateSampleVariance()
        {
            double mean = CalculateMean();
            double sumOfSquareDistances = 0;
            int[] salesValues = GetSales();
            foreach (int saleValue in salesValues)
            {
                double squareDistance = Math.Pow((saleValue - mean), 2);
                sumOfSquareDistances += squareDistance;
            }
            double variance = sumOfSquareDistances / (salesValues.Length - 1);
            return variance;
        }
        private int[] GetSales()
        {
            int[] Sales = new int[salesHistory.Count];
            for (int i = 0; i < salesHistory.Count; i++)
            {
                Sales[i] = salesHistory[i].GetAmount();
            }
            return Sales;
        }
        private double RoundValue(double value)
        {
            return Math.Round(value, roundingLength);
        }


    }
}
