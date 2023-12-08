using NEA.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DOMAIN;

namespace NEA.DAO
{
    internal interface IStockInspectionDAO
    {
        List<StockInspection> GetRecordHistory(Medicine selectedMedicine);
        StockInspection GetByDateID(Medicine selectedMedicine, DateTime Date);
    }
}
