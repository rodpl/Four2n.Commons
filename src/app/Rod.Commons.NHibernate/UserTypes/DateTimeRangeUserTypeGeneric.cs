// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeRangeUserTypeGeneric.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   NHibernate database representation of
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.NHibernate.UserTypes
{
    using System;

    using global::NHibernate;
    using global::NHibernate.Engine;
    using global::NHibernate.SqlTypes;
    using global::NHibernate.Type;
    using global::NHibernate.UserTypes;

    using global::System;
    using global::System.Data;

    /// <summary>
    /// NHibernate database representation of generic type <see cref="IDateTimeRange"/>
    /// </summary>
    /// <typeparam name="T">
    /// Class of <see cref="IDateTimeRange"/>
    /// </typeparam>
    public abstract class DateTimeRangeUserType<T> : ICompositeUserType where T : IDateTimeRange
    {
        private static readonly string[] propertyNames = new[] { "Begins", "Ends" };

        private static readonly IType[] propertyTypes = new IType[] { NHibernateUtil.DateTime, NHibernateUtil.DateTime };

        private static readonly Type returnedClass = typeof(T);

        /// <summary>
        /// Gets a value indicating whether objects of this type are mutable.
        /// </summary>
        public bool IsMutable
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the "property names" that may be used in a query. 
        /// </summary>
        public string[] PropertyNames
        {
            get { return propertyNames; }
        }

        /// <summary>
        /// Gets the corresponding "property types"
        /// </summary>
        public IType[] PropertyTypes
        {
            get { return propertyTypes; }
        }

        /// <summary>
        /// Gets type of the class returned by NullSafeGet().
        /// </summary>
        public Type ReturnedClass
        {
            get { return returnedClass; }
        }

        /// <summary>
        /// Reconstruct an object from the cacheable representation.
        /// At the very least this method should perform a deep copy. (optional operation)
        /// </summary>
        /// <param name="cached">the object to be cached</param>
        /// <param name="session">session imlementor</param>
        /// <param name="owner">the owner of the cached object</param>
        /// <returns>
        /// A reconstructed object from the cachable representation.
        /// </returns>
        public object Assemble(object cached, ISessionImplementor session, object owner)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return a deep copy of the persistent state, stopping at entities and at collections.
        /// </summary>
        /// <param name="value">generally a collection element or entity field</param>
        /// <returns>A deep copy</returns>
        public object DeepCopy(object value)
        {
            if (value == null)
            {
                return null;
            }

            var casted = (T)value;
            return this.Create(casted.Begins, casted.Ends);
        }

        /// <summary>
        /// Transform the object into its cacheable representation.
        /// At the very least this method should perform a deep copy.
        /// That may not be enough for some implementations, method should perform a deep copy. That may not be enough for some implementations, however; for example, associations must be cached as identifier values. (optional operation)
        /// </summary>
        /// <param name="value">the object to be cached</param>
        /// <param name="session">session imlementor</param>
        /// <returns>a cacheable representation of the object</returns>
        public object Disassemble(object value, ISessionImplementor session)
        {
            return this.DeepCopy(value);
        }

        /// <summary>
        /// Compare two instances of the class mapped by this type for persistence
        /// "equality", ie. equality of persistent state.
        /// </summary>
        /// <param name="x">The <see cref="object"/> to compare with this instance.</param>
        /// <param name="y">The other object to compare.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public new abstract bool Equals(object x, object y);

        /// <summary>
        /// Get a hashcode for the instance, consistent with persistence "equality"
        /// </summary>
        /// <param name="x">The target object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        /// <summary>
        /// Get the value of a property
        /// </summary>
        /// <param name="component">an instance of class mapped by this "type"</param>
        /// <param name="property">Property index</param>
        /// <returns>
        /// the property value
        /// </returns>
        public object GetPropertyValue(object component, int property)
        {
            var casted = (T)component;
            return property == 0 ? casted.Begins : casted.Ends;
        }

        /// <summary>
        /// Retrieve an instance of the mapped class from a IDataReader. Implementors
        /// should handle possibility of null values.
        /// </summary>
        /// <param name="dr">a IDataReader</param>
        /// <param name="names">column names</param>
        /// <param name="session">session imlementor</param>
        /// <param name="owner">the containing entity</param>
        /// <returns>Always null</returns>
        /// <exception cref="T:NHibernate.HibernateException">HibernateException</exception>
        public object NullSafeGet(IDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            var start = (DateTime?)NHibernateUtil.DateTime.NullSafeGet(dr, names[0], session, owner);
            var end = (DateTime?)NHibernateUtil.DateTime.NullSafeGet(dr, names[1], session, owner);
            if (start == null && end == null)
            {
                return null;
            }

            return this.Create(start, end);
        }

        /// <summary>
        /// Write an instance of the mapped class to a prepared statement.
        /// Implementors should handle possibility of null values.
        /// A multi-column type should be written to parameters starting from index.
        /// </summary>
        /// <param name="cmd">a IDbCommand</param>
        /// <param name="value">the object to write</param>
        /// <param name="index">command parameter index</param>
        /// <param name="session">session imlementor</param>
        /// <exception cref="T:NHibernate.HibernateException">HibernateException</exception>
        public void NullSafeSet(IDbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var casted = (T)value;
            if (value == null)
            {
                NHibernateUtil.DateTime.NullSafeSet(cmd, null, index, session);
                NHibernateUtil.DateTime.NullSafeSet(cmd, null, index + 1, session);
            }
            else
            {
                NHibernateUtil.DateTime.NullSafeSet(cmd, casted.Begins, index, session);
                NHibernateUtil.DateTime.NullSafeSet(cmd, casted.Ends, index + 1, session);
            }
        }

        /// <summary>
        /// During merge, replace the existing (target) value in the entity we are merging to
        /// with a new (original) value from the detached entity we are merging. For immutable
        /// objects, or null values, it is safe to simply return the first parameter. For
        /// mutable objects, it is safe to return a copy of the first parameter. However, since
        /// composite user types often define component values, it might make sense to recursively 
        /// replace component values in the target object.
        /// </summary>
        /// <param name="original">the value from the detached entity being merged</param>
        /// <param name="target">the value in the managed entity</param>
        /// <param name="session">session imlementor</param>
        /// <param name="owner">the managed entity</param>
        /// <returns>the value to be merged</returns>
        public object Replace(object original, object target, ISessionImplementor session, object owner)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set the value of a property
        /// </summary>
        /// <param name="component">an instance of class mapped by this "type"</param>
        /// <param name="property">Property index</param>
        /// <param name="value">the value to set</param>
        public void SetPropertyValue(object component, int property, object value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates <see cref="IDateTimeRange"/> instance.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>Instance of date range object.</returns>
        protected abstract T Create(DateTime? startDate, DateTime? endDate);
    }
}