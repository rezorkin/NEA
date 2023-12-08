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
            MedicineTable table = new MedicineTable(4);
            var furtherAction = MenuAction.Default;
            while (furtherAction != MenuAction.GoToTheMainMenu) 
            {
                Console.Clear();
                table.OutputPage();
                table.MakeChoice();
                furtherAction = RecieveFurtherAction();
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
                      table.SortRows();
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
                        table.FilterRows();
                    }
                    catch (MenuException e)
                    {

                        Console.WriteLine(e.Message + "Try again");
                        Console.ReadKey();

                    }
                }
                else if(furtherAction == MenuAction.ResetFiltersAndSortings)
                {
                    table.ResetFiltersAndSortings();
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
            AnalysisTable table = new AnalysisTable(10, sample);
        }
        private static MenuAction RecieveFurtherAction()
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
            else if(key == ConsoleKey.A)
            {
                return MenuAction.GoToAnalysisTable;
            }
            else
                return MenuAction.Default;
        }
    }
}
