// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_two_instances_has_the_same_simplified_value_but_different_business_value.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the When_two_instances_has_the_same_simplified_value_but_different_business_value type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.Collections.SimplifiedKey
{
    using global::System;

    using NUnit.Framework;

    [TestFixture]
    public class When_two_instances_has_the_same_simplified_value_but_different_business_value
    {
        private SimplifiedKeyIdentifiableMother.Implementation itemOne;

        private SimplifiedKeyIdentifiableMother.Implementation itemTwo;

        [SetUp]
        public void SetUp()
        {
            this.itemOne = new SimplifiedKeyIdentifiableMother.Implementation("Daniel", "Dabrowski", new DateTime(1922, 02, 17));
            this.itemTwo = new SimplifiedKeyIdentifiableMother.Implementation("John", "Doe", new DateTime(1978, 02, 10));
        }

        [Test]
        public void Calling_Equals_returns_true()
        {
            Assert.IsTrue(this.itemOne.Key.Equals(this.itemTwo.Key));
        }

        [Test]
        public void Calling_BusinessEquals_returns_false()
        {
            Assert.IsFalse(this.itemOne.Key.BusinessEquals(this.itemTwo.Key));
        }
    }
}