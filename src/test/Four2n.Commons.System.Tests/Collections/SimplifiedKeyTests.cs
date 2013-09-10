// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimplifiedKeyTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the SimplifiedKeyTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System.Collections
{
    using global::System;

    using NUnit.Framework;

    using Reflection;

    [TestFixture]
    public class SimplifiedKeyTests
    {
        [Test]
        public void BusinessEquals_ToInstancesWithTheSameSimplifiedKeyValueAndTheSameBussinesKeyValue_ReturnsFalse()
        {
            var itemOne = new SimplifiedKeyIdentifiableMother.Implementation("Daniel", "Dabrowski", new DateTime(1922, 02, 17));
            var itemTwo = new SimplifiedKeyIdentifiableMother.Implementation("Daniel", "Dabrowski", new DateTime(1922, 02, 17));
            Assert.IsTrue(itemOne.Key.Equals(itemTwo.Key));
            Assert.IsTrue(itemOne.Key.BusinessEquals(itemTwo.Key));
        }

        [Test]
        public void BusinessEquals_ToInstancesWithTheSameSimplifiedKeyValueButDifferentBussinesKeyValue_ReturnsFalse()
        {
            var itemOne = new SimplifiedKeyIdentifiableMother.Implementation("Daniel", "Dabrowski", new DateTime(1922, 02, 17));
            var itemTwo = Runtime.Isx64()
                              ? new SimplifiedKeyIdentifiableMother.Implementation("John", "Doe", new DateTime(1971, 02, 10))
                              : new SimplifiedKeyIdentifiableMother.Implementation("John", "Doe", new DateTime(1978, 02, 10));

            Assert.IsTrue(itemOne.Key.Equals(itemTwo.Key));
            Assert.IsFalse(itemOne.Key.BusinessEquals(itemTwo.Key));
        }

        [Test]
        public void EqualOperator_ToInstancesWithTheDifferentKeyValue_ReturnsFalse()
        {
            var modelOne = ReflectionHelper.For(new SimplifiedKeyMother.Implementation()).Field("simplifiedValue").SetValue((byte?)10).Return<SimplifiedKeyMother.Implementation>();
            var modelTwo = ReflectionHelper.For(new SimplifiedKeyMother.Implementation()).Field("simplifiedValue").SetValue((byte?)11).Return<SimplifiedKeyMother.Implementation>();

            Assert.IsFalse(ReferenceEquals(modelOne, modelTwo));
            Assert.IsFalse(modelOne.SimplifiedValue.Equals(modelTwo.SimplifiedValue));
            Assert.IsFalse(modelOne == modelTwo);
        }

        [Test]
        public void EqualOperator_ToInstancesWithTheSameKeyValue_ReturnsTrue()
        {
            var modelOne = ReflectionHelper.For(new SimplifiedKeyMother.Implementation()).Field("simplifiedValue").SetValue((byte?)10).Return<SimplifiedKeyMother.Implementation>();
            var modelTwo = ReflectionHelper.For(new SimplifiedKeyMother.Implementation()).Field("simplifiedValue").SetValue((byte?)10).Return<SimplifiedKeyMother.Implementation>();

            Assert.IsFalse(ReferenceEquals(modelOne, modelTwo));
            Assert.IsTrue(modelOne.SimplifiedValue.Equals(modelTwo.SimplifiedValue));
            Assert.IsTrue(modelOne == modelTwo);
        }

        [Test]
        public void Equals_ToInstancesWithTheDifferentKeyValue_ReturnsFalse()
        {
            var modelOne = ReflectionHelper.For(new SimplifiedKeyMother.Implementation()).Field("simplifiedValue").SetValue((byte?)10).Return<SimplifiedKeyMother.Implementation>();
            var modelTwo = ReflectionHelper.For(new SimplifiedKeyMother.Implementation()).Field("simplifiedValue").SetValue((byte?)11).Return<SimplifiedKeyMother.Implementation>();

            Assert.IsFalse(ReferenceEquals(modelOne, modelTwo));
            Assert.IsFalse(modelOne.SimplifiedValue.Equals(modelTwo.SimplifiedValue));
            Assert.IsFalse(modelOne.Equals(modelTwo));
        }

        [Test]
        public void Equals_ToInstancesWithTheSameKeyValue_ReturnsTrue()
        {
            var modelOne = ReflectionHelper.For(new SimplifiedKeyMother.Implementation()).Field("simplifiedValue").SetValue((byte?)10).Return<SimplifiedKeyMother.Implementation>();
            var modelTwo = ReflectionHelper.For(new SimplifiedKeyMother.Implementation()).Field("simplifiedValue").SetValue((byte?)10).Return<SimplifiedKeyMother.Implementation>();

            Assert.IsFalse(ReferenceEquals(modelOne, modelTwo));
            Assert.IsTrue(modelOne.SimplifiedValue.Equals(modelTwo.SimplifiedValue));
            Assert.IsTrue(modelOne.Equals(modelTwo));
        }

        [Test]
        public void Equals_ToReferenceEqualInstances_ReturnsTrue()
        {
            var modelOne = new SimplifiedKeyMother.Implementation();
            var modelTwo = modelOne;

            Assert.IsTrue(ReferenceEquals(modelOne, modelTwo));
            Assert.IsTrue(modelOne.Equals(modelTwo));
        }

        [Test]
        public void NonEqualOperator_ToInstancesWithTheDifferentKeyValue_ReturnsTrue()
        {
            var modelOne = ReflectionHelper.For(new SimplifiedKeyMother.Implementation()).Field("simplifiedValue").SetValue((byte?)10).Return<SimplifiedKeyMother.Implementation>();
            var modelTwo = ReflectionHelper.For(new SimplifiedKeyMother.Implementation()).Field("simplifiedValue").SetValue((byte?)11).Return<SimplifiedKeyMother.Implementation>();

            Assert.IsFalse(ReferenceEquals(modelOne, modelTwo));
            Assert.IsFalse(modelOne.SimplifiedValue.Equals(modelTwo.SimplifiedValue));
            Assert.IsTrue(modelOne != modelTwo);
        }

        [Test]
        public void NonEqualOperator_ToInstancesWithTheSameKeyValue_ReturnsFalse()
        {
            var modelOne = ReflectionHelper.For(new SimplifiedKeyMother.Implementation()).Field("simplifiedValue").SetValue((byte?)10).Return<SimplifiedKeyMother.Implementation>();
            var modelTwo = ReflectionHelper.For(new SimplifiedKeyMother.Implementation()).Field("simplifiedValue").SetValue((byte?)10).Return<SimplifiedKeyMother.Implementation>();

            Assert.IsFalse(ReferenceEquals(modelOne, modelTwo));
            Assert.IsTrue(modelOne.SimplifiedValue.Equals(modelTwo.SimplifiedValue));
            Assert.IsFalse(modelOne != modelTwo);
        }
    }
}