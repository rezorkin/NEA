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
        protected override string tableName => "AssortmentOfTheMedicalSupplies";
        public MedicineDAO() : base() {}

       
        public List<Medicine> FindByActiveSubstance(string activeSubstance)
        {
            return FindByPart("ActiveSubstance", activeSubstance);
        }
        public List<Medicine> FindByCompanyName(string companyName)
        {
            return FindByAttributeValue("CompanyName", companyName);
        }

        public List<Medicine> FindByName(string name)
        {
            return FindByAttributeValue("ProductName", name);
        }
        
        public Medicine FindByID(int id)
        {
            return FindByAttributeValue("ProductID", id.ToString()).First();
        }

        public List<Medicine> GetAll()
        {
            try
            {
                List<Medicine> result = new List<Medicine>();
                List<NameValueCollection> undecodedResultSet = new List<NameValueCollection>();
                using (SQLiteConnection connection = new SQLiteConnection(DAOConnecter.GetConnectionString()))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = $"SELECT *\r\nFROM {tableName}";
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
                    result.Add(MapDBRowToItemFields(row));
                }
                return result;
            }
            catch (SQLiteException)
            {
                throw new DAOException();
            }
        }
        private List<Medicine> FindByPart(string attributeName, string value)
        {
            try
            {
                List<Medicine> result = new List<Medicine>();
                List<NameValueCollection> undecodedResultSet = new List<NameValueCollection>();
                using (SQLiteConnection connection = new SQLiteConnection(DAOConnecter.GetConnectionString()))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = $"SELECT *\r\nFROM {tableName}\r\nWHERE {attributeName} like \"{value}%\"";
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
                    result.Add(MapDBRowToItemFields(row));
                }
                return result;
            }
            catch (SQLiteException)
            {
                throw new DAOException();
            }
        }

        protected override Medicine MapDBRowToItemFields(NameValueCollection row)
        {
            int medicineID = int.Parse(row["ProductID"]);
            string medicineName = row["ProductName"];
            string medicineCompanyName = row["CompanyName"];
            string medicineActiveSubstance = row["ActiveSubstance"];
            return new Medicine(medicineID, medicineName, medicineCompanyName, medicineActiveSubstance);
        }

        public List<Medicine> FindInIDRange(int startRange, int endRange)
        {   
            try
            {
                string commandText = $"SELECT ProductID, ProductName, CompanyName, ActiveSubstance\r\n" +
                $"FROM {tableName}\r\n" +
                $"WHERE ProductID > {startRange - 1} AND ProductID < {endRange + 1}";

                List<Medicine> result = new List<Medicine>();
                List<NameValueCollection> undecodedResultSet = new List<NameValueCollection>();
                using (SQLiteConnection connection = new SQLiteConnection(DAOConnecter.GetConnectionString()))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = commandText;
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
                    result.Add(MapDBRowToItemFields(row));
                }
                return result;
            }
            catch(SQLiteException)
            {
                throw new DAOException("Wrong boundaries");
            }
            
        }
        public bool AddNewMedicine(Medicine m)
        {
           try
           {
                if (isAlreadyInAssortment(m) == true)
                throw new DAOException("Identical medicine is already in assortment");

                using (SQLiteConnection conn = new SQLiteConnection(DAOConnecter.GetConnectionString()))
                {
                    conn.Open();
                    using(SQLiteCommand cmd = conn.CreateCommand()) 
                    {
                        cmd.CommandText = $"INSERT INTO \"{tableName}\" VALUES({m.GetID()},\"{m.GetName()}\"," +
                            $"\"{m.GetCompanyName()}\",\"{m.GetActiveSubstance()}\")";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    conn.Close();
                }
                return true;
           }
           catch(SQLiteException e)
           {
                throw new DAOException(e.Message);
           }
        }
        private bool isAlreadyInAssortment(Medicine medicine)
        {
            try
            {
                string commandText = $"SELECT *" +
                $"FROM {tableName}\r\n" +
                $"WHERE ProductName = \"{medicine.GetName()}\" AND CompanyName = \"{medicine.GetCompanyName()}\" AND Activesubstance = \"{medicine.GetActiveSubstance()}\"";

                List<Medicine> result = new List<Medicine>();
                List<NameValueCollection> undecodedResultSet = new List<NameValueCollection>();
                using (SQLiteConnection connection = new SQLiteConnection(DAOConnecter.GetConnectionString()))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = commandText;
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
                if(undecodedResultSet.Count > 0) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                throw new DAOException(e.Message);
            }
        }

        public List<Medicine> FindByPartName(string name)
        {
            return FindByPart("ProductName", name);
        }

        public List<Medicine> FindByPartCompanyName(string name)
        {
            return FindByPart("CompanyName", name);
        }

        public int GetLastID()
        {
            try
            {
                string commandText = $"SELECT ProductID " +
                $"FROM {tableName} ORDER BY ProductID DESC";
                int result;
                using (SQLiteConnection connection = new SQLiteConnection(DAOConnecter.GetConnectionString()))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = commandText;
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            bool b = reader.Read();
                            if (b == false)
                            {
                                result = 0;
                            }
                            else
                            {
                                result = MapDBRowToItemFields(reader.GetValues()).GetID();
                            }
                            
                        }
                    }
                    connection.Close();
                }
                return result;
            }
            catch (SQLiteException e)
            {
                throw new DAOException(e.Message);
            }
        }
    }
}
