// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionHelperExtensionsTest.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the ReflectionHelperExtensionsTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System.Extensions.Reflection
{
    using System.Reflection;

    using NUnit.Framework;

    [TestFixture]
    public class ReflectionHelperExtensionsTest
    {
        private const string TEXT = "Some string";

        [Test]
        public void ReflectExtension_OnObject_ReturnsReflectionHelper()
        {
            var model = new ReflectionHelperTests.B();
            Assert.AreEqual(model.Reflect().GetType(), typeof(ReflectionHelper));

            model
                .Reflect()
                .Field("protectedString")
                .SetValue(TEXT);
            Assert.AreEqual(TEXT, model.ProtectedString);
        }
    }
}