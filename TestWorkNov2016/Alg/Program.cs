using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Alg
{
    internal delegate void OutputAction<T>(ICollection<T> collection, string caption)
        where T : IComparable;

    internal class Program
    {
        private static readonly int elementsCount = 50;
        private static readonly bool isCollectionOutputEnabled = true;
        private static readonly bool isAscendingSort = true;
        private static readonly Stopwatch sw = new Stopwatch();

        static void Main()
        {
            var utils = new Utilities();
            var unsortedCollection = utils.GetRandomNumbersCollection(elementsCount, 100);

            OutputAction<int> outputAction = GetOutputAction<int>(utils);

            PerfromSort(new MergeSorter<int>(), unsortedCollection, outputAction, isAscendingSort);
            PerfromSort(new QuickSorter<int>(), unsortedCollection, outputAction, isAscendingSort);

            Console.ReadKey();
        }

        private static void PerfromSort<T>(SorterBase<T> sorter, ICollection<T> unsortedCollection, 
            OutputAction<T> outputAction, bool isAsc)
            where T : IComparable
        {
            DisplaySortCaption(sorter, unsortedCollection.Count);

            outputAction(unsortedCollection, nameof(unsortedCollection));

            sw.Start();
            var sortedCollection = sorter.Sort(unsortedCollection, isAsc);
            sw.Stop();

            outputAction(sortedCollection, nameof(sortedCollection));

            DisplaySortPerformanceResults();
        }

        private static OutputAction<T> GetOutputAction<T>(Utilities utils)
            where T : IComparable
        {
            return isCollectionOutputEnabled ?
                (col, caption) => utils.OutputCollectionContentString(col, caption, Console.WriteLine) :
                (OutputAction<T>)((col, caption) => { });
        }

        private static void DisplaySortCaption<T>(SorterBase<T> sorter, int itemsCount)
            where T : IComparable
        {
            var str = $"`{sorter.SortName}` of collection with {itemsCount} {typeof(T).FullName} elements {Environment.NewLine}";

            Console.WriteLine(str);
        }

        private static void DisplaySortPerformanceResults()
        {
            var str = $"Sorted collection in {sw.ElapsedMilliseconds} milliseconds {Environment.NewLine}{Environment.NewLine}";

            Console.WriteLine(str);
        }
    }
}
