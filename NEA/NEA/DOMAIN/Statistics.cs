using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class Statistics 
    {
        private double mean;
        private double median;
        private Dictionary<int, int> modes;
        private double standrardDeviation;
        private readonly int roundingLength;
        private Medicine medicine;

        public Statistics(double mean, double median, Dictionary<int, int> modes, double standrardDeviation, int roundingLength, Medicine medicine)
        {
            this.mean = mean;
            this.median = median;
            this.modes = modes;
            this.standrardDeviation = standrardDeviation;
            this.roundingLength = roundingLength;
            this.medicine = medicine;
        }
        public double GetMean()
        {
            return RoundValue(mean);
        }
        public double GetMedian()
        {
            return RoundValue(median);
        }
        public double GetStandrardDeviation() 
        {
            return RoundValue(standrardDeviation);
        }
        public Dictionary<int, int> GetModes()
        {
            return modes;
        }
        public string GetMedicineName()
        {
            return medicine.GetName();
        }
        public int GetMedicineID()
        {
            return medicine.GetID();
        }
        private double RoundValue(double value)
        {
            return Math.Round(value,roundingLength);
        }
        public override string ToString()
        {
            string modesToString = "";
            if (modes.Count == 0)
            {
                modesToString = "-";
            }
            else
            {
                foreach (int key in modes.Keys)
                {
                    if(key != 0)
                    modesToString += key + " ";
                }
            }
            string[] valuesToString = new string[3] { GetMean().ToString(), GetMedian().ToString(), GetStandrardDeviation().ToString() };
            for (int i = 0; i < valuesToString.Length; i++)
            {
                if (valuesToString[i] == "-1")
                {
                    valuesToString[i] = "-";
                }
            }
            return $"{GetMedicineID()},{GetMedicineName()},{valuesToString[0]},{valuesToString[1]},{modesToString},{valuesToString[2]}";
        }
        
    }
}
