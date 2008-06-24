using System;

namespace rod.Commons.System.Collections
{
    /// <summary>
    /// Abstract class which is used as simplified key ex. hashed value.
    /// </summary>
    /// <typeparam name="V">Type of the key value</typeparam>
    public abstract class SimplifiedKey<V> : IEquatable<SimplifiedKey<V>> where V : struct
    {
        protected V? _simplifiedValue;
        protected string _businessValue;

        public V SimplifiedValue
        {
            get
            {
                if (!_simplifiedValue.HasValue)
                    _simplifiedValue = GenerateSimplifiedKeyValue();
                return (V) _simplifiedValue;
            }
        }

        public string BusinessValue
        {
            get
            {
                if (_businessValue == null)
                    _businessValue = GenerateBussinessKeyValue();
                return _businessValue;
            }
        }

        #region IEquatable<SimplifiedKey<V>> Members

        public bool Equals(SimplifiedKey<V> obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.SimplifiedValue.Equals(SimplifiedValue);
        }

        #endregion

        public bool BusinessEquals(SimplifiedKey<V> obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return GenerateBussinessKeyValue().Equals(obj.GenerateBussinessKeyValue());
        }

        protected abstract string GenerateBussinessKeyValue();
        protected abstract V GenerateSimplifiedKeyValue();

        public override string ToString()
        {
            return SimplifiedValue.ToString();
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is SimplifiedKey<V>)) return false;
            return Equals((SimplifiedKey<V>) obj);
        }

        public override int GetHashCode()
        {
            return SimplifiedValue.GetHashCode();
        }

        public static bool operator ==(SimplifiedKey<V> left, SimplifiedKey<V> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SimplifiedKey<V> left, SimplifiedKey<V> right)
        {
            return !Equals(left, right);
        }
    }
}