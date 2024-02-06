using NEA.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class AccountingAuditor : ISortable<SalesStatistic>
    {
        StockInspectionDAO inspectionDAO;
        PurchaseOrderDAO purchaseOrderDAO;
        SalesAnalyser analyser;
        private readonly int roundingLength;
        public AccountingAuditor(int roundingLength)
        {
            inspectionDAO = new StockInspectionDAO();
            purchaseOrderDAO = new PurchaseOrderDAO();
            this.roundingLength = roundingLength;
        }
        public AccountingAuditor()
        {
            inspectionDAO = new StockInspectionDAO();
            purchaseOrderDAO = new PurchaseOrderDAO();
            this.roundingLength = 1;
        }
        public List<PurchaseOrder> GetPurchaseOrderHistory(int medicineID, OrderBy order)
        {
            try
            {
                List<PurchaseOrder> records = purchaseOrderDAO.GetRecordHistory(medicineID);
                return MergeSort<PurchaseOrder>.MergeSortByDate(records, order);
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message, e);
            }
        }
        public List<StockInspection> GetStockInspectionHistory(int medicineID, OrderBy order)
        {
            try
            {
                return MergeSort<StockInspection>.MergeSortByDate(inspectionDAO.GetRecordHistory(medicineID), order);
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message, e);
            }
        }

        public List<SaleRecord> GetSalesHistory(int medicineID , OrderBy order)
        {
            try
            {
                var purchases = GetPurchaseOrderHistory(medicineID, order);
                var inspections = GetStockInspectionHistory(medicineID, order);
                return CalculateSalesHistory(purchases, inspections);
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message);
            }
        }
        public List<SaleRecord> GetSalesHistory(int medicineID)
        {
            try
            {
                var purchases = GetPurchaseOrderHistory(medicineID, OrderBy.ASC);
                var inspections = GetStockInspectionHistory(medicineID, OrderBy.ASC);
                return CalculateSalesHistory(purchases, inspections);
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message);
            }
        }
        public List<SalesStatistic> GetStatisticRecords(List<Medicine> sample)
        {
            var result = new List<SalesStatistic>();
            foreach (Medicine medicine in sample)
            {
                try
                {
                    var saleshistory = GetSalesHistory(medicine.GetID());
                    analyser = new SalesAnalyser(medicine, saleshistory, roundingLength);
                    result.Add(analyser.GetStatistics(false));
                }
                catch (DomainException)
                {
                    result.Add(new SalesStatistic(-1, -1, (-1, -1), -1, medicine));
                }
            }
            return result;
        }
        public List<SalesStatistic> UpdateStatisticRecords(List<SalesStatistic> statistics, SaleRecord startDate, SaleRecord endDate)
        {
            var result = new List<SalesStatistic>();
            foreach (SalesStatistic record in statistics)
            {
                try
                {
                    List<SaleRecord> salesHistory = GetSalesHistory(record.GetMedicineID());
                    List<SaleRecord> boundedSalesHistory = new List<SaleRecord>();
                    for(int i = 0; i < salesHistory.Count; i++)
                    {
                        if ((salesHistory[i] > startDate || salesHistory[i] == startDate) && 
                            (salesHistory[i] < endDate || salesHistory[i] == endDate))
                        {
                            boundedSalesHistory.Add(salesHistory[i]);
                        }
                    }
                    if(boundedSalesHistory.Count == 0)
                    {
                        throw new DomainException();
                    }
                    analyser = new SalesAnalyser(record.GetMedicine(), boundedSalesHistory, roundingLength);
                    result.Add(analyser.GetStatistics(true));
                }
                catch (DomainException)
                {
                    result.Add(new SalesStatistic(-1, -1, (-1,-1), -1, record.GetMedicine()));
                }
            }
            return result;
        }
        public SaleRecord GetFirstSalesRecordFromTheSample(List<SalesStatistic> statistics, OrderBy order)
        {
            if(statistics.Count == 0)
            {
                throw new DomainException("The sample does not include medicines with existing sales history");
            }
            if (GetSalesHistory(statistics[0].GetMedicineID(), order).Count == 0)
            {
                return GetFirstSalesRecordFromTheSample(statistics.Skip(1).ToList(), order);
            }
            else
            {
               var result = GetSalesHistory(statistics[0].GetMedicineID(), order)[0];
                foreach (var medicine in statistics)
                {
                    List<SaleRecord> medicineSalesHistory = GetSalesHistory(medicine.GetMedicineID(), order);
                    if (medicineSalesHistory.Count != 0)
                    {
                        result = MergeSort<SaleRecord>.Compare(result, medicineSalesHistory[0], order);
                    }

                }
                return result;
            }
            


        } 
        private List<SaleRecord> CalculateSalesHistory(List<PurchaseOrder> purchaseOrders, List<StockInspection> inspections)
        {
            var result = new List<SaleRecord>();
            int cumulativeAmountLeft = 0;
            foreach (StockInspection inspection in inspections)
            {
                int year = inspection.GetRecordDate().Year;
                int month = inspection.GetRecordDate().Month - 1;
                if(inspection.GetRecordDate().Month == 1)
                {
                    year--;
                    month = 12;
                }
                DateTime saleDate = new DateTime(year, month, inspection.GetRecordDate().Day);
                int saleAmount = -1;
                bool IsFirstPurchaseOrderInThisMonth = true;
                Record previousMonth = new Record(inspection.GetMedicine(), inspection.GetAmount(), saleDate);
                for (int i = 0; i < purchaseOrders.Count; i++)
                {
                    if (previousMonth == purchaseOrders[i] && IsFirstPurchaseOrderInThisMonth == true)
                    {
                        saleAmount = purchaseOrders[i].GetAmount() + cumulativeAmountLeft - inspection.GetAmount();
                        IsFirstPurchaseOrderInThisMonth = false;
                    }
                    else if (previousMonth == purchaseOrders[i] && IsFirstPurchaseOrderInThisMonth != true)
                    {
                        saleAmount += purchaseOrders[i].GetAmount();
                    }
                }
                cumulativeAmountLeft = inspection.GetAmount(); 
                if (saleAmount >= 0)
                {
                    result.Add(new SaleRecord(inspection.GetMedicine(), saleAmount, saleDate));
                }
            }
            return result;
        }

        public List<SalesStatistic> Sort<TKey>(List<SalesStatistic> stats, Func<SalesStatistic, TKey> attributeToSortBy, OrderBy order)
        {
            if (order == OrderBy.ASC)
            {
                return stats.OrderBy(attributeToSortBy).ToList();
            }
            return stats.OrderByDescending(attributeToSortBy).ToList();
        }
        
        private static class MergeSort<T> where T : Record
        {
            public static List<T> MergeSortByDate(List<T> records, OrderBy order)
            {
                if (records.Count < 2)
                {
                    return records;
                }
                int mid = records.Count / 2;
                var leftPart = new List<T>();
                var rightPart = new List<T>();
                for(int i = 0; i < records.Count; i++)
                {
                    if(i <  mid)
                    {
                        leftPart.Add(records[i]);
                    }
                    else
                        rightPart.Add(records[i]);
                }
                leftPart = MergeSortByDate(leftPart, order);

                
                rightPart = MergeSortByDate(rightPart, order);

                return Merge(leftPart, rightPart, order);
            }

            public static List<T> Merge(List<T> left, List<T> right, OrderBy order)
            {
                List<T> result = new List<T>();
                int l = 0;
                int r = 0;
                while (l < left.Count && r < right.Count)
                {
                    result.Add(Compare(left[l], right[r], order));
                    if (result.Last() == left[l])
                        l++;
                    else
                        r++;
                }
                while (l < left.Count)
                {
                    result.Add(left[l]);
                    l++;
                }
                while (r < right.Count)
                {
                    result.Add(right[r]);
                    r++;
                }
                return result;
            }
            public static T Compare(T left, T right, OrderBy order)
            {
                if (order == OrderBy.ASC)
                {
                    if (left < right)
                    {
                        return left;
                    }
                    else
                    {
                        return right;
                    }
                }
                else
                {
                    if (left < right)
                    {
                        return right;
                    }
                    else
                        return left;
                }

            }
        }

    }

}
