using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DOMAIN;

namespace NEA.MENU
{
    internal abstract class MedicineTable : Table
    {
        private List<Page> pages;
        private int currentPageIndex;
        private readonly int pageSize;
        private int spacesToDivider;
        protected abstract string[] attributes { get; }
        public MedicineTable(int pageLength, int spacesToDivider, ConsoleColor defaultFontColour) : base(defaultFontColour)
        {
            this.pageSize = pageLength;
            pages = new List<Page>();
            this.spacesToDivider = spacesToDivider;
        }
        protected abstract override void PrintOptions();
        public abstract void SortRows(string attribute, Order order);
        public abstract void Select();
        public abstract void ResetToInitialTable();

        public string[] GetAttributes()
        {
            return attributes;
        }
        protected void UpdatePages(List<string> newRows)
        {
            pages = new List<Page>();
            CutRowsToPages(newRows);
        }
        public override void OutputPage()
        { 
            pages[currentPageIndex].DisplayPage();
            Console.WriteLine();
            Console.Write($"Current page: {currentPageIndex+1}                                       Pages:");
            for (int i = 1; i <= pages.Count; i++)
            {
                if (i == currentPageIndex + 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write(i + " ");
                Console.ForegroundColor = defaultFontColour;
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
        private void CutRowsToPages(List<string> rows) 
        {
            int g = 0;
            string[] cutedSet = new string[pageSize];
            for(int i = 0; i < rows.Count; i++)
            {
                if(g == pageSize) 
                {
                    pages.Add(new Page(attributes, cutedSet, spacesToDivider));
                    g = 0;
                    cutedSet = new string[pageSize];
                }
                cutedSet[g] = rows[i].ToString();
                g++;
            }
            pages.Add(new Page(attributes, cutedSet, spacesToDivider));
        }
    }
}
