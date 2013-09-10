namespace Four2n.Commons.NHibernate.Tests.UserTypes
{
    using System;

    using Domain;

    using NHibernate.UserTypes;

    using NUnit.Framework;

    using global::System;
    using global::System.Collections;
    using global::System.Xml;

    [TestFixture]
    public class XmlUserTypeTests : UserTypeTests<XmlType>
    {
        protected override IList Mappings
        {
            get {  return new[] { "Domain.XmlTypeModel.hbm.xml" }; }
        }

        protected override void OnSetUp()
        {
            base.OnSetUp();
            Sut = new XmlType();
        }

        [Test]
        public void SaveModelTest()
        {
            Assert.AreEqual(0, this.SessionFactoryImplementor.Statistics.EntityUpdateCount);
            var test = new XmlTypeModel.Test();
            test.Name = "John Doe";
            test.Birth = new DateTime(1950, 1, 1);

            var model = new XmlTypeModel();
            model.TestXml = XmlHelper.SerializeToXmlDocument(test);

            this.Session.Save(model);
            this.Session.Flush();
            Assert.AreEqual(0, this.SessionFactoryImplementor.Statistics.EntityUpdateCount);
            this.Session.Clear();

            var modelFromDb = this.Session.Get<XmlTypeModel>(model.Id);
            Assert.That(model.Id, Is.GreaterThan(0));

            var testFromDocument = XmlHelper.Deserialize<XmlTypeModel.Test>(modelFromDb.TestXml);

            Assert.AreEqual(testFromDocument.Name, test.Name);
            Assert.AreEqual(testFromDocument.Birth, test.Birth);
            this.Session.Flush();
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

        [Test]
        public void SaveNullModelTest()
        {
            Assert.AreEqual(0, this.SessionFactoryImplementor.Statistics.EntityUpdateCount);
            var model = new XmlTypeModel();
            Assert.IsNull(model.TestXml);
            this.Session.Save(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<XmlTypeModel>(model.Id);
            Assert.That(modelFromDb.Id, Is.GreaterThan(0));
            ////modelFromDb.TestXml = new XmlDocument();

            this.Session.Flush();
            Assert.AreEqual(0, this.SessionFactoryImplementor.Statistics.EntityUpdateCount);
        }

        [Test]
        public void SaveEmptyXmlDocumentModelTest()
        {
            Assert.AreEqual(0, this.SessionFactoryImplementor.Statistics.EntityUpdateCount);
            var model = new XmlTypeModel();
            model.TestXml = new XmlDocument();

            this.Session.Save(model);
            this.Session.Flush();
            this.Session.Clear();

            var modelFromDb = this.Session.Get<XmlTypeModel>(model.Id);
            Assert.That(modelFromDb.Id, Is.GreaterThan(0));

            this.Session.Flush();
            Assert.AreEqual(0, this.SessionFactoryImplementor.Statistics.EntityUpdateCount);
        }
    }
}