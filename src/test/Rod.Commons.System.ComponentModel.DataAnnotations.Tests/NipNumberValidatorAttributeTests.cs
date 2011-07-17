// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NipNumberValidatorAttributeTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the NipNumberValidatorAttributeTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.ComponentModel.DataAnnotations
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Text;

    using NUnit.Framework;

    [TestFixture]
    public class NipNumberValidatorAttributeTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("7680002466")]
        public void IsValid_ForValidValues_ReturnsTrue(object value)
        {
            var sut = new NipNumberValidatorAttribute();
            Assert.IsTrue(sut.IsValid(value));
        }

        [Test]
        [TestCase("asdad")]
        [TestCase("1234567890 ")]
        public void IsValid_ForValidValues_ReturnsFalse(object value)
        {
            var sut = new NipNumberValidatorAttribute();
            Assert.IsFalse(sut.IsValid(value));
        }

        [Test]
        public void IsValidCalledTwice_ForValidValues_ReturnsTrue()
        {
            var sut = new NipNumberValidatorAttribute();
            Assert.IsFalse(sut.IsValid("7680002462"));
            Assert.IsTrue(sut.IsValid("7680002466"));
        }
    }
}
