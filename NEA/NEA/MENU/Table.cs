using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DOMAIN;

namespace NEA.MENU
{
    internal abstract class Table<T> : IHasOptions
    {
        private List<Page> pages;
        private int currentPageIndex;
        private readonly int pageSize;
        protected List<T> items;
        protected ConsoleColor defaultFontColour { get; }
        protected abstract int spacesToDivider { get; }
        protected abstract Dictionary<ConsoleKey, string> attributesKeys { get; }
        public Table(int pageLength, ConsoleColor defaultFontColour) 
        {
            this.pageSize = pageLength;
            pages = new List<Page>();
            this.defaultFontColour = defaultFontColour;
            items = new List<T>();
            currentPageIndex = 0;
        }
        protected abstract List<T> GetItems();
        public abstract void PrintOptions();
        public abstract void SortRows();
        public abstract void Select();
        public abstract void ResetFiltersAndSorts();
        protected string ReceiveAttributeFromUser()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Choose attribute by pressing:");
                var keys = attributesKeys.Keys.ToList();
                for (int i = 0; i < attributesKeys.Count; i++)
                {
                    Console.Write($"'{i + 1}' for {attributesKeys[keys[i]]} ");
                }
                var pressedKey = Console.ReadKey(true).Key;
                return attributesKeys[pressedKey];
            }
            catch(Exception)
            {
                throw new MenuException("Wrong key was pressed");
            }
        }
        protected OrderBy ReceiveSortOrderFromUser()
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
        private void UpdatePages()
        {
            pages.Clear();
            var result = new List<string>();
            if(items.Count == 0)
            {
                items = GetItems();
            }
            foreach (T item in items)
            {
                result.Add(item.ToString());
            }
            CutRowsToPages(result);
        }
        public virtual void OutputTable()
        {
            UpdatePages();
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
                    pages.Add(new Page(attributesKeys.Values.ToArray(), cutedSet, spacesToDivider));
                    g = 0;
                    cutedSet = new string[pageSize];
                }
                cutedSet[g] = rows[i].ToString();
                g++;
            }
            pages.Add(new Page(attributesKeys.Values.ToArray(), cutedSet, spacesToDivider));
        }
    }
}
