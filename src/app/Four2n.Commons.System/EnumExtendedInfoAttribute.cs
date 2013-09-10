// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtendedInfoAttribute.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the ExtendedEnumAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System
{
    using global::System;
    using global::System.Collections;
    using global::System.Collections.Generic;
    using global::System.Globalization;
    using global::System.Reflection;

    /// <summary>
    /// Attribute for extending enum by custom name as description
    /// and value as database representations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum, AllowMultiple = false)]
    public class EnumExtendedInfoAttribute : Attribute
    {
        /// <summary>
        /// Separator for joining flag enum custom valuesand names.
        /// </summary>
        public static readonly string FlagEnumSeparator = ", ";

        /// <summary>
        /// Separator array.
        /// </summary>
        public static readonly string[] FlagEnumSeparatorArray = new[] { FlagEnumSeparator };

        private static readonly IDictionary<object, EnumExtendedInfoAttribute> cachedAtttributes =
                new Dictionary<object, EnumExtendedInfoAttribute>();

        private static readonly object lockAttributes = new object();

        private object customValue;

        private object[] customValues;

        private object enumValue;

        private string name;

        public EnumExtendedInfoAttribute()
        {
        }

        public EnumExtendedInfoAttribute(string name)
        {
            this.Name = name;
        }

        public EnumExtendedInfoAttribute(string name, object customValue)
        {
            this.Name = name;
            this.CustomValue = customValue;
        }

        /// <summary>
        /// Gets or sets custom value definition of enum value.
        /// </summary>
        public object CustomValue
        {
            get
            {
                if (this.enumValue == null)
                {
                    return null;
                }

                return this.customValue ?? (this.customValue = this.Name);
            }

            set
            {
                if (value is string)
                {
                    var dec = 0m;
                    if (Decimal.TryParse((string)value, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out dec))
                    {
                        this.customValue = dec;
                        return;
                    }
                }

                this.customValue = value;
            }
        }

        /// <summary>
        /// Gets or sets name definition of enum value.
        /// </summary>
        public string Name
        {
            get
            {
                if (this.enumValue == null)
                {
                    return null;
                }

                if (this.name == null)
                {
                    string representedValueString = this.enumValue.ToString();
                    if (representedValueString.Contains(", "))
                    {
                        string[] splits = representedValueString.Split(
                                new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < splits.Length; i++)
                        {
                            object singleValue = Enum.Parse(this.enumValue.GetType(), splits[i]);
                            this.name += GetExtendedInfoByEnumValue(singleValue).Name;
                            if (i < splits.Length - 1)
                            {
                                this.name += FlagEnumSeparator;
                            }
                        }
                    }
                    else
                    {
                        this.name = representedValueString;
                    }
                }

                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets enum value by custom value.
        /// </summary>
        /// <typeparam name="TEnum">Type of enum value.</typeparam>
        /// <param name="value">Custom value.</param>
        /// <returns>Enum value.</returns>
        public static TEnum GetEnumValueByCustomValue<TEnum>(object value)
        {
            foreach (TEnum item in Enum.GetValues(typeof(TEnum)))
            {
                if (GetExtendedInfoByEnumValue(item).CustomValue.Equals(value))
                {
                    return item;
                }
            }

            throw new ArgumentException(
                    string.Format(
                            "{0} does not contains such custom value : {1} - {2}", 
                            typeof(TEnum), 
                            value, 
                            value.GetType().Name));
        }

        /// <summary>
        /// Gets <see cref="EnumExtendedInfoAttribute"/> instance for passed type of enum value.
        /// </summary>
        /// <param name="field">Enum value.</param>
        /// <returns>Extended info attribute for passed type of enum value.</returns>
        public static EnumExtendedInfoAttribute GetExtendedInfoByEnumValue(object field)
        {
            if (field == null)
            {
                return CreateEmptyFor(field);
            }

            EnumExtendedInfoAttribute result;

            if (cachedAtttributes.TryGetValue(field, out result) == false)
            {
                lock (lockAttributes)
                {
                    if (cachedAtttributes.TryGetValue(field, out result) == false)
                    {
                        Type type = field.GetType();
                        FieldInfo fieldInfo = type.GetField(field.ToString());
                        if (fieldInfo == null)
                        {
                            result = CreateEmptyFor(field);
                            cachedAtttributes.Add(field, result);
                            return result;
                        }

                        object[] attribs = fieldInfo.GetCustomAttributes(typeof(EnumExtendedInfoAttribute), false);
                        foreach (object item in attribs)
                        {
                            if (item is EnumExtendedInfoAttribute)
                            {
                                result = item as EnumExtendedInfoAttribute;
                                result.enumValue = field;
                                cachedAtttributes.Add(field, result);
                                return result;
                            }
                        }

                        var resultLast = CreateEmptyFor(field);
                        cachedAtttributes.Add(field, resultLast);
                        return resultLast;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the custom values.
        /// Usefull for flag enum. It returns collection of custom values.
        /// If Enum us not decorated as Flag this method usualy returns one element.
        /// </summary>
        /// <returns>Array of custom values</returns>
        public object[] GetCustomValues()
        {
            if (this.customValues == null)
            {
                var ulongValue = ToUInt64(this.enumValue);
                var values = new Stack();
                var possibleValues = Enum.GetValues(this.enumValue.GetType());
                int index = possibleValues.GetLength(0) - 1;
                while (index >= 0)
                {
                    var item = possibleValues.GetValue(index);
                    var ulongItem = ToUInt64(item);
                    if ((index == 0) && (ulongItem == 0))
                    {
                        break;
                    }

                    if ((ulongValue & ulongItem) == ulongItem)
                    {
                        ulongValue -= ulongItem;
                        values.Push(GetExtendedInfoByEnumValue(item).CustomValue);
                    }

                    index--;
                }

                this.customValues = values.ToArray();
            }

            return this.customValues;
        }

        /// <summary>
        /// Gets names as collection.
        /// Usefull for flag enum.
        /// </summary>
        /// <returns>Names as array.</returns>
        public string[] GetNames()
        {
            return this.Name.Split(FlagEnumSeparatorArray, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Creates empty instance of <see cref="EnumExtendedInfoAttribute"/> with name.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="field">Enum value field.</param>
        /// <returns>Empty instance of <see cref="EnumExtendedInfoAttribute"/>.</returns>
        private static EnumExtendedInfoAttribute CreateEmptyFor<T>(T field)
        {
            return new EnumExtendedInfoAttribute { enumValue = field };
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

                    // All unsigned types will be directly cast
                    throw new InvalidOperationException("InvalidOperation_UnknownEnumType");
            }

            return result;
        }
    }
}