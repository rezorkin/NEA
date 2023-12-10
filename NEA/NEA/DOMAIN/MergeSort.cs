using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.MENU;

namespace NEA.DOMAIN
{
    internal static class MergeSort
    {
        public static int[] MergeSortArray(int[] values, Order order)
        {
            if (values.Length <= 1)
            {
                return values;
            }
            int middleIndex = values.Length / 2;
            var leftPart = new int[middleIndex];
            var rightPart = new int[values.Length - middleIndex];
            for (int i = 0; i < middleIndex; i++)
            {
                leftPart[i] = values[i];
            }
            int g = 0;
            for (int i = middleIndex; i < values.Length; i++)
            {
                rightPart[g] = values[i];
                g++;
            }
            leftPart = MergeSortArray(leftPart, order);
            rightPart = MergeSortArray(rightPart, order);
            return MergeCompare(leftPart, rightPart, order);
        }
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
        public static List<MedicineStatistics> MergeSortByName(List<MedicineStatistics> records, Order order)
        {
            if (records.Count <= 1)
            {
                return records;
            }
            int middleIndex = records.Count / 2;
            var leftPart = new List<MedicineStatistics>();
            var rightPart = new List<MedicineStatistics>();
            for (int i = 0; i < middleIndex; i++)
            {
                leftPart.Add(records[i]);
            }
            for (int i = middleIndex; i < records.Count; i++)
            {
                rightPart.Add(records[i]);
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
        public static List<MedicineStatistics> MergeSortByID(List<MedicineStatistics> records, Order order)
        {
            if (records.Count <= 1)
            {
                return records;
            }
            int middleIndex = records.Count / 2;
            var leftPart = new List<MedicineStatistics>();
            var rightPart = new List<MedicineStatistics>();
            for (int i = 0; i < middleIndex; i++)
            {
                leftPart.Add(records[i]);
            }
            for (int i = middleIndex; i < records.Count; i++)
            {
                rightPart.Add(records[i]);
            }
            leftPart = MergeSortByID(leftPart, order);
            rightPart = MergeSortByID(rightPart, order);
            return MergeCompareByID(leftPart, rightPart, order);
        }
        public static List<MedicineStatistics> MergeSortByMean(List<MedicineStatistics> records, Order order)
        {
            if (records.Count <= 1)
            {
                return records;
            }
            int middleIndex = records.Count / 2;
            var leftPart = new List<MedicineStatistics>();
            var rightPart = new List<MedicineStatistics>();
            for (int i = 0; i < middleIndex; i++)
            {
                leftPart.Add(records[i]);
            }
            for (int i = middleIndex; i < records.Count; i++)
            {
                rightPart.Add(records[i]);
            }
            leftPart = MergeSortByMean(leftPart, order);
            rightPart = MergeSortByMean(rightPart, order);
            return MergeCompareByMean(leftPart, rightPart, order);
        }
        public static List<MedicineStatistics> MergeSortByMedian(List<MedicineStatistics> records, Order order)
        {
            if (records.Count <= 1)
            {
                return records;
            }
            int middleIndex = records.Count / 2;
            var leftPart = new List<MedicineStatistics>();
            var rightPart = new List<MedicineStatistics>();
            for (int i = 0; i < middleIndex; i++)
            {
                leftPart.Add(records[i]);
            }
            for (int i = middleIndex; i < records.Count; i++)
            {
                rightPart.Add(records[i]);
            }
            leftPart = MergeSortByMedian(leftPart, order);
            rightPart = MergeSortByMedian(rightPart, order);
            return MergeCompareByMedian(leftPart, rightPart, order);
        }
        public static List<MedicineStatistics> MergeSortByModes(List<MedicineStatistics> records, Order order)
        {
            if (records.Count <= 1)
            {
                return records;
            }
            int middleIndex = records.Count / 2;
            var leftPart = new List<MedicineStatistics>();
            var rightPart = new List<MedicineStatistics>();
            for (int i = 0; i < middleIndex; i++)
            {
                leftPart.Add(records[i]);
            }
            for (int i = middleIndex; i < records.Count; i++)
            {
                rightPart.Add(records[i]);
            }
            leftPart = MergeSortByModes(leftPart, order);
            rightPart = MergeSortByModes(rightPart, order);
            return MergeCompareByModes(leftPart, rightPart, order);
        }
        public static List<MedicineStatistics> MergeSortByDeviation(List<MedicineStatistics> records, Order order)
        {
            if (records.Count <= 1)
            {
                return records;
            }
            int middleIndex = records.Count / 2;
            var leftPart = new List<MedicineStatistics>();
            var rightPart = new List<MedicineStatistics>();
            for (int i = 0; i < middleIndex; i++)
            {
                leftPart.Add(records[i]);
            }
            for (int i = middleIndex; i < records.Count; i++)
            {
                rightPart.Add(records[i]);
            }
            leftPart = MergeSortByDeviation(leftPart, order);
            rightPart = MergeSortByDeviation(rightPart, order);
            return MergeCompareByDeviation(leftPart, rightPart, order);
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
                    if (order == Order.DESC)
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
                    if (order == Order.DESC)
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
                    if (order == Order.DESC)
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
        private static List<MedicineStatistics> MergeCompareByName(List<MedicineStatistics> leftPart, List<MedicineStatistics> rightPart, Order order)
        {
            var result = new List<MedicineStatistics>();
            int leftIndex = 0, rightIndex = 0;
            while (leftIndex < leftPart.Count || rightIndex < rightPart.Count)
            {
                if (leftIndex < leftPart.Count && rightIndex < rightPart.Count)
                {
                    string leftName = leftPart[leftIndex].name;
                    string rightName = rightPart[rightIndex].name;
                    int stringComparison = string.Compare(leftName, rightName);
                    if (order == Order.DESC)
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
        private static List<MedicineStatistics> MergeCompareByMean(List<MedicineStatistics> leftPart, List<MedicineStatistics> rightPart, Order order)
        {
            var result = new List<MedicineStatistics>();
            int leftIndex = 0, rightIndex = 0;
            while (leftIndex < leftPart.Count || rightIndex < rightPart.Count)
            {
                if (leftIndex < leftPart.Count && rightIndex < rightPart.Count)
                {
                    double leftMean = leftPart[leftIndex].mean;
                    double rightMean = rightPart[rightIndex].mean;
                    if (order == Order.DESC)
                    {
                        if (leftMean >= rightMean)
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
                        if (leftMean == -1)
                        {
                            result.Add(rightPart[rightIndex]);
                            rightIndex++;
                        }
                        else if (leftMean <= rightMean || rightMean == -1)
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
        private static List<MedicineStatistics> MergeCompareByMedian(List<MedicineStatistics> leftPart, List<MedicineStatistics> rightPart, Order order)
        {
            var result = new List<MedicineStatistics>();
            int leftIndex = 0, rightIndex = 0;
            while (leftIndex < leftPart.Count || rightIndex < rightPart.Count)
            {
                if (leftIndex < leftPart.Count && rightIndex < rightPart.Count)
                {
                    double leftMedian = leftPart[leftIndex].median;
                    double rightMedian = rightPart[rightIndex].median;
                    if (order == Order.DESC)
                    {
                        if (leftMedian >= rightMedian)
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
                        if (leftMedian == -1)
                        {
                            result.Add(rightPart[rightIndex]);
                            rightIndex++;
                        }
                        else if (leftMedian <= rightMedian || rightMedian == -1)
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
        private static List<MedicineStatistics> MergeCompareByDeviation(List<MedicineStatistics> leftPart, List<MedicineStatistics> rightPart, Order order)
        {
            var result = new List<MedicineStatistics>();
            int leftIndex = 0, rightIndex = 0;
            while (leftIndex < leftPart.Count || rightIndex < rightPart.Count)
            {
                if (leftIndex < leftPart.Count && rightIndex < rightPart.Count)
                {
                    double leftDeviation = leftPart[leftIndex].standrardDeviation;
                    double rightDeviation = rightPart[rightIndex].standrardDeviation;
                    if (order == Order.DESC)
                    {
                        if (leftDeviation >= rightDeviation)
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
                        if (leftDeviation == -1)
                        {
                            result.Add(rightPart[rightIndex]);
                            rightIndex++;
                        }
                        else if (leftDeviation <= rightDeviation || rightDeviation == -1)
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
        private static List<MedicineStatistics> MergeCompareByModes(List<MedicineStatistics> leftPart, List<MedicineStatistics> rightPart, Order order)
        {
            var result = new List<MedicineStatistics>();
            int leftIndex = 0, rightIndex = 0;
            while (leftIndex < leftPart.Count || rightIndex < rightPart.Count)
            {
                if (leftIndex < leftPart.Count && rightIndex < rightPart.Count)
                {
                    Dictionary<int,int> leftModes = leftPart[leftIndex].modes;
                    Dictionary<int,int> rightModes = rightPart[rightIndex].modes;
                    int leftMaxMode, rightMaxMode;
                    if (leftModes.Count == 0)
                    {
                        leftMaxMode = -1;
                    }
                    else
                    {
                        leftMaxMode = leftModes.Max().Key;
                    }
                    if(rightModes.Count == 0)
                    {
                        rightMaxMode = -1;
                    }
                    else
                    {
                        rightMaxMode = leftModes.Max().Key;
                    }
                    if (order == Order.DESC)
                    {
                        if (leftMaxMode >= rightMaxMode)
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
                        if (leftMaxMode == -1)
                        {
                            result.Add(rightPart[rightIndex]);
                            rightIndex++;
                        }
                        else if (leftMaxMode <= rightMaxMode || rightMaxMode == -1)
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
                    if (order == Order.DESC)
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
        private static List<MedicineStatistics> MergeCompareByID(List<MedicineStatistics> leftPart, List<MedicineStatistics> rightPart, Order order)
        {
            var result = new List<MedicineStatistics>();
            int leftIndex = 0, rightIndex = 0;
            while (leftIndex < leftPart.Count || rightIndex < rightPart.Count)
            {
                if (leftIndex < leftPart.Count && rightIndex < rightPart.Count)
                {
                    if (order == Order.DESC)
                    {
                        if (leftPart[leftIndex].id >= rightPart[rightIndex].id)
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
                        if (leftPart[leftIndex].id <= rightPart[rightIndex].id)
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
                    if (order == Order.DESC)
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
