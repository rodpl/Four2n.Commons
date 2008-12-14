//------------------------------------------------------------------------------------------------- 
// <copyright file="ReflectionHelperTests.cs" company="Daniel Dabrowski - rod.blogsome.com">
// Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>Defines the ReflectionHelperTests type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Rod.Commons.System.Reflection
{
    using NUnit.Framework;

    [TestFixture]
    public class ReflectionHelperTests
    {
        private const string TEXT = "someText";

        [Test]
        public void ProtectedFieldTest()
        {
            var b = new B();
            ReflectionHelper.For(b)
                .Field("protectedString")
                .SetValue(TEXT);
            Assert.AreEqual(TEXT, b.ProtectedString);
        }

        [Test]
        public void PrivateSetterTest()
        {
            var b = new B();
            ReflectionHelper.For(b)
                .Property("ProtectedString")
                .SetValue(TEXT);
            Assert.AreEqual(TEXT, b.ProtectedString);
        }

        #region Nested type: A

        public class A
        {
            protected string protectedString;

            public string ProtectedString
            {
                get { return this.protectedString; }
                private set { this.protectedString = value; }
            }
        }

        #endregion

        #region Nested type: B

        public class B : A
        {
        }

        #endregion
    }
}