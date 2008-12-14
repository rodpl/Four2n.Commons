//------------------------------------------------------------------------------------------------- 
// <copyright file="HiPerfTimerTests.cs" company="Daniel Dabrowski - rod.blogsome.com">
// Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>Defines the HiPerfTimerTests type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Rod.Commons.System.Diagnostics
{
    using NUnit.Framework;

    [TestFixture]
    public class HiPerfTimerTests
    {
        [Test]
        public void SimpleTest()
        {
            var timer = new HiPerfTimer();

            // Initial duration is set to zero
            Assert.AreEqual(0, timer.Duration);

            timer.Start();

            // If we did not stop the timer, Duration sould be zero
            Assert.AreEqual(0, timer.Duration);

            timer.Stop();

            // After stop, Duration is > 0
            Assert.Less(0, timer.Duration);

            var lastTime = timer.Duration;

            // Duration should be the same as before
            Assert.AreEqual(lastTime, timer.Duration);

            timer.Stop();

            // Now it should be updated
            Assert.Less(lastTime, timer.Duration);

            timer.Start();

            // Should equals to zero
            Assert.AreEqual(0, timer.Duration);
            Assert.AreEqual("0 seconds", timer.ToString());
        }
    }
}