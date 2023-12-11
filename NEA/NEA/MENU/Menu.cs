using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DOMAIN;

namespace NEA.MENU
{
    static internal class Menu
    {

        public static void Start()
        {
            MainMenuTable table = new MainMenuTable(ConsoleColor.White);
            var furtherAction = MenuAction.Default;
            while(furtherAction != MenuAction.Exit)
            {
                Console.Clear();
                table.OutputPage();
                furtherAction = ReceiveInitialAction();
                if(furtherAction == MenuAction.GoToAssortmentTable)
                {
                    OpenAssortmentOfMedicine(table.numberOfItemsPerPage, table.defaultFontColour, table.roundingLength);
                }
                else if(furtherAction == MenuAction.GoToSettings)
                {
                    table = OpenSettings(table);
                }
                else if(furtherAction == MenuAction.GoToDataBaseSettings)
                {
                    table = OpenDBSettings(table);
                }

            }
            Console.ReadKey();

        }
        private static MainMenuTable OpenDBSettings(MainMenuTable menu)
        {
            DatabaseSettingsTable table = new DatabaseSettingsTable(menu, menu.defaultFontColour);
            ConsoleKey key = new ConsoleKey();
            while (key != ConsoleKey.D3)
            {
                Console.Clear();
                table.OutputPage();
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
            return table.GetSettedMenu;
        }
        private static MainMenuTable OpenSettings(MainMenuTable menu)
        {
            ProgramSettingsTable table = new ProgramSettingsTable(menu, menu.defaultFontColour);
            ConsoleKey key = new ConsoleKey();
            while (key != ConsoleKey.D4)
            {
                Console.Clear();
                table.OutputPage();
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
            return table.GetSettedMenu;
        }
        private static void OpenAssortmentOfMedicine(int pageLength, ConsoleColor defaultFontColour, int roundingLength)
        {
            AssortmentTable table = new AssortmentTable(pageLength, 28, defaultFontColour);
            var furtherAction = MenuAction.Default;
            while (furtherAction != MenuAction.GoToMainMenu) 
            {
                Console.Clear();
                table.OutputPage();
                furtherAction = ReceiveFurtherAction();
                if(furtherAction == MenuAction.GoToNextPage || furtherAction == MenuAction.GoToPreviousPage)
                {
                    table.GoToAnotherPage(furtherAction);
                }
                else if(furtherAction == MenuAction.Select)
                {
                    try
                    {
                        table.Select();
                    }
                    catch (MenuException ex) 
                    {
                        Console.WriteLine();
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
                else if(furtherAction == MenuAction.Sort)
                {
                    
                    Console.WriteLine();
                    try
                    {
                        ConsoleKey[] acceptedKeys = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4 };
                        string[] attributes = table.GetAttributes();
                        string chosenAttribute = ReceiveAttributeFromUser(attributes, acceptedKeys);
                        Order order = ReceiveSortOrderFromUser();
                        table.SortRows(chosenAttribute, order);
                    }
                    catch (MenuException)
                    {
                      Console.WriteLine();
                      Console.WriteLine("Pressed key is invalid, try again");
                      Console.ReadKey();
                         
                    }
                }
                else if(furtherAction == MenuAction.Filter)
                {
                    Console.WriteLine();
                    try
                    {
                        ConsoleKey[] acceptedKeys = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4 };
                        string[] attributes = table.GetAttributes();
                        string chosenAttribute = ReceiveAttributeFromUser(attributes, acceptedKeys);
                        table.FilterRows(chosenAttribute);
                    }
                    catch (MenuException e)
                    {

                        Console.WriteLine(e.Message + "Try again");
                        Console.ReadKey();

                    }
                }
                else if(furtherAction == MenuAction.ResetFiltersAndSortings)
                {
                    table.ResetToInitialTable();
                }
                else if(furtherAction == MenuAction.ResetSelection)
                {
                    table.ResetSelection();
                }
                else if(furtherAction == MenuAction.GoToAnalysisTable)
                {
                    List<Medicine> sample = table.GetSample();
                    if(sample.Count > 0)
                    {
                        OpenAnalysisTable(sample, pageLength, defaultFontColour, roundingLength);
                    }
                }
            }
        }
        private static void OpenAnalysisTable(List<Medicine> sample, int pageLength, ConsoleColor defaultConsoleColour, int roundingLength)
        {
            AnalysisTable table = new AnalysisTable(pageLength, 9, defaultConsoleColour,sample, roundingLength);
            var furtherAction = MenuAction.Default;
            while (furtherAction != MenuAction.GoToAssortmentTable)
            {
                Console.Clear();
                table.OutputPage();
                furtherAction = ReceiveFurtherAction();
                if (furtherAction == MenuAction.GoToNextPage || furtherAction == MenuAction.GoToPreviousPage)
                {
                    table.GoToAnotherPage(furtherAction);
                }
                else if (furtherAction == MenuAction.Select)
                {
                    try
                    {
                        table.Select();
                    }
                    catch (MenuException ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
                else if (furtherAction == MenuAction.Sort)
                {

                    Console.WriteLine();
                    try
                    {
                        ConsoleKey[] acceptedKeys = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6 };
                        Console.WriteLine();
                        string[] attributes = table.GetAttributes();
                        string chosenAttribute = ReceiveAttributeFromUser(attributes, acceptedKeys);
                        Order order = ReceiveSortOrderFromUser();
                        table.SortRows(chosenAttribute, order);
                    }
                    catch (MenuException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Pressed key is invalid, try again");
                        Console.ReadKey();

                    }
                }
                else if (furtherAction == MenuAction.Filter)
                {
                    Console.WriteLine();
                    try
                    {
                        table.ApplyDateBoundaries();
                    }
                    catch (MenuException e)
                    {

                        Console.WriteLine(e.Message);
                        Console.ReadKey();

                    }
                }
                else if (furtherAction == MenuAction.ResetFiltersAndSortings)
                {
                    table.ResetToInitialTable();
                }
            }
        }
        private static MenuAction ReceiveInitialAction()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.D1)
            {
                return MenuAction.GoToAssortmentTable;
            }
            else if (key == ConsoleKey.D2)
            {
                return MenuAction.GoToDataBaseSettings;
            }
            else if (key == ConsoleKey.D3)
            {
                return MenuAction.GoToSettings;
            }
            else if (key == ConsoleKey.D4)
            {
                return MenuAction.Exit;
            }
            else 
            {
                return MenuAction.Default;
            }
        }
        private static MenuAction ReceiveFurtherAction()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.RightArrow)
            {
                return MenuAction.GoToNextPage;
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                return MenuAction.GoToPreviousPage;
            }
            else if (key == ConsoleKey.A)
            {
                return MenuAction.GoToAnalysisTable;
            }
            else if (key == ConsoleKey.S)
            {
                return MenuAction.Sort;
            }
            else if (key == ConsoleKey.F)
            {
                return MenuAction.Filter;
            }
            else if(key == ConsoleKey.X)
            {
                return MenuAction.ResetFiltersAndSortings;
            }
            else if(key == ConsoleKey.P)
            {
                return MenuAction.Select;
            }
            else if(key == ConsoleKey.Backspace)
            {
                return MenuAction.GoToAssortmentTable;
            }
            else if(key == ConsoleKey.V)
            {
                return MenuAction.ResetSelection;
            }
            else if(key == ConsoleKey.E)
            {
                return MenuAction.GoToMainMenu;
            }
            else
                return MenuAction.Default;
        }
        private static string ReceiveAttributeFromUser(string[] attributes, ConsoleKey[] acceptedKeys)
        {
            Console.WriteLine();
            Console.WriteLine("Choose attribute by pressing:");
            for (int i = 0; i < attributes.Length; i++)
            {
                Console.Write($"'{i + 1}' for {attributes[i]} ");
            }
            var pressedKey = Console.ReadKey(true).Key;
            for (int i = 0; i < acceptedKeys.Length; i++)
            {
                if (pressedKey == acceptedKeys[i])
                {
                    return attributes[i];
                }
            }
            throw new MenuException("Wrong key was pressed");
        }
        private static Order ReceiveSortOrderFromUser()
        {
            Console.WriteLine("Press 'A' for ascending order and 'D' for descending");
            var pressedKey = Console.ReadKey(true).Key;
            if (pressedKey == ConsoleKey.A)
            {
                return Order.ASC;
            }
            else if (pressedKey == ConsoleKey.D)
            {
                return Order.DESC;
            }
            else
                throw new MenuException("Wrong key was pressed");
        }
    }
}
