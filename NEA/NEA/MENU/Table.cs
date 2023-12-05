using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal abstract class Table
    {
        private List<string> rowSet;
        private List<Page> pages;
        private int currentPageIndex;
        private readonly int pageLength;

        public Table(int pageLength)
        {
            this.rowSet = getRowSet();
            currentPageIndex = 0;
            this.pageLength = pageLength;
            pages = CutRowSet();

        }
        protected abstract List<string> getRowSet();
        protected abstract List<string> getAttributes();
        public abstract void ViewAllCommands();
        public abstract void MakeChoice();
        public abstract void Sort(string command);
        protected abstract void Select();
        public void OutputPage()
        {
            pages[currentPageIndex].DisplayPage();
            Console.Write($"Current page: {currentPageIndex+1}                                       Pages:");
            for(int i = 1; i <= pages.Count; i++) 
            {
                if(i == currentPageIndex + 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write(i + " ");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public void GoToAnotherPage(MenuAction direction)
        {
            if(direction == MenuAction.GoToPreviousPage)
            {
                GoToPreviousPage();
            }
            if(direction == MenuAction.GoToNextPage)
            { 
                GoToNextPage(); 
            }
        }
        public void GoToNextPage() 
        {
            if (currentPageIndex != pages.Count - 1)
            {
              currentPageIndex++;
            }
        }
        public void GoToPreviousPage() 
        {
            if(currentPageIndex != 0)
            {
                currentPageIndex--;
            }
        }
        private List<Page> CutRowSet() 
        {
            var result = new List<Page>();
            int g = 0;
            string[] cutedSet = new string[pageLength];
            for(int i = 0; i < rowSet.Count; i++)
            {
                if(g == pageLength) 
                {
                    result.Add(new Page(getAttributes(), cutedSet));
                    g = 0;
                    cutedSet = new string[pageLength];
                }
                cutedSet[g] = rowSet[i];
                g++;
            }
            result.Add(new Page(getAttributes(), cutedSet));
            return result;
        }
    }
}
