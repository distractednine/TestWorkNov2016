using System;

namespace Alg
{
    public delegate bool Comparer(IComparable left, IComparable right);

    public static class Comparers
    {
        public static Comparer AscComparer = (left, right) => left.CompareTo(right) < 0;

        public static Comparer DescComparer = (left, right) => left.CompareTo(right) > 0;

        public static Comparer GetComparer(bool isAscending)
        {
            return isAscending ? AscComparer : DescComparer;
        }
    }
}
