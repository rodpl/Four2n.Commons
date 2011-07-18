namespace Rod.Commons.NHibernate.Tests.UserTypes
{
    using System;

    using Domain;

    using NUnit.Framework;

    using global::System;
    using global::System.Collections;

    [TestFixture]
    public class XmlUserTypeTests : NHibernateTestCase
    {
        protected override IList Mappings
        {
            get {  return new[] { "Domain.XmlTypeModel.hbm.xml" }; }
        }

        [Test]
        public void SaveModelTest()
        {
            var test = new XmlTypeModel.Test();
            test.Name = "John Doe";
            test.Birth = new DateTime(1950, 1, 1);

            var model = new XmlTypeModel();
            model.TestXml = XmlHelper.SerializeToXmlDocument(test);

            this.Session.Save(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<XmlTypeModel>(model.Id);
            Assert.That(model.Id, Is.GreaterThan(0));

            var testFromDocument = XmlHelper.Deserialize<XmlTypeModel.Test>(modelFromDb.TestXml);

            Assert.AreEqual(testFromDocument.Name, test.Name);
            Assert.AreEqual(testFromDocument.Birth, test.Birth);
        }

        [Test]
        public void SaveOrUpdateCopyModelTest()
        {
            var test = new XmlTypeModel.Test();
            test.Name = "John Doe";
            test.Birth = new DateTime(1950, 1, 1);

            var model = new XmlTypeModel();
            model.TestXml = XmlHelper.SerializeToXmlDocument(test);

            this.Session.Save(model);
            this.Session.Evict(model);

            this.Session.SaveOrUpdateCopy(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<XmlTypeModel>(model.Id);
            Assert.That(model.Id, Is.GreaterThan(0));

            var testFromDocument = XmlHelper.Deserialize<XmlTypeModel.Test>(modelFromDb.TestXml);

            Assert.AreEqual(testFromDocument.Name, test.Name);
            Assert.AreEqual(testFromDocument.Birth, test.Birth);

            test.Name = "BatMan";
            model.TestXml = XmlHelper.SerializeToXmlDocument(test);

            this.Session.SaveOrUpdateCopy(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDbTwo = this.Session.Get<XmlTypeModel>(model.Id);
            Assert.That(model.Id, Is.GreaterThan(0));

            var testFromDocumentTwo = XmlHelper.Deserialize<XmlTypeModel.Test>(modelFromDbTwo.TestXml);

            Assert.AreEqual(testFromDocumentTwo.Name, test.Name);
            Assert.AreEqual(testFromDocumentTwo.Birth, test.Birth);
        }
    }
}