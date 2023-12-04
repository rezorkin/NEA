using NEA.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Domain;
using System.Collections.Specialized;

namespace NEA.DAO
{
    internal class PurchaseOrderDAO : MedicineRecordDAO<PurchaseOrder>
    {
        public PurchaseOrderDAO() : base()
        { }
        protected override string setTableName()
        {
            return "PurchaseOrders";
        }
        protected override PurchaseOrder SetValuesFromTableToObjectFields(NameValueCollection row)
        {
            DateTime dateOfInspection = ConvertStringToDate(row["Date"]);
            int amountOfMedicine = int.Parse(row["Amount"]);
            int inspectedMedicineID = int.Parse(row["MedicineID"]);
            Medicine inspectedMedicine = new MedicineDAO().FindByID(inspectedMedicineID);

            return new PurchaseOrder(inspectedMedicine, amountOfMedicine, dateOfInspection);
        }
    }
}
