using NEA.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DOMAIN;

namespace NEA.DAO
{
    internal interface IMedicineRecordDAO<T> where T : MedicineRecord
    {
       List<T> GetRecordHistory(Medicine selectedMedicine);
       List<T> GetRecordHistory(int medicineID);
        T GetByDateID(Medicine selectedMedicine, DateTime Date);
    }
}
