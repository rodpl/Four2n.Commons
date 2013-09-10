// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeRangeTypeConverterGeneric.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateRangeTypeConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System
{
    using global::System.ComponentModel;

    /// <summary>
    /// Type converter for DateTimeRange classes.
    /// </summary>
    /// <typeparam name="T">Type of DateTimeRange</typeparam>
    public abstract class DateTimeRangeTypeConverter<T> : TypeConverter
        where T : IDateTimeRange
    {
        /// <summary>
        /// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="sourceType">A <see cref="T:System.Type"/> that represents the type you want to convert from.</param>
        /// <returns>
        /// true if this converter can perform the conversion; otherwise, false.
        /// </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, global::System.Type sourceType)
        {
            return sourceType == typeof(string) ? true : base.CanConvertFrom(context, sourceType);
        }
    }
}