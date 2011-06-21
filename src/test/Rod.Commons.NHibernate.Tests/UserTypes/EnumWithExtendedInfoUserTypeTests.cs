// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumWithExtendedInfoUserTypeTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the EnumWithExtendedInfoUserTypeTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.NHibernate.Tests.UserTypes
{
    using System;

    using Domain;

    using global::System;
    using global::System.Collections;

    using NUnit.Framework;

    [TestFixture]
    public class EnumWithExtendedInfoUserTypeTests : NHibernateTestCase
    {
        protected override IList Mappings
        {
            get { return new[] { "Domain.EnumExtendedModel.hbm.xml" }; }
        }

        [Test]
        public void StringValue_Persistance()
        {
            var enumField = ExtendedTestEnum.Something;
            Assert.That(EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(enumField).CustomValue, Is.TypeOf<string>());

            var model = new EnumExtendedModel();
            model.SampleEnum = enumField;
            model.SampleNullableEnum = ExtendedTestEnum.Misc;

            this.Session.Save(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<EnumExtendedModel>(model.Id);
            Assert.That(modelFromDb.SampleEnum, Is.EqualTo(enumField));

            modelFromDb.SampleEnum = ExtendedTestEnum.Pending;
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDbTwo = this.Session.Get<EnumExtendedModel>(model.Id);
            Assert.That(modelFromDbTwo.SampleEnum, Is.EqualTo(ExtendedTestEnum.Pending));
        }

        [Test]
        public void StringValue_SaveOrUpdateCopyPersistance()
        {
            var enumField = ExtendedTestEnum.Something;
            Assert.That(EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(enumField).CustomValue, Is.TypeOf<string>());

            var model = new EnumExtendedModel();
            model.SampleEnum = enumField;
            model.SampleNullableEnum = ExtendedTestEnum.Misc;

            this.Session.Save(model);
            this.Session.Evict(model);

            this.Session.SaveOrUpdateCopy(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<EnumExtendedModel>(model.Id);
            Assert.That(modelFromDb.SampleEnum, Is.EqualTo(enumField));

            model.SampleEnum = ExtendedTestEnum.Pending;
            this.Session.SaveOrUpdateCopy(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDbTwo = this.Session.Get<EnumExtendedModel>(model.Id);
            Assert.That(modelFromDbTwo.SampleEnum, Is.EqualTo(ExtendedTestEnum.Pending));
        }

        [Test]
        public void NullableStringValue_Persistance()
        {
            var enumField = ExtendedTestEnum.Something;
            Assert.That(EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(enumField).CustomValue, Is.TypeOf<string>());

            var model = new EnumExtendedModel();
            model.SampleNullableEnum = null;

            this.Session.Save(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<EnumExtendedModel>(model.Id);
            Assert.That(modelFromDb.SampleNullableEnum, Is.EqualTo(null));

            modelFromDb.SampleNullableEnum = ExtendedTestEnum.Pending;
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDbTwo = this.Session.Get<EnumExtendedModel>(model.Id);
            Assert.That(modelFromDbTwo.SampleNullableEnum, Is.EqualTo(ExtendedTestEnum.Pending));
        }
    }
}