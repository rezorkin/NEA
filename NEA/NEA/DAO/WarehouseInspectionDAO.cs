﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.Domain;

namespace Prototype.DAO
{
    internal class WarehouseInspectionDAO : MedicineRecordDAO<WarehouseInspection>, IWarehouseInspectionDAO
    {
        public WarehouseInspectionDAO() : base()
        {}
        protected override string setTableName()
        {
            return "WarehouseInspections";
        }
        protected override WarehouseInspection SetValuesFromTableToObjectFields(NameValueCollection row)
        {
            DateTime dateOfInspection = ConvertStringToDate(row["Date"]);
            int amountOfMedicine = int.Parse(row["Amount"]);
            int inspectedMedicineID = int.Parse(row["MedicineID"]);
            Medicine inspectedMedicine = new MedicineDAO().FindById(inspectedMedicineID);
          
            return new WarehouseInspection(inspectedMedicine, amountOfMedicine, dateOfInspection);
        }
        protected override WarehouseInspection FindByDateID(List<WarehouseInspection> foundByDate, List<WarehouseInspection> foundByID)
        {
            foreach (WarehouseInspection record in foundByDate)
            {
                for (int i = 0; i < foundByID.Count; i++)
                {
                    if (record == foundByID[i])
                    {
                        return record;
                    }
                }
            }
            throw new DAOException("Particular record by Date and ID was not found");
        }
    }
}
