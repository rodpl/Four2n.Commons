//------------------------------------------------------------------------------------------------- 
// <copyright file="SimplifiedKey.cs" company="Daniel Dabrowski - rod.blogsome.com">
// Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>Defines the SimplifiedKey type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Rod.Commons.System.Collections
{
    using global::System;

    /// <summary>
    /// Abstract class which is used as simplified key ex. hashed value.
    /// </summary>
    /// <typeparam name="V">Type of the key value.</typeparam>
    public abstract class SimplifiedKey<V> : IEquatable<SimplifiedKey<V>> where V : struct
    {
        /// <summary>
        /// Business value of the key.
        /// </summary>
        private string businessValue;

        /// <summary>
        /// Simplified, calculated value ot the key.
        /// </summary>
        private V? simplifiedValue;

        /// <summary>
        /// Gets BusinessValue.
        /// </summary>
        public string BusinessValue
        {
            get
            {
                if (this.businessValue == null)
                {
                    this.businessValue = this.GenerateBussinessKeyValue();
                }

                return this.businessValue;
            }
        }

        /// <summary>
        /// Gets SimplifiedValue.
        /// </summary>
        public V SimplifiedValue
        {
            get
            {
                if (!this.simplifiedValue.HasValue)
                {
                    this.simplifiedValue = this.GenerateSimplifiedKeyValue();
                }

                return (V) this.simplifiedValue;
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left side.</param>
        /// <param name="right">The right side.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(SimplifiedKey<V> left, SimplifiedKey<V> right)
        {
            return object.Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left side.</param>
        /// <param name="right">The right side.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(SimplifiedKey<V> left, SimplifiedKey<V> right)
        {
            return !object.Equals(left, right);
        }

        /// <summary>
        /// Equalses the specified key by business value.
        /// </summary>
        /// <param name="key">The compared key.</param>
        /// <returns>Returns true if keys are equal.</returns>
        public bool BusinessEquals(SimplifiedKey<V> key)
        {
            if (ReferenceEquals(null, key))
            {
                return false;
            }

            if (ReferenceEquals(this, key))
            {
                return true;
            }

            return this.GenerateBussinessKeyValue().Equals(key.GenerateBussinessKeyValue());
        }

        /// <summary>
        /// Equalses the specified key by simplified value.
        /// </summary>
        /// <param name="key">The compared key.</param>
        /// <returns>Returns true if keys are equal.</returns>
        public bool Equals(SimplifiedKey<V> key)
        {
            if (ReferenceEquals(null, key))
            {
                return false;
            }

            if (ReferenceEquals(this, key))
            {
                return true;
            }

            return key.SimplifiedValue.Equals(this.SimplifiedValue);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (!(obj is SimplifiedKey<V>))
            {
                return false;
            }

            return this.Equals((SimplifiedKey<V>) obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return this.SimplifiedValue.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return this.SimplifiedValue.ToString();
        }

        /// <summary>
        /// Generates the bussiness key value.
        /// </summary>
        /// <returns>Value of generated business key.</returns>
        protected abstract string GenerateBussinessKeyValue();

        /// <summary>
        /// Generates the simplified key value.
        /// </summary>
        /// <returns>Value of generated simplified key.</returns>
        protected abstract V GenerateSimplifiedKeyValue();
    }
}