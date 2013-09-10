// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtendedInfoHelper.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the EnumExtendedInfoHelpers type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System.Web.Mvc
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Web.Mvc;

    /// <summary>
    /// Helper MVC methods for enums decorated with <see cref="EnumExtendedInfoAttribute"/>.
    /// </summary>
    public static class EnumExtendedInfoHelper
    {
        /// <summary>
        /// Creates the select list for.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <param name="selected">The selected.</param>
        /// <param name="title">The title.</param>
        /// <param name="value">Value of title.</param>
        /// <returns>
        /// Select list with enum possible values
        /// </returns>
        public static IList<SelectListItem> CreateSelectListItemsFor<T>(T selected, string title = null, string value = null)
        {
            Type type = typeof(T);
            var items = new List<SelectListItem>();
            Array options = null;
            if (IsNullable(selected, type))
            {
                items.Add(new SelectListItem { Text = title, Value = value, Selected = object.Equals(selected, null) });
                options = Enum.GetValues(Nullable.GetUnderlyingType(type));
            }
            else
            {
                options = Enum.GetValues(type);
            }

            foreach (var option in options)
            {
                int numericValue = Convert.ToInt32(option);
                var selectListItem = new SelectListItem { Text = EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(option).Name, Value = numericValue.ToString(), Selected = object.Equals(selected, option) };
                items.Add(selectListItem);
            }

            return items;
        }

        private static bool IsNullable<T>(T obj, Type type)
        {
            if (obj == null)
            {
                return true; // obvious
            }

            if (!type.IsValueType)
            {
                return true; // ref-type
            }

            if (Nullable.GetUnderlyingType(type) != null)
            {
                return true; // Nullable<T>
            }

            return false; // value-type
        }
    }
}
