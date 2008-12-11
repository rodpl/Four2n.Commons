using NUnit.Framework;
using Rod.Commons.System.Diagnostics;

namespace Rod.Commons.System.Diagnostics
{
    [TestFixture]
    public class HiPerfTimerTests
    {
        [Test]
        public void SimpleTest()
        {
            var hiPerfTimer = new HiPerfTimer();

            // Initial duration is set to zero
            Assert.AreEqual(0, hiPerfTimer.Duration);

            hiPerfTimer.Start();
            // If we did not stop the timer, Duration sould be zero
            Assert.AreEqual(0, hiPerfTimer.Duration);

            hiPerfTimer.Stop();
            // After stop, Duration is > 0
            Assert.Less(0, hiPerfTimer.Duration);

            var lastTime = hiPerfTimer.Duration;
            // Duration should be the same as before
            Assert.AreEqual(lastTime, hiPerfTimer.Duration);

            hiPerfTimer.Stop();
            // Now it should be updated
            Assert.Less(lastTime, hiPerfTimer.Duration);

            hiPerfTimer.Start();
            // Should equals to zero
            Assert.AreEqual(0, hiPerfTimer.Duration);
            Assert.AreEqual("0 seconds", hiPerfTimer.ToString());
        }
    }
}