using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal class MainMenuTable : Table
    {
        public int numberOfItemsPerPage { get;}
        public int roundingLength { get; }
        public MainMenuTable(ConsoleColor defaultFontColour) : base(defaultFontColour)
        {
            numberOfItemsPerPage = 5;
            roundingLength = 1;
        }
        public MainMenuTable(int numberOfItemsPerPage, int numberOfDecimalPointsToRound, ConsoleColor defaaultFontColour): base(defaaultFontColour)
        {
            this.numberOfItemsPerPage = numberOfItemsPerPage;
            this.roundingLength = numberOfDecimalPointsToRound;
        }

        protected override void PrintOptions()
        {
            Console.WriteLine("Press 1 to open assortment of medicines");
            Console.WriteLine("Press 2 to open database settings");
            Console.WriteLine("Press 3 to open setting");
            Console.WriteLine("Press 4 to Exit");
        }
        public override void OutputPage()
        {
            PrintOptions();
        }
    }
}
