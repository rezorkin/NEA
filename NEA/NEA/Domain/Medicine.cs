using NEA.Domain;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Domain
{
    internal class Medicine
    {
        private int ID;
        private string name;
        private string companyName;
        private string activeSubstance;
        private List<StockInspection> inspectionHistory;
        private List<PurchaseOrder> purchaseOrders;
        public StockInspection lastInspection { get { return inspectionHistory.Last(); } }

        public Medicine(int ID, string name, string companyName, string activeSubstance)
        {
            this.name = name;
            this.companyName = companyName;
            this.ID = ID;
            this.activeSubstance = activeSubstance;
            inspectionHistory = new List<StockInspection>();
            purchaseOrders = new List<PurchaseOrder>();
        }
        public void AddNewInspection(StockInspection newInspection) 
        {
            if (inspectionHistory.Count == 0)
            {
                inspectionHistory.Add(newInspection);
            }
            else
            {
                int i = 0;
                do
                {
                    if (inspectionHistory[i] == newInspection)
                    {
                        throw new DomainException("this inspection is already in the list");
                    }
                    i++;
                }
                while (i < inspectionHistory.Count);
            }
        }
        public void SetNewInspectionHistory(List<StockInspection> inspectionHistory)
        {
            this.inspectionHistory.Clear();
            this.inspectionHistory = inspectionHistory;
        }
        public void SetNewPurchaseOrders(List<PurchaseOrder> purchaseOrders) 
        {
            this.purchaseOrders.Clear();
            this.purchaseOrders = purchaseOrders;
        }
        public string GetName() { return name; }
        public string GetCompanyName() { return companyName; }
        public int GetID() { return ID; }
        public string GetActiveSubstance() { return activeSubstance; }
        public List<StockInspection> GetInspectionHistory() { return inspectionHistory; }
        public List<PurchaseOrder> GetPurchaseOrders()
        {
            return purchaseOrders;
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
            return $"{ID},{name},{companyName},{activeSubstance}";
        }

    }
}
