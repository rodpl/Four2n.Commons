// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateRangeTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateRangeTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Text;

    using NUnit.Framework;

    [TestFixture]
    public class DateRangeTests : DateTimeRangeGenericTestCase<DateRange>
    {
        [Test]
        public void Ctor_WithGreaterStartDateThenEndDateButWithinTheSameDay_DoesNotThrowsException()
        {
            var sut = new DateRange(new DateTime(2000, 1, 1, 12, 0, 1), new DateTime(2000, 1, 1, 12, 0, 0));
            Assert.That(sut.Begins, Is.EqualTo(new DateTime(2000, 1, 1)));
            Assert.That(sut.Ends, Is.EqualTo(new DateTime(2000, 1, 2).AddMilliseconds(-1)));
        }

        [Test]
        [TestCase("2000-01-01 12:00:00", "2000-01-02 12:00:00", "2000-01-01 13:00:00", "2000-01-02 13:00:00", true)]
        public void Equals_TwoObjects_ReturnsExpected_Exceptions(string startOne, string endOne, string startTwo, string endTwo, bool expected)
        {
            this.Equals_TwoObjects_ReturnsExpected(startOne, endOne, startTwo, endTwo, expected);
        }

        protected override DateRange Create(DateTime? startDate, DateTime? endDate)
        {
            return new DateRange(startDate, endDate);
        }

        protected override bool EqualsStatic(object sutOne, object sutTwo)
        {
            return DateRange.Equals(sutOne, sutTwo);
        }
    }
}
