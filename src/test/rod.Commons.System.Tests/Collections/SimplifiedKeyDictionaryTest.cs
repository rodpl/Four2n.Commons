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
            AssertEnumerator(_model);
        }


        [Test]
        public void AddAndRemove_ItemToEmptyDictionary_AddsOneProperly()
        {
            SimplifiedKeyIdentifiableMother.Implementation item =
                SimplifiedKeyIdentifiableMother.Implementation.CreateListOfOne()[0];

            _model.Add(item);
            Assert.AreEqual(1, _model.Count);
            AssertEnumerator(_model);
            Assert.IsTrue(_model.ContainsKey(item.Key));
            Assert.AreEqual(item, _model[item.Key]);

            Assert.IsTrue(_model.Remove(item));
            Assert.AreEqual(0, _model.Count);
            AssertEnumerator(_model);
            Assert.IsFalse(_model.ContainsKey(item.Key));

            _model.Add(item);
            Assert.AreEqual(1, _model.Count);
            AssertEnumerator(_model);
            Assert.IsTrue(_model.ContainsKey(item.Key));
            Assert.AreEqual(item, _model[item.Key]);

            Assert.IsTrue(_model.Remove(item.Key));
            Assert.AreEqual(0, _model.Count);
            AssertEnumerator(_model);
            Assert.IsFalse(_model.ContainsKey(item.Key));
        }

        [Test]
        public void
            AddAndRemove_TwoItemWithUniqueSimplifiedKeyValueAndUniqueBusinessKeyValueToEmptyDictionary_AddsProperly()
        {
            List<SimplifiedKeyIdentifiableMother.Implementation> list =
                SimplifiedKeyIdentifiableMother.Implementation.CreateListOfTwoUnique();

            list.ForEach(i => _model.Add(i));
            Assert.AreEqual(2, _model.Count);
            AssertEnumerator(_model);
            list.ForEach(i => Assert.IsTrue(_model.ContainsKey(i.Key)));
            list.ForEach(i => Assert.AreEqual(i, _model[i.Key]));

            list.ForEach(i => _model.Remove(i));
            Assert.AreEqual(0, _model.Count);
            AssertEnumerator(_model);
            list.ForEach(i => Assert.IsFalse(_model.ContainsKey(i.Key)));

            list.ForEach(i => _model.Add(i));
            Assert.AreEqual(2, _model.Count);
            AssertEnumerator(_model);
            list.ForEach(i => Assert.IsTrue(_model.ContainsKey(i.Key)));
            list.ForEach(i => Assert.AreEqual(i, _model[i.Key]));

            list.ForEach(i => _model.Remove(i.Key));
            Assert.AreEqual(0, _model.Count);
            AssertEnumerator(_model);
            list.ForEach(i => Assert.IsFalse(_model.ContainsKey(i.Key)));

        }


        [Test]
        public void
            Add_TwoItemsWithNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValueToEmptyDictionary_AddsProperly()
        {
            List<SimplifiedKeyIdentifiableMother.Implementation> list =
                SimplifiedKeyIdentifiableMother.Implementation.CreateListOfTwoNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValue();

            list.ForEach(i => _model.Add(i));
            Assert.AreEqual(2, _model.Count);
            AssertEnumerator(_model);
            list.ForEach(i => Assert.IsTrue(_model.ContainsKey(i.Key)));
            list.ForEach(i => Assert.AreEqual(i, _model[i.Key]));

            list.ForEach(i => _model.Remove(i));
            Assert.AreEqual(0, _model.Count);
            AssertEnumerator(_model);
            list.ForEach(i => Assert.IsFalse(_model.ContainsKey(i.Key)));

            list.ForEach(i => _model.Add(i));
            Assert.AreEqual(2, _model.Count);
            AssertEnumerator(_model);
            list.ForEach(i => Assert.IsTrue(_model.ContainsKey(i.Key)));
            list.ForEach(i => Assert.AreEqual(i, _model[i.Key]));

            list.ForEach(i => _model.Remove(i.Key));
            Assert.AreEqual(0, _model.Count);
            AssertEnumerator(_model);
            list.ForEach(i => Assert.IsFalse(_model.ContainsKey(i.Key)));

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
            AssertEnumerator(_model);
            Assert.IsTrue(_model.ContainsKey(list[0].Key));
            Assert.AreEqual(list[0], _model[list[0].Key]);
            Assert.IsTrue(wasError);

            Assert.IsTrue(_model.Remove(list[0]));
            Assert.IsFalse(_model.Remove(list[1]));
            Assert.AreEqual(0, _model.Count);
            AssertEnumerator(_model);
            Assert.IsFalse(_model.ContainsKey(list[0].Key));

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
            AssertEnumerator(_model);
            list.ForEach(i => Assert.IsTrue(_model.ContainsKey(i.Key)));
            list.ForEach(i => Assert.AreEqual(i, _model[i.Key]));

            list.ForEach(i => _model.Remove(i));
            Assert.AreEqual(0, _model.Count);
            AssertEnumerator(_model);
            list.ForEach(i => Assert.IsFalse(_model.ContainsKey(i.Key)));

            list.ForEach(i => _model.Add(i));
            Assert.AreEqual(3, _model.Count);
            AssertEnumerator(_model);
            list.ForEach(i => Assert.IsTrue(_model.ContainsKey(i.Key)));
            list.ForEach(i => Assert.AreEqual(i, _model[i.Key]));

            list.ForEach(i => _model.Remove(i.Key));
            Assert.AreEqual(0, _model.Count);
            AssertEnumerator(_model);
            list.ForEach(i => Assert.IsFalse(_model.ContainsKey(i.Key)));
        }

        [Test]
        public void ContainsKey_ExistsKeyWithTheSameSimplifiedValueAndDifferentBussinessValue_ReturnsFalse()
        {
            var list = 
                SimplifiedKeyIdentifiableMother.Implementation.CreateListOfTwoNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValue();

            _model.Add(list[0]);
            Assert.IsTrue(_model.ContainsKey(list[0].Key));
            Assert.IsFalse(_model.ContainsKey(list[1].Key));
            _model.Add(list[1]);
            Assert.IsTrue(_model.ContainsKey(list[0].Key));
            Assert.IsTrue(_model.ContainsKey(list[1].Key));
        }

        private void AssertEnumerator(SimplifiedKeyDictionaryMother.Implementation model)
        {
            var counter = 0;
            foreach(var item in model)
            {
                counter++;
            }
            Assert.AreEqual(model.Count, counter);
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