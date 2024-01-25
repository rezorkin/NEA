using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DOMAIN;

namespace NEA.MENU
{
    static internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu menu = new MainMenu();
            menu.defaultFontColour = ConsoleColor.White;
            var furtherAction = ProgramAction.Default;
            while (furtherAction != ProgramAction.Exit)
            {
                Console.Clear();
                menu.PrintOptions();
                furtherAction = ReceiveInitialAction();
                if (furtherAction == ProgramAction.GoToAssortmentTable)
                {
                    menu.OpenAssortmentOfMedicine();
                }
                else if (furtherAction == ProgramAction.GoToSettings)
                {
                    menu = SetMenuTable(menu);
                }
                else if (furtherAction == ProgramAction.GoToDataBaseSettings)
                {   
                    OpenDBSettings();
                }

            }
            Console.ReadKey();
        }
        private static void OpenDBSettings()
        {
            DatabaseSettings table = new DatabaseSettings();
            ConsoleKey key = new ConsoleKey();
            while (key != ConsoleKey.D3)
            {
                Console.Clear();
                table.PrintOptions();
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.D1)
                {
                    try
                    {
                        table.ConnectToPracticeDB();
                    }
                    catch (MenuException e)
                    {
                        Console.WriteLine(e.Message + ". Try again");
                        Console.ReadKey();
                    }
                }
                else if (key == ConsoleKey.D2)
                {
                    try
                    {
                        table.ConnectCreateLocalDB();
                    }
                    catch (MenuException e)
                    {
                        Console.WriteLine(e.Message + ". Try again");
                        Console.ReadKey();
                    }
                }
            }
        }
        private static MainMenu SetMenuTable(MainMenu menu)
        {
            ProgramSettings table = new ProgramSettings(menu);
            ConsoleKey key = new ConsoleKey();
            while (key != ConsoleKey.D4)
            {
                Console.Clear();
                table.PrintOptions();
                key = Console.ReadKey(true).Key;
                if(key == ConsoleKey.D1)
                {
                    try
                    {
                        table.ChangeRoundLength();
                    }
                    catch(MenuException e) 
                    {
                        Console.WriteLine(e.Message+ ". Try again");
                        Console.ReadKey();
                    }
                }
                else if(key == ConsoleKey.D2)
                {
                    try
                    {
                        table.ChangeItemsPerPage();
                    }
                    catch (MenuException e)
                    {
                        Console.WriteLine(e.Message + ". Try again");
                        Console.ReadKey();
                    }
                }
                else if (key == ConsoleKey.D3)
                {
                    try
                    {
                        table.ChangeTheme();
                    }
                    catch (MenuException e)
                    {
                        Console.WriteLine(e.Message + ". Try again");
                        Console.ReadKey();
                    }
                }
            }
            return table.menu;
        }

        private enum ProgramAction
        {
            GoToAssortmentTable,
            GoToDataBaseSettings,
            GoToSettings,
            Exit,
            Default

        }

        private static ProgramAction ReceiveInitialAction()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.D1)
            {
                return ProgramAction.GoToAssortmentTable;
            }
            else if (key == ConsoleKey.D2)
            {
                return ProgramAction.GoToDataBaseSettings;
            }
            else if (key == ConsoleKey.D3)
            {
                return ProgramAction.GoToSettings;
            }
            else if (key == ConsoleKey.D4)
            {
                return ProgramAction.Exit;
            }
            else 
            {
                return ProgramAction.Default;
            }
        }
       
        
    }
}
