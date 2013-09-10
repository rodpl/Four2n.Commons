// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtendedInfoHelperTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the EnumExtendedInfoHelperTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.Web.Mvc
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;

    using Html;

    using NUnit.Framework;

    [TestFixture]
    public class EnumExtendedInfoHelperTests
    {
        [Test]
        public void NullableEnumWithTitleAndSelectedAsNull_ReturnsListWithSelectedTitle()
        {
            var result = EnumExtendedInfoHelper.CreateSelectListItemsFor((EnumExtendedInfoExtensionTests.TestEnum?)null, "Title");
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Title", result[0].Text);
            Assert.AreEqual(null, result[0].Value);
            Assert.AreEqual(true, result[0].Selected);
        }

        [Test]
        public void NullableEnumWithTitleAndSelectedAsBoo_ReturnsListWithSelectedBooAsInt32()
        {
            var result = EnumExtendedInfoHelper.CreateSelectListItemsFor((EnumExtendedInfoExtensionTests.TestEnum?)EnumExtendedInfoExtensionTests.TestEnum.Boo, "Title");
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Title", result[0].Text);
            Assert.AreEqual(null, result[0].Value);
            Assert.AreEqual(false, result[0].Selected);
            Assert.AreEqual("Boos", result[1].Text);
            Assert.AreEqual("0", result[1].Value);
            Assert.AreEqual(true, result[1].Selected);
        }

        [Test]
        public void NotNullableEnumWithTitleAndSelectedAsBoo_ReturnsListWithSelectedBooAsInt32()
        {
            var result = EnumExtendedInfoHelper.CreateSelectListItemsFor(EnumExtendedInfoExtensionTests.TestEnum.Boo, "Title");
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Boos", result[0].Text);
            Assert.AreEqual("0", result[0].Value);
            Assert.AreEqual(true, result[0].Selected);
        }
    }
}
