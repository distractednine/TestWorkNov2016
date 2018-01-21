using System;
using System.Collections.Generic;
using System.Linq;

namespace Alg
{
    internal class Utilities
    {
        private static readonly Random Random = new Random();

        public ICollection<int> GetRandomNumbersCollection(int count, int upperBound)
        {
            Func<int> getRandomInt = () => Random.Next(0, upperBound);

            return Enumerable.Range(0, count)
                .Select(x => getRandomInt())
                .ToArray();
        }

        public void OutputCollectionContentString<T>(IEnumerable<T> enumerable, string caption, Action<string> outputAction)
        {
            var str = GetCollectionContentString(enumerable, caption);
            outputAction(str);
        }

        private string GetCollectionContentString<T>(IEnumerable<T> enumerable, string caption)
        {
            Func<string, T, string> formatter =
                (x, y) => string.IsNullOrEmpty(x) ?
                y.ToString() :
                $"{x.ToString()}, {y.ToString()}";

            var result = enumerable.Aggregate(string.Empty, formatter);

            return $"{caption.ToUpperInvariant()}:{Environment.NewLine}[{result}] {Environment.NewLine}";
        }
    }
}
