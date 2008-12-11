//------------------------------------------------------------------------------------------------- 
// <copyright file="PagePerformanceModule.cs" company="Daniel Dabrowski - rod.blogsome.com">
// Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>Defines the PagePerformanceModule type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Rod.Commons.System.Web.Modules
{
    using global::System;
    using global::System.Web;

    using Diagnostics;

    /// <summary>
    /// </summary>
    public class PagePerformanceModule : IHttpModule
    {
        /// <summary>
        /// </summary>
        private const string STR_TIMER_CONTEXT_KEY = "PagePerformanceTimer";

        /// <summary>
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.ContextOnBeginRequest;
            context.EndRequest += this.ContextOnEndRequest;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param><param name="e"></param>
        public void ContextOnBeginRequest(object sender, EventArgs e)
        {
            var timer = new HiPerfTimer();
            timer.Start();
            HttpContext.Current.Items[STR_TIMER_CONTEXT_KEY] = timer;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param><param name="e"></param>
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
    }
}