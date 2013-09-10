namespace Four2n.Commons.NHibernate.UserTypes
{
    using SqlTypes;

    using global::NHibernate.SqlTypes;

    using global::System;
    using global::System.Xml.Serialization;

    [Serializable]
    public class XmlStringType : XmlType
    {
        [NonSerialized]
        private static readonly SqlType[] sqlTypes = new SqlType[1] { new SqlXmlStringType() };

        [XmlIgnore]
        public override SqlType[] SqlTypes
        {
            get { return sqlTypes; }
        }
    }
}