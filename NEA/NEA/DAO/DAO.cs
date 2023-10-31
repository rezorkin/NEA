using Prototype.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Prototype.DAO
{
    internal abstract class DAO<T> : IDAO<T> where T : class
    {
        private string path;
        private string connectionString;
        
        public DAO()
        {
            path = "PharmacyDB.db;";
            connectionString = "Data Source= " + path;
        }
        protected abstract T SetRetrievedValuesFromDBRow(NameValueCollection row);
        public abstract List<T> GetAll();
        public abstract int Insert(T entity);
        protected List<NameValueCollection> GetMatchedRows(string table, string attributeName, string value)
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
        protected List<NameValueCollection> GetMatchedRows(string table)
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
