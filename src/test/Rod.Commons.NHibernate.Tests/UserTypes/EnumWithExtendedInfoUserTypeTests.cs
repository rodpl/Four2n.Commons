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

            this.Session.Save(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<EnumExtendedModel>(model.Id);
            Assert.That(modelFromDb.SampleEnum, Is.EqualTo(enumField));
        }
    }
}