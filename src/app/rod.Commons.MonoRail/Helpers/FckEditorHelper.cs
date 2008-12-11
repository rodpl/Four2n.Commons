using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Castle.MonoRail.Framework.Helpers;

namespace Rod.Commons.MonoRail.Helpers
{
    /// <summary>
    /// Helper for FCKEditor.
    /// </summary>
    public class FckEditorHelper : AbstractHelper
    {
        public static readonly string STR_BASE_PATH = "/content/helpers/fckeditor/";
        private static readonly string user_path = "/user/";
        private static readonly string default_width = "100%";
        private static readonly string default_height = "400";
        private static readonly string default_toolbar = "Default";

        public virtual string InstallScripts()
        {
            return string.Format("<script type=\"text/javascript\" src=\"{0}fckeditor.js\"></script>", CurrentContext.ApplicationPath + STR_BASE_PATH);
        }

        public string CreateHtml(string instanceName)
        {
            return CreateHtml(instanceName, null, default_width, default_height, default_toolbar, null);
        }

        public string CreateHtml(string instanceName, string value)
        {
            return CreateHtml(instanceName, value, default_width, default_height, default_toolbar, null);
        }

        public string CreateHtml(string instanceName, string value, IDictionary parameters)
        {
            return CreateHtml(instanceName, value, default_width, default_height, default_toolbar, parameters);
        }

        public string CreateHtml(string instanceName, string value, string width,
            string height, string toolbarSet, IDictionary parameters)
        {
            string htmlValue = HtmlEncode(value);
            StringBuilder html = new StringBuilder();

            html.Append("<div>\n");

            bool isCompatibleBrowser;
            try
            {
                isCompatibleBrowser = IsCompatibleBrowser();
            }
            catch
            {
                isCompatibleBrowser = false;
            }

            if (isCompatibleBrowser)
            {
                string File = ("true" == CurrentContext.Request.QueryString["fcksource"]) ?
                    "fckeditor.original.html" : "fckeditor.html";

                string Link = string.Format("{0}editor/{1}?InstanceName={2}",
                                            CurrentContext.ApplicationPath
                                              + STR_BASE_PATH,
                                            File, instanceName);
                if (!string.IsNullOrEmpty(toolbarSet))
                {
                    Link += string.Format("&amp;Toolbar={0}", toolbarSet);
                }

                html.AppendFormat("<input type=\"hidden\" id=\"{0}\" name=\"{0}\" " +
                                  "value=\"{1}\" style=\"display:none\" />\n",
                                  instanceName, htmlValue);

                html.AppendFormat("<input type=\"hidden\" id=\"{0}___Config\" " +
                    "value=\"{1}\" style=\"display:none\" />\n",
                                  instanceName, BuildQueryString(parameters));

                html.AppendFormat("<iframe id=\"{0}__Frame\" src=\"{1}\" " +
                    "width=\"{2}\" height=\"{3}\" frameborder=\"0\" " +
                    "scrolling=\"no\"></iframe>\n",
                    instanceName, Link, width, height);
            }
            else
            {
                string WidthCSS = (width.IndexOf('%') > 0) ? width : (width + "px");
                string HeightCSS = (height.IndexOf('%') > 0) ? height : (height + "px");

                html.AppendFormat("<textarea name=\"{0}\" rows=\"4\" cols=\"40\" " +
                    "style=\"width: {1}; height: {2};\" wrap=\"virtual\">{3}</textarea>\n",
                    instanceName, WidthCSS, HeightCSS, htmlValue);
            }
            html.Append("</div>\n");

            return html.ToString();
        }

        public virtual bool IsCompatibleBrowser()
        {
            string agent = Context.Request.Params["HTTP_USER_AGENT"];

            // IE
            if (agent.IndexOf("MSIE") >= 0 && agent.IndexOf("Windows") >= 0 && agent.IndexOf("Opera") < 0)
            {
                var match = Regex.Match(agent, @"(?<=MSIE )[\d\.]+");
                return (match.Success && float.Parse(match.Value, CultureInfo.InvariantCulture) >= 5.5);
            }
            // Gecko based
            if (agent.IndexOf("Gecko/") >= 0 && agent.IndexOf("Opera") < 0)
            {
                var match = Regex.Match(agent, @"(?<=Gecko/)\d{8}");
                if (!match.Success)
                    return false;

                var geckoNumber = int.Parse(match.Value, CultureInfo.InvariantCulture);

                if(agent.IndexOf("Firefox/") >= 0)
                {
                    var fireMatch = Regex.Match(agent, @"(?<=Firefox/)[\d]+[.][\d]+");
                    return (fireMatch.Success && float.Parse(fireMatch.Value, CultureInfo.InvariantCulture) >= 1.5);
                }

                if ((agent.IndexOf("Netscape") >= 0 || agent.IndexOf("Navigator") >= 0) && geckoNumber >= 20030210)
                    return true;

                if (agent.IndexOf("Camino") >= 0 && geckoNumber >= 20060214)
                    return true;
            }

            if (agent.IndexOf("Opera") >= 0)
            {
                var match = Regex.Match(agent, @"(?<=Opera[ ,/])[\d\.]+");
                return (match.Success && float.Parse(match.Value, CultureInfo.InvariantCulture) >= 9.5);
            }

            if (agent.IndexOf("AppleWebKit/") >= 0)
            {
                var match = Regex.Match(agent, @"(?<=Version/)[\d]+[.][\d]+");
                return (match.Success && float.Parse(match.Value, CultureInfo.InvariantCulture) >= 3.0);
            }

            return false;
        }
    }
}