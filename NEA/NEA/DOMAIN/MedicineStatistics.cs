using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal struct MedicineStatistics
    {
        public int id;
        public string name;
        public double mean;
        public double median;
        public Dictionary<int, int> modes;
        public double standrardDeviation;
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
            string[] valuesToString = new string[3] { mean.ToString(), median.ToString(), standrardDeviation.ToString() };
            for (int i = 0; i < valuesToString.Length; i++)
            {
                if (valuesToString[i] == "-1")
                {
                    valuesToString[i] = "-";
                }
            }
            return $"{id},{name},{valuesToString[0]},{valuesToString[1]},{modesToString},{valuesToString[2]}";
        }
    }
}
