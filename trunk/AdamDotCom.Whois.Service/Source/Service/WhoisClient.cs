using System.Collections.Generic;
using System.Net;
using AdamDotCom.Whois.Service.Extensions;

namespace AdamDotCom.Whois.Service
{
    public class WhoisClient
    {
        private WhoisRecord whoisRecord;
        public List<KeyValuePair<string, string>> Errors { get; set; }
        private static string searchPageUri = "http://ws.arin.net/whois/?queryinput={0}";

        public WhoisClient(string query)
        {
            Errors = new List<KeyValuePair<string, string>>();
            
            var webClient = new WebClient();
            var result = webClient.DownloadString(string.Format(searchPageUri, query));

            if (string.IsNullOrEmpty(result) || result.ToLower().Contains("timeout") || result.ToLower().Contains("no match"))
            {
                Errors.Add(new KeyValuePair<string, string>("WhoisClient", string.Format("Whois lookup for {0} could not be found", query)));
            }
            else
            {
                whoisRecord = WhoisRecordExtensions.Translate(query, result);                
            }
        }

        public WhoisRecord GetWhoisRecord()
        {
            return whoisRecord;
        }
    }
}