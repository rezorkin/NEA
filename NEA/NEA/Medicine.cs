using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySalesAnalysis
{
    internal class Medicine
    {
        private string name { get; set; }
        private string companyName { get; set; }
        private string activeSubstance { get; set; }
        private int ID { get; set; }

        public Medicine(string name, string companyName, int ID, string activeSubstance)
        {
            this.name = name;
            this.companyName = companyName;
            this.ID = ID;
            this.activeSubstance = activeSubstance;    
        }

        public override bool Equals(object obj)
        {
            return obj is Medicine medicine &&
                   name == medicine.name &&
                   companyName == medicine.companyName &&
                   activeSubstance == medicine.activeSubstance &&
                   ID == medicine.ID;
        }
        public static bool operator ==(Medicine left, Medicine right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Medicine left, Medicine right)
        {
            return !left.Equals(right);
        }
        public override int GetHashCode() 
        {
            return name.GetHashCode() ^ companyName.GetHashCode() ^ activeSubstance.GetHashCode() ^ ID.GetHashCode();
        }
        public override string ToString()
        {
            return $"Name: {name}, company name: {companyName}, active substance: {activeSubstance}, ID: , {ID}";
        }

    }
}
