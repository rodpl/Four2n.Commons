// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BenchmarkTests.cs" company="Daniel Dabrowski - rod.blogsome.com">
//   Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
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
                Thread.Sleep(100);
            }
        
            StringAssert.StartsWith("10", result);
        }
    }
}