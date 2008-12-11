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
        const string TEXT = "someText";

        [Test]
        public void ProtectedFieldTest()
        {
                    
            var b = new B();
            ReflectionHelper.For(b)
                .Field("_protectedString")
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

        ///<summary>
        ///
        ///</summary>
        public class A
        {
            protected string _protectedString;

            public string ProtectedString
            {
                get { return _protectedString; }
                private set { _protectedString = value; }
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