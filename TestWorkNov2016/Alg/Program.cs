using System;

namespace Alg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var utils = new Utilities();
            var mergeSorter = new MergeSorter<int>();

            var unsortedCollection = utils.GetRandomNumbersCollection(55, 100);

            utils.OutputCollectionContentString(unsortedCollection, nameof(unsortedCollection), Console.WriteLine);

            var sortedCollection = mergeSorter.Sort(unsortedCollection, false);

            utils.OutputCollectionContentString(sortedCollection, nameof(sortedCollection), Console.WriteLine);

            Console.ReadKey();
        }
    }
}
