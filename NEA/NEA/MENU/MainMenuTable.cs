using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal class MainMenuTable : Table
    {
        public bool isConnectedToPracticeDatabase { get; }
        public int numberOfItemsPerPage { get;}
        public int roundingLength { get; }
        public MainMenuTable(ConsoleColor defaultFontColour) : base(defaultFontColour)
        {
            isConnectedToPracticeDatabase = true;
            numberOfItemsPerPage = 5;
            roundingLength = 1;
        }
        public MainMenuTable(bool isConnectedToPractiseDatabase, int numberOfItemsPerPage, int numberOfDecimalPointsToRound, ConsoleColor defaaultFontColour): base(defaaultFontColour)
        {
            this.isConnectedToPracticeDatabase = isConnectedToPractiseDatabase;
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
            string database = "";
            if(isConnectedToPracticeDatabase == true) 
            {
                database = "practice database";
            }
            else
            {
                database = "local database";
            }
            Console.WriteLine("Currently connected to: " + database);
        }
    }
}
