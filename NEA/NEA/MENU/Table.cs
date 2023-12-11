using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal abstract class Table
    {
        public ConsoleColor defaultFontColour { get; }
        public Table(ConsoleColor defaultFontColour)
        {
            this.defaultFontColour = defaultFontColour;
        }
        protected abstract void PrintOptions();
        public abstract void OutputPage();
    }
}
