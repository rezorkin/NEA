using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Domain;
using NEA.DAO;
using NEA.MENU;

namespace PharmacySalesAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] a = { 1,567,34,21,78,4,9,43,25,567,5435,67,98,0,98,54,235,654,2342,6,87,8};
            int[] sortedA = AttributeSorter.MergeSort(a, Order.ascending);
            foreach (var item in sortedA)
            {
                Console.Write(item + " ");
            }
            Console.ReadKey();
            Menu.Start();
        }
    }
}
