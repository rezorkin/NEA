using NEA.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class DatabaseFinder
    {
        private bool isConnectedToPracticeDB;
        public DatabaseFinder() 
        {
            isConnectedToPracticeDB = true;
        }

        public void RunPracticeDB()
        {
            DAOConnecter.ConnectToPracticeDB();
            isConnectedToPracticeDB = true;
        }
        public void RunLocalDB()
        {
            try
            {
                DAOConnecter.ConnectToLocalDB();
                isConnectedToPracticeDB=false;
            }
            catch(DAOException) 
            {
                throw new DomainException("Local database was not found.");
            }
        }
        public void CreateLocalDB()
        {
            try
            {
                DAOConnecter.CreateDB();
                isConnectedToPracticeDB=false;
            }
            catch (DAOException)
            {
                throw new DomainException();
            }
        }
        public bool IsConnectedToPracticeDB()
        {
            return isConnectedToPracticeDB;
        }
    }
}
