using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace AdamDotCom.Whois.Service.Unit.Tests
{
    public static class TestHelper
    {
        public static XmlDocument SearchResult { get { return LoadXml("68.146.10.100.xml"); } }

        public static XmlDocument ShawcResult { get { return LoadXml("SHAWC.xml"); } }

        public static XmlDocument PocsResult { get { return LoadXml("POCS.xml"); } }

        public static XmlDocument AbuseResult { get { return LoadXml("ABUSE.xml"); } }

        public static XmlDocument AdminResult { get { return LoadXml("ADMIN.xml"); } }

        public static XmlDocument TechResult { get { return LoadXml("TECH.xml"); } }

        private static XmlDocument LoadXml(string filename)
        {
            var document = new XmlDocument();
            document.Load(string.Format(@"Data\{0}", filename));
            return document;
        }

        public static List<XmlDocument> Pocs
        {
            get { return  new List<XmlDocument> {AbuseResult,AdminResult,TechResult};}
        }
    }
}