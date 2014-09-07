// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtendedInfoExtensions.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the EnumExtendedInfoExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System.Web.Mvc.Html
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
        /// <param name="html">The html.</param>
        /// <param name="expression">The name of control.</param>
        /// <param name="value">The value.</param>
        /// <returns>Mvc string</returns>
        public static MvcHtmlString DropDownListEnumExtendedInfo<TValue>(
            this HtmlHelper html,
            string expression,
            TValue value,
            object htmlAttributes = null) where TValue : struct
        {
            return DropDownListEnumExtendedInfoInternal(
                html,
                expression,
                value,
                false,
                Enum.GetValues(typeof(TValue)),
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Enums the value info dropdown.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The html.</param>
        /// <param name="expression">The name of control.</param>
        /// <param name="value">The value.</param>
        /// <param name="choices">The choices.</param>
        /// <returns>Mvc string</returns>
        public static MvcHtmlString DropDownListEnumExtendedInfo<TValue>(
            this HtmlHelper html,
            string expression,
            TValue value,
            IEnumerable<TValue> choices,
            object htmlAttributes = null)
                where TValue : struct
        {
            return DropDownListEnumExtendedInfoInternal(
                html,
                expression,
                value,
                false,
                choices,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Enums the value info dropdown.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The html.</param>
        /// <param name="expression">The name of control.</param>
        /// <param name="value">The value.</param>
        /// <returns>Mvc string</returns>
        public static MvcHtmlString DropDownListEnumExtendedInfo<TValue>(
            this HtmlHelper html,
            string expression,
            TValue? value,
            object htmlAttributes = null) where TValue : struct
        {
            return DropDownListEnumExtendedInfoInternal(
                html,
                expression,
                value,
                true,
                Enum.GetValues(typeof(TValue)),
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Enums the value info dropdown.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The html.</param>
        /// <param name="expression">The name of control.</param>
        /// <param name="value">The value.</param>
        /// <param name="choices">The choices.</param>
        /// <returns>Mvc string</returns>
        public static MvcHtmlString DropDownListEnumExtendedInfo<TValue>(
            this HtmlHelper html,
            string expression,
            TValue? value,
            IEnumerable<TValue> choices,
            object htmlAttributes = null) where TValue : struct
        {
            return DropDownListEnumExtendedInfoInternal(
                html,
                expression,
                value,
                true,
                choices,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Enums the value info dropdown for.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The html.</param>
        /// <param name="expression">The expression.</param>
        /// <returns> Mvc string </returns>
        public static MvcHtmlString DropDownListEnumExtendedInfoFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null) where TValue : struct
        {
            var data = expression.Compile()(html.ViewData.Model);
            return DropDownListEnumExtendedInfoInternal(
                html,
                ExpressionHelper.GetExpressionText(expression),
                data,
                false,
                Enum.GetValues(expression.Body.Type),
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Enums the value info dropdown.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The html.</param>
        /// <param name="expression">Tree expression.</param>
        /// <returns> Mvc string </returns>
        public static MvcHtmlString DropDownListEnumExtendedInfoFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue?>> expression,
            object htmlAttributes = null) where TValue : struct
        {
            var data = expression.Compile()(html.ViewData.Model);
            return DropDownListEnumExtendedInfoInternal(
                html,
                ExpressionHelper.GetExpressionText(expression),
                data,
                true,
                Enum.GetValues(Nullable.GetUnderlyingType(expression.Body.Type)),
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Enums the value info dropdown internal.
        /// </summary>
        /// <param name="htmlHelper">The html. </param>
        /// <param name="name"> The name. </param>
        /// <param name="value"> The value. </param>
        /// <param name="isNullable"> The is Nullable. </param>
        /// <param name="choices"> The choices. </param>
        /// <param name="htmlAttributes">Html attributes.</param>
        /// <returns>
        /// Mvc string
        /// </returns>
        internal static MvcHtmlString DropDownListEnumExtendedInfoInternal(
            HtmlHelper htmlHelper,
            string name,
            object value,
            bool isNullable,
            IEnumerable choices,
            IDictionary<string, object> htmlAttributes = null)
        {
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("Null or empty name", "name");
            }

            var listItemBuilder = BuildItems(htmlHelper, value, isNullable, choices);
            var tagBuilder = new TagBuilder("select")
            {
                InnerHtml = listItemBuilder.ToString()
            };

            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("name", fullName, true);
            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }

        private static StringBuilder BuildItems(
            HtmlHelper html,
            object value,
            bool isNullable,
            IEnumerable choices)
        {
            StringBuilder build = new StringBuilder();
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
                        html.Encode(EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(choice).Name),
                        object.Equals(value, choice));
                }
            }

            return build;
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
