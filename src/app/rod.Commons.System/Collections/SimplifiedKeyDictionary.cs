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
    public abstract class SimplifiedKeyDictionary<V, K, T> : ICollection<T>
        where V : struct
        where K : SimplifiedKey<V>
        where T : ISimplifiedKeyIdentifiable<V, K>
    {
        private readonly IDictionary<K, int> _doubledSimplifiedKeyValues = new Dictionary<K, int>();
        private readonly IDictionary<string, T> _recordsWithUniqueBusinessKey = new Dictionary<string, T>();
        private readonly IDictionary<K, T> _recordsWithUniqueSimplifiedKey = new Dictionary<K, T>();

        public T this[K key]
        {
            get
            {
                if(_recordsWithUniqueSimplifiedKey.ContainsKey(key))
                    return _recordsWithUniqueSimplifiedKey[key];
                if (_doubledSimplifiedKeyValues.ContainsKey(key) && _recordsWithUniqueBusinessKey.ContainsKey(key.BusinessValue))
                    return _recordsWithUniqueBusinessKey[key.BusinessValue];
                throw new ArgumentException("There is no such key.");

            }
            set { throw new NotImplementedException(); }
        }

        #region ICollection<T> Members

        public int Count
        {
            get { return _recordsWithUniqueSimplifiedKey.Count + _recordsWithUniqueBusinessKey.Count; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public void Clear()
        {
            _recordsWithUniqueSimplifiedKey.Clear();
            _recordsWithUniqueBusinessKey.Clear();
            _doubledSimplifiedKeyValues.Clear();
        }

        public bool Contains(T item)
        {
            return ContainsKey(item.Key);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _recordsWithUniqueSimplifiedKey.Values.CopyTo(array, arrayIndex);
            _recordsWithUniqueBusinessKey.Values.CopyTo(array, arrayIndex + _recordsWithUniqueSimplifiedKey.Count);
        }

        public bool Remove(T item)
        {
            return Remove(item.Key);
        }

        public void Add(T item)
        {
            Add(item.Key, item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in _recordsWithUniqueSimplifiedKey.Values)
                yield return item;
            foreach (T item in _recordsWithUniqueBusinessKey.Values)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public bool ContainsKey(K key)
        {
            if (_recordsWithUniqueSimplifiedKey.ContainsKey(key) && _recordsWithUniqueSimplifiedKey[key].Key.BusinessEquals(key)) return true;
            if (_doubledSimplifiedKeyValues.ContainsKey(key) == false) return false;
            return _recordsWithUniqueBusinessKey.ContainsKey(key.BusinessValue);
        }

        private void Add(K key, T value)
        {
            if (_recordsWithUniqueSimplifiedKey.ContainsKey(key))
            {
                if (_recordsWithUniqueSimplifiedKey[key].Key.BusinessEquals(key))
                    throw new ArgumentException("There is element with that key.", "key");
                T item = _recordsWithUniqueSimplifiedKey[key];
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
            if (_recordsWithUniqueSimplifiedKey.Remove(key))
                return true;
            if(_recordsWithUniqueBusinessKey.Remove(key.BusinessValue))
            {
                _doubledSimplifiedKeyValues[key]--;
                if(_doubledSimplifiedKeyValues[key] <= 0)
                    _doubledSimplifiedKeyValues.Remove(key);
                return true;
            }
            return false;
        }
    }
}