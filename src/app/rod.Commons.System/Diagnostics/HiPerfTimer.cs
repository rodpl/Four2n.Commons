//------------------------------------------------------------------------------------------------- 
// <copyright file="HiPerfTimer.cs" company="Daniel Dabrowski - rod.blogsome.com">
// Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>Defines the HiPerfTimer type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Rod.Commons.System.Diagnostics
{
    using global::System;
    using global::System.ComponentModel;
    using global::System.Runtime.InteropServices;
    using global::System.Threading;

    /// <summary>
    /// Hi-performance timer.
    /// </summary>
    [Obsolete("Use System.Diagnostic.Stopwatch instead of this.")]
    public class HiPerfTimer
    {
        /// <summary>
        /// Clock's frequency.
        /// </summary>
        private readonly long freq;

        /// <summary>
        /// Start and stop time.
        /// </summary>
        private long startTime, stopTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="HiPerfTimer"/> class.
        /// </summary>
        /// <exception cref="Win32Exception">High performance counter is not supported on this machine.</exception>
        public HiPerfTimer()
        {
            this.startTime = 0;
            this.stopTime = 0;

            if (QueryPerformanceFrequency(out this.freq) == false)
            {
                throw new Win32Exception("High performance counter is not supported on this machine.");
            }
        }

        /// <summary>
        /// Gets the duration of the timer (in seconds).
        /// </summary>
        /// <value>The duration in seconds.</value>
        public double Duration
        {
            get { return this.stopTime <= this.startTime ? 0d : (this.stopTime - this.startTime) / (double)this.freq; }
        }

        /// <summary>
        /// Starts timer.
        /// </summary>
        public void Start()
        {
            // lets do the waiting threads there work
            Thread.Sleep(0);

            QueryPerformanceCounter(out this.startTime);
        }

        /// <summary>
        /// Stops timer.
        /// </summary>
        public void Stop()
        {
            QueryPerformanceCounter(out this.stopTime);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} seconds", this.Duration);
        }

        /// <summary>
        /// Queries the performance counter.
        /// </summary>
        /// <param name="performanceCount">The performance count.</param>
        /// <returns>True if command runs correctly.</returns>
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long performanceCount);

        /// <summary>
        /// Queries the performance frequency.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <returns>True if command runs correctly.</returns>
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long frequency);
    }
}