using System.Collections;
using System.Text;
using Castle.MonoRail.Framework.Helpers;

namespace rod.Commons.MonoRail.Helpers
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

        public string InstallScripts()
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
            if (IsCompatibleBrowser())
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

        public bool IsCompatibleBrowser()
        {
            //            string Agent = Controller.Params["HTTP_USER_AGENT"];
            //
            //            if((Agent.IndexOf("MSIE") >= 0) && (Agent.IndexOf("mac") < 0)
            //                && (Agent.IndexOf("Opera") < 0))
            //            {
            //                int p = Agent.IndexOf("MSIE");
            //                double v = Convert.ToDouble(Agent.Substring(p + 5, 3));
            //                return v >= 5.5;
            //            }
            //            else if (Agent.IndexOf("Gecko/") >= 0)
            //            {
            //                int p = Agent.IndexOf("Gecko/");
            //                int v = Convert.ToInt32(Agent.Substring(p + 6, 8));
            //                return v >= 20030210;
            //            }
            //            else
            //            {
            //                return false;
            //            }

            return true;
        }
    } 
}