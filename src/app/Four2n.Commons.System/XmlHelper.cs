namespace Four2n.Commons.System
{
    using global::System.IO;
    using global::System.Text;
    using global::System.Xml;
    using global::System.Xml.Serialization;

    public class XmlHelper
    {
        public static XmlDocument SerializeToXmlDocument(object calculation)
        {
            if (calculation == null)
            {
                return null;
            }

            var serializer = new XmlSerializer(calculation.GetType());

            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            serializer.Serialize(writer, calculation);
            var doc = new XmlDocument();
            doc.LoadXml(sb.ToString());
            return doc;
        }

        public static T Deserialize<T>(XmlDocument xml)
        {
            if (xml == null || xml.DocumentElement == null)
            {
                return default(T);
            }

            var reader = new XmlNodeReader(xml.DocumentElement);
            var ser = new XmlSerializer(typeof(T));
            var obj = ser.Deserialize(reader);
            return (T)obj;
        }
    }
}
