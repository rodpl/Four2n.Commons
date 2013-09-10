// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumWithExtendedInfoNullableUserType.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the EnumWithExtendedInfoNullableUserType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.NHibernate.UserTypes
{
    using System;

    using global::System;
    using global::System.Data;
    using global::System.Xml.Serialization;

    /// <summary>
    /// Database representation of nullable enum decorated with <see cref="EnumExtendedInfoAttribute"/>.
    /// CustomValue property is database representation for enum field value.
    /// </summary>
    /// <typeparam name="TEnum">Enum type decorated with <see cref="EnumExtendedInfoAttribute"/>.</typeparam>
    [Serializable]
    public class EnumWithExtendedInfoNullableUserType<TEnum> : EnumWithExtendedInfoUserType<TEnum>
            where TEnum : struct
    {
        [NonSerialized]
        private static readonly Type returnedType = typeof(TEnum?);

        /// <summary>
        /// Gets the type of the returned.
        /// </summary>
        /// <value>The type of the returned.</value>
        [XmlIgnore]
        public override Type ReturnedType
        {
            get { return returnedType; }
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
        public override object NullSafeGet(global::System.Data.IDataReader rs, string[] names, object owner)
        {
            object value = rs[names[0]];
            if (value == DBNull.Value)
            {
                return null;
            }

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
        public override void NullSafeSet(global::System.Data.IDbCommand cmd, object value, int index)
        {
            ((IDataParameter)cmd.Parameters[index]).Value =
                EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(value).CustomValue ?? DBNull.Value;
        }
    }
}