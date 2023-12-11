using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class MedicineStatistics : Medicine
    {
        private double mean;
        private double median;
        private Dictionary<int, int> modes;
        private double standrardDeviation;
        private readonly int roundingLength;

        public MedicineStatistics(int ID, string name, string companyName, string activeSubstance) : base(ID, name, companyName, activeSubstance)
        {
            mean = -1;
            median = -1;
            modes = new Dictionary<int, int>();
            standrardDeviation = -1;
        }
        public MedicineStatistics(int ID, string name, double mean, double median, Dictionary<int, int> modes, double standrardDeviation, int roundingLength) : base(ID,name)
        {
            this.mean = mean;
            this.median = median;
            this.modes = modes;
            this.standrardDeviation = standrardDeviation;
            this.roundingLength = roundingLength;
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
            return $"{GetID()},{GetName()},{valuesToString[0]},{valuesToString[1]},{modesToString},{valuesToString[2]}";
        }
        
    }
}
