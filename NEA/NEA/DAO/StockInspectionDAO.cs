using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Domain;

namespace NEA.DAO
{
    internal class StockInspectionDAO : MedicineRecordDAO<StockInspection>, IStockInspectionDAO
    {
        public StockInspectionDAO() : base()
        {}
        protected override string setTableName()
        {
            return "WarehouseInspections";
        }
        protected override StockInspection SetValuesFromTableToObjectFields(NameValueCollection row)
        {
            DateTime dateOfInspection = ConvertStringToDate(row["Date"]);
            int amountOfMedicine = int.Parse(row["Amount"]);
            int inspectedMedicineID = int.Parse(row["MedicineID"]);
            Medicine inspectedMedicine = new MedicineDAO().FindByID(inspectedMedicineID);
          
            return new StockInspection(inspectedMedicine, amountOfMedicine, dateOfInspection);
        }
    }
}
