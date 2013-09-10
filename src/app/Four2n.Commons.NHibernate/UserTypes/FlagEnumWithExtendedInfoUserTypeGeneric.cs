// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlagEnumWithExtendedInfoUserTypeGeneric.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Database representation of enum decorated with  and with .
//   CustomValue property is database representation for enum field value.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.NHibernate.UserTypes
{
    using System;

    using global::NHibernate;
    using global::NHibernate.SqlTypes;
    using global::NHibernate.UserTypes;

    using global::System;
    using global::System.Collections;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Globalization;
    using global::System.Xml.Serialization;

    /// <summary>
    /// Database representation of enum decorated with <see cref="EnumExtendedInfoAttribute"/> and with <see cref="FlagsAttribute"/>.
    /// CustomValue property is database representation for enum field value.
    /// There is "separator" param for definining custom separator. 
    /// Default separator is "|".
    /// </summary>
    /// <typeparam name="TEnum">Enum type decorated with <see cref="EnumExtendedInfoAttribute"/>.</typeparam>
    [Serializable]
    public class FlagEnumWithExtendedInfoUserType<TEnum> : EnumWithExtendedInfoUserType<TEnum>, IParameterizedType
    {
        [NonSerialized]
        private const string DefaultSeparator = "|";

        [NonSerialized]
        private const string SeparatorParameterName = "separator";

        [NonSerialized]
        private static readonly SqlType[] sqlTypes = new SqlType[] { SqlTypeFactory.GetString(255) };

        private string separator = DefaultSeparator;

        /// <summary>
        /// Gets the SQL types.
        /// </summary>
        /// <value>The SQL types.</value>
        [XmlIgnore]
        public override SqlType[] SqlTypes
        {
            get { return sqlTypes; }
        }

        /// <summary>
        /// Nulls the safe get.
        /// </summary>
        /// <param name="rs">The data reader.</param>
        /// <param name="names">The column names.</param>
        /// <param name="owner">The object owner.</param>
        /// <returns>Object converted from database</returns>
        public override object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            int result = 0;

            var valueInDatabase = rs[names[0]];
            if (valueInDatabase is DBNull)
            {
                return default(TEnum);
            }

            var stringvalueInDatabase = (string)rs[names[0]];

            if (string.IsNullOrEmpty(stringvalueInDatabase))
            {
                return default(TEnum);
            }

            foreach (string value in stringvalueInDatabase.Split(new[] { this.separator }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                object valueInInt = EnumExtendedInfoAttribute.GetEnumValueByCustomValue<TEnum>(value);
                result = result + (int)valueInInt;
            }

            return Enum.ToObject(typeof(TEnum), result);
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
        public override void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (value != null)
            {
                if (value is TEnum)
                {
                    var customValues = EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(value).GetCustomValues();
                    string result = null;

                    if (customValues.Length != 0)
                    {
                        result = string.Join(this.separator, Array.ConvertAll(customValues, i => i.ToString()));
                    }

                    NHibernateUtil.String.NullSafeSet(cmd, result, index);
                }
            }
        }

        /// <summary>
        /// Gets called by Hibernate to pass the configured type parameters to
        /// the implementation.
        /// </summary>
        /// <param name="parameters">Type parameters as dictionary.</param>
        public void SetParameterValues(IDictionary<string, string> parameters)
        {
            if (parameters != null)
            {
                parameters.TryGetValue(SeparatorParameterName, out this.separator);
            }
        }

        private static ulong ToUInt64(object value)
        {
            // Helper function to silently convert the value to UInt64 from the other base types for enum without throwing an exception. 
            // This is need since the Convert functions do overflow checks.
            TypeCode typeCode = Convert.GetTypeCode(value);
            ulong result;

            switch (typeCode)
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    result = (ulong)Convert.ToInt64(value, CultureInfo.InvariantCulture);
                    break;

                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    result = Convert.ToUInt64(value, CultureInfo.InvariantCulture);
                    break;

                default:
                    throw new InvalidOperationException("InvalidOperation_UnknownEnumType");
            }

            return result;
        }
    }
}