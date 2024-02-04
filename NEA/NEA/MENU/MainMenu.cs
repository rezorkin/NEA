using NEA.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal class MainMenu : IHasOptions
    {
        public int pageLength { get; private set; }
        public int roundingLength { get; private set; }
        public ConsoleColor defaultFontColour { get; private set; }

        private DatabaseSettings DBsettings;
        private Settings settings;

        public MainMenu(int pageLength, int roundingLength, ConsoleColor defaultFontColour)
        {
            this.pageLength = pageLength;
            this.roundingLength = roundingLength;
            this.defaultFontColour = defaultFontColour;
            DBsettings = new DatabaseSettings();
            settings = new Settings();
        }

        public void PrintOptions()
        {
            Console.WriteLine("Press 1 to open assortment of medicines");
            Console.WriteLine("Press 2 to write new records");
            Console.WriteLine("Press 3 to open database settings");
            Console.WriteLine("Press 4 to open setting");
            Console.WriteLine("Press 5 to Exit");
        }
        public void OpenAssortmentOfMedicine()
        {
            AssortmentTable table = new AssortmentTable(pageLength, defaultFontColour);
            var furtherAction = MenuAction.Default;
            while (furtherAction != MenuAction.GoToMainMenu)
            {
                try
                {
                    Console.Clear();
                    table.OutputTable();
                    furtherAction = ReceiveFurtherAction();
                    if (furtherAction == MenuAction.GoToNextPage)
                    {
                        table.GoToNextPage();
                    }
                    else if (furtherAction == MenuAction.GoToPreviousPage)
                    {
                        table.GoToPreviousPage();
                    }
                    else if (furtherAction == MenuAction.Select)
                    {
                        table.Select();
                    }
                    else if (furtherAction == MenuAction.Sort)
                    {
                        table.SortRows();
                    }
                    else if (furtherAction == MenuAction.Filter)
                    {
                        table.FilterRows();
                    }
                    else if (furtherAction == MenuAction.ResetFiltersAndSortings)
                    {
                        table.ResetFiltersAndSorts();
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
                catch (MenuException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }
        }
        public void OpenRecordTable()
        {
            if(DBsettings.canWriteNewRecords == false)
            {
                throw new MenuException("Can't write new records to the practice database");
            }
            RecordTable table = new RecordTable(pageLength, defaultFontColour);
            var furtherAction = MenuAction.Default;
            while (furtherAction != MenuAction.GoToMainMenu)
            {
                try
                {
                    Console.Clear();
                    table.OutputTable();
                    furtherAction = ReceiveFurtherAction();
                    if (furtherAction == MenuAction.GoToNextPage)
                    {
                        table.GoToNextPage();
                    }
                    else if (furtherAction == MenuAction.GoToPreviousPage)
                    {
                        table.GoToPreviousPage();
                    }
                    else if (furtherAction == MenuAction.Select)
                    {
                        table.Select();
                    }
                    else if (furtherAction == MenuAction.Sort)
                    {
                        table.SortRows();
                    }
                    else if (furtherAction == MenuAction.Filter)
                    {
                        table.FilterRows();
                    }
                    else if (furtherAction == MenuAction.ResetFiltersAndSortings)
                    {
                        table.ResetFiltersAndSorts();
                    }
                    else if (furtherAction == MenuAction.ResetSelection)
                    {
                        table.ResetSelection();
                    }
                    else if (furtherAction == MenuAction.AddNewMedicineRecord)
                    {
                        table.AddMedicineToAssortment();
                    }
                }
                catch (MenuException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }

        }
        public void OpenAnalysisTable(List<Medicine> sample)
        {
            AnalysisTable table = new AnalysisTable(pageLength, defaultFontColour, sample, roundingLength);
            var furtherAction = MenuAction.Default;
            while (furtherAction != MenuAction.GoToAssortmentTable)
            {
                try
                {
                    Console.Clear();
                    table.OutputTable();
                    furtherAction = ReceiveFurtherAction();
                    Console.WriteLine();
                    if (furtherAction == MenuAction.GoToNextPage)
                    {
                        table.GoToNextPage();
                    }
                    else if (furtherAction == MenuAction.GoToPreviousPage)
                    {
                        table.GoToPreviousPage();
                    }
                    else if (furtherAction == MenuAction.Select)
                    {
                        table.Select();
                    }
                    else if (furtherAction == MenuAction.Sort)
                    {
                        table.SortRows();
                    }
                    else if (furtherAction == MenuAction.Filter)
                    {
                        table.ApplyDateBoundaries();
                    }
                    else if (furtherAction == MenuAction.ResetFiltersAndSortings)
                    {
                        table.ResetFiltersAndSorts();
                    }
                }
                catch (MenuException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }
        }

        static MenuAction ReceiveFurtherAction()
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
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

                case ConsoleKey.B: return MenuAction.AddNewMedicineRecord;

                default: return MenuAction.Default;
            }
        }
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
            ResetSelection,
            AddNewMedicineRecord
        }
        public void OpenDBSettings()
        {
            ConsoleKey key = new ConsoleKey();
            while (key != ConsoleKey.D3)
            {
                Console.Clear();
                DBsettings.PrintOptions();
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.D1)
                {
                    try
                    {
                        DBsettings.ConnectToPracticeDB();
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
                        DBsettings.ConnectCreateLocalDB();
                    }
                    catch (MenuException e)
                    {
                        Console.WriteLine(e.Message + ". Try again");
                        Console.ReadKey();
                    }
                }
            }
        }
        public void SetMenuTable()
        {
            ConsoleKey key = new ConsoleKey();
            while (key != ConsoleKey.D4)
            {
                try
                {
                    Console.Clear();
                    settings.PrintOptions();
                    key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.D1)
                    {
                        roundingLength = settings.GetRoundLength();
                    }
                    else if (key == ConsoleKey.D2)
                    {
                        pageLength = settings.ChangeItemsPerPage();
                    }
                    else if (key == ConsoleKey.D3)
                    {
                        defaultFontColour = settings.ChangeTheme();
                    }
                }
                catch (MenuException e)
                {
                    Console.WriteLine(e.Message + ". Try again");
                    Console.ReadKey();
                }
            }
        }
    }
}
