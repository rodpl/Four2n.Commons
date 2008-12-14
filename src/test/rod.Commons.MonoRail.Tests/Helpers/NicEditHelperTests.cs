//------------------------------------------------------------------------------------------------- 
// <copyright file="NicEditHelperTests.cs" company="Daniel Dabrowski - rod.blogsome.com">
// Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>Defines the NicEditHelperTests type.</summary>
//-------------------------------------------------------------------------------------------------
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
            Assert.That(html, Text.StartsWith("<div>\n"));
            Assert.That(html, Text.EndsWith("</div>\n"));
        }
    }
}