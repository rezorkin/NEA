using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal enum MenuAction
    {
        Default,
        Select,
        GoToNextPage,
        GoToPreviousPage,
        GoToAnalysisTable,
        ViewAllCommands,
        Sort,
        Filter,
        ResetFiltersAndSortings,
        GoToMainMenu,
        GoToAssortmentTable,
        ResetSelection,
        GoToDataBaseSettings,
        GoToSettings,
        Exit
    }
}
