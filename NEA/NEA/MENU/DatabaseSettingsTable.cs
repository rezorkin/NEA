using NEA.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal class DatabaseSettingsTable : SettingsTable
    {
        DatabaseFinder finder;
        public DatabaseSettingsTable(MainMenuTable menu, ConsoleColor defaultFontColour) : base(menu, defaultFontColour) 
        {
            finder = new DatabaseFinder();
        }
        public override void OutputPage()
        {
            PrintOptions();
        }
        protected override void PrintOptions()
        {
            Console.WriteLine("Press 1 to connect to the practice database");
            Console.WriteLine("Press 2 to connect to the local database");
            Console.WriteLine("Press 3 to get back to the main menu");
        }
        public void ConnectToPracticeDB()
        {
            finder.RunPracticeDB();
            menuTable = new MainMenuTable(finder.IsConnectedToPracticeDB(), menuTable.numberOfItemsPerPage, menuTable.numberOfItemsPerPage, menuTable.defaultFontColour);
            Console.WriteLine();
            Console.WriteLine("Successfully connected to practice database. Press any key to move on");
            Console.ReadKey();
        }
        public void ConnectCreateLocalDB()
        {
            Console.WriteLine();
            try 
            {
                finder.RunLocalDB();
                menuTable = new MainMenuTable(finder.IsConnectedToPracticeDB(), menuTable.numberOfItemsPerPage, menuTable.numberOfItemsPerPage, menuTable.defaultFontColour);
                Console.WriteLine("Successfully connected to local database. Press any key to move on");
                Console.ReadKey();
            }
            catch(DomainException e) 
            {
                Console.WriteLine(e.Message + " Press 'C' to create local database instead.");
                ConsoleKey key = Console.ReadKey(true).Key;
                if(key == ConsoleKey.C) 
                {
                    finder.CreateLocalDB();
                    menuTable = new MainMenuTable(finder.IsConnectedToPracticeDB(), menuTable.numberOfItemsPerPage, menuTable.numberOfItemsPerPage, menuTable.defaultFontColour);
                    Console.WriteLine("Successfully created local database. Press any key to move on");
                    Console.ReadKey();
                }
                else
                {
                    throw new MenuException("Local database was not created. Currently connected to practice database.");
                }

            }
        }

    }
}
