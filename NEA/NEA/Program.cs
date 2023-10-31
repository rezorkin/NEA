using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Prototype.Domain;
using Prototype.DAO;
using System.Net.Http.Headers;

namespace PharmacySalesAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MedicineDAO MedicineDAO = new MedicineDAO();
            foreach (Medicine m in MedicineDAO.FindAllByActiveSubstance("A0JWA85V8F"))
            {
                Console.WriteLine(m.ToString());
            }
            Console.ReadKey();
        }
    }
}
