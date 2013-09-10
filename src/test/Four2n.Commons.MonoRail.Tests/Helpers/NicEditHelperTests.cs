// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NicEditHelperTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the NicEditHelperTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.MonoRail.Helpers
{
    using NUnit.Framework;

    [TestFixture]
    public class NicEditHelperTests : HelperTestFixture<NicEditHelper>
    {
        private NicEditHelper sut;

        [SetUp]
        public void SetUp()
        {
            this.sut = new NicEditHelper();
            InitializeSut(this.sut);
        }

        [Test]
        public void CreateHtmlTest_ReturnsHtmlInDivTags()
        {
            var html = this.sut.CreateHtml("instance");
            Assert.That(html, Is.StringStarting("<div>\n"));
            Assert.That(html, Is.StringEnding("</div>\n"));
        }
    }
}