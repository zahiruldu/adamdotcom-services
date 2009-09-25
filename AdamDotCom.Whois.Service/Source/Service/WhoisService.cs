using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AdamDotCom.Whois.Service.Extensions;
using AdamDotCom.Whois.Service.Utilities;

namespace AdamDotCom.Whois.Service
{
    public class WhoisService : IWhois
    {
        public Response WhoisXml(string filters)
        {
            return Whois(filters, null);
        }

        public Response WhoisJson(string filters)
        {
            return Whois(filters, null);
        }

        public Response WhoisWithReferrerXml(string filters, string referrer)
        {
            return Whois(filters, referrer);
        }

        public Response WhoisWithReferrerJson(string filters, string referrer)
        {
            return Whois(filters, referrer);
        }

        private Response Whois(string filters, string referrer)
        {
            filters = Scrub(filters);
            referrer = Scrub(referrer);

            Response response;
            try
            {
                string remoteAddress = ((RemoteEndpointMessageProperty) OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]).Address;

                if(remoteAddress == "127.0.0.1")
                {
                    remoteAddress = "68.146.10.123";
                }

                var whoisServiceTranslator = new WhoisServiceTranslator(remoteAddress);

                response = (Response) whoisServiceTranslator.GetResponse();

                response.SetOrganizationFromSecondarySource(remoteAddress);
                response.SetCountryName();
                response.ProcessFilters(filters, referrer);
                response.ProcessFriendly(referrer);
            }
            catch(Exception ex)
            {
                throw new RestException(ex.ToString());
            }

            return response;
        }

        private static string Scrub(string value)
        {
            return string.IsNullOrEmpty(value) ? null : value.Replace(" ", "%20").Replace("-", " ");
        }

        private static void HandleErrors(List<KeyValuePair<string, string>> errors)
        {
            if(errors != null && errors.Count != 0)
            {
                throw new RestException(HttpStatusCode.BadRequest, errors, (int)ErrorCode.InternalError);
            }
        }
    }
}