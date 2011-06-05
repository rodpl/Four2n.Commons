// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumWithExtendedInfoUserTypeGeneric.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the EnumWithExtendedInfoUserType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.NHibernate.UserTypes
{
    using System;

    using global::NHibernate.SqlTypes;
    using global::NHibernate.UserTypes;

    using global::System;
    using global::System.Data;

    /// <summary>
    /// Database representation of enum decorated with <see cref="EnumExtendedInfoAttribute"/>.
    /// CustomValue property is database representation for enum field value.
    /// </summary>
    /// <typeparam name="TEnum">Enum type decorated with <see cref="EnumExtendedInfoAttribute"/>.</typeparam>
    public class EnumWithExtendedInfoUserType<TEnum> : IUserType
    {
        private static readonly Type returnedType = typeof(TEnum);
        private static readonly SqlType[] sqlTypes = new SqlType[] { SqlTypeFactory.GetString(50) };

        /// <summary>
        /// Gets a value indicating whether this instance is mutable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is mutable; otherwise, <c>false</c>.
        /// </value>
        public bool IsMutable
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the type of the returned.
        /// </summary>
        /// <value>The type of the returned.</value>
        public Type ReturnedType
        {
            get { return returnedType; }
        }

        /// <summary>
        /// Gets the SQL types.
        /// </summary>
        /// <value>The SQL types.</value>
        public virtual SqlType[] SqlTypes
        {
            get { return sqlTypes; }
        }

        /// <summary>
        /// Reconstruct an object from the cacheable representation. At the very least this
        /// method should perform a deep copy if the type is mutable. (optional operation)
        /// </summary>
        /// <param name="cached">the object to be cached</param>
        /// <param name="owner">the owner of the cached object</param>
        /// <returns>
        /// a reconstructed object from the cachable representation
        /// </returns>
        public object Assemble(object cached, object owner)
        {
            return this.DeepCopy(cached);
        }

        /// <summary>
        /// Return a deep copy of the persistent state, stopping at entities and at collections.
        /// </summary>
        /// <param name="value">generally a collection element or entity field</param>
        /// <returns>a copy of object</returns>
        public object DeepCopy(object value)
        {
            return value;
        }

        /// <summary>
        /// Transform the object into its cacheable representation. At the very least this
        /// method should perform a deep copy if the type is mutable. That may not be enough
        /// for some implementations, however; for example, associations must be cached as
        /// identifier values. (optional operation)
        /// </summary>
        /// <param name="value">the object to be cached</param>
        /// <returns>a cacheable representation of the object</returns>
        public object Disassemble(object value)
        {
            return value;
        }

        /// <summary>
        /// Compare two instances of the class mapped by this type for persistent "equality"
        /// ie. equality of persistent state
        /// </summary>
        /// <param name="x">The left side.</param>
        /// <param name="y">The right side.</param>
        /// <returns>True if objects are equal.</returns>
        public new bool Equals(object x, object y)
        {
            if (x == y)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Equals(y);
        }

        /// <summary>
        /// Get a hashcode for the instance, consistent with persistence "equality"
        /// </summary>
        /// <param name="x">The instance.</param>
        /// <returns>Hash code of passed instance.</returns>
        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        /// <summary>
        /// Retrieve an instance of the mapped class from a JDBC resultset.
        /// Implementors should handle possibility of null values.
        /// </summary>
        /// <param name="rs">a IDataReader</param>
        /// <param name="names">column names</param>
        /// <param name="owner">the containing entity</param>
        /// <returns>Object converted from database</returns>
        /// <exception cref="T:NHibernate.HibernateException">HibernateException</exception>
        public virtual object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            object value = rs[names[0]];
            return EnumExtendedInfoAttribute.GetEnumValueByCustomValue<TEnum>(value);
        }

        /// <summary>
        /// Write an instance of the mapped class to a prepared statement.
        /// Implementors should handle possibility of null values.
        /// A multi-column type should be written to parameters starting from index.
        /// </summary>
        /// <param name="cmd">a IDbCommand</param>
        /// <param name="value">the object to write</param>
        /// <param name="index">command parameter index</param>
        /// <exception cref="T:NHibernate.HibernateException">HibernateException</exception>
        public virtual void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            ((IDataParameter)cmd.Parameters[index]).Value =
                    EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(value).CustomValue;
        }

        /// <summary>
        /// During merge, replace the existing (<paramref name="target"/>) value in the entity
        /// we are merging to with a new (<paramref name="original"/>) value from the detached
        /// entity we are merging. For immutable objects, or null values, it is safe to simply
        /// return the first parameter. For mutable objects, it is safe to return a copy of the
        /// first parameter. For objects with component values, it might make sense to
        /// recursively replace component values.
        /// </summary>
        /// <param name="original">the value from the detached entity being merged</param>
        /// <param name="target">the value in the managed entity</param>
        /// <param name="owner">the managed entity</param>
        /// <returns>the value to be merged</returns>
        public object Replace(object original, object target, object owner)
        {
            return original;
        }
    }
}