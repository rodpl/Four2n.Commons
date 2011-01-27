// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimplifiedKeyDictionaryTest.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the SimplifiedKeyDictionaryTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.Collections
{
    using global::System;
    using global::System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class SimplifiedKeyDictionaryTest
    {
        private SimplifiedKeyDictionaryMother.Implementation model;

        [SetUp]
        public void SetUp()
        {
            this.model = SimplifiedKeyDictionaryMother.Implementation.Create();
        }

        [Test]
        public void
                Add_ThreeItemWithNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValueToEmptyDictionary_AddsProperly()
        {
            List<SimplifiedKeyIdentifiableMother.Implementation> list =
                    SimplifiedKeyIdentifiableMother.Implementation.CreateListOfThreeNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValue();

            list.ForEach(i => this.model.Add(i));
            Assert.AreEqual(3, this.model.Count);
            AssertEnumerator(this.model);
            list.ForEach(i => Assert.IsTrue(this.model.ContainsKey(i.Key)));
            list.ForEach(i => Assert.AreEqual(i, this.model[i.Key]));

            list.ForEach(i => this.model.Remove(i));
            Assert.AreEqual(0, this.model.Count);
            AssertEnumerator(this.model);
            list.ForEach(i => Assert.IsFalse(this.model.ContainsKey(i.Key)));

            list.ForEach(i => this.model.Add(i));
            Assert.AreEqual(3, this.model.Count);
            AssertEnumerator(this.model);
            list.ForEach(i => Assert.IsTrue(this.model.ContainsKey(i.Key)));
            list.ForEach(i => Assert.AreEqual(i, this.model[i.Key]));

            list.ForEach(i => this.model.Remove(i.Key));
            Assert.AreEqual(0, this.model.Count);
            AssertEnumerator(this.model);
            list.ForEach(i => Assert.IsFalse(this.model.ContainsKey(i.Key)));
        }

        [Test]
        public void
                Add_TwoItemsWithNonUniqueSimplifiedKeyValueAndNonUniqueBusinessKeyValueToEmptyDictionary_AddsProperly()
        {
            bool wasError = false;
            List<SimplifiedKeyIdentifiableMother.Implementation> list =
                    SimplifiedKeyIdentifiableMother.Implementation.
                            CreateListOfTwoNonUniqueSimplifiedKeyValueAndNonUniqueBusinessKeyValue();

            try
            {
                list.ForEach(i => this.model.Add(i));
            }
            catch (ArgumentException)
            {
                wasError = true;
            }

            Assert.AreEqual(1, this.model.Count);
            AssertEnumerator(this.model);
            Assert.IsTrue(this.model.ContainsKey(list[0].Key));
            Assert.AreEqual(list[0], this.model[list[0].Key]);
            Assert.IsTrue(wasError);

            Assert.IsTrue(this.model.Remove(list[0]));
            Assert.IsFalse(this.model.Remove(list[1]));
            Assert.AreEqual(0, this.model.Count);
            AssertEnumerator(this.model);
            Assert.IsFalse(this.model.ContainsKey(list[0].Key));
        }

        [Test]
        public void
                Add_TwoItemsWithNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValueToEmptyDictionary_AddsProperly()
        {
            List<SimplifiedKeyIdentifiableMother.Implementation> list =
                    SimplifiedKeyIdentifiableMother.Implementation.CreateListOfTwoNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValue();

            list.ForEach(i => this.model.Add(i));
            Assert.AreEqual(2, this.model.Count);
            AssertEnumerator(this.model);
            list.ForEach(i => Assert.IsTrue(this.model.ContainsKey(i.Key)));
            list.ForEach(i => Assert.AreEqual(i, this.model[i.Key]));

            list.ForEach(i => this.model.Remove(i));
            Assert.AreEqual(0, this.model.Count);
            AssertEnumerator(this.model);
            list.ForEach(i => Assert.IsFalse(this.model.ContainsKey(i.Key)));

            list.ForEach(i => this.model.Add(i));
            Assert.AreEqual(2, this.model.Count);
            AssertEnumerator(this.model);
            list.ForEach(i => Assert.IsTrue(this.model.ContainsKey(i.Key)));
            list.ForEach(i => Assert.AreEqual(i, this.model[i.Key]));

            list.ForEach(i => this.model.Remove(i.Key));
            Assert.AreEqual(0, this.model.Count);
            AssertEnumerator(this.model);
            list.ForEach(i => Assert.IsFalse(this.model.ContainsKey(i.Key)));
        }

        [Test]
        public void AddAndRemove_ItemToEmptyDictionary_AddsOneProperly()
        {
            SimplifiedKeyIdentifiableMother.Implementation item =
                SimplifiedKeyIdentifiableMother.Implementation.CreateListOfOne()[0];

            this.model.Add(item);
            Assert.AreEqual(1, this.model.Count);
            AssertEnumerator(this.model);
            Assert.IsTrue(this.model.ContainsKey(item.Key));
            Assert.AreEqual(item, this.model[item.Key]);

            Assert.IsTrue(this.model.Remove(item));
            Assert.AreEqual(0, this.model.Count);
            AssertEnumerator(this.model);
            Assert.IsFalse(this.model.ContainsKey(item.Key));

            this.model.Add(item);
            Assert.AreEqual(1, this.model.Count);
            AssertEnumerator(this.model);
            Assert.IsTrue(this.model.ContainsKey(item.Key));
            Assert.AreEqual(item, this.model[item.Key]);

            Assert.IsTrue(this.model.Remove(item.Key));
            Assert.AreEqual(0, this.model.Count);
            AssertEnumerator(this.model);
            Assert.IsFalse(this.model.ContainsKey(item.Key));
        }

        [Test]
        public void
            AddAndRemove_TwoItemWithUniqueSimplifiedKeyValueAndUniqueBusinessKeyValueToEmptyDictionary_AddsProperly()
        {
            List<SimplifiedKeyIdentifiableMother.Implementation> list =
                SimplifiedKeyIdentifiableMother.Implementation.CreateListOfTwoUnique();

            list.ForEach(i => this.model.Add(i));
            Assert.AreEqual(2, this.model.Count);
            AssertEnumerator(this.model);
            list.ForEach(i => Assert.IsTrue(this.model.ContainsKey(i.Key)));
            list.ForEach(i => Assert.AreEqual(i, this.model[i.Key]));

            list.ForEach(i => this.model.Remove(i));
            Assert.AreEqual(0, this.model.Count);
            AssertEnumerator(this.model);
            list.ForEach(i => Assert.IsFalse(this.model.ContainsKey(i.Key)));

            list.ForEach(i => this.model.Add(i));
            Assert.AreEqual(2, this.model.Count);
            AssertEnumerator(this.model);
            list.ForEach(i => Assert.IsTrue(this.model.ContainsKey(i.Key)));
            list.ForEach(i => Assert.AreEqual(i, this.model[i.Key]));

            list.ForEach(i => this.model.Remove(i.Key));
            Assert.AreEqual(0, this.model.Count);
            AssertEnumerator(this.model);
            list.ForEach(i => Assert.IsFalse(this.model.ContainsKey(i.Key)));
        }

        [Test]
        public void Clear_ModelContainsOneItem_MakesDictionaryEmpty()
        {
            List<SimplifiedKeyIdentifiableMother.Implementation> list =
                    SimplifiedKeyIdentifiableMother.Implementation.CreateListOfOne();
            list.ForEach(i => this.model.Add(i));
            Assert.AreEqual(1, this.model.Count);

            this.model.Clear();
            Assert.AreEqual(0, this.model.Count);
        }

        [Test]
        public void ContainsKey_ExistsKeyWithTheSameSimplifiedValueAndDifferentBussinessValue_ReturnsFalse()
        {
            var list = 
                SimplifiedKeyIdentifiableMother.Implementation.CreateListOfTwoNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValue();

            this.model.Add(list[0]);
            Assert.IsTrue(this.model.ContainsKey(list[0].Key));
            Assert.IsFalse(this.model.ContainsKey(list[1].Key));
            this.model.Add(list[1]);
            Assert.IsTrue(this.model.ContainsKey(list[0].Key));
            Assert.IsTrue(this.model.ContainsKey(list[1].Key));
        }

        [Test]
        public void Ctor_WithNoArguments_CreatesModelWithZeroCount()
        {
            var local = new SimplifiedKeyDictionaryMother.Implementation();
            Assert.IsNotNull(local);
            Assert.AreEqual(0, local.Count);
            AssertEnumerator(local);
        }

        [Test]
        public void TestKeyObjectMohter()
        {
            SimplifiedKeyIdentifiableMother.Implementation.CreateListOfTwoUnique();
            SimplifiedKeyIdentifiableMother.Implementation.
                    CreateListOfTwoNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValue();
            SimplifiedKeyIdentifiableMother.Implementation.
                    CreateListOfTwoNonUniqueSimplifiedKeyValueAndNonUniqueBusinessKeyValue();
            SimplifiedKeyIdentifiableMother.Implementation.CreateListOfThreeUnique();
            SimplifiedKeyIdentifiableMother.Implementation.
                    CreateListOfThreeNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValue();
        }

        [Test]
        [Explicit]
        public void FindNonUniqueValues()
        {
            var one = new SimplifiedKeyIdentifiableMother.Implementation("Daniel", "Dabrowski", new DateTime(1922, 02, 17));
            SimplifiedKeyIdentifiableMother.Implementation two = null;
            SimplifiedKeyIdentifiableMother.Implementation three = null;
            for (int i = 1900; i < 2000; i++)
            {
                two = new SimplifiedKeyIdentifiableMother.Implementation("John", "Doe", new DateTime(i, 02, 10));
                three = new SimplifiedKeyIdentifiableMother.Implementation("Jeremy", "Brown", new DateTime(i, 10, 02));
                if (one.Key.Equals(two.Key))
                {
                    Console.Out.WriteLine("TWO " + i);
                }

                if (one.Key.Equals(three.Key))
                {
                    Console.Out.WriteLine("THREE " + i);
                }
            }
        }

        private static void AssertEnumerator(SimplifiedKeyDictionaryMother.Implementation dictionary)
        {
            var counter = 0;
            foreach (var item in dictionary)
            {
                counter++;
            }

            Assert.AreEqual(dictionary.Count, counter);
        }
    }
}