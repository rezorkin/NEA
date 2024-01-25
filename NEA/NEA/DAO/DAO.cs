using NEA.DOMAIN;
using NEA.MENU;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NEA.DAO
{
    internal abstract class DAO<T> 
    {
        protected abstract T MapDBRowToItemFields(NameValueCollection row);

        protected List<T> FindByAttributeValue(string tableName, string attributeName, string attributeValue)
        {
            try
            {
                List<T> result = new List<T>();
                List<NameValueCollection> undecodedResultSet = GetMatchedRows(tableName, attributeName, attributeValue);
                foreach (NameValueCollection row in undecodedResultSet)
                {
                    result.Add(MapDBRowToItemFields(row));
                }
                return result;
            }
            catch(SQLiteException) 
            {
                throw new DAOException("Invalid id value");
            }
        }
   
        private List<NameValueCollection> GetMatchedRows(string table, string attributeName, string value)
        {
            List<NameValueCollection> result = new List<NameValueCollection>();

            using (SQLiteConnection connection = new SQLiteConnection(DAOConnecter.GetConnectionString()))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = $"SELECT *\r\nFROM {table}\r\nWHERE {attributeName} like \"{value} %\" or {attributeName} like \"% {value} %\" or {attributeName} like \"% {value}\" or {attributeName} like \"{value}\"";
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
