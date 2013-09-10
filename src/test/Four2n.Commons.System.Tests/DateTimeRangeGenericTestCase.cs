// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeRangeGenericTestCase.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateTimeRangeGenericTestCase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System
{
    using global::System;

    using NUnit.Framework;

    using global::System.IO;
    using global::System.Text;
    using global::System.Xml;
    using global::System.Xml.Serialization;

    public abstract class DateTimeRangeGenericTestCase<T> where T : IDateTimeRange
    {
        [Test]
        public void CtorWithNulls_BeginsAndEnds_ReturnsNull()
        {
            var sut = this.Create(null, null);
            Assert.IsNull(sut.Begins);
            Assert.IsNull(sut.Ends);
        }

        [Test]
        public void Ctor_WithGreaterStartDateThenEndDate_ThrowsException()
        {
            Assert.That(
                    () => this.Create(new DateTime(2001, 1, 1), new DateTime(2000, 1, 1)), 
                    Throws.TypeOf<ArgumentOutOfRangeException>().With.Message.ContainsSubstring("greater"));
        }

        [Test]
        public void EqualsStatic_BothNulls_ReturnsTrue()
        {
            Assert.That(this.EqualsStatic(null, null), Is.True);
        }

        [Test]
        public void EqualsStatic_FirstNotNullAndSecondNull_ReturnsFalse()
        {
            var sutOne = this.CreateSample();
            Assert.That(this.EqualsStatic(sutOne, null), Is.False);
        }

        [Test]
        public void EqualsStatic_FirstNullAndSecondNotNull_ReturnsFalse()
        {
            var sutOne = this.CreateSample();
            Assert.That(this.EqualsStatic(null, sutOne), Is.False);
        }

        [Test]
        [TestCase("2000-01-01 12:00:00", "2000-01-02 12:00:00", "2000-01-01 12:00:00", "2000-01-02 12:00:00", true)]
        [TestCase("2000-01-01 12:00:00", "null", "2000-01-01 12:00:00", "null", true)]
        [TestCase("null", "2000-01-02 12:00:00", "null", "2000-01-02 12:00:00", true)]
        [TestCase("null", "null", "null", "null", true)]
        [TestCase("2000-01-01 12:00:00", "2000-01-02 12:00:00", "2000-01-01 12:00:00", "2001-01-02 12:00:00", false)]
        [TestCase("2000-01-01 12:00:00", "2000-01-02 12:00:00", "2001-01-01 12:00:00", "2001-01-02 12:00:00", false)]
        [TestCase("2000-01-01 12:00:00", "2000-01-02 12:00:00", "2001-01-01 13:00:00", "2001-01-02 13:00:00", false)]
        [TestCase("null", "null", "2000-01-01 12:00:00", "2000-01-02 12:00:00", false)]
        [TestCase("2000-01-01 12:00:00", "2000-01-02 12:00:00", "null", "null", false)]
        public virtual void Equals_TwoObjects_ReturnsExpected(
                string startOne, string endOne, string startTwo, string endTwo, bool expected)
        {
            var startDateOne = "null".Equals(startOne) ? (DateTime?)null : DateTime.Parse(startOne);
            var endDateOne = "null".Equals(endOne) ? (DateTime?)null : DateTime.Parse(endOne);
            var startDateTwo = "null".Equals(startTwo) ? (DateTime?)null : DateTime.Parse(startTwo);
            var endDateTwo = "null".Equals(endTwo) ? (DateTime?)null : DateTime.Parse(endTwo);

            var sutOne = this.Create(startDateOne, endDateOne);
            var sutTwo = this.Create(startDateTwo, endDateTwo);
            sutOne.GetHashCode();

            Assert.AreEqual(expected, this.EqualsStatic(sutOne, sutTwo));
            Assert.AreEqual(expected, sutOne.Equals(sutTwo));
        }

        [Test]
        public void Equals_TheSameReferenced_ReturnsTrue()
        {
            var sut = this.CreateSample().Equals(DateTime.Today);
            Assert.That(sut.Equals(sut), Is.True);
        }

        [Test]
        public void Equals_Null_ReturnsFalse()
        {
            Assert.That(this.CreateSample().Equals(null), Is.False);
        }

        [Test]
        public void Equals_DifferentObjectType_ReturnsFalse()
        {
            Assert.That(this.CreateSample().Equals(DateTime.Today), Is.False);
        }

        [Test]
        public void Include_Tests()
        {
            var sut = this.Create(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            Assert.IsTrue(sut.LaterEqualThanBegins(DateTime.Now));
            Assert.IsFalse(sut.LaterEqualThanBegins(DateTime.Now.AddDays(-2)));
            Assert.IsTrue(sut.EarlierEqualThanEnds(DateTime.Now));
            Assert.IsFalse(sut.EarlierEqualThanEnds(DateTime.Now.AddDays(2)));
            Assert.IsTrue(sut.Includes(DateTime.Now));
            Assert.IsFalse(sut.Includes(DateTime.Now.AddDays(5)));
        }

        [Test]
        public void Can_serialize_by_xml()
        {
            // Arrange
            var sut = this.CreateSample();
            var begins = sut.Begins;
            var ends = sut.Ends;

            // Act
            var serializer = new XmlSerializer(sut.GetType());

            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            serializer.Serialize(writer, sut);
            var doc = new XmlDocument();
            doc.LoadXml(sb.ToString());

            // Assert
            var reader = new XmlNodeReader(doc.DocumentElement);
            var ser = new XmlSerializer(typeof(T));
            var obj = ser.Deserialize(reader);
            var serializedSut = (T)obj;

            Assert.AreEqual(begins, serializedSut.Begins);
            Assert.AreEqual(ends, serializedSut.Ends);
        }

        protected abstract T Create(DateTime? startDate, DateTime? endDate);

        protected abstract bool EqualsStatic(object sutOne, object sutTwo);

        private T CreateSample()
        {
            return this.Create(DateTime.Today.AddDays(-1), DateTime.Today);
        }
    }
}