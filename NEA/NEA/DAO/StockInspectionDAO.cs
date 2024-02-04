using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DOMAIN;
using System.Data.SQLite;

namespace NEA.DAO
{
    internal class StockInspectionDAO : RecordDAO<StockInspection>
    {
        protected override string tableName => "StockInspections";

        public override bool AddNewRecord(int ID, int amount, DateTime date)
        {
            try
            {
                if (IsInspectedAlready(ID, date) == true)
                    throw new DomainException("In this month the medicine was already inspected.");

                using (SQLiteConnection conn = new SQLiteConnection(DAOConnecter.GetConnectionString()))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"INSERT INTO \"{tableName}\"\r\nVALUES(\"{ConvertDateToString(date)}\",{ID}," +
                            $"{amount})";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    conn.Close();
                }
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        protected override StockInspection MapDBRowToItemFields(NameValueCollection row)
        {
            DateTime dateOfInspection = ConvertStringToDate(row["Date"]);
            int amountOfMedicine = int.Parse(row["Amount"]);
            int inspectedMedicineID = int.Parse(row["MedicineID"]);
            Medicine inspectedMedicine = new MedicineDAO().FindByID(inspectedMedicineID);
          
            return new StockInspection(inspectedMedicine, amountOfMedicine, dateOfInspection);
        }
        private bool IsInspectedAlready(int medicineID, DateTime Date)
        {
            List<StockInspection> foundByDate = FindByAttributeValue("Date", ConvertDateToString(Date));
            List<StockInspection> foundByID = FindByAttributeValue("MedicineID", medicineID.ToString());
            foreach (StockInspection record in foundByDate)
            {
                for (int i = 0; i < foundByID.Count; i++)
                {
                    if (record == foundByID[i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
