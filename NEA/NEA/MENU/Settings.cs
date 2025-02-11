﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal class Settings : IHasOptions
    {

        public void PrintOptions()
        {
            Console.WriteLine("Press 1 to change variables' round length");
            Console.WriteLine("Press 2 to change the length of pages in the tables");
            Console.WriteLine("Press 3 to change the theme");
            Console.WriteLine("Press 4 to get back to the main menu");
        }
        public int GetRoundLength()
        {
            Console.WriteLine();
            Console.WriteLine("Press '1' to set rounding to 1dp, '2' for 2dp, '3' for 3dp and '4' for 4dp");
            var key = Console.ReadKey(true).Key;
            int roundingLength;
            if(key == ConsoleKey.D1) 
            {
                roundingLength = 1;
            }
            else if(key == ConsoleKey.D2) 
            {
                roundingLength = 2;
            }
            else if (key == ConsoleKey.D3) 
            {
                roundingLength = 3;
            }
            else if(key == ConsoleKey.D4)
            {
                roundingLength = 4;
            }
            else
            {
                throw new MenuException("Inappropriate key");
            }
            return roundingLength;
        }
        public ConsoleColor ChangeTheme()
        {
            Console.WriteLine();
            Console.WriteLine("Press '1' to set black theme, '2' to set white theme");
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.D1)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                return ConsoleColor.White;
            }
            else if (key == ConsoleKey.D2)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                return ConsoleColor.Black;
            }
            else
            {
                throw new MenuException("Inappropriate key");
            }
        }
        public int ChangeItemsPerPage()
        {
            Console.WriteLine();
            Console.WriteLine("Press '1' to set 5 items per page, '2' for 10, '3' for 20");
            var key = Console.ReadKey(true).Key;
            int pageLength;
            if (key == ConsoleKey.D1)
            {
                pageLength = 5;
            }
            else if (key == ConsoleKey.D2)
            {
                pageLength = 10;
            }
            else if (key == ConsoleKey.D3)
            {
                pageLength = 20;
            }
            else
            {
                throw new MenuException("Inappropriate key");
            }
            return pageLength;
        }
    }
}
