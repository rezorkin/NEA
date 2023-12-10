using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal class Page
    {
        string[] attributes;
        string[] rows;
        string spacesToDivider;
        public Page(string[] attributes, string[] rows,int amountOfSpacesToDivider) 
        {
            this.attributes = attributes;
            spacesToDivider = "";
            this.rows = rows;
            for (int i = 0; i < amountOfSpacesToDivider; i++)
            {
                spacesToDivider += " ";
            }
        }
        public void DisplayPage()
        {
            if (attributes != null) 
            {
                for(int i  = 0; i < attributes.Length-1; i++) 
                {
                    Console.Write(" " + attributes[i] + spacesToDivider + " ");
                }
                Console.Write(" " + attributes.Last());
                Console.WriteLine();
                Console.WriteLine();
                
            }
            foreach (var row in rows)
            {
                if(row != null) 
                {
                    string[] values = row.Split(',');
                    string settedRow = AddRowDivider(values);
                    Console.WriteLine(settedRow);
                }   
            }
        }
        private string AddRowDivider(string[] values)
        {
            string result = "";
            for (int i = 0; i < attributes.Length; i++)
            {
                
                int lengthOfAttributeFieldSpace = attributes[i].Length + spacesToDivider.Length;
                int amountOfValueSpaces = lengthOfAttributeFieldSpace - values[i].Length;
                if (amountOfValueSpaces >= 0)
                {
                    string valueSpacesBeforeDivider = "";
                    for (int g = 0; g < amountOfValueSpaces; g++)
                    {
                        valueSpacesBeforeDivider += " ";
                    }
                    result += " " + values[i] + valueSpacesBeforeDivider + "|";
                }
                else
                {
                    int lengthOfAbbrevation = values[i].Length + (amountOfValueSpaces - 3);
                    string valueAbbrevation = values[i].Substring(0, lengthOfAbbrevation);
                    result += " " + valueAbbrevation + "..."  + "|";
                }
            }
            return result;
        }
    }
}
