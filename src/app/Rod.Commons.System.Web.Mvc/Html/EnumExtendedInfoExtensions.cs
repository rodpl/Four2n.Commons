// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtendedInfoExtensions.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the EnumExtendedInfoExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.Web.Mvc.Html
{
    using global::System;
    using global::System.Collections;
    using global::System.Collections.Generic;
    using global::System.Linq.Expressions;
    using global::System.Text;
    using global::System.Web.Mvc;

    /// <summary>
    /// Mvc extensions for <see cref="EnumExtendedInfoAttribute"/>.
    /// </summary>
    public static class EnumExtendedInfoExtensions
    {
        /// <summary>
        /// Enums the value info dropdown.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name of control.</param>
        /// <param name="value">The value.</param>
        /// <returns>Mvc string</returns>
        public static MvcHtmlString DropDownListEnumExtendedInfo<TValue>(this HtmlHelper helper, string name, TValue value)
                where TValue : struct
        {
            return DropDownListEnumExtendedInfoInternal(helper, name, value, false, Enum.GetValues(typeof(TValue)));
        }

        /// <summary>
        /// Enums the value info dropdown.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name of control.</param>
        /// <param name="value">The value.</param>
        /// <param name="choices">The choices.</param>
        /// <returns>Mvc string</returns>
        public static MvcHtmlString DropDownListEnumExtendedInfo<TValue>(this HtmlHelper helper, string name, TValue value, IEnumerable<TValue> choices)
            where TValue : struct
        {
            return DropDownListEnumExtendedInfoInternal(helper, name, value, false, choices);
        }

        /// <summary>
        /// Enums the value info dropdown.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name of control.</param>
        /// <param name="value">The value.</param>
        /// <returns>Mvc string</returns>
        public static MvcHtmlString DropDownListEnumExtendedInfo<TValue>(this HtmlHelper helper, string name, TValue? value)
                where TValue : struct
        {
            return DropDownListEnumExtendedInfoInternal(helper, name, value, true, Enum.GetValues(typeof(TValue)));
        }

        /// <summary>
        /// Enums the value info dropdown.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name of control.</param>
        /// <param name="value">The value.</param>
        /// <param name="choices">The choices.</param>
        /// <returns>Mvc string</returns>
        public static MvcHtmlString DropDownListEnumExtendedInfo<TValue>(this HtmlHelper helper, string name, TValue? value, IEnumerable<TValue> choices)
            where TValue : struct
        {
            return DropDownListEnumExtendedInfoInternal(helper, name, value, true, choices);
        }

        /// <summary>
        /// Enums the value info dropdown for.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="expression">The expression.</param>
        /// <returns> Mvc string </returns>
        public static MvcHtmlString DropDownListEnumExtendedInfoFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
            where TValue : struct
        {
            var data = expression.Compile()(helper.ViewData.Model);
            var memberExpression = expression.Body as MemberExpression;
            return DropDownListEnumExtendedInfoInternal(helper, memberExpression.Member.Name, data, false, Enum.GetValues(expression.Body.Type));
        }

        /// <summary>
        /// Enums the value info dropdown.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="expression">Tree expression.</param>
        /// <returns> Mvc string </returns>
        public static MvcHtmlString DropDownListEnumExtendedInfoFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue?>> expression)
            where TValue : struct
        {
            var data = expression.Compile()(helper.ViewData.Model);
            var memberExpression = expression.Body as MemberExpression;
            return DropDownListEnumExtendedInfoInternal(helper, memberExpression.Member.Name, data, true, Enum.GetValues(Nullable.GetUnderlyingType(expression.Body.Type)));
        }

        /// <summary>
        /// Enums the value info dropdown internal.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name of control.</param>
        /// <param name="value">The value.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <param name="choices">The choices.</param>
        /// <returns>Mvc string</returns>
        internal static MvcHtmlString DropDownListEnumExtendedInfoInternal(HtmlHelper helper, string name, object value, bool isNullable, IEnumerable choices)
        {
            StringBuilder build = new StringBuilder();
            build.AppendFormat("<select name='{0}'>", helper.Encode(name));
            if (choices != null)
            {
                if (isNullable)
                {
                    build.AppendOption(string.Empty, string.Empty, value == null);
                }

                foreach (var choice in choices)
                {
                    build.AppendOption(
                            Convert.ToInt32(choice),
                            helper.Encode(EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(choice).Name),
                            object.Equals(value, choice));
                }
            }

            build.Append("</select>");
            return MvcHtmlString.Create(build.ToString());
        }

        /// <summary>
        /// Appends the option.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="value">The value.</param>
        /// <param name="text">The text to display.</param>
        /// <param name="isSelected">if set to <c>true</c> [is selected].</param>
        private static void AppendOption(this StringBuilder builder, object value, string text, bool isSelected)
        {
            builder.AppendFormat(
                "<option value=\"{0}\"{2}>{1}</option>",
                value,
                text,
                isSelected ? " selected='selected'" : string.Empty);
        }
    }
}
