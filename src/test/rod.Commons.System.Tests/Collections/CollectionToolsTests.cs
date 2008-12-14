//------------------------------------------------------------------------------------------------- 
// <copyright file="CollectionToolsTests.cs" company="Daniel Dabrowski - rod.blogsome.com">
// Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>Defines the CollectionToolsTests type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Rod.Commons.System.Collections
{
    using global::System;
    using global::System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class CollectionToolsTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AddToDictionaryFromCollection_InvalidCollectionNonEmpty_ThrowsException()
        {
            var dict = new Dictionary<int, ObjectWithKey>();
            var list = new List<ObjectWithKey> { new ObjectWithKey(1, "One"), new ObjectWithKey(2, "Two") };
            var invalid = new List<ObjectWithKey> { new ObjectWithKey(10, "Ten") };
            CollectionTools.AddToDictionaryFromCollection(dict, list, invalid, o => o.Key);
        }

        [Test]
        public void AddToDictionaryFromCollection_NewDictionaryAndListWithNonUniqueKeys_ReturnsInvalidRecordListAndCorrectDictionary()
        {
            var dict = new Dictionary<int, ObjectWithKey>();
            var list = new List<ObjectWithKey> { new ObjectWithKey(1, "One"), new ObjectWithKey(2, "Two"), new ObjectWithKey(2, "Two") };
            var invalid = new List<ObjectWithKey>();
            CollectionTools.AddToDictionaryFromCollection(dict, list, invalid, o => o.Key);

            Assert.AreEqual(1, dict.Count);
            Assert.AreEqual(2, invalid.Count);
        }

        [Test]
        public void AddToDictionaryFromCollection_NewDictionaryAndListWithUniqueKeys_ReturnsEmptyInvalidRecordListAndCorrectDictionary()
        {
            var dict = new Dictionary<int, ObjectWithKey>();
            var list = new List<ObjectWithKey> { new ObjectWithKey(1, "One"), new ObjectWithKey(2, "Two") };
            var invalid = new List<ObjectWithKey>();
            CollectionTools.AddToDictionaryFromCollection(dict, list, invalid, o => o.Key);

            Assert.AreEqual(2, dict.Count);
            Assert.IsEmpty(invalid);
        }

        [Test]
        public void AddToDictionaryFromCollection_NonEmptyDictionaryAndListWithNonUniqueKeysFromSourceDictionary_ReturnsInvalidRecordListAndCorrectDictionary()
        {
            var item = new ObjectWithKey(3, "Three");
            var dict = new Dictionary<int, ObjectWithKey> { { item.Key, item } };
            var list = new List<ObjectWithKey>
                           {
                               new ObjectWithKey(1, "One"),
                               new ObjectWithKey(2, "Two"),
                               new ObjectWithKey(2, "Two"),
                               new ObjectWithKey(3, "Three")
                           };
            var invalid = new List<ObjectWithKey>();
            CollectionTools.AddToDictionaryFromCollection(dict, list, invalid, o => o.Key);

            Assert.AreEqual(2, dict.Count);
            Assert.AreEqual(3, invalid.Count);
        }

        [Test]
        public void AddToDictionaryFromCollection_NonEmptyDictionaryAndListWithNonUniqueKeysNotFromSourceDictionary_ReturnsInvalidRecordListAndCorrectDictionary()
        {
            var item = new ObjectWithKey(3, "Three");
            var dict = new Dictionary<int, ObjectWithKey> { { item.Key, item } };
            var list = new List<ObjectWithKey> { new ObjectWithKey(1, "One"), new ObjectWithKey(2, "Two"), new ObjectWithKey(2, "Two") };
            var invalid = new List<ObjectWithKey>();
            CollectionTools.AddToDictionaryFromCollection(dict, list, invalid, o => o.Key);

            Assert.AreEqual(2, dict.Count);
            Assert.AreEqual(2, invalid.Count);
        }

        [Test]
        public void AddToDictionaryFromCollection_NonEmptyDictionaryAndListWithUniqueKeys_ReturnsEmptyRecordListAndCorrectDictionary()
        {
            var item = new ObjectWithKey(3, "Three");
            var dict = new Dictionary<int, ObjectWithKey> { { item.Key, item } };
            var list = new List<ObjectWithKey> { new ObjectWithKey(1, "One"), new ObjectWithKey(2, "Two") };
            var invalid = new List<ObjectWithKey>();
            CollectionTools.AddToDictionaryFromCollection(dict, list, invalid, o => o.Key);

            Assert.AreEqual(3, dict.Count);
            Assert.IsEmpty(invalid);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddToDictionaryFromCollection_NullInvalidCollection_ThrowsException()
        {
            var dict = new Dictionary<int, ObjectWithKey>();
            var list = new List<ObjectWithKey> { new ObjectWithKey(1, "One"), new ObjectWithKey(2, "Two") };
            CollectionTools.AddToDictionaryFromCollection(dict, list, null, o => o.Key);
        }

        [Test]
        public void JoinEnumerators_Test()
        {
            var listOne = new List<int> { 1, 2 };
            var listTwo = new List<int> { 3, 4 };

            var joined = CollectionTools.JoinEnumerators(listOne, listTwo);

            var counter = 0;
            var text = string.Empty;
            foreach (var item in joined)
            {
                counter++;
                text = text + item;
            }

            Assert.AreEqual(4, counter);
            Assert.AreEqual("1234", text);
        }

        public class ObjectWithKey
        {
            public ObjectWithKey(int key, string value)
            {
                this.Key = key;
                this.Value = value;
            }

            public int Key { get; set; }

            public string Value { get; set; }
        }
    }
}