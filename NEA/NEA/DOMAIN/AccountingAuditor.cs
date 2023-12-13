using NEA.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class AccountingAuditor
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
        public List<PurchaseOrder> GetPurchaseOrderHistory(Medicine medicine)
        {
            try
            {
                return purchaseOrderDAO.GetRecordHistory(medicine);
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message, e);
            }
        }
        public List<StockInspection> GetStockInspectionHistory(Medicine medicine)
        {
            try
            {
                return inspectionDAO.GetRecordHistory(medicine);
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message, e);
            }
        }
        public List<SaleRecord> GetSalesHistory(Medicine medicine)
        {
            try
            {
                var purchases = purchaseOrderDAO.GetRecordHistory(medicine, OrderBy.ASC);
                var inspections = inspectionDAO.GetRecordHistory(medicine, OrderBy.ASC);
                return CalculateSalesHistory(purchases, inspections);
            }
            catch(DAOException e) 
            {
                throw new DomainException(e.Message);
            }
        }
        public List<SaleRecord> GetSalesHistory(int medicineId)
        {
            try
            {
                var purchases = purchaseOrderDAO.GetRecordHistory(medicineId);
                var inspections = inspectionDAO.GetRecordHistory(medicineId);
                return CalculateSalesHistory(purchases, inspections);
            }
            catch (DAOException e)
            {
                throw new DomainException(e.Message);
            }
        }
        public List<MedicineStatistics> GetStatisticRecords(List<Medicine> sample)
        {
            var result = new List<MedicineStatistics>();
            foreach (Medicine medicine in sample)
            {
                try
                {
                    var saleshistory = GetSalesHistory(medicine);
                    analyser = new SalesAnalyser(saleshistory);
                    int id = medicine.GetID();
                    string name = medicine.GetName();
                    double mean = analyser.CalculateMean();
                    double median = analyser.CalculateMedian();
                    var modes = analyser.CalculateModes();
                    double standrardDeviation = analyser.CalculateStandartDeviation();
                    var analysedMedicine = new MedicineStatistics(id, name, mean, median, modes, standrardDeviation,roundingLength);
                    result.Add(analysedMedicine);
                }
                catch (DomainException)
                {
                    int id = medicine.GetID();
                    string name = medicine.GetName();
                    double mean = -1;
                    double median = -1;
                    var modes = new Dictionary<int, int>();
                    double standrardDeviation = -1;
                    var analysedMedicine = new MedicineStatistics(id, name, mean, median, modes, standrardDeviation, roundingLength);
                    result.Add(analysedMedicine);
                }
            }
            return result;
        }
        public List<MedicineStatistics> GetStatisticRecords(List<Medicine> sample, SaleRecord startDate, SaleRecord endDate)
        {
            var result = new List<MedicineStatistics>();
            foreach (Medicine medicine in sample)
            {
                try
                {
                    List<SaleRecord> salesHistory = GetSalesHistory(medicine);
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
                    analyser = new SalesAnalyser(boundedSalesHistory);
                    int id = medicine.GetID();
                    string name = medicine.GetName();
                    double mean = analyser.CalculateMean();
                    double median = analyser.CalculateMedian();
                    var modes = analyser.CalculateModes();
                    double standrardDeviation = analyser.CalculateSampleDeviation();
                    var analysedMedicine = new MedicineStatistics(id, name, mean, median, modes, standrardDeviation, roundingLength);
                    result.Add(analysedMedicine);
                }
                catch (DomainException)
                {
                    int id = medicine.GetID();
                    string name = medicine.GetName();
                    double mean = -1;
                    double median = -1;
                    var modes = new Dictionary<int, int>();
                    double standrardDeviation = -1;
                    var analysedMedicine = new MedicineStatistics(id, name, mean, median, modes, standrardDeviation, roundingLength);
                    result.Add(analysedMedicine);
                }
            }
            return result;
        }
        public SaleRecord GetEarlisetSaleRecordFromTheSample(List<Medicine> sample)
        {
            return GetSaleRecordFromTheSample(sample, true);
        }
        public SaleRecord GetLatestSaleRecordFromTheSample(List<Medicine> sample)
        {
            return GetSaleRecordFromTheSample(sample, false);
        }
        private SaleRecord GetSaleRecordFromTheSample(List<Medicine> sample, bool IsEarliest) 
        {
            
            SaleRecord result;
            if (sample.Count == 0)
            {
                throw new DomainException("The sample does not include medicines with existing sales history");
            }
            try
            {
                if (IsEarliest == true)
                {
                    result = GetSalesHistory(sample[0])[0];
                }
                else
                {
                    result = GetSalesHistory(sample[0]).Last();
                }
            }
            catch (DomainException)
            {
                return GetSaleRecordFromTheSample(sample.Skip(1).ToList(), IsEarliest);
            }
            foreach (var medicine in sample)
            {
                try
                {
                    List<SaleRecord> medicineSalesHistory = GetSalesHistory(medicine);
                    if (medicineSalesHistory.Count != 0)
                    {
                        if (IsEarliest == true)
                        {
                            if (medicineSalesHistory[0] < result)
                            {
                                result = medicineSalesHistory[0];
                            }
                        }
                        else
                        {
                            if (medicineSalesHistory.Last() > result)
                            {
                                result = medicineSalesHistory[0];
                            }
                        }

                    }
                }
                catch (DomainException) { }
            }
            return result;


        }
        private List<SaleRecord> CalculateSalesHistory(List<PurchaseOrder> purchaseOrders, List<StockInspection> inspections)
        {
            var result = new List<SaleRecord>();
            foreach (StockInspection inspection in inspections)
            {
                DateTime saleDate = inspection.GetRecordDate();
                int saleAmount = 0;
                bool IsFirstPurchaseOrderInThisMonth = true;
                for (int i = 0; i < purchaseOrders.Count; i++)
                {
                    if (inspection == purchaseOrders[i] && IsFirstPurchaseOrderInThisMonth == true)
                    {
                        saleAmount = purchaseOrders[i].GetAmount() - inspection.GetAmount();
                        IsFirstPurchaseOrderInThisMonth = false;
                    }
                    else if (inspection.GetRecordDate() == purchaseOrders[i].GetRecordDate() && IsFirstPurchaseOrderInThisMonth != true)
                    {
                        saleAmount += purchaseOrders[i].GetAmount();
                    }
                }
                var sale = new SaleRecord(inspection.GetMedicine(), saleAmount, saleDate);
                result.Add(sale);
            }
            return result;
        }
    }
    
}
