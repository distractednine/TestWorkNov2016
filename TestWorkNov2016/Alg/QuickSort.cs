using System;

namespace Alg
{
    internal class QuickSorter<T> : SorterBase<T>
        where T : IComparable
    {
        public override String SortName => "Quick sort";

        protected override T[] Sort(T[] sourceArr)
        {
            var sorted = QuickSort(sourceArr, 0, sourceArr.Length - 1);

            return sorted;
        }

        private T[] QuickSort(T[] arr, int startIndex, int endIndex)
        {
            if (arr.Length == 1)
            {
                return arr;
            }

            int pivotIndex = Random.Next(startIndex, endIndex);
            T pivot = arr[pivotIndex];

            // leftIterator - (index of last item that is less than pivot) + 1
            int lItr = startIndex;

            // right iterator - (index of last item that is bigger than pivot) + 1
            int rItr = startIndex;

            for (; rItr <= endIndex; rItr++)
            {
                //OutputToDebug(arr, $". l: {lItr} r:{rItr}");

                var current = arr[rItr];
                var isBiggerThanPivot = ComparerFunc(pivot, current);

                if (isBiggerThanPivot || pivotIndex == rItr)
                {
                    // if current item is bigger than pivot - just continue
                    continue;
                }

                if (pivotIndex == lItr)
                {
                    // update pivot index before the pivot is swapped
                    pivotIndex = rItr;
                }

                // put current after the last elem less than pivot
                Swap(arr, lItr, rItr);
                lItr++;
            }

            //OutputToDebug(arr, $". l: {lItr} r:{rItr}");

            var shouldSwapPivot = ComparerFunc(pivot, arr[lItr]);

            if (shouldSwapPivot)
            {
                // swap pivot with the first item bigger than pivot - so the pivot takes its sorted place
                Swap(arr, lItr, pivotIndex);

                //OutputToDebug(arr, $". l: {lItr} r:{rItr}");
            }

            // sort the part of array that is to the left from pivot
            if (startIndex < lItr - 1)
            {
                QuickSort(arr, startIndex, lItr - 1);
            }
            // sort the part of array that is to the right from pivot
            if (lItr + 1 < endIndex)
            {
                QuickSort(arr, lItr + 1, endIndex);
            }

            return arr;
        }        
    }
}
