// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimplifiedKeyDictionary.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the SimplifiedKeyDictionary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System.Collections
{
    using global::System;
    using global::System.Collections;
    using global::System.Collections.Generic;

    /// <summary>
    /// Szybki s³ownik ktory trzyma obiekty zawierajace SimplifiedKey.
    /// Jest to taka fasada ktora ukrywa ulomnoci przeksztalcenia zbioru wiekszego w mniejszy.
    /// Moze byc obiekt ktory ma takie same hashed key ale inne bussiness value.
    /// </summary>
    /// <typeparam name="V">Type of the key identificator (long, int, byte).</typeparam>
    /// <typeparam name="K">Type of the key.</typeparam>
    /// <typeparam name="T">Type of object which is identified by the key.</typeparam>
    public abstract class SimplifiedKeyDictionary<V, K, T> : ICollection<T>
        where V : struct
        where K : SimplifiedKey<V>
        where T : ISimplifiedKeyIdentifiable<V, K>
    {
        /// <summary>
        /// Collection of keys which are duplicated.
        /// </summary>
        private readonly IDictionary<K, int> doubledSimplifiedKeyValues = new Dictionary<K, int>();

        /// <summary>
        /// Collection of record where simplified key is not unique but bussiness key is unique.
        /// </summary>
        private readonly IDictionary<string, T> recordsWithUniqueBusinessKey = new Dictionary<string, T>();

        /// <summary>
        /// Collection of records with unique simplified key.
        /// </summary>
        private readonly IDictionary<K, T> recordsWithUniqueSimplifiedKey = new Dictionary<K, T>();

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        {
            get { return this.recordsWithUniqueSimplifiedKey.Count + this.recordsWithUniqueBusinessKey.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        /// <exception cref="NotImplementedException"><c>NotImplementedException</c>.</exception>
        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets or sets the <see cref="T"/> with the specified key.
        /// </summary>
        /// <param name="key">The key as index.</param>
        /// <value>The <see cref="T"/> with the specified key.</value>
        /// <exception cref="ArgumentException">There is no such key.</exception>
        /// <exception cref="NotImplementedException"><c>NotImplementedException</c>.</exception>
        public T this[K key]
        {
            get
            {
                if (this.recordsWithUniqueSimplifiedKey.ContainsKey(key))
                {
                    return this.recordsWithUniqueSimplifiedKey[key];
                }
                
                if (this.doubledSimplifiedKeyValues.ContainsKey(key) && this.recordsWithUniqueBusinessKey.ContainsKey(key.BusinessValue))
                {
                    return this.recordsWithUniqueBusinessKey[key.BusinessValue];
                }

                throw new ArgumentException("There is no such key.");
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public void Add(T item)
        {
            this.Add(item.Key, item);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public void Clear()
        {
            this.recordsWithUniqueSimplifiedKey.Clear();
            this.recordsWithUniqueBusinessKey.Clear();
            this.doubledSimplifiedKeyValues.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        public bool Contains(T item)
        {
            return this.ContainsKey(item.Key);
        }

        /// <summary>
        /// Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">The simplified key.</param>
        /// <returns>
        ///     <c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsKey(K key)
        {
            if (this.recordsWithUniqueSimplifiedKey.ContainsKey(key) && this.recordsWithUniqueSimplifiedKey[key].Key.BusinessEquals(key))
            {
                return true;
            }

            if (this.doubledSimplifiedKeyValues.ContainsKey(key) == false)
            {
                return false;
            }

            return this.recordsWithUniqueBusinessKey.ContainsKey(key.BusinessValue);
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="arrayIndex"/> is less than 0.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="array"/> is multidimensional.
        /// -or-
        /// <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.
        /// -or-
        /// The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.
        /// -or-
        /// Type <paramref name="T"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.recordsWithUniqueSimplifiedKey.Values.CopyTo(array, arrayIndex);
            this.recordsWithUniqueBusinessKey.Values.CopyTo(array, arrayIndex + this.recordsWithUniqueSimplifiedKey.Count);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in this.recordsWithUniqueSimplifiedKey.Values)
            {
                yield return item;
            }

            foreach (T item in this.recordsWithUniqueBusinessKey.Values)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public bool Remove(T item)
        {
            return this.Remove(item.Key);
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The simplified key.</param>
        /// <returns>
        /// true if <paramref name="key"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="key"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public bool Remove(K key)
        {
            if (this.recordsWithUniqueSimplifiedKey.Remove(key))
            {
                return true;
            }

            if (this.recordsWithUniqueBusinessKey.Remove(key.BusinessValue))
            {
                this.doubledSimplifiedKeyValues[key]--;
                if (this.doubledSimplifiedKeyValues[key] <= 0)
                {
                    this.doubledSimplifiedKeyValues.Remove(key);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The simplified key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentException">There is element with that key.</exception>
        private void Add(K key, T value)
        {
            if (this.recordsWithUniqueSimplifiedKey.ContainsKey(key))
            {
                if (this.recordsWithUniqueSimplifiedKey[key].Key.BusinessEquals(key))
                {
                    throw new ArgumentException("There is element with that key.", "key");
                }

                T item = this.recordsWithUniqueSimplifiedKey[key];
                this.recordsWithUniqueBusinessKey.Add(item.Key.BusinessValue, item);
                this.recordsWithUniqueBusinessKey.Add(key.BusinessValue, value);
                this.doubledSimplifiedKeyValues.Add(key, 2);
                this.recordsWithUniqueSimplifiedKey.Remove(key);
            }
            else if (this.doubledSimplifiedKeyValues.ContainsKey(key))
            {
                this.doubledSimplifiedKeyValues[key]++;
                this.recordsWithUniqueBusinessKey.Add(key.BusinessValue, value);
            }
            else
            {
                this.recordsWithUniqueSimplifiedKey.Add(key, value);
            }
        }
    }
}