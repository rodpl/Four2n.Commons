using System;
using System.Web;
using rod.Commons.System.Diagnostics;

namespace rod.Commons.System.Web.Modules
{
    public class PagePerformanceModule : IHttpModule
    {
        private const string STR_TIMER_CONTEXT_KEY = "PagePerformanceTimer";

        #region IHttpModule Members
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextOnBeginRequest;
            context.EndRequest += ContextOnEndRequest;
        }
        #endregion

        #region public methods...
        public void ContextOnBeginRequest(object sender, EventArgs e)
        {
            var timer = new HiPerfTimer();
            timer.Start();
            HttpContext.Current.Items[STR_TIMER_CONTEXT_KEY] = timer;
        }

        public void ContextOnEndRequest(object sender, EventArgs e)
        {
            var context = HttpContext.Current;

            var timer = context.Items[STR_TIMER_CONTEXT_KEY] as HiPerfTimer;
            if (timer == null)
                return;

            if (!context.Response.ContentType.ToLower().Contains("text/html"))
                return;
            if (!context.Request.FilePath.EndsWith(".aspx") || !context.Request.FilePath.EndsWith(".sdm"))
                return;

            if (!String.IsNullOrEmpty(context.Request.Form["__CALLBACKID"]))
                return;

            timer.Stop();
            context.Response.Write(String.Format("<i class=\"performanceCounter\">This request took: {0} seconds.<i><br>", timer.Duration));
        }
        #endregion
    }
}