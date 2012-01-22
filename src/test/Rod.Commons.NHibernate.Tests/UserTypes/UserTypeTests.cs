namespace Rod.Commons.NHibernate.Tests.UserTypes
{
    using System;

    using NUnit.Framework;

    public abstract class UserTypeTests<TUserType> : NHibernateTestCase
    {
        protected TUserType Sut;

        [Test]
        public void CanSerialize()
        {
            Assert.IsNotNull(this.Sut);
            Assert.DoesNotThrow(() => XmlHelper.SerializeToXmlDocument(this.Sut));
        }
    }
}