using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    public struct SaleDate
    {
        public int amount;
        public DateTime Date;
        public override string ToString()
        {
            return $"Date:{Date.Day}/{Date.Month}/{Date.Year}, sold: {amount}";
        }
    }
}
