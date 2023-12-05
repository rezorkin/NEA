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
        public static int[] MergeSort(int[] values, Order order)
        {
            if(order == Order.ascending)
            {
                return MergeSortAscending(values);
            }
            return MergeSortDescending(values);
        }
        private static int[] MergeSortDescending(int[] values)
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
            leftPart = MergeSortDescending(leftPart);
            rightPart = MergeSortDescending(rightPart);
            return MergeDescending(leftPart, rightPart);
        }
        private static int[] MergeSortAscending(int[] values)
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
            leftPart = MergeSortAscending(leftPart);
            rightPart = MergeSortAscending(rightPart);
            return MergeAscending(leftPart, rightPart);
        }
        private static int[] MergeDescending(int[] leftPart, int[] rightPart)
        {
            int[] result = new int[rightPart.Length + leftPart.Length];
            int leftIndex = 0, rightIndex = 0, resultIndex = 0;
            while (leftIndex < leftPart.Length || rightIndex < rightPart.Length)
            {
                if (leftIndex < leftPart.Length && rightIndex < rightPart.Length)
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
        private static int[] MergeAscending(int[] leftPart, int[] rightPart)
        {
            int[] result = new int[rightPart.Length + leftPart.Length];
            int leftIndex = 0, rightIndex = 0, resultIndex = 0;
            while (leftIndex < leftPart.Length || rightIndex < rightPart.Length)
            {
                if (leftIndex < leftPart.Length && rightIndex < rightPart.Length)
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
                }
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
    }
}
