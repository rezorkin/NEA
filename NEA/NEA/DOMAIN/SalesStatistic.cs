using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class SalesStatistic 
    {
        private double mean;
        private double median;
        private (int minimum, int maximum) saleExtremums;
        private double Deviation;
        private Medicine medicine;

        public SalesStatistic(double mean, double median, (int minimum, int maximum) saleExtremums, double Deviation, Medicine medicine)
        {
            this.mean = mean;
            this.median = median;
            this.saleExtremums = saleExtremums;
            this.Deviation = Deviation;
            this.medicine = medicine;
        }
        public double GetMean()
        {
            return mean;
        }
        public double GetMedian()
        {
            return median;
        }
        public double GetDeviation() 
        {
            return Deviation;
        }
        public (int, int) GetSalesExtremums()
        {
            return saleExtremums;
        }
        public string GetMedicineName()
        {
            return medicine.GetName();
        }
        public int GetMedicineID()
        {
            return medicine.GetID();
        }
        public Medicine GetMedicine()
        {
            return medicine;
        }
        public override string ToString()
        {
            
            double[] values = { GetMean(), GetMedian(),GetSalesExtremums().Item1,GetSalesExtremums().Item2, GetDeviation()};
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetMedicineID() + ",");
            stringBuilder.Append(GetMedicineName());
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] <= 0)
                {
                    stringBuilder.Append(",-");
                }
                else
                {
                    stringBuilder.Append("," + values[i]);
                }
            }
            return stringBuilder.ToString();
        }
        
    }
}
