using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.Domain;
using Prototype.DAO;


namespace PharmacySalesAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Now;
            Console.WriteLine(dateTime.Year);
            MedicineDAO MedicineDAO = new MedicineDAO();
            foreach (Medicine m in MedicineDAO.FindAllByActiveSubstance("A0JWA85V8F"))
            {
                Console.WriteLine(m.ToString());
            }
            Console.ReadKey();
        }
    }
}
