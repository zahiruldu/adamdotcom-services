using System;
using System.Net;
using System.Xml;

namespace AdamDotCom.Whois.Service.Extensions
{
    public static class WebClientExtensions
    {
        public static XmlDocument DownloadXml(this WebClient webClient, string uri)
        {
            var result = string.Empty;
            try
            {
                result = webClient.DownloadString(uri);
            }
            catch (Exception ex)
            {
                
            }

            var xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.LoadXml(result);
            }
            catch (Exception ex)
            {
                
            }

            return xmlDocument;   
        }
    }
}
