using NEA.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DOMAIN;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.Security.Cryptography;

namespace NEA.DAO
{
    internal class PurchaseOrderDAO : RecordDAO<PurchaseOrder>
    {
        protected override string tableName => "PurchaseOrders";

        public override bool AddNewRecord(int ID, int amount, DateTime date)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DAOConnecter.GetConnectionString()))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        int orderNumber = GetLastOrderNumber() + 1;
                        cmd.CommandText = $"INSERT INTO \"{tableName}\" VALUES(\"{ConvertDateToString(date)}\",{ID}," +
                            $"{amount}, {orderNumber})";
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

        public int GetLastOrderNumber()
        {
            try
            {
                string commandText = $"SELECT OrderNumber " +
                $"FROM \"{tableName}\" " +
                $"ORDER BY OrderNumber DESC";
                int result = new int();
                using (SQLiteConnection connection = new SQLiteConnection(DAOConnecter.GetConnectionString()))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = commandText;
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            bool b = reader.Read();
                            if (b == false)
                            {
                                result = 0;
                            }
                            else
                            {
                                result = int.Parse(reader.GetValues()["OrderNumber"]);
                            }
                        }
                    }
                    connection.Close();
                }
                return result;
            }
            catch (SQLiteException)
            {
                throw new DAOException("No records were found");
            }
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
