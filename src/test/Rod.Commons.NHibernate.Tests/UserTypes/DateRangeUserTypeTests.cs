// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateRangeUserTypeTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateRangeUserTypeTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.NHibernate.Tests.UserTypes
{
    using System.Diagnostics;

    using Domain;

    using global::System;
    using global::System.Collections;

    using NHibernate.UserTypes;

    using NUnit.Framework;

    using Rod.Commons.NHibernate.Tests;
    using Rod.Commons.System;

    [TestFixture]
    public class DateRangeUserTypeTests : NHibernateTestCase
    {
        protected override IList Mappings
        {
            get { return new[] { "Domain.DateRangeModel.hbm.xml" }; }
        }

        [Test]
        public void SimplePersistingTest()
        {
            var model = new DateRangeModel();
            model.DatePeriod = new DateRange(new DateTime(2000, 1, 1), new DateTime(2000, 1, 31));
            model.DateTimePeriod = new DateTimeRange(new DateTime(2000, 1, 1, 12, 0, 4), new DateTime(2000, 1, 31, 13, 1, 5));

            this.Session.Save(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<DateRangeModel>(model.Id);
            Assert.That(modelFromDb.DatePeriod.Equals(model.DatePeriod), Is.True);
            Assert.AreEqual(modelFromDb.DateTimePeriod.Begins, model.DateTimePeriod.Begins);
            Assert.That(modelFromDb.DateTimePeriod.Equals(model.DateTimePeriod), Is.True);
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
    }
}
