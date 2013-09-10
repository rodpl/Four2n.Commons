// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlType.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   UserType allowing easy saving of   property.
//   Based on example from here http://ayende.com/Blog/archive/2006/05/30/NHibernateAndXMLColumnTypes.aspx.
//   With some modifications by Tobin Harris
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.NHibernate.UserTypes
{
    using global::NHibernate.SqlTypes;
    using global::NHibernate.UserTypes;

    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.Common;
    using global::System.Xml;
    using global::System.Xml.Serialization;

    using Rod.Commons.NHibernate.SqlTypes;

    /// <summary>
    /// UserType allowing easy saving of <see cref="NHibernate"/> <see cref="XmlDocument"/> property.
    /// Based on example from here http://ayende.com/Blog/archive/2006/05/30/NHibernateAndXMLColumnTypes.aspx.
    /// With some modifications by Tobin Harris
    /// </summary>
    [Serializable]
    public class XmlType : IUserType
    {
        [NonSerialized]
        private static readonly SqlType[] sqlTypes = new SqlType[1] { new SqlXmlType() };

        [NonSerialized]
        private static readonly Type returnedType = typeof(XmlDocument);

        /// <summary>
        /// Gets a value indicating whether this instance is mutable.
        /// </summary>
        [XmlIgnore]
        public bool IsMutable
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the type returned by <c>NullSafeGet()</c>
        /// </summary>
        [XmlIgnore]
        public Type ReturnedType
        {
            get { return returnedType; }
        }

        /// <summary>
        /// Gets the SQL types for the columns mapped by this type.
        /// </summary>
        [XmlIgnore]
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
        /// A reconstructed object from the cachable representation.
        /// </returns>
        public object Assemble(object cached, object owner)
        {
            var str = cached as string;
            if (str == null)
            {
                return null;
            }

            var doc = new XmlDocument();
            doc.LoadXml(str);
            return doc;
        }

        /// <summary>
        /// Return a deep copy of the persistent state, stopping at entities and at collections.
        /// </summary>
        /// <param name="value">generally a collection element or entity field</param>
        /// <returns>A deep copy</returns>
        public object DeepCopy(object value)
        {
            var toCopy = value as XmlDocument;

            if (toCopy == null)
            {
                return null;
            }

            var copy = new XmlDocument();
            copy = (XmlDocument)toCopy.Clone();
            return copy;
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
            var val = value as XmlDocument;
            return val != null ? val.OuterXml : null;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="x">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <param name="y">The other object to compare.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
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

            var xdocX = (XmlDocument)x;
            var xdocY = (XmlDocument)y;
            return xdocY.OuterXml == xdocX.OuterXml;
        }

        /// <summary>
        /// Returns a hash code for this instance.
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
        /// Retrieve an instance of the mapped class from a JDBC resultset.
        /// Implementors should handle possibility of null values.
        /// </summary>
        /// <param name="rs">a IDataReader</param>
        /// <param name="names">column names</param>
        /// <param name="owner">the containing entity</param>
        /// <returns>Always null</returns>
        /// <exception cref="T:NHibernate.HibernateException">HibernateException</exception>
        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            if (names.Length != 1)
            {
                throw new InvalidOperationException("names array has more than one element. can't handle this!");
            }

            var document = new XmlDocument();
            var val = rs[names[0]] as string;

            if (val != null)
            {
                if (val.Trim() == string.Empty)
                {
                    return document;
                }

                document.LoadXml(val);
                return document;
            }

            return null;
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
        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            var parameter = (DbParameter)cmd.Parameters[index];

            if (value == null)
            {
                parameter.Value = DBNull.Value;
                return;
            }

            parameter.Value = ((XmlDocument)value).OuterXml;
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