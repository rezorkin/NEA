using Prototype.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.Domain;

namespace Prototype.DAO
{
    internal interface IWarehouseInspectionDAO 
    {
        List<WarehouseInspection> GetCountHistory(Medicine selectedMedicine);
    }
}
