// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateRangeUserTypeTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateRangeUserTypeTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.NHibernate.Tests.UserTypes
{
    using global::System;

    using global::System.Collections;

    using Four2n.Commons.NHibernate.Tests.Domain;
    using Four2n.Commons.NHibernate.UserTypes;
    using Four2n.Commons.System;
    using Four2n.Commons.System.Diagnostics;

    using NUnit.Framework;

    [TestFixture]
    public class DateRangeUserTypeTests : UserTypeTests<DateRangeUserType>
    {
        [Test]
        [Explicit]
        public void PerformanceTest()
        {
            using (Benchmark.InMiliseconds().ToConsole("Performance: {0}"))
            {
                for (int i = 0; i < 10000000; i++)
                {
                    var userType = new DateTimeRangeUserType();
                    var names = userType.PropertyNames;
                    var types = userType.PropertyTypes;
                    var @class = userType.ReturnedClass;
                }
            }
        }

        [Test]
        [TestCase(typeof(DateRange), "2000-1-1", "2000-1-31")]
        [TestCase(typeof(DateRange), "2000-1-1", null)]
        [TestCase(typeof(DateRange), null, "2000-1-31")]
        [TestCase(typeof(DateTimeRange), "2000-1-1 12:00:04", "2000-1-31 13:01:05")]
        [TestCase(typeof(DateTimeRange), null, "2000-1-31 13:01:05")]
        [TestCase(typeof(DateTimeRange), "2000-1-1 12:00:04", null)]
        [TestCase(typeof(DateRange), null, null)]
        [TestCase(typeof(DateTimeRange), null, null)]
        public void SaveModelTest(Type type, string beginsString, string endsString)
        {
            var model = new DateRangeModel();
            DateTime? begins = beginsString == null ? (DateTime?)null : DateTime.Parse(beginsString);
            DateTime? ends = endsString == null ? (DateTime?)null : DateTime.Parse(endsString);

            model.DatePeriod = new DateRange(begins, ends);
            model.DateTimePeriod = new DateTimeRange(begins, ends);

            this.Session.Save(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<DateRangeModel>(model.Id);
            Assert.That(model.Id, Is.GreaterThan(0));

            if (typeof(DateRange) == type)
            {
                Assert.AreEqual(modelFromDb.DatePeriod, model.DatePeriod);
            }
            else if (typeof(DateTimeRange) == type)
            {
                Assert.AreEqual(modelFromDb.DateTimePeriod, model.DateTimePeriod);
            }

            // Check if get and flush doesn't update entity
            var actualVersion = modelFromDb.Version;
            this.Session.Flush();
            this.Session.Clear();
            var modelAfterFlush = this.Session.Get<DateRangeModel>(model.Id);
            Assert.AreEqual(actualVersion, modelAfterFlush.Version);
        }

        [Test]
        [TestCase(typeof(DateRange), "2000-1-1", "2000-1-31", "2001-1-1", "2001-1-31")]
        [TestCase(typeof(DateRange), "2000-1-1", null, "2001-1-1", "2001-1-31")]
        [TestCase(typeof(DateRange), null, "2000-1-31", "2001-1-1", "2001-1-31")]
        [TestCase(typeof(DateTimeRange), "2000-1-1 12:00:04", "2000-1-31 13:01:05", "2001-1-1 12:00:04", "2001-1-31 13:01:05")]
        [TestCase(typeof(DateTimeRange), null, "2000-1-31 13:01:05", "2001-1-1 12:00:04", "2001-1-31 13:01:05")]
        [TestCase(typeof(DateTimeRange), "2000-1-1 12:00:04", null, "2001-1-1 12:00:04", "2001-1-31 13:01:05")]
        [TestCase(typeof(DateRange), null, null, null, null)]
        [TestCase(typeof(DateTimeRange), null, null, null, null)]
        public void SaveOrUpdateCopyModelTest(Type type, string beginsString, string endsString, string beginsTwoString, string endsTwoString)
        {
            var model = new DateRangeModel();
            DateTime? begins = beginsString == null ? (DateTime?)null : DateTime.Parse(beginsString);
            DateTime? ends = endsString == null ? (DateTime?)null : DateTime.Parse(endsString);

            DateTime? beginsTwo = beginsTwoString == null ? (DateTime?)null : DateTime.Parse(beginsTwoString);
            DateTime? endsTwo = endsTwoString == null ? (DateTime?)null : DateTime.Parse(endsTwoString);

            model.DatePeriod = new DateRange(begins, ends);
            model.DateTimePeriod = new DateTimeRange(begins, ends);

            this.Session.Save(model);
            this.Session.Evict(model);

            this.Session.SaveOrUpdateCopy(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<DateRangeModel>(model.Id);
            Assert.That(model.Id, Is.GreaterThan(0));

            if (typeof(DateRange).Equals(type))
            {
                Assert.AreEqual(modelFromDb.DatePeriod, model.DatePeriod);
            }
            else if (typeof(DateTimeRange).Equals(type))
            {
                Assert.AreEqual(modelFromDb.DateTimePeriod, model.DateTimePeriod);
            }

            model.DatePeriod = new DateRange(beginsTwo, endsTwo);
            model.DateTimePeriod = new DateTimeRange(beginsTwo, endsTwo);
            this.Session.SaveOrUpdateCopy(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDbTwo = this.Session.Get<DateRangeModel>(model.Id);
            Assert.That(model.Id, Is.GreaterThan(0));

            if (typeof(DateRange).Equals(type))
            {
                Assert.AreEqual(modelFromDbTwo.DatePeriod, model.DatePeriod);
            }
            else if (typeof(DateTimeRange).Equals(type))
            {
                Assert.AreEqual(modelFromDbTwo.DateTimePeriod, model.DateTimePeriod);
            }
        }

        [Test]
        [Ignore("Fix later")]
        public void Save_WithDateMinAndDateMax()
        {
            var model = new DateRangeModel();
            model.DatePeriod = new DateRange(DateTime.MinValue, DateTime.MaxValue);
            model.DateTimePeriod = new DateTimeRange(DateTime.MinValue, DateTime.MaxValue);

            this.Session.Save(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<DateRangeModel>(model.Id);
            Assert.That(modelFromDb.DatePeriod.Equals(model.DatePeriod), Is.True);
            Assert.AreEqual(modelFromDb.DateTimePeriod.Begins, model.DateTimePeriod.Begins);
            Assert.That(modelFromDb.DateTimePeriod.Equals(model.DateTimePeriod), Is.True);
        }

        protected override IList Mappings
        {
            get { return new[] { "Domain.DateRangeModel.hbm.xml" }; }
        }

        protected override void OnSetUp()
        {
            base.OnSetUp();
            Sut = new DateRangeUserType();
        }
    }
}
