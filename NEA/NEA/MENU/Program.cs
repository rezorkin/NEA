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
        static MainMenu menu;
        static void Main(string[] args)
        {
            menu = new MainMenu(5, 1, ConsoleColor.White);
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
                else if (furtherAction == ProgramAction.GoToRecordTable)
                {
                    menu.OpenRecordTable();
                }
                else if (furtherAction == ProgramAction.GoToSettings)
                {
                    menu.SetMenuTable();
                }
                else if (furtherAction == ProgramAction.GoToDataBaseSettings)
                {
                    menu.OpenDBSettings();
                }
                /*try
                {
                    Console.Clear();
                    menu.PrintOptions();
                    furtherAction = ReceiveInitialAction();
                    if (furtherAction == ProgramAction.GoToAssortmentTable)
                    {
                        menu.OpenAssortmentOfMedicine();
                    }
                    else if (furtherAction == ProgramAction.GoToRecordTable)
                    {
                        menu.OpenRecordTable();
                    }
                    else if (furtherAction == ProgramAction.GoToSettings)
                    {
                        menu.SetMenuTable();
                    }
                    else if (furtherAction == ProgramAction.GoToDataBaseSettings)
                    {
                        menu.OpenDBSettings();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to move on");
                    Console.ReadKey();
                }*/
            }
            Console.ReadKey();
            
        }
       
        enum ProgramAction
        {
            GoToAssortmentTable,
            GoToRecordTable,
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
                return ProgramAction.GoToRecordTable;
            }
            else if (key == ConsoleKey.D3)
            {
                return ProgramAction.GoToDataBaseSettings;
            }
            else if (key == ConsoleKey.D4)
            {
                return ProgramAction.GoToSettings;
            }
            else if (key == ConsoleKey.D5)
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
