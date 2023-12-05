using System;
using System.Collections.Generic;
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
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.RightArrow)
                {
                    furtherAction = MenuAction.GoToNextPage;
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    furtherAction = MenuAction.GoToPreviousPage;
                }
                else if (key == ConsoleKey.V)
                {
                    furtherAction = MenuAction.ViewAllCommands;
                }
                if(furtherAction == MenuAction.GoToNextPage || furtherAction == MenuAction.GoToPreviousPage)
                {
                    table.GoToAnotherPage(furtherAction);
                }
                else if(furtherAction == MenuAction.ViewAllCommands)
                {
                    table.ViewAllCommands();
                    bool IsAlreadySortOnScreen = false;
                    bool IsAlreadySearchOnScreen = false;
                    while(key == ConsoleKey.V || key == ConsoleKey.S || key == ConsoleKey.M)
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
            }
            

        }
        private static void OpenAnalysisTable(List<Medicine> sample)
        {
            AnalysisTable table = new AnalysisTable(2,sample);

        }
    }
}
