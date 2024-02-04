using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DAO
{
    internal static class DAOConnecter
    {
        private static string path = "PracticeDB.db;";
        private static string connectionString = "Data Source= " + path + "Version=3;New=False;Compress=True;Read Only=true";
       
        public static void ConnectToPracticeDB()
        {
            path = "PracticeDB.db;";
            connectionString = "Data Source= " + path + "Version=3;New=False;Compress=True;Read Only=true";
        }
        public static void ConnectToLocalDB()
        {
            path = "PharmacyDB.db;";
            connectionString = "Data Source= " + path + "Version=3;New=False;Compress=True;Read Only=false;FailIfMissing=True;";
            try
            {
                using(SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    conn.Close();
                }
            }
            catch (SQLiteException) 
            {
                throw new DAOException();
            }
        }
        public static void CreateDB() 
        {
            path = "PharmacyDB.db;";
            connectionString = "Data Source= " + path + "Version=3;New=True;Compress=True;";
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                   conn.Open();
                   using(SQLiteCommand cmd = conn.CreateCommand()) 
                   {
                        cmd.CommandText = GetAssortmentTableCreateStatement();
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = GetWareHouseInspectionTableCreateStatement();
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = GetPurchaseOrderTableCreateStatement();
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                   }
                   conn.Close();
                }
            }
            catch (SQLiteException)
            {
                throw new DAOException();
            }
        }
        public static string GetConnectionString()
        {
            return connectionString;
        }
        private static string GetWareHouseInspectionTableCreateStatement()
        {
            return "CREATE TABLE \"StockInspections\" (\r\n\t\"Date\"\tTEXT,\r\n\t\"MedicineID\"\tINTEGER,\r\n\t\"Amount\"\tINTEGER,\r\n\tPRIMARY KEY(\"Date\",\"MedicineID\"),\r\n\tFOREIGN KEY(\"MedicineID\") REFERENCES \"AssortmentOfTheMedicalSupplies\"(\"ProductID\")\r\n)";
        }
        private static string GetPurchaseOrderTableCreateStatement()
        {
            return "CREATE TABLE \"PurchaseOrders\" (\r\n\t\"Date\"\tTEXT,\r\n\t\"MedicineID\"\tINTEGER,\r\n\t\"Amount\"\tINTEGER,\r\n\t\"OrderNumber\"\tINTEGER,\r\n\tFOREIGN KEY(\"MedicineID\") REFERENCES \"AssortmentOfTheMedicalSupplies\"(\"ProductID\"),\r\n\tPRIMARY KEY(\"OrderNumber\")\r\n)";
        }
        private static string GetAssortmentTableCreateStatement()
        {
            return "CREATE TABLE \"AssortmentOfTheMedicalSupplies\" (\r\n\t\"ProductID\"\tINTEGER UNIQUE,\r\n\t\"ProductName\"\tTEXT,\r\n\t\"CompanyName\"\tTEXT,\r\n\t\"ActiveSubstance\"\tTEXT,\r\n\tPRIMARY KEY(\"ProductID\")\r\n)";
        }

    }
}
