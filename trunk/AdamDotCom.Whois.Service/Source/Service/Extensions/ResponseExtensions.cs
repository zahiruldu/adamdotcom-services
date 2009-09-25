using System.Collections.Generic;
using System.Net;

namespace AdamDotCom.Whois.Service.Extensions
{
    public static class ResponseExtensions
    {
        public static Response SetCountryName(this Response response)
        {
            var countryTranslator = new CountryNameTranslator();
            response.Country = countryTranslator.GetCountryName(response.CountryCode2);
            return response;
        }

        public static Response ProcessFriendly(this Response response, string referrer)
        {
            response = IsFriendlyMatchInOrganization(response);

            response = IsFriendlyMatchInReferrer(response, referrer);

            return response;
        }

        private static Response IsFriendlyMatchInReferrer(Response response, string referrer)
        {
            if (!string.IsNullOrEmpty(referrer))
            {
                string[] friendlyReferrerFilters = {
                                                       "twitter", "github", "friendfeed", "asp.net", "facebook",
                                                       "linkedin",
                                                       "code.google", "flickr", "delicious"
                                                   };
                foreach (var referrerName in friendlyReferrerFilters)
                {
                    if (referrer.ToLower().Contains(referrerName))
                    {
                        if (response.FriendlyMatches == null)
                        {
                            response.FriendlyMatches = new List<string>();
                        }
                        response.FriendlyMatches.Add(referrerName);
                        response.IsFriendly = true;
                    }
                }
            }
            return response;
        }

        public static Response SetOrganizationFromSecondarySource(this Response response, string remoteAddress)
        {
            if (string.IsNullOrEmpty(response.Organization))
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(remoteAddress);
                response.Organization = hostInfo.HostName;
            }
            return response;
        }

        private static Response IsFriendlyMatchInOrganization(Response response)
        {
            if (!string.IsNullOrEmpty(response.Organization))
            {
                string[] friendlyOrganizationFilters = {
                                                           "google", "yahoo", "amazon", "microsoft", "corbis", "q9",
                                                           "agilent", "critical mass", "cactus", "accenture",
                                                           "componet art",
                                                           "ibm", "intel", "telerik", "ebay", "momentous"
                                                       };
                foreach (var organizationName in friendlyOrganizationFilters)
                {
                    if (response.Organization.ToLower().Contains(organizationName))
                    {
                        if (response.FriendlyMatches == null)
                        {
                            response.FriendlyMatches = new List<string>();
                        }
                        response.FriendlyMatches.Add(organizationName);
                        response.IsFriendly = true;
                    }
                }
            }
            return response;
        }

        public static Response ProcessFilters(this Response response, string filters, string referrer)
        {
            filters = filters.ToLower();
            referrer = referrer.ToLower();
            var splitFilters = filters.Split(',');
            foreach (var filter in splitFilters)
            {
                if (!string.IsNullOrEmpty(IsFilterMatchInWhoisInfo(filter, response)))
                {
                    if(response.FilterMatches == null)
                    {
                        response.FilterMatches = new List<string>();
                    }
                    response.FilterMatches.Add(IsFilterMatchInWhoisInfo(filter, response));
                    response.IsFilterMatch = true;
                }
                if (!string.IsNullOrEmpty(IsFilterMatchInReferrer(filter, referrer)))
                {
                    if (response.FilterMatches == null)
                    {
                        response.FilterMatches = new List<string>();
                    }
                    response.FilterMatches.Add(IsFilterMatchInReferrer(filter, referrer));
                    response.IsFilterMatch = true;
                }
            }
            return response;
        }

        private static string IsFilterMatchInReferrer(string filter, string referrer)
        {
            if (!string.IsNullOrEmpty(referrer) && referrer.ToLower().Contains(filter))
            {
                return "Referrer";
            }
            return null;
        }

        private static string IsFilterMatchInWhoisInfo(string filter, Response response)
        {
            if ((!string.IsNullOrEmpty(response.Country) && response.Country.ToLower().Contains(filter)) ||
                (!string.IsNullOrEmpty(response.CountryCode2) && response.CountryCode2.ToLower().Contains(filter)))
            {
                return "Country";
            }
            if (!string.IsNullOrEmpty(response.Region) && response.Region.ToLower().Contains(filter))
            {
                return "Region";
            }
            if (!string.IsNullOrEmpty(response.City) && response.City.ToLower().Contains(filter))
            {
                return "City";
            }
            if (!string.IsNullOrEmpty(response.Organization) && response.Organization.ToLower().Contains(filter))
            {
                return "Organization";
            }
            return null;
        }
    }
}