//------------------------------------------------------------------------------------------------- 
// <copyright file="HtmlHelperTests.cs" company="Daniel Dabrowski - rod.blogsome.com">
// Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>Defines the HtmlHelperTests type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Rod.Commons.MonoRail.Helpers
{
    using NUnit.Framework;

    [TestFixture]
    public class HtmlHelperTests
    {
        private HtmlHelper model;

        [SetUp]
        public void SetUp()
        {
            this.model = new HtmlHelper();
        }

        [TestCase(null, HtmlHelper.NBSP)]
        [TestCase("", HtmlHelper.NBSP)]
        [TestCase(" ", HtmlHelper.NBSP)]
        [TestCase("  ", HtmlHelper.NBSP + HtmlHelper.NBSP)]
        [TestCase("   ", HtmlHelper.NBSP + HtmlHelper.NBSP + HtmlHelper.NBSP)]
        [TestCase(" text", " text")]
        [TestCase("text ", "text ")]
        [TestCase("text", "text")]
        [TestCase(1, "1")]
        public void ToNbsp_StringInputs_ReturnsInputOrNbsp(object input, string expected)
        {
            Assert.AreEqual(expected, this.model.ToNbsp(input));
        }
    }
}