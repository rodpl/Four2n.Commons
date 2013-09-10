namespace Four2n.Commons.System
{
    using NUnit.Framework;

    using global::System;
    using global::System.IO;
    using global::System.Text;
    using global::System.Xml;
    using global::System.Xml.Serialization;

    [TestFixture]
    public class XmlHelperTests
    {
        [Test]
        public void SerializeToXmlDocument_Null_ReturnsNull()
        {
            Assert.IsNull(XmlHelper.SerializeToXmlDocument(null));
        }

        [Test]
        public void SerializeToXmlDocument_TestModel_ReturnsXmlDocument()
        {
            var model = new Test { Name = "John Doe", Birth = new DateTime(1950, 1, 1) };
            var document = XmlHelper.SerializeToXmlDocument(model);
            Assert.IsNotNull(document);
            Assert.AreEqual("John Doe", document.DocumentElement.FirstChild.FirstChild.Value);
        }

        [Test]
        public void Deserialize_Null_ReturnsNull()
        {
            Assert.IsNull(XmlHelper.Deserialize<Test>(null));
        }

        [Test]
        public void SerializeAndDeserializeTestModel()
        {
            var model = new Test { Name = "John Doe", Birth = new DateTime(1950, 1, 1) };
            var document = XmlHelper.SerializeToXmlDocument(model);
            var modelFromDocument = XmlHelper.Deserialize<Test>(document);

            Assert.AreEqual(modelFromDocument.Name, model.Name);
            Assert.AreEqual(modelFromDocument.Birth, model.Birth);
        }

        [Serializable]
        public class Test
        {
            public string Name { get; set; }

            public DateTime Birth { get; set; }
        }
    }
}