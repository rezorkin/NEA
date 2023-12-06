using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Domain;

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
            MedicineTable table = new MedicineTable(2);
            var furtherAction = MenuAction.Default;
            while (furtherAction != MenuAction.GoToAnalysisTable) 
            {
                Console.Clear();
                table.OutputPage();
                table.MakeChoice();
                furtherAction = RecieveFurtherAction();
                if(furtherAction == MenuAction.GoToNextPage || furtherAction == MenuAction.GoToPreviousPage)
                {
                    table.GoToAnotherPage(furtherAction);
                }
                else if(furtherAction == MenuAction.ViewAllCommands)
                {
                    ViewAllCommandsChoice(table);
                }
                else if(furtherAction == MenuAction.SortCommand)
                {
                    
                    
                    Console.WriteLine();
                    Console.WriteLine("Enter sort command:");
                    
                    try
                    {   
                      SortChoice(table);
                    }
                    catch (MenuException)
                    {
                   
                      Console.WriteLine("Entered command is invalid, try again");
                      Console.ReadKey();
                         
                    }
                    
                        
                }
                else if(furtherAction == MenuAction.SearchCommand)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter search command");
                }
            }
            

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
            else if (key == ConsoleKey.V)
            {
                return MenuAction.ViewAllCommands;
            }
            else if (key == ConsoleKey.A)
            {
                return MenuAction.GoToAnalysisTable;
            }
            else if (key == ConsoleKey.S)
            {
                return MenuAction.SortCommand;
            }
            else if (key == ConsoleKey.M)
            {
                return MenuAction.SearchCommand;
            } 
            else
                return MenuAction.Default;
        }
        private static void SortChoice(MedicineTable table)
        {
            string command = Console.ReadLine();
            table.SortItems(command);
        }
        private static void ViewAllCommandsChoice(MedicineTable table)
        {
            var key = ConsoleKey.V;
            table.ViewAllCommands();
            bool IsAlreadySortOnScreen = false;
            bool IsAlreadySearchOnScreen = false;
            while (key == ConsoleKey.V || key == ConsoleKey.S || key == ConsoleKey.M)
            {
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.S && IsAlreadySortOnScreen != true)
                {
                    table.SortCommandExplanation();
                    IsAlreadySortOnScreen = true;
                }
                else if (key == ConsoleKey.M && IsAlreadySearchOnScreen != true)
                {
                    table.SearchCommandExplanation();
                    IsAlreadySearchOnScreen = true;
                }
            }
        }
        private static void OpenAnalysisTable(List<Medicine> sample)
        {
            AnalysisTable table = new AnalysisTable(2,sample);

        }
    }
}
