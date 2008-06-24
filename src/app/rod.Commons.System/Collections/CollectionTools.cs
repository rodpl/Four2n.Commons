using System;
using System.Collections.Generic;

namespace rod.Commons.System.Collections
{
    /// <summary>
    /// Helper class with static methods
    /// </summary>
    public static class CollectionTools
    {
        /// <summary>
        /// Adds elements to dictionary from collection.
        /// </summary>
        /// <typeparam name="K">Key type</typeparam>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="dictionary">The target dictionary.</param>
        /// <param name="source">The collection source.</param>
        /// <param name="nonUnique">The collection for non unique items. This collection must be empty.</param>
        /// <param name="keyExtractor">Delegate for pointing key value.</param>
        public static void AddToDictionaryFromCollection<K, T>(IDictionary<K, T> dictionary, ICollection<T> source,
                                                               ICollection<T> nonUnique, Converter<T, K> keyExtractor)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");
            if (nonUnique == null) throw new ArgumentNullException("nonUnique");
            if (nonUnique.Count > 0) throw new ArgumentException("nonUnique must be empty");

            var isSourceCameEmpty = dictionary.Count == 0;
            var tempDict = isSourceCameEmpty ? dictionary : new Dictionary<K, T>();
            var nonUniqueKeys = new Dictionary<K, K>();

            foreach (var item in source)
            {
                var key = keyExtractor(item);
                if (!tempDict.ContainsKey(key))
                    tempDict.Add(key, item);
                else
                {
                    nonUnique.Add(item);
                    if (!nonUniqueKeys.ContainsKey(key))
                        nonUniqueKeys.Add(key, key);
                }
            }
            if (nonUnique.Count > 0)
            {
                foreach (var item in nonUniqueKeys.Keys)
                {
                    nonUnique.Add(tempDict[item]);
                    tempDict.Remove(item);
                }
            }

            if (isSourceCameEmpty) return;

            foreach (var item in tempDict)
            {
                if (dictionary.ContainsKey(item.Key))
                    nonUnique.Add(item.Value);
                else
                    dictionary.Add(item);
            }
        }

        /// <summary>
        /// Creates the dictionary from collection.
        /// </summary>
        /// <typeparam name="K">Key type</typeparam>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="source">The collection source.</param>
        /// <param name="nonUnique">The collection for non unique items. This collection must be empty.</param>
        /// <param name="keyExtractor">Delegate for pointing key value.</param>
        /// <returns>Dictionary with K keys and T values.</returns>
        public static IDictionary<K, T> CreateDictionaryFromCollection<K, T>(ICollection<T> source, ICollection<T> nonUnique,
                                                                             Converter<T, K> keyExtractor)
        {
            if (nonUnique == null) throw new ArgumentNullException("nonUnique");
            if (nonUnique.Count > 0) throw new ArgumentException("nonUnique must be empty");

            IDictionary<K, T> dict = new Dictionary<K, T>();
            AddToDictionaryFromCollection(dict, source, nonUnique, keyExtractor);
            return dict;
        }
    }
}