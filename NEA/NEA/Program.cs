using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace PharmacySalesAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "PharmacyDB.db;";
            string connectionString = "Data Source= " + path;
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "INSERT INTO \"AssortmentOfTheMedicalSupplies\"\r\nVALUES (3, 'TEST', 'TEST', 'TEST', 0, 'TEST')"; //it works!
            cmd.ExecuteNonQuery();
            Console.ReadKey();
        }
    }
}
