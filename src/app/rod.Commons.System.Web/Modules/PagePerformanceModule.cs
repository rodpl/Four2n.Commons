//------------------------------------------------------------------------------------------------- 
// <copyright file="PagePerformanceModule.cs" company="Daniel Dabrowski - rod.blogsome.com">
// Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>Defines the PagePerformanceModule type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Rod.Commons.System.Web.Modules
{
    using global::System;
    using global::System.Diagnostics;
    using global::System.Web;

    using Rod.Commons.System.Diagnostics;

    /// <summary>
    /// Http Module for counting how much log time will page process
    /// and write result at the end of the page.
    /// </summary>
    public class PagePerformanceModule : IHttpModule
    {
        /// <summary>
        /// Constants key name for timer value in Context
        /// </summary>
        private const string TimerContextKey = "PagePerformanceTimer";

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.ContextOnBeginRequest;
            context.EndRequest += this.ContextOnEndRequest;
        }

        /// <summary>
        /// Contexts the on begin request.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void ContextOnBeginRequest(object sender, EventArgs e)
        {
            var timer = new Stopwatch();
            timer.Start();
            HttpContext.Current.Items[TimerContextKey] = timer;
        }

        /// <summary>
        /// Contexts the on end request.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void ContextOnEndRequest(object sender, EventArgs e)
        {
            var context = HttpContext.Current;

            var timer = context.Items[TimerContextKey] as Stopwatch;
            if (timer == null)
            {
                return;
            }

            if (!context.Response.ContentType.ToLower().Contains("text/html"))
            {
                return;
            }

            if (!context.Request.FilePath.EndsWith(".aspx") || !context.Request.FilePath.EndsWith(".sdm"))
            {
                return;
            }

            if (!String.IsNullOrEmpty(context.Request.Form["__CALLBACKID"]))
            {
                return;
            }

            timer.Stop();
            context.Response.Write(String.Format("<i class=\"performanceCounter\">This request took: {0} seconds.<i><br>", timer.ElapsedMilliseconds / 1000d));
        }
    }
}