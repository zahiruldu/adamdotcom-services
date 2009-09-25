using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

namespace AdamDotCom.Whois.Service
{
    public class WhoisServiceTranslator
    {
        private const string whoisSearchUri = "http://www.whoisxmlapi.com/whoisserver/WhoisService?domainName={0}&outputFormat=xml";
        private readonly XmlDocument whoisSearchResponse;

        public List<KeyValuePair<string, string>> Errors { get; set; }

        public WhoisServiceTranslator()
        {
            Errors = new List<KeyValuePair<string, string>>();
        }

        public WhoisServiceTranslator(string ipAddress)
        {
            Errors = new List<KeyValuePair<string, string>>();
            whoisSearchResponse = new XmlDocument();

            var webClient = new WebClient();

            try
            {
                whoisSearchResponse.LoadXml(webClient.DownloadString(string.Format(whoisSearchUri, ipAddress)));
            }
            catch(Exception ex)
            {
                Errors.Add(new KeyValuePair<string, string>("WhoisServiceTranslator", ex.Message));
            }
        }

        public WhoisTranslatorResponse GetResponse()
        {
            return new WhoisTranslatorResponse
                       {
                           CountryCode2 = GetInnerText("country"),
                           Region = GetInnerText("state"),
                           City = GetInnerText("city"),
                           Organization = GetInnerText("name")
                       };
        }

        private string GetInnerText(string token)
        {
            return whoisSearchResponse.GetElementsByTagName(token)[0].InnerText;
        }
    }
}