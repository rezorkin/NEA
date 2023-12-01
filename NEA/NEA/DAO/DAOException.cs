using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DAO
{
    internal class DAOException : Exception
    {
        public DAOException() : base() { }
        public DAOException(string message, Exception cause) : base(message, cause) { }
        public DAOException (string message) : base(message) { }
    }
}
