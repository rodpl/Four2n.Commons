// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlagEnumWithExtendedInfoUserTypeTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the FlagEnumWithExtendedInfoUserTypeTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.NHibernate.Tests.UserTypes
{
    using System;

    using Domain;

    using global::System.Collections;

    using NUnit.Framework;

    [TestFixture]
    public class FlagEnumWithExtendedInfoUserTypeTests : NHibernateTestCase
    {
        protected override IList Mappings
        {
            get { return new[] { "Domain.FlagEnumExtendedModel.hbm.xml" }; }
        }

        [Test]
        public void DefaultValue_Persistance()
        {
            var model = new FlagEnumExtendedModel();

            this.Session.Save(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<FlagEnumExtendedModel>(model.Id);
            Assert.That(modelFromDb.SampleEnum, Is.EqualTo((ExtendedTestFlagEnum)0));
            Assert.That(modelFromDb.SampleEnumTwo, Is.EqualTo((ExtendedTestFlagEnum)0));
        }

        [Test]
        public void DefaultValue_SaveOrUpdateCopyPersistance()
        {
            var model = new FlagEnumExtendedModel();

            this.Session.Save(model);
            this.Session.Evict(model);

            this.Session.SaveOrUpdateCopy(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<FlagEnumExtendedModel>(model.Id);
            Assert.That(modelFromDb.SampleEnum, Is.EqualTo((ExtendedTestFlagEnum)0));
            Assert.That(modelFromDb.SampleEnumTwo, Is.EqualTo((ExtendedTestFlagEnum)0));
        }

        [Test]
        public void StringValue_WithDefaultSeparator_Persistance()
        {
            var enumField = ExtendedTestFlagEnum.Something | ExtendedTestFlagEnum.Misc;

            var model = new FlagEnumExtendedModel();
            model.SampleEnum = enumField;

            this.Session.Save(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<FlagEnumExtendedModel>(model.Id);
            Assert.That(modelFromDb.SampleEnum, Is.EqualTo(enumField));
        }

        [Test]
        public void StringValue_WithDefaultSeparator_SaveOrUpdateCopyPersistance()
        {
            var enumField = ExtendedTestFlagEnum.Something | ExtendedTestFlagEnum.Misc;

            var model = new FlagEnumExtendedModel();
            model.SampleEnum = enumField;

            this.Session.Save(model);
            this.Session.Evict(model);

            this.Session.SaveOrUpdateCopy(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<FlagEnumExtendedModel>(model.Id);
            Assert.That(modelFromDb.SampleEnum, Is.EqualTo(enumField));
        }

        [Test]
        public void StringValue_WithCustomSeparator_Persistance()
        {
            var enumField = ExtendedTestFlagEnum.Something | ExtendedTestFlagEnum.Misc;

            var model = new FlagEnumExtendedModel();
            model.SampleEnumTwo = enumField;

            this.Session.Save(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<FlagEnumExtendedModel>(model.Id);
            Assert.That(modelFromDb.SampleEnumTwo, Is.EqualTo(enumField));
        }
    }
}