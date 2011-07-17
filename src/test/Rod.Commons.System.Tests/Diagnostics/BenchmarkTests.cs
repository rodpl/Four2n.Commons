// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BenchmarkTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the BenchmarkTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.Diagnostics
{
    using global::System.Threading;

    using NUnit.Framework;

    [TestFixture]
    public class BenchmarkTests
    {
        [Test]
        public void Benchmark_ToDelegate()
        {
            string result = null;
            using (Benchmark.InMiliseconds().ToDelegate(x => result = x.ToString()))
            {
                Thread.Sleep(105);
            }
        
            StringAssert.StartsWith("10", result);
        }
    }
}