using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace rod.Commons.System.Diagnostics
{
    /// <summary>
    /// Hi-performance timer.
    /// </summary>
    public class HiPerfTimer
    {
        private readonly long freq;
        private long startTime, stopTime;

        public HiPerfTimer()
        {
            startTime = 0;
            stopTime = 0;

            if (QueryPerformanceFrequency(out freq) == false)
                throw new Win32Exception("High performance counter is not supported on this machine.");
        }

        /// <summary>
        /// Gets the duration of the timer (in seconds).
        /// </summary>
        /// <value>The duration in seconds.</value>
        public double Duration
        {
            get
            {
                if (stopTime <= startTime)
                    return 0d;
                return (stopTime - startTime)/(double) freq;
            }
        }

        /// <summary>
        /// Starts timer.
        /// </summary>
        public void Start()
        {
            // lets do the waiting threads there work
            Thread.Sleep(0);

            QueryPerformanceCounter(out startTime);
        }

        /// <summary>
        /// Stops timer.
        /// </summary>
        public void Stop()
        {
            QueryPerformanceCounter(out stopTime);
        }

        public override string ToString()
        {
            return string.Format("{0} seconds", Duration);
        }

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);
    }
}