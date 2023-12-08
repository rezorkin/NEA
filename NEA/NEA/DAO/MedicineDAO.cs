using NEA.DOMAIN;
using NEA.MENU;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NEA.DAO
{
    internal class MedicineDAO : DAO<Medicine>, IMedicineDAO
    {
        private const string tableName = "AssortmentOfTheMedicalSupplies";
        public MedicineDAO() : base() {}

       
        public List<Medicine> FindAllByActiveSubstance(string activeSubstance)
        {
            string command = $"SELECT * FROM AssortmentOfTheMedicalSupplies WHERE ActiveSubstance like \"{activeSubstance}%\"";
            return ExecuteSQlQuerry(command);
        }
        public List<Medicine> FindAllByCompanyName(string companyName, bool IsCompleteName)
        {
            if (IsCompleteName == true)
            {
                return FindByAttributeValue(tableName, "CompanyName", companyName);
            }
            else
            {
                string command = $"SELECT *\r\nFROM AssortmentOfTheMedicalSupplies\r\nWHERE CompanyName like \"{companyName}%\"";
                return ExecuteSQlQuerry(command);
            }
        }

        public List<Medicine> FindAllByName(string name, bool IsCompleteName)
        {
            if(IsCompleteName == true)
            {
                return FindByAttributeValue(tableName, "ProductName", name);
            }
            else
            {
                string command = $"SELECT *\r\nFROM AssortmentOfTheMedicalSupplies\r\nWHERE ProductName like \"{name}%\"";
                return ExecuteSQlQuerry(command);
            }
        }
        public Medicine FindByID(int id)
        {
            string command = $"SELECT *\r\nFROM AssortmentOfTheMedicalSupplies\r\nWHERE ProductID = {id}";
            return ExecuteSQlQuerry(command)[0];
        }

        public List<Medicine> GetAll()
        {
           string command = $"SELECT ProductID, ProductName, CompanyName, ActiveSubstance\r\n" +
                            $"FROM {tableName};";
           return ExecuteSQlQuerry(command);
        }

        private List<Medicine> GetAll(string attribute, Order order)
        {
            string command = $"SELECT ProductID, ProductName, CompanyName, ActiveSubstance\r\n" +
                            $"FROM {tableName}\r\n" +
                            $"ORDER BY\r\n{attribute} {order};";
            return ExecuteSQlQuerry(command);
        }
        private List<Medicine> ExecuteSQlQuerry(string commandStatement)
        {
            try
            {
                List<Medicine> result = new List<Medicine>();
                List<NameValueCollection> undecodedResultSet = new List<NameValueCollection>();
                using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString() + "Version=3;New=False;Compress=True;Read Only=true"))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = commandStatement;
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NameValueCollection currentRowValues = reader.GetValues();
                                undecodedResultSet.Add(currentRowValues);
                            }
                        }
                    }
                    connection.Close();
                }
                foreach (NameValueCollection row in undecodedResultSet)
                {
                    result.Add(SetValuesFromTableToObjectFields(row));
                }
                return result;
            }
            catch (SQLiteException)
            {
                throw new DAOException();
            }
        }

        protected override Medicine SetValuesFromTableToObjectFields(NameValueCollection row)
        {
            int medicineID = int.Parse(row["ProductID"]);
            string medicineName = row["ProductName"];
            string medicineCompanyName = row["CompanyName"];
            string medicineActiveSubstance = row["ActiveSubstance"];
            return new Medicine(medicineID, medicineName, medicineCompanyName, medicineActiveSubstance);
        }

        public List<Medicine> GetSortedByID(Order order)
        {
            return GetAll("ProductID", order);
        }

        public List<Medicine> GetSortedByName(Order order)
        {
            return GetAll("ProductName", order);
        }

        public List<Medicine> GetSortedByCompanyName(Order order)
        {
            return GetAll("CompanyName", order);
        }

        public List<Medicine> GetSortedByActiveSubstance(Order order)
        {
            return GetAll("ActiveSubstance", order);
        }

        public List<Medicine> FindInIDRange(int startRange, int endRange)
        {
            string rangeStatement;
            if (startRange == 0 && endRange > 0)
            {
                rangeStatement = $"ProductID < {endRange}";
            }
            else if(startRange > 0 && endRange > startRange)
            {
                rangeStatement = $"ProductID > {startRange} AND ProductID < {endRange}";
            }
            else if(startRange > 0 && endRange == 0)
            {
                rangeStatement = $"ProductID > {startRange}";
            }
            else
            {
                throw new DAOException(" Wrong boundaries ");
            }
            try
            {
                string command = $"SELECT ProductID, ProductName, CompanyName, ActiveSubstance\r\n" +
                $"FROM AssortmentOfTheMedicalSupplies\r\n" +
                $"WHERE {rangeStatement}";
                return ExecuteSQlQuerry(command);
            }
            catch(SQLiteException)
            {
                throw new DAOException(" Wrong boundaries");
            }
            
        }
    }
}
