using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCore.Extensions
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class CollectionExtension
    {
        /// <summary>
        /// Add list of items to the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oc"></param>
        /// <param name="collection"></param>
        public static void AddRange<T>(this ObservableCollection<T> oc, IEnumerable<T> collection)
        {
            if (collection != null && oc != null)
            {
                foreach (var item in collection)
                {
                    oc.Add(item);
                }
            }
        }

        /// <summary>
        /// Removes all the items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        public static void RemoveAll<T>(this ICollection<T> source, Func<T, bool> predicate)
        {
            if (source == null && predicate != null)
            {
                source.Where(predicate).ToList().ForEach(e => source.Remove(e));
            }
        }
    }
}
