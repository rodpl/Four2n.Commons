// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FckEditorHelper.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the FckEditorHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.MonoRail.Helpers
{
    using System.Collections;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    using Castle.MonoRail.Framework.Helpers;

    /// <summary>
    /// Helper for FCKEditor.
    /// </summary>
    public class FckEditorHelper : AbstractHelper
    {
        /// <summary>
        /// Base path for FCK Editor javascript files location.
        /// </summary>
        public static readonly string BasePath = "/content/helpers/fckeditor/";

        /// <summary>
        /// Default height of the editor
        /// </summary>
        private const string DefaultHeight = "400";

        /// <summary>
        /// Default name of the toolbar
        /// </summary>
        private const string DefaultToolbar = "Default";

        /// <summary>
        /// Default width of the editor.
        /// </summary>
        private const string DefaultWidth = "100%";

        /// <summary>
        /// Creates the HTML.
        /// </summary>
        /// <param name="instanceName">Name of the instance.</param>
        /// <returns>HTML which displays editor.</returns>
        public string CreateHtml(string instanceName)
        {
            return this.CreateHtml(instanceName, null, DefaultWidth, DefaultHeight, DefaultToolbar, null);
        }

        /// <summary>
        /// Creates the HTML.
        /// </summary>
        /// <param name="instanceName">Name of the instance.</param>
        /// <param name="value">The value.</param>
        /// <returns>HTML which displays editor.</returns>
        public string CreateHtml(string instanceName, string value)
        {
            return this.CreateHtml(instanceName, value, DefaultWidth, DefaultHeight, DefaultToolbar, null);
        }

        /// <summary>
        /// Creates the HTML.
        /// </summary>
        /// <param name="instanceName">Name of the instance.</param>
        /// <param name="value">The value.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>HTML which displays editor.</returns>
        public string CreateHtml(string instanceName, string value, IDictionary parameters)
        {
            return this.CreateHtml(instanceName, value, DefaultWidth, DefaultHeight, DefaultToolbar, parameters);
        }

        /// <summary>
        /// Creates the HTML.
        /// </summary>
        /// <param name="instanceName">Name of the instance.</param>
        /// <param name="value">The value.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="toolbarSet">The toolbar set.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>HTML which displays editor.</returns>
        public string CreateHtml(
            string instanceName,
            string value,
            string width,
            string height,
            string toolbarSet,
            IDictionary parameters)
        {
            string htmlValue = this.HtmlEncode(value);
            StringBuilder html = new StringBuilder();

            html.Append("<div>\n");

            bool isCompatibleBrowser;
            try
            {
                isCompatibleBrowser = this.IsCompatibleBrowser();
            }
            catch
            {
                isCompatibleBrowser = false;
            }

            if (isCompatibleBrowser)
            {
                string file = ("true" == this.CurrentContext.Request.QueryString["fcksource"]) ?
                    "fckeditor.original.html" : "fckeditor.html";

                string link = string.Format(
                    "{0}editor/{1}?InstanceName={2}",
                    this.CurrentContext.ApplicationPath + BasePath,
                    file,
                    instanceName);

                if (!string.IsNullOrEmpty(toolbarSet))
                {
                    link += string.Format("&amp;Toolbar={0}", toolbarSet);
                }

                html.AppendFormat(
                    "<input type=\"hidden\" id=\"{0}\" name=\"{0}\" " +
                    "value=\"{1}\" style=\"display:none\" />\n",
                    instanceName,
                    htmlValue);

                html.AppendFormat(
                    "<input type=\"hidden\" id=\"{0}___Config\" " +
                    "value=\"{1}\" style=\"display:none\" />\n",
                    instanceName,
                    BuildQueryString(parameters));

                html.AppendFormat(
                    "<iframe id=\"{0}__Frame\" src=\"{1}\" " +
                    "width=\"{2}\" height=\"{3}\" frameborder=\"0\" " +
                    "scrolling=\"no\"></iframe>\n",
                    instanceName,
                    link,
                    width,
                    height);
            }
            else
            {
                string widthCss = (width.IndexOf('%') > 0) ? width : (width + "px");
                string heightCss = (height.IndexOf('%') > 0) ? height : (height + "px");

                html.AppendFormat(
                    "<textarea name=\"{0}\" rows=\"4\" cols=\"40\" " +
                    "style=\"width: {1}; height: {2};\" wrap=\"virtual\">{3}</textarea>\n",
                    instanceName,
                    widthCss,
                    heightCss,
                    htmlValue);
            }

            html.Append("</div>\n");
            return html.ToString();
        }

        /// <summary>
        /// Installs the scripts.
        /// </summary>
        /// <returns>Html output which installs scripts.</returns>
        public virtual string InstallScripts()
        {
            return string.Format("<script type=\"text/javascript\" src=\"{0}fckeditor.js\"></script>", this.CurrentContext.ApplicationPath + BasePath);
        }

        /// <summary>
        /// Determines whether [is compatible browser].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is compatible browser]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsCompatibleBrowser()
        {
            string agent = this.Context.Request.Params["HTTP_USER_AGENT"];

            // IE
            if (agent.IndexOf("MSIE") >= 0 && agent.IndexOf("Windows") >= 0 && agent.IndexOf("Opera") < 0)
            {
                var match = Regex.Match(agent, @"(?<=MSIE )[\d\.]+");
                return match.Success && float.Parse(match.Value, CultureInfo.InvariantCulture) >= 5.5;
            }

            // Gecko based
            if (agent.IndexOf("Gecko/") >= 0 && agent.IndexOf("Opera") < 0)
            {
                var match = Regex.Match(agent, @"(?<=Gecko/)\d{8}");
                if (!match.Success)
                {
                    return false;
                }

                var geckoNumber = int.Parse(match.Value, CultureInfo.InvariantCulture);

                if (agent.IndexOf("Firefox/") >= 0)
                {
                    var fireMatch = Regex.Match(agent, @"(?<=Firefox/)[\d]+[.][\d]+");
                    return fireMatch.Success && float.Parse(fireMatch.Value, CultureInfo.InvariantCulture) >= 1.5;
                }

                if ((agent.IndexOf("Netscape") >= 0 || agent.IndexOf("Navigator") >= 0) && geckoNumber >= 20030210)
                {
                    return true;
                }

                if (agent.IndexOf("Camino") >= 0 && geckoNumber >= 20060214)
                {
                    return true;
                }
            }

            if (agent.IndexOf("Opera") >= 0)
            {
                var match = Regex.Match(agent, @"(?<=Opera[ ,/])[\d\.]+");
                return match.Success && float.Parse(match.Value, CultureInfo.InvariantCulture) >= 9.5;
            }

            if (agent.IndexOf("AppleWebKit/") >= 0)
            {
                var match = Regex.Match(agent, @"(?<=Version/)[\d]+[.][\d]+");
                return match.Success && float.Parse(match.Value, CultureInfo.InvariantCulture) >= 3.0;
            }

            return false;
        }
    }
}