using NEA.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal class MainMenu : IPrintable
    {
        private enum MenuAction
        {
            Default,
            Select,
            GoToNextPage,
            GoToPreviousPage,
            GoToAnalysisTable,
            Sort,
            Filter,
            ResetFiltersAndSortings,
            GoToMainMenu,
            GoToAssortmentTable,
            ResetSelection
        }
        public int pageLength { get; set; }
        public int roundingLength { get; set; }
        public ConsoleColor defaultFontColour { get; set; }
        public MainMenu()
        {
            pageLength = 5;
            roundingLength = 1;
        }

        public void PrintOptions()
        {
            Console.WriteLine("Press 1 to open assortment of medicines");
            Console.WriteLine("Press 2 to open database settings");
            Console.WriteLine("Press 3 to open setting");
            Console.WriteLine("Press 4 to Exit");
        }
        public void OpenAssortmentOfMedicine()
        {
            AssortmentTable table = new AssortmentTable(pageLength, defaultFontColour);
            var furtherAction = MenuAction.Default;
            while (furtherAction != MenuAction.GoToMainMenu)
            {
                Console.Clear();
                table.OutputTable();
                furtherAction = ReceiveFurtherAction();
                if (furtherAction == MenuAction.GoToNextPage)
                {
                    table.GoToNextPage();
                }
                else if(furtherAction == MenuAction.GoToPreviousPage)
                {
                    table.GoToPreviousPage();
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
                        ConsoleKey[] acceptedKeys = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4 };
                        string[] attributes = table.GetAttributes();
                        string chosenAttribute = ReceiveAttributeFromUser(attributes, acceptedKeys);
                        OrderBy order = ReceiveSortOrderFromUser();
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
                else if (furtherAction == MenuAction.ResetSelection)
                {
                    table.ResetSelection();
                }
                else if (furtherAction == MenuAction.GoToAnalysisTable)
                {
                    List<Medicine> sample = table.GetSample();
                    if (sample.Count > 0)
                    {
                        OpenAnalysisTable(sample);
                    }
                }
            }
        }
        public void OpenAnalysisTable(List<Medicine> sample)
        {
            AnalysisTable table = new AnalysisTable(pageLength, defaultFontColour, sample, roundingLength);
            var furtherAction = MenuAction.Default;
            while (furtherAction != MenuAction.GoToAssortmentTable)
            {
                Console.Clear();
                table.OutputTable();
                furtherAction = ReceiveFurtherAction();
                if (furtherAction == MenuAction.GoToNextPage)
                {
                    table.GoToNextPage();
                }
                else if(furtherAction == MenuAction.GoToPreviousPage)
                {
                    table.GoToPreviousPage();
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
                        OrderBy order = ReceiveSortOrderFromUser();
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
        private string ReceiveAttributeFromUser(string[] attributes, ConsoleKey[] acceptedKeys)
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
        private OrderBy ReceiveSortOrderFromUser()
        {
            Console.WriteLine("Press 'A' for ascending order and 'D' for descending");
            var pressedKey = Console.ReadKey(true).Key;
            if (pressedKey == ConsoleKey.A)
            {
                return OrderBy.ASC;
            }
            else if (pressedKey == ConsoleKey.D)
            {
                return OrderBy.DESC;
            }
            else
                throw new MenuException("Wrong key was pressed");
        }
        private MenuAction ReceiveFurtherAction()
        {
            var key = Console.ReadKey(true).Key;
            switch(key)
            {
                case ConsoleKey.A: return MenuAction.GoToAnalysisTable;

                case ConsoleKey.S: return MenuAction.Sort;

                case ConsoleKey.F: return MenuAction.Filter;

                case ConsoleKey.X: return MenuAction.ResetFiltersAndSortings;

                case ConsoleKey.P: return MenuAction.Select;

                case ConsoleKey.Backspace: return MenuAction.GoToAssortmentTable;

                case ConsoleKey.V: return MenuAction.ResetSelection;

                case ConsoleKey.E: return MenuAction.GoToMainMenu;

                case ConsoleKey.RightArrow: return MenuAction.GoToNextPage;

                case ConsoleKey.LeftArrow: return MenuAction.GoToPreviousPage;

                default: return MenuAction.Default;
            }
        }
    }
}
