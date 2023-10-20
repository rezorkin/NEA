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
            SQLiteConnection conn = new SQLiteConnection("Data Source= PharmacyDB.db;");
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "INSERT INTO \"AssortmentOfTheMedicalSupplies\"\r\nVALUES (3, 'TEST', 'TEST', 'TEST', 0, 'TEST')"; //it works!
            cmd.ExecuteNonQuery();
            Console.ReadKey();
        }
    }
}
