using System;
using System.Collections;
using System.Collections.Generic;

namespace rod.Commons.System.Collections
{
    /// <summary>
    /// Szybki s³ownik ktory trzyma obiekty zawierajace SimplifiedKey.
    /// Jest to taka fasada ktora ukrywa ulomnoci przeksztalcenia zbioru wiekszego w mniejszy.
    /// Moze byc obiekt ktory ma takie same hashed key ale inne bussiness value.
    /// </summary>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="T"></typeparam>
    public abstract class SimplifiedKeyDictionary<V, K, T> : IDictionary<K, T>
        where V : struct
        where K : SimplifiedKey<V>
        where T : SimplifiedKeyIdentifiable<V, K>
    {
        private readonly IDictionary<string, T> _recordsWithUniqueBusinessKey = new Dictionary<string, T>();
        private readonly IDictionary<K, T> _recordsWithUniqueSimplifiedKey = new Dictionary<K, T>();
        private readonly IDictionary<K, int> _doubledSimplifiedKeyValues = new Dictionary<K, int>();


//        public void Add(ICollection<T> list)
//        {
//            if (list == null) return;
//            var invalidRecords = new List<T>();
//            CollectionTools.AddToDictionaryFromCollection(_recordsWithUniqueKey, list, invalidRecords, i => i.Key);
//        }

        #region IDictionary<K,T> Members

        public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<K, T> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _recordsWithUniqueSimplifiedKey.Clear();
            _recordsWithUniqueBusinessKey.Clear();
            _doubledSimplifiedKeyValues.Clear();
        }

        public bool Contains(KeyValuePair<K, T> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<K, T>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<K, T> item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return _recordsWithUniqueSimplifiedKey.Count + _recordsWithUniqueBusinessKey.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool ContainsKey(K key)
        {
            if (_recordsWithUniqueSimplifiedKey.ContainsKey(key)) return true;
            if (_doubledSimplifiedKeyValues.ContainsKey(key) == false) return false;
            return _recordsWithUniqueBusinessKey.ContainsKey(key.BusinessValue);
        }

        public void Add(K key, T value)
        {
            if(_recordsWithUniqueSimplifiedKey.ContainsKey(key))
            {
                if(_recordsWithUniqueSimplifiedKey[key].Key.BusinessEquals(key))
                    throw new ArgumentException("There is element with that key.", "key");
                var item = _recordsWithUniqueSimplifiedKey[key];
                _recordsWithUniqueBusinessKey.Add(item.Key.BusinessValue, item);
                _recordsWithUniqueBusinessKey.Add(key.BusinessValue, value);
                _doubledSimplifiedKeyValues.Add(key, 2);
                _recordsWithUniqueSimplifiedKey.Remove(key);

            }
            else if (_doubledSimplifiedKeyValues.ContainsKey(key))
            {
                _doubledSimplifiedKeyValues[key]++;
                _recordsWithUniqueBusinessKey.Add(key.BusinessValue, value);
            }
            else
            {
                _recordsWithUniqueSimplifiedKey.Add(key, value);
            }
        }

        public bool Remove(K key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(K key, out T value)
        {
            throw new NotImplementedException();
        }

        public T this[K key]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ICollection<K> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public ICollection<T> Values
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        public void Add(T item)
        {
            Add(item.Key, item);
        }
    }
}