using Prototype.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.Domain;
using System.Collections.Specialized;

namespace Prototype.DAO
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
            Medicine inspectedMedicine = new MedicineDAO().FindById(inspectedMedicineID);

            return new PurchaseOrder(inspectedMedicine, amountOfMedicine, dateOfInspection);
        }
        protected override PurchaseOrder FindByDateID(List<PurchaseOrder> foundByDate, List<PurchaseOrder> foundByID)
        {
            foreach (PurchaseOrder record in foundByDate)
            {
                for (int i = 0; i < foundByID.Count; i++)
                {
                    if (record == foundByID[i])
                    {
                        return record;
                    }
                }
            }
            throw new DAOException("Particular record by Date and ID was not found");
        }
    }
}
