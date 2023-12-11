using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal abstract class SettingsTable : Table
    {
        protected MainMenuTable menuTable;
        protected SettingsTable(MainMenuTable table, ConsoleColor defaultFontColour) : base(defaultFontColour)
        {
            menuTable = table;
        }
        public MainMenuTable GetSettedMenu { get { return menuTable; } }
    }
}
