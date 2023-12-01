using NEA.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NEA.DAO
{
    internal abstract class DAO<T> 
    {
        private readonly string path;
        private readonly string connectionString;
        public DAO()
        {
            path = "PharmacyDB.db;";
            connectionString = "Data Source= " + path;
        }
        public DAO(string fileName)
        {
            path = fileName;
            connectionString = "Data Source= " + path;
        }
        protected abstract T SetValuesFromTableToObjectFields(NameValueCollection row);
        protected virtual List<T> FindAll(string tableName, string attributeName, string attributeValue)
        {
            List<T> result = new List<T>();
            List<NameValueCollection> undecodedResultSet = GetMatchedRows(tableName, attributeName, attributeValue);
            foreach (NameValueCollection row in undecodedResultSet)
            {
                result.Add(SetValuesFromTableToObjectFields(row));
            }
            return result;
        }
        protected virtual List<T> FindAll(string tableName)
        {
            List<T> result = new List<T>();
            List<NameValueCollection> undecodedResultSet = GetMatchedRows(tableName);
            foreach (NameValueCollection row in undecodedResultSet)
            {
                result.Add(SetValuesFromTableToObjectFields(row));
            }
            return result;
        }
        private List<NameValueCollection> GetMatchedRows(string table, string attributeName, string value)
        {
            List<NameValueCollection> result = new List<NameValueCollection>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString + "Version=3;New=False;Compress=True;Read Only=true"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = $"SELECT * FROM {table} WHERE {attributeName} =@value";
                    command.Parameters.AddWithValue("@value", value);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NameValueCollection currentRowValues = reader.GetValues();
                            result.Add(currentRowValues);
                        }
                    }
                }
                connection.Close();
            }

            return result;
        }
        private List<NameValueCollection> GetMatchedRows(string table)
        {
            List<NameValueCollection> result = new List<NameValueCollection>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString + "Version=3;New=False;Compress=True;Read Only=true"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = $"SELECT * FROM {table}";
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NameValueCollection currentRowValues = reader.GetValues();
                            result.Add(currentRowValues);
                        }
                    }
                }
                connection.Close();
            }

            return result;
        }
    }
}
