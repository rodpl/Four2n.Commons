using System;
using System.Collections.Generic;
using MbUnit.Framework;

namespace rod.Commons.System.Tests.Collections
{
    [TestFixture]
    public class SimplifiedKeyDictionaryTest
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _model = SimplifiedKeyDictionaryMother.Implementation.Create();
        }

        #endregion

        private SimplifiedKeyDictionaryMother.Implementation
            _model;


        [Test]
        public void Ctor_WithNoArguments_CreatesModelWithZeroCount()
        {
            var model = new SimplifiedKeyDictionaryMother.Implementation();
            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.Count);
        }


        [Test]
        public void Add_ItemToEmptyDictionary_AddsOneProperly()
        {
            SimplifiedKeyIdentifiableMother.Implementation item =
                SimplifiedKeyIdentifiableMother.Implementation.CreateListOfOne()[0];
            _model.Add(item);

            Assert.AreEqual(1, _model.Count);
            Assert.IsTrue(_model.ContainsKey(item.Key));
        }

        [Test]
        public void
            Add_TwoItemWithUniqueSimplifiedKeyValueAndUniqueBusinessKeyValueToEmptyDictionary_AddsProperly()
        {
            List<SimplifiedKeyIdentifiableMother.Implementation> list =
                SimplifiedKeyIdentifiableMother.Implementation.CreateListOfTwoUnique();

            list.ForEach(i => _model.Add(i));

            Assert.AreEqual(2, _model.Count);
            list.ForEach(i => Assert.IsTrue(_model.ContainsKey(i.Key)));
        }

        [Test]
        public void
            Add_TwoItemsWithNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValueToEmptyDictionary_AddsProperly()
        {
            List<SimplifiedKeyIdentifiableMother.Implementation> list =
                SimplifiedKeyIdentifiableMother.Implementation.CreateListOfTwoNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValue();

            list.ForEach(i => _model.Add(i));

            Assert.AreEqual(2, _model.Count);
            list.ForEach(i => Assert.IsTrue(_model.ContainsKey(i.Key)));
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
                list.ForEach(i => _model.Add(i));
            }
            catch (ArgumentException)
            {
                wasError = true;
            }

            Assert.AreEqual(1, _model.Count);
            Assert.IsTrue(_model.ContainsKey(list[0].Key));
            Assert.IsTrue(wasError);
        }

        [Test]
        public void
            Add_ThreeItemWithNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValueToEmptyDictionary_AddsProperly()
        {
            List<SimplifiedKeyIdentifiableMother.Implementation> list =
                SimplifiedKeyIdentifiableMother.Implementation.CreateListOfThreeNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValue
                    ();

            list.ForEach(i => _model.Add(i));

            Assert.AreEqual(3, _model.Count);
            list.ForEach(i => Assert.IsTrue(_model.ContainsKey(i.Key)));
        }

        [Test]
        public void Clear_ModelContainsOneItem_MakesDictionaryEmpty()
        {
            List<SimplifiedKeyIdentifiableMother.Implementation> list =
                SimplifiedKeyIdentifiableMother.Implementation.CreateListOfOne();
            list.ForEach(i => _model.Add(i));
            Assert.AreEqual(1, _model.Count);

            _model.Clear();
            Assert.AreEqual(0, _model.Count);
        }
    }
}