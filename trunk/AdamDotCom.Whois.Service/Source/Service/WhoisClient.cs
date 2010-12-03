using System.Collections.Generic;
using System.Net;
using System.Xml;
using AdamDotCom.Whois.Service.Extensions;

namespace AdamDotCom.Whois.Service
{
    public class WhoisClient
    {
        private WhoisRecord whoisRecord;
        public List<KeyValuePair<string, string>> Errors { get; set; }
        private static string searchUri = "http://whois.arin.net/rest/ip/{0}";
        private static string contactsUri = "{0}/pocs";

        public WhoisClient(string query)
        {
            Errors = new List<KeyValuePair<string, string>>();
            var webClient = new WebClient();

            XmlDocument searchResult = webClient.DownloadXml(string.Format(searchUri, query));
            if (!searchResult.WasLoaded())
            {
                Errors.AddFailedLookupError(string.Format(searchUri, query));
            }

            XmlDocument orgResult = null;
            var orgUri = string.Empty;
            if (Errors.AreEmpty())
            {
                orgUri = searchResult["net"]["orgRef"].InnerText;

                orgResult = webClient.DownloadXml(orgUri);
                if (!orgResult.WasLoaded())
                {
                    Errors.AddFailedLookupError(orgUri);
                }
            }

            XmlDocument pocsSearchResult = null;
            if (Errors.AreEmpty())
            {
                var pocsSearchUri = string.Format(contactsUri, orgUri);

                pocsSearchResult = webClient.DownloadXml(pocsSearchUri);
                if (!pocsSearchResult.WasLoaded())
                {
                    Errors.AddFailedLookupError(pocsSearchUri);
                }
            }

            List<XmlDocument> pocsResult = null;
            if (Errors.AreEmpty())
            {
                pocsResult = new List<XmlDocument>();

                foreach (XmlElement poc in pocsSearchResult["pocs"].ChildNodes)
                {
                    var pocSearchUri = poc.InnerText;

                    var result = webClient.DownloadXml(pocSearchUri);
                    if (!result.WasLoaded())
                    {
                        Errors.AddFailedLookupError(pocSearchUri);
                    }
                    else
                    {
                        pocsResult.Add(result);
                    }
                }                
            }

            if (Errors.AreEmpty())
            {
                whoisRecord = new WhoisRecord().Translate(query, searchResult, orgResult, pocsSearchResult, pocsResult);
            }
        }

        public WhoisRecord GetWhoisRecord()
        {
            return whoisRecord;
        }

    }
}