using System;
using System.Net;

namespace AdamDotCom.Common.Service.Utilities
{
    public class WebClientWithCookies : WebClient
    {
        private readonly CookieContainer m_container = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = m_container;
            }
            return request;
        }
    }
}