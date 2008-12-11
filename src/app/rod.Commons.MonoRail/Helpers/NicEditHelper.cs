using System.Text;
using Castle.MonoRail.Framework.Helpers;

namespace rod.Commons.MonoRail.Helpers
{
    public class NicEditHelper : AbstractHelper
    {
        public string CreateHtml(string instanceName)
        {
            var html = new StringBuilder();
            html.Append("<div>\n</div>\n");
            return html.ToString();
        }
    }
}