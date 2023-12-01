using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.MENU
{
    internal class Page
    {
        List<string> attributes;
        string[] rows;
        string spacesToDivider = "                            ";
        public Page(string[] rows)
        {
            this.rows = rows;

        }
        public Page(List<string> attributes, string[] rows) 
        {
            this.attributes = attributes;
            this.rows = rows;
        }
        public void DisplayPage()
        {
            if (attributes != null) 
            {
                for(int i  = 0; i < attributes.Count-1; i++) 
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
            for (int i = 0; i < attributes.Count; i++)
            {
                
                int lengthOfAttributeString = attributes[i].Length + spacesToDivider.Length;
                int amountOfValueSpaces = lengthOfAttributeString - values[i].Length;
                string valueSpacesBeforeDivider = "";
                for(int g = 0; g < amountOfValueSpaces; g++)
                {
                    valueSpacesBeforeDivider += " ";
                }

                result += " " + values[i] + valueSpacesBeforeDivider + "|";
            }
            return result;
        }
    }
}
