namespace Rod.Commons.NHibernate.Tests.Domain
{
    using global::System;
    using global::System.Xml;

    public class XmlTypeModel : BaseTestModel
    {
        public virtual XmlDocument TestXml { get; set; }

        [Serializable]
        public class Test
        {
            public string Name { get; set; }

            public DateTime Birth { get; set; }
        }
    }
}