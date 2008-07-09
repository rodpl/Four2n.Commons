using System;
using Castle.MonoRail.Framework.Helpers;

namespace rod.Commons.MonoRail.Helpers
{
    /// <summary>
    /// Provides useful common methods to generate HTML elements.
    /// </summary>
    /// <remarks>
    /// All of this methods return <see cref="string"/> that holds resulting HTML code.
    /// </remarks>
    public class HtmlHelper : AbstractHelper
    {
        /// <summary>
        /// "Space" in Html notation.
        /// </summary>
        public const string NBSP = "&nbsp;";

        /// <summary>
        /// Returns "&amp;nbsp;" if input text is null empty or " ".
        /// </summary>
        /// <param name="text">Input text.</param>
        /// <returns>"&amp;nbsp;" or input text.</returns>
        /// <remarks>This method is very useful for filling table cells to avoid empty borders.</remarks>
        public string ToNbsp(string text)
        {
            if (String.IsNullOrEmpty(text))
                return NBSP;
            if (text.Trim().Length == 0)
                return text.Replace(" ", NBSP);
            return text;
        }

        /// <summary>
        /// Returns "&amp;nbsp;" if input text is null empty or " ".
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>"&amp;nbsp;" or input text.</returns>
        /// <remarks>This method is very useful for filling table cells to avoid empty borders.</remarks>
        public string ToNbsp(object input)
        {
            return input == null ? NBSP : ToNbsp(input.ToString());
        }
    }
}