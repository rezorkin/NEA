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
            while (furtherAction == MenuAction.Default) 
            {
                table.OutputPage();
                furtherAction = table.MakeChoice();
                Console.Clear();
                if(furtherAction == MenuAction.GoToNextPage || furtherAction == MenuAction.GoToPreviousPage)
                {
                    table.GoToAnotherPage(furtherAction);
                    furtherAction = MenuAction.Default;
                }
                else if(furtherAction == MenuAction.ViewAllCommands)
                {
                    table.OutputPage();
                    table.ViewAllCommands();
                    furtherAction = MenuAction.Default;
                }
            }
            

        }
        private static void OpenAnalysisTable(List<Medicine> sample)
        {
            AnalysisTable table = new AnalysisTable(2,sample);

        }
    }
}
