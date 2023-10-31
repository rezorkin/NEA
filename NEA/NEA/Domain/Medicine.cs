using Prototype.Domain;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Domain
{
    internal class Medicine
    {
        private int ID;
        private string name;
        private string companyName;
        private string activeSubstance;
        private WarehouseInspection lastInspection;

        public Medicine(int ID, string name, string companyName, string activeSubstance)
        {
            this.name = name;
            this.companyName = companyName;
            this.ID = ID;
            this.activeSubstance = activeSubstance;    
        }

        public string GetName()
        {
            return name;
        }
        public string GetCompanyName()
        {
            return companyName;
        }
        public int GetID()
        {
            return ID;
        }
        public string GetActiveSubstance()
        {
            return activeSubstance;
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
            return $"Name: {name}, company name: {companyName}, active substance: {activeSubstance}, ID: {ID}";
        }

    }
}
