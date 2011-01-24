// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NicEditHelper.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the NicEditHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.MonoRail.Helpers
{
    using System.Text;

    using Castle.MonoRail.Framework.Helpers;

    /// <summary>
    /// Helper for NicEditor. http://www.nicedit.com/.
    /// </summary>
    public class NicEditHelper : AbstractHelper
    {
        /// <summary>
        /// Creates the HTML.
        /// </summary>
        /// <param name="instanceName">Name of the instance.</param>
        /// <returns>HTML rendered string.</returns>
        public string CreateHtml(string instanceName)
        {
            var html = new StringBuilder();
            html.Append("<div>\n</div>\n");
            return html.ToString();
        }
    }
}