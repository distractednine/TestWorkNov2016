using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Alg
{
    internal abstract class SorterBase<T> 
        where T : IComparable
    {
        // ReSharper disable once StaticMemberInGenericType
        // ReSharper disable once InconsistentNaming
        private static readonly Random random = new Random();

        protected Random Random => random;

        protected virtual bool IsDebuggerOutputEnabled => false;

        protected Comparer ComparerFunc { get; private set; }

        public abstract string SortName { get; }

        protected void SetComparer(bool isAscSort)
        {
            ComparerFunc = Comparers.GetComparer(isAscSort);
        }

        protected void CheckCollection(ICollection<T> sourceCollection)
        {
            if (sourceCollection == null || !sourceCollection.Any())
            {
                throw new ApplicationException("The source enumeration must have at least one element.");
            }
        }

        protected void OutputToDebug(T[] arr, string additionalStr = null)
        {
            if (!IsDebuggerOutputEnabled)
            {
                return;
            }

            var str = string.Join(", ", arr.Select(x => x.ToString())) + additionalStr;
            Debug.WriteLine(str);
        }

        protected T[] Swap(T[] arr, int i, int j)
        {
            var tmp = arr[i];
            arr[i] = arr[j];
            arr[j] = tmp;

            return arr;
        }

        protected abstract T[] Sort(T[] sourceArr);

        public ICollection<T> Sort(ICollection<T> sourceCollection, bool isAscSort)
        {
            SetComparer(isAscSort);
            CheckCollection(sourceCollection);

            var sorted = Sort(sourceCollection.ToArray());

            return sorted;
        }
    }
}
