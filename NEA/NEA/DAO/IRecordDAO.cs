using NEA.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DOMAIN;
using System.ComponentModel;

namespace NEA.DAO
{
    internal interface IRecordDAO<T> where T : Record
    {
       List<T> GetRecordHistory(Medicine selectedMedicine);
       List<T> GetRecordHistory(int medicineID);
       bool AddNewRecord(int MedicineID, int amount, DateTime date);
    }
}
