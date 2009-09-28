using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;

namespace AdamDotCom.Whois.Service.WhoisClient
{
    public class WhoisClient
    {
        private WhoisRecord whoisRecord;
        public List<KeyValuePair<string, string>> Errors { get; set; }
        private const string whoisServersFilename = @"Service\WhoisClient\WhoisServerList.xml";
        private static XElement whoisServers;

        public WhoisClient(string query)
        {
            Errors = new List<KeyValuePair<string, string>>();

            if (whoisServers == null)
            {
                whoisServers = XElement.Load(whoisServersFilename);
            }

            var wasResultFound = false;
            foreach (var server in whoisServers.Elements())
            {
                if(GetWhoisQuery(server.Value, query))
                {
                    wasResultFound = true;
                    break;
                }
            }
            if(!wasResultFound)
            {
                Errors.Add(new KeyValuePair<string, string>("WhoisClient", string.Format("Result for {0} could not be found", query)));
            }
        }

        private bool GetWhoisQuery(string whoisServer, string query)
        {
            try
            {
                var tcpClient = new TcpClient();
                tcpClient.SendTimeout = tcpClient.ReceiveTimeout = 15000;
                tcpClient.Connect(whoisServer, 43);

                Stream tcpStream = tcpClient.GetStream();
                tcpStream.Write(Encoding.ASCII.GetBytes(string.Format("{0}\r\n", query)), 0, string.Format("{0}\r\n", query).Length);
                tcpStream.Flush();

                var result = new StreamReader(tcpStream, Encoding.ASCII).ReadToEnd();

                if (string.IsNullOrEmpty(result) || result.ToLower().Contains("timeout") || result.ToLower().Contains("no match"))
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(WhoisRecordExtensions.GetToken(result, "Whois Server")))
                {
                    GetWhoisQuery(WhoisRecordExtensions.GetToken(result, "Whois Server"), query);
                    return true;
                }

                whoisRecord = WhoisRecordExtensions.Translate(query, result);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public WhoisRecord GetWhoisRecord()
        {
            return whoisRecord;
        }
    }
}