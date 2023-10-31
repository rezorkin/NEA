using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.Domain;

namespace Prototype.DAO
{
    internal class WarehouseInspectionDAO : DAO<WarehouseInspection>, IWarehouseInspectionDAO
    {
        public override List<WarehouseInspection> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<WarehouseInspection> GetCountHistory(Medicine selectedMedicine)
        {
            throw new NotImplementedException();
        }

        public override int Insert(WarehouseInspection entity)
        {
            throw new NotImplementedException();
        }

        protected override WarehouseInspection SetRetrievedValuesFromDBRow(NameValueCollection row)
        {
            throw new NotImplementedException();
        }
    }
}
