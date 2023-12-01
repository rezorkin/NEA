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
        public abstract List<string> ViewAllCommands();
        public abstract void MakeChoice();
        public abstract void Sort(string attribute, Order order);
        protected abstract void Select();
        public void OutputPage()
        {
            Console.Clear();
            pages[currentPageIndex].DisplayPage();
            MakeChoice();
        }
        public void GoToNextPage() 
        {
            if (currentPageIndex == pages.Count - 1)
            {
              OutputPage();
            }
            else 
            {
              currentPageIndex++;
              OutputPage();
            }
        }
        public void GoToPreviousPage() 
        {
            if(currentPageIndex == 0)
            {
                OutputPage();
            }
            else
            {
                currentPageIndex--;
                OutputPage();
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
