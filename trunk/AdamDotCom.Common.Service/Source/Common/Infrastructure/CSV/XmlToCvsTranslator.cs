using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;

namespace AdamDotCom.Common.Service.Infrastructure.CSV
{
    public class XmlToCvsTranslator
    {
        private XmlDocument xslt;

        public XmlToCvsTranslator()
        {
            LoadXslt();
        }

        public StreamWriter Translate(XmlReader contents, StreamWriter writer)
        {
            var transform = new XslCompiledTransform();
            transform.Load(xslt);

            transform.Transform(contents, null, writer);
            
            return writer;
        }

        private void LoadXslt()
        {
            xslt = new XmlDocument();

            var fileWithNamespace = string.Format("{0}.{1}", GetType().Namespace, "XmlToCsv.xslt");
            var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileWithNamespace);

            using (var reader = new StreamReader(fileStream))
            {
                xslt.LoadXml(reader.ReadToEnd());
            }
        }
    }
}
