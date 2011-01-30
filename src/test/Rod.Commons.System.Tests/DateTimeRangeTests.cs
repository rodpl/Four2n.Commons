// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeRangeTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateTimeRangeTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System
{
    using global::System;

    using NUnit.Framework;

    [TestFixture]
    public class DateTimeRangeTests : DateTimeRangeGenericTestCase<DateTimeRange>
    {
        [Test]
        public void Ctor_WithGreaterStartDateThenEndDateButWithinTheSameDay_ThrowsException()
        {
            Assert.That(
                    () => new DateTimeRange(new DateTime(2000, 1, 1, 12, 0, 1), new DateTime(2000, 1, 1, 12, 0, 0)),
                    Throws.TypeOf<ArgumentOutOfRangeException>().With.Message.ContainsSubstring("greater"));
        }

        [Test]
        [TestCase("2000-01-01 12:00:00", "2000-01-02 12:00:00", "2000-01-01 13:00:00", "2000-01-02 13:00:00", false)]
        public void Equals_TwoObjects_ReturnsExpected_Exceptions(string startOne, string endOne, string startTwo, string endTwo, bool expected)
        {
            this.Equals_TwoObjects_ReturnsExpected(startOne, endOne, startTwo, endTwo, expected);
        }

        protected override DateTimeRange Create(DateTime? startDate, DateTime? endDate)
        {
            return new DateTimeRange(startDate, endDate);
        }

        protected override bool EqualsStatic(object sutOne, object sutTwo)
        {
            return DateTimeRange.Equals(sutOne, sutTwo);
        }
    }
}