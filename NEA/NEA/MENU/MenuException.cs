using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal class MenuException : Exception
    {
        public MenuException() : base() { }
        public MenuException(string message, Exception cause) : base(message, cause) { }
        public MenuException(string message) : base(message) { }
    }
}
