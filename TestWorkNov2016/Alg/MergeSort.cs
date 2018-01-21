using System;
using System.Collections.Generic;
using System.Linq;

namespace Alg
{
    internal class MergeSorter<T> where T : IComparable
    {
        private Func<T, T, bool> comparerFunc;

        private readonly Func<T, T, bool> ascComparer = (T left, T right) => left.CompareTo(right) < 0;

        private readonly Func<T, T, bool> descComparer = (T left, T right) => left.CompareTo(right) > 0;

        public ICollection<T> Sort(ICollection<T> sourceCollection, bool isAscSort)
        {
            if (sourceCollection == null || !sourceCollection.Any())
            {
                throw new ApplicationException("The source enumeration must have at least one element.");
            }

            comparerFunc = isAscSort ? ascComparer : descComparer;

            var sorted = Sort(sourceCollection.ToArray());

            return sorted;
        }

        private T[] Sort(T[] sourceArr)
        {
            // split on 2 until there are 2 items
            if (sourceArr.Length > 2)
            {
                int middleIndex = sourceArr.Length / 2;
                int itemsToCopyCount = sourceArr.Length - middleIndex;

                // get sourceArr split on two equal parts
                var leftArr = Split(sourceArr, 0, itemsToCopyCount);
                var rightArr = Split(sourceArr, itemsToCopyCount, itemsToCopyCount);

                // recursively sort left
                leftArr = Sort(leftArr);

                // recursively sort right
                rightArr = Sort(rightArr);

                // merge sorted results
                return Merge(leftArr, rightArr);
            }
            if (sourceArr.Length == 1)
            {
                return sourceArr;
            }

            // return sorted array of 2 items
            var leftItemIsBigger = comparerFunc(sourceArr[0], sourceArr[1]);

            return leftItemIsBigger ?
                new[] { sourceArr[1], sourceArr[0] } :
                new[] { sourceArr[0], sourceArr[1] };
        }

        private T[] Split(T[] sourceArr, int skipCout, int takeCout)
        {
            return sourceArr
                .Select(x => x)
                .Skip(skipCout)
                .Take(takeCout)
                .ToArray();
        }

        private T[] Merge(T[] leftArr, T[] rightArr)
        {
            int leftPivot = 0,
                rightPivot = 0;
            int resultArrCount = leftArr.Length + rightArr.Length;

            var resultArr = new T[resultArrCount];

            for (int i = 0; i < resultArrCount; i++)
            {
                // take from right array if left array is out of range
                if (leftArr.Length <= leftPivot)
                {
                    resultArr[i] = rightArr[rightPivot];
                    rightPivot++;
                    continue;
                }
                // take left right array if right array is out of range
                if (rightArr.Length <= rightPivot)
                {
                    resultArr[i] = leftArr[leftPivot];
                    leftPivot++;
                    continue;
                }

                var isLeftItemBigger = comparerFunc(leftArr[leftPivot], rightArr[rightPivot]);

                if (!isLeftItemBigger)
                {
                    resultArr[i] = leftArr[leftPivot];
                    leftPivot++;
                }
                else
                {
                    resultArr[i] = rightArr[rightPivot];
                    rightPivot++;
                }
            }

            return resultArr;
        }
    }
}
