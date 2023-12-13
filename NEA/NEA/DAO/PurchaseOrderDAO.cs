using NEA.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DOMAIN;
using System.Collections.Specialized;
using System.Data.SQLite;

namespace NEA.DAO
{
    internal class PurchaseOrderDAO : RecordDAO<PurchaseOrder>
    {
        public PurchaseOrderDAO() : base()
        { }
        protected override string setTableName()
        {
            return "PurchaseOrders";
        }
        protected override PurchaseOrder MapDBRowToItemFields(NameValueCollection row)
        {
            DateTime dateOfInspection = ConvertStringToDate(row["Date"]);
            int amountOfMedicine = int.Parse(row["Amount"]);
            int inspectedMedicineID = int.Parse(row["MedicineID"]);
            int orderNumber = int.Parse(row["OrderNumber"]);
            Medicine inspectedMedicine = new MedicineDAO().FindByID(inspectedMedicineID);

            return new PurchaseOrder(orderNumber,inspectedMedicine, amountOfMedicine, dateOfInspection);
        }
    }
}
