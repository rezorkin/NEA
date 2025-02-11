﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.DOMAIN
{
    internal class DomainException : Exception
    {
        public DomainException() : base() { }
        public DomainException(string message, Exception cause) : base(message, cause) { }
        public DomainException(string message) : base(message) { }
    }
}
