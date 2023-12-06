using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.MENU;

namespace NEA.Domain
{
    internal static class AttributeSorter
    {
        public static List<Medicine> MergeSortByActiveSubstance(List<Medicine> medicines, Order order)
        {
            if (medicines.Count <= 1)
            {
                return medicines;
            }
            int middleIndex = medicines.Count / 2;
            var leftPart = new List<Medicine>();
            var rightPart = new List<Medicine>();
            for (int i = 0; i < middleIndex; i++)
            {
                leftPart.Add(medicines[i]);
            }
            for (int i = middleIndex; i < medicines.Count; i++)
            {
                rightPart.Add(medicines[i]);
            }
            leftPart = MergeSortByActiveSubstance(leftPart, order);
            rightPart = MergeSortByActiveSubstance(rightPart, order);
            return MergeCompareByActiveSubstance(leftPart, rightPart, order);
        }
        public static List<Medicine> MergeSortByName(List<Medicine> medicines, Order order)
        {
            if (medicines.Count <= 1)
            {
                return medicines;
            }
            int middleIndex = medicines.Count / 2;
            var leftPart = new List<Medicine>();
            var rightPart = new List<Medicine>();
            for (int i = 0; i < middleIndex; i++)
            {
                leftPart.Add(medicines[i]);
            }
            for (int i = middleIndex; i < medicines.Count; i++)
            {
                rightPart.Add(medicines[i]);
            }
            leftPart = MergeSortByName(leftPart, order);
            rightPart = MergeSortByName(rightPart, order);
            return MergeCompareByName(leftPart, rightPart, order);
        }
        public static List<Medicine> MergeSortByCompName(List<Medicine> medicines, Order order)
        {
            if (medicines.Count <= 1)
            {
                return medicines;
            }
            int middleIndex = medicines.Count / 2;
            var leftPart = new List<Medicine>();
            var rightPart = new List<Medicine>();
            for (int i = 0; i < middleIndex; i++)
            {
                leftPart.Add(medicines[i]);
            }
            for (int i = middleIndex; i < medicines.Count; i++)
            {
                rightPart.Add(medicines[i]);
            }
            leftPart = MergeSortByCompName(leftPart, order);
            rightPart = MergeSortByCompName(rightPart, order);
            return MergeCompareByCompName(leftPart, rightPart, order);
        }
        public static List<Medicine> MergeSortByID(List<Medicine> medicines, Order order)
        {
            
            if (medicines.Count <= 1)
            {
                return medicines;
            }
            int middleIndex = medicines.Count / 2;
            var leftPart = new List<Medicine>();
            var rightPart = new List<Medicine>();
            for (int i = 0; i < middleIndex; i++)
            {
                leftPart.Add(medicines[i]);
            }
            for (int i = middleIndex; i < medicines.Count; i++)
            {
                rightPart.Add(medicines[i]);
            }
            leftPart = MergeSortByID(leftPart, order);
            rightPart = MergeSortByID(rightPart, order);
            return MergeCompareByID(leftPart, rightPart, order);
        }
        public static int[] MergeSort(int[] values, Order order)
        {
            int[] leftPart;
            int[] rightPart;
            if (values.Length <= 1)
            {
                return values;
            }
            int middleIndex = values.Length / 2;
            leftPart = new int[middleIndex];
            rightPart = new int[values.Length - middleIndex];
            for (int i = 0; i < leftPart.Length; i++)
            {
                leftPart[i] = values[i];
            }
            int rPartIndex = 0;
            for (int i = middleIndex; i < values.Length; i++)
            {
                rightPart[rPartIndex] = values[i];
                rPartIndex++;
            }
            leftPart = MergeSort(leftPart, order);
            rightPart = MergeSort(rightPart, order);
            return MergeCompare(leftPart, rightPart, order);
        }
        private static List<Medicine> MergeCompareByActiveSubstance(List<Medicine> leftPart, List<Medicine> rightPart, Order order)
        {
            var result = new List<Medicine>();
            int leftIndex = 0, rightIndex = 0;
            while (leftIndex < leftPart.Count || rightIndex < rightPart.Count)
            {
                if (leftIndex < leftPart.Count && rightIndex < rightPart.Count)
                {
                    string leftSubstance = leftPart[leftIndex].GetActiveSubstance().ToUpper();
                    string rightSubstance = rightPart[rightIndex].GetActiveSubstance().ToUpper();
                    int stringComparison = string.CompareOrdinal(leftSubstance, rightSubstance);
                    if (order == Order.descending)
                    {
                        if (stringComparison >= 0)
                        {
                            result.Add(leftPart[leftIndex]);
                            leftIndex++;
                        }
                        else
                        {
                            result.Add(rightPart[rightIndex]);
                            rightIndex++;
                        }
                    }
                    else
                    {
                        if (stringComparison <= 0)
                        {
                            result.Add(leftPart[leftIndex]);
                            leftIndex++;
                        }
                        else
                        {
                            result.Add(rightPart[rightIndex]);
                            rightIndex++;
                        }
                    }
                }
                else if (leftIndex < leftPart.Count && rightIndex == rightPart.Count)
                {
                    result.Add(leftPart[leftIndex]);
                    leftIndex++;
                }
                else if (rightIndex < rightPart.Count && leftIndex == leftPart.Count)
                {
                    result.Add(rightPart[rightIndex]);
                    rightIndex++;
                }
            }
            return result;
        }
        private static List<Medicine> MergeCompareByCompName(List<Medicine> leftPart, List<Medicine> rightPart, Order order)
        {
            var result = new List<Medicine>();
            int leftIndex = 0, rightIndex = 0;
            while (leftIndex < leftPart.Count || rightIndex < rightPart.Count)
            {
                if (leftIndex < leftPart.Count && rightIndex < rightPart.Count)
                {
                    string leftName = leftPart[leftIndex].GetCompanyName().ToLower();
                    string rightName = rightPart[rightIndex].GetCompanyName().ToLower();
                    int stringComparison = string.Compare(leftName, rightName);
                    if (order == Order.descending)
                    {
                        if (stringComparison >= 0)
                        {
                            result.Add(leftPart[leftIndex]);
                            leftIndex++;
                        }
                        else
                        {
                            result.Add(rightPart[rightIndex]);
                            rightIndex++;
                        }
                    }
                    else
                    {
                        if (stringComparison <= 0)
                        {
                            result.Add(leftPart[leftIndex]);
                            leftIndex++;
                        }
                        else
                        {
                            result.Add(rightPart[rightIndex]);
                            rightIndex++;
                        }
                    }
                }
                else if (leftIndex < leftPart.Count && rightIndex == rightPart.Count)
                {
                    result.Add(leftPart[leftIndex]);
                    leftIndex++;
                }
                else if (rightIndex < rightPart.Count && leftIndex == leftPart.Count)
                {
                    result.Add(rightPart[rightIndex]);
                    rightIndex++;
                }
            }
            return result;
        }
        private static List<Medicine> MergeCompareByName(List<Medicine> leftPart, List<Medicine> rightPart, Order order)
        {
            var result = new List<Medicine>();
            int leftIndex = 0, rightIndex = 0;
            while (leftIndex < leftPart.Count || rightIndex < rightPart.Count)
            {
                if (leftIndex < leftPart.Count && rightIndex < rightPart.Count)
                {
                    string leftName = leftPart[leftIndex].GetName().ToLower();
                    string rightName = rightPart[rightIndex].GetName().ToLower();
                    int stringComparison = string.Compare(leftName, rightName);
                    if (order == Order.descending)
                    {
                        if (stringComparison >= 0)
                        {
                            result.Add(leftPart[leftIndex]);
                            leftIndex++;
                        }
                        else
                        {
                            result.Add(rightPart[rightIndex]);
                            rightIndex++;
                        }
                    }
                    else
                    {
                        if (stringComparison <= 0)
                        {
                            result.Add(leftPart[leftIndex]);
                            leftIndex++;
                        }
                        else
                        {
                            result.Add(rightPart[rightIndex]);
                            rightIndex++;
                        }
                    }
                }
                else if (leftIndex < leftPart.Count && rightIndex == rightPart.Count)
                {
                    result.Add(leftPart[leftIndex]);
                    leftIndex++;
                }
                else if (rightIndex < rightPart.Count && leftIndex == leftPart.Count)
                {
                    result.Add(rightPart[rightIndex]);
                    rightIndex++;
                }
            }
            return result;
        }
        private static List<Medicine> MergeCompareByID(List<Medicine> leftPart, List<Medicine> rightPart, Order order)
        {
            var result = new List<Medicine>();
            int leftIndex = 0, rightIndex = 0;
            while (leftIndex < leftPart.Count || rightIndex < rightPart.Count)
            {
                if (leftIndex < leftPart.Count && rightIndex < rightPart.Count)
                {
                    if (order == Order.descending)
                    {
                        if (leftPart[leftIndex].GetID() >= rightPart[rightIndex].GetID())
                        {
                            result.Add(leftPart[leftIndex]);
                            leftIndex++;
                        }
                        else
                        {
                            result.Add(rightPart[rightIndex]);
                            rightIndex++;
                        }
                    }
                    else
                    {
                        if (leftPart[leftIndex].GetID() <= rightPart[rightIndex].GetID())
                        {
                            result.Add(leftPart[leftIndex]);
                            leftIndex++;
                        }
                        else
                        {
                            result.Add(rightPart[rightIndex]);
                            rightIndex++;
                        }
                    }
                }
                else if (leftIndex < leftPart.Count && rightIndex == rightPart.Count)
                {
                    result.Add(leftPart[leftIndex]);
                    leftIndex++;
                }
                else if (rightIndex < rightPart.Count && leftIndex == leftPart.Count)
                {
                    result.Add(rightPart[rightIndex]);
                    rightIndex++;
                }
            }
            return result;
        }
        
