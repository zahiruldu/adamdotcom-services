using System.Xml;

namespace AdamDotCom.Whois.Service.Extensions
{
    public static class XmlDocumentExtensions
    {
        public static bool WasLoaded(this XmlDocument xmlDocument)
        {
            if(xmlDocument == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(xmlDocument.InnerXml))
            {
                return false;
            }

            return true;
        }
    }
}
