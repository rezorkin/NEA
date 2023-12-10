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

            OpenAssortmentOfMedicine();
            Console.ReadKey();

        }
        private static void OpenAssortmentOfMedicine()
        {
            MedicineTable table = new MedicineTable(4, 28);
            var furtherAction = MenuAction.Default;
            while (furtherAction != MenuAction.GoToTheMainMenu) 
            {
                Console.Clear();
                table.OutputPage();
                table.PrintOptions();
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
                        OpenAnalysisTable(sample);
                    }
                }
            }
        }
        private static void OpenAnalysisTable(List<Medicine> sample)
        {
            AnalysisTable table = new AnalysisTable(10, 9, sample);
            var furtherAction = MenuAction.Default;
            while (furtherAction != MenuAction.GoToTheAssortmentTable)
            {
                Console.Clear();
                table.OutputPage();
                table.PrintOptions();
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
                else if (furtherAction == MenuAction.ResetFiltersAndSortings)
                {
                    table.ResetToInitialTable();
                }
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
                return MenuAction.GoToTheAssortmentTable;
            }
            else if(key == ConsoleKey.V)
            {
                return MenuAction.ResetSelection;
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
            throw new MenuException();
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
                throw new MenuException();
        }
    }
}