        private static int[] MergeCompare(int[] leftPart, int[] rightPart, Order order)
        {
            int[] result = new int[rightPart.Length + leftPart.Length];
            int leftIndex = 0, rightIndex = 0, resultIndex = 0;
            while (leftIndex < leftPart.Length || rightIndex < rightPart.Length)
            {
                if (leftIndex < leftPart.Length && rightIndex < rightPart.Length)
                {
                    if (order == Order.descending)
                    {
                        if (leftPart[leftIndex] >= rightPart[rightIndex])
                        {
                            result[resultIndex] = leftPart[leftIndex];
                            leftIndex++;
                            resultIndex++;
                        }
                        else
                        {
                            result[resultIndex] = rightPart[rightIndex];
                            rightIndex++;
                            resultIndex++;
                        }

                    }
                    else
                    {
                        if (leftPart[leftIndex] <= rightPart[rightIndex])
                        {
                            result[resultIndex] = leftPart[leftIndex];
                            leftIndex++;
                            resultIndex++;
                        }
                        else
                        {
                            result[resultIndex] = rightPart[rightIndex];
                            rightIndex++;
                            resultIndex++;
                        }
                    }                }
                else if (leftIndex < leftPart.Length && rightIndex == rightPart.Length)
                {
                    result[resultIndex] = leftPart[leftIndex];
                    leftIndex++;
                    resultIndex++;
                }
                else if (rightIndex < rightPart.Length && leftIndex == leftPart.Length)
                {
                    result[resultIndex] = rightPart[rightIndex];
                    rightIndex++;
                    resultIndex++;
                }
            }
            return result;
        }
        private static int CompareActiveSubstances(string left, string right)
        {
            string[] leftToSubGroups = ConvertSubstanceToSubGroups(left);
            string[] rightToSubGroups = ConvertSubstanceToSubGroups(right);
            string emptyGroupCode = "empty";
            for(int i = 0; i < 5; i++) 
            {
                if (leftToSubGroups[i] == emptyGroupCode && rightToSubGroups[i] != emptyGroupCode)
                {
                    return -1;
                }
                else if (leftToSubGroups[i] != emptyGroupCode && rightToSubGroups[i] == emptyGroupCode)
                {
                    return 1;
                }
                else
                {
                    int stringComparison = string.CompareOrdinal(leftToSubGroups[i], rightToSubGroups[i]);
                    if (stringComparison != 0)
                    {
                        return stringComparison;
                    }
                }
            }
            return 0;
        }
        private static string[] ConvertSubstanceToSubGroups(string substance)
        {
            string[] result = new string[5];
            int currentIndex = 0;
            for (int i = 0; i < 5; i++)
            {
                int subGroupLength = 1;
                if (i == 1 || i == 4)
                {
                    subGroupLength = 2;
                }
                int substringedLength = currentIndex - 1;
                if (subGroupLength <= substringedLength)
                {
                    result[i] = substance.Substring(currentIndex, subGroupLength);
                }
                else
                {
                    result[i] = "empty";
                }
            }
            return result;
        }
        static int Binarysearch(int[] values, int soughtFor)
        {
            int middleIndex;
            int leftindex = 0;
            int rightIndex = values.Length - 1;
            while (leftindex < rightIndex)
            {
                middleIndex = (leftindex + rightIndex) / 2;
                if (values[middleIndex] == soughtFor)
                {
                    return middleIndex;
                }
                if (values[middleIndex] > soughtFor)
                {
                  rightIndex = middleIndex - 1;
                }
                else
                {
                  leftindex = middleIndex + 1;
                }
            } 
            return -1;
        }


    }
}
