using System.Collections.Generic;
using System.Net;

namespace AdamDotCom.Whois.Service.Extensions
{
    public static class ResponseExtensions
    {
        public static WhoisEnhancedRecord SetCountryName(this WhoisEnhancedRecord whoisEnhancedRecord)
        {
            var countryTranslator = new CountryNameLookup.CountryNameLookup();
            whoisEnhancedRecord.Country = countryTranslator.GetCountryName(whoisEnhancedRecord.CountryCode2);
            return whoisEnhancedRecord;
        }

        public static WhoisEnhancedRecord ProcessFriendly(this WhoisEnhancedRecord whoisEnhancedRecord, string referrer)
        {
            whoisEnhancedRecord = IsFriendlyMatchInOrganization(whoisEnhancedRecord);

            whoisEnhancedRecord = IsFriendlyMatchInReferrer(whoisEnhancedRecord, referrer);

            return whoisEnhancedRecord;
        }

        private static WhoisEnhancedRecord IsFriendlyMatchInReferrer(WhoisEnhancedRecord whoisEnhancedRecord, string referrer)
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
                        if (whoisEnhancedRecord.FriendlyMatches == null)
                        {
                            whoisEnhancedRecord.FriendlyMatches = new List<string>();
                        }
                        whoisEnhancedRecord.FriendlyMatches.Add(referrerName);
                        whoisEnhancedRecord.IsFriendly = true;
                    }
                }
            }
            return whoisEnhancedRecord;
        }

        public static WhoisEnhancedRecord SetOrganizationFromSecondarySource(this WhoisEnhancedRecord whoisEnhancedRecord, string remoteAddress)
        {
            if (string.IsNullOrEmpty(whoisEnhancedRecord.Organization))
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(remoteAddress);
                whoisEnhancedRecord.Organization = hostInfo.HostName;
            }
            return whoisEnhancedRecord;
        }

        private static WhoisEnhancedRecord IsFriendlyMatchInOrganization(WhoisEnhancedRecord whoisEnhancedRecord)
        {
            if (!string.IsNullOrEmpty(whoisEnhancedRecord.Organization))
            {
                string[] friendlyOrganizationFilters = {
                                                           "google", "yahoo", "amazon", "microsoft", "corbis", "q9",
                                                           "agilent", "critical mass", "cactus", "accenture",
                                                           "componet art",
                                                           "ibm", "intel", "telerik", "ebay", "momentous"
                                                       };
                foreach (var organizationName in friendlyOrganizationFilters)
                {
                    if (whoisEnhancedRecord.Organization.ToLower().Contains(organizationName))
                    {
                        if (whoisEnhancedRecord.FriendlyMatches == null)
                        {
                            whoisEnhancedRecord.FriendlyMatches = new List<string>();
                        }
                        whoisEnhancedRecord.FriendlyMatches.Add(organizationName);
                        whoisEnhancedRecord.IsFriendly = true;
                    }
                }
            }
            return whoisEnhancedRecord;
        }

        public static WhoisEnhancedRecord ProcessFilters(this WhoisEnhancedRecord whoisEnhancedRecord, string filters, string referrer)
        {
            referrer = referrer == null ? null : referrer.ToLower();
            if (filters != null)
            {
                var splitFilters = filters.ToLower().Split(',');
                foreach (var filter in splitFilters)
                {
                    if (!string.IsNullOrEmpty(IsFilterMatchInWhoisInfo(filter, whoisEnhancedRecord)))
                    {
                        if(whoisEnhancedRecord.FilterMatches == null)
                        {
                            whoisEnhancedRecord.FilterMatches = new List<string>();
                        }
                        whoisEnhancedRecord.FilterMatches.Add(IsFilterMatchInWhoisInfo(filter, whoisEnhancedRecord));
                        whoisEnhancedRecord.IsFilterMatch = true;
                    }
                    if (!string.IsNullOrEmpty(IsFilterMatchInReferrer(filter, referrer)))
                    {
                        if (whoisEnhancedRecord.FilterMatches == null)
                        {
                            whoisEnhancedRecord.FilterMatches = new List<string>();
                        }
                        whoisEnhancedRecord.FilterMatches.Add(IsFilterMatchInReferrer(filter, referrer));
                        whoisEnhancedRecord.IsFilterMatch = true;
                    }
                }
            }
            return whoisEnhancedRecord;
        }

        private static string IsFilterMatchInReferrer(string filter, string referrer)
        {
            if (!string.IsNullOrEmpty(referrer) && referrer.ToLower().Contains(filter))
            {
                return "Referrer";
            }
            return null;
        }

        private static string IsFilterMatchInWhoisInfo(string filter, WhoisEnhancedRecord whoisEnhancedRecord)
        {
            if ((!string.IsNullOrEmpty(whoisEnhancedRecord.Country) && whoisEnhancedRecord.Country.ToLower().Contains(filter)) ||
                (!string.IsNullOrEmpty(whoisEnhancedRecord.CountryCode2) && whoisEnhancedRecord.CountryCode2.ToLower().Contains(filter)))
            {
                return "Country";
            }
            if (!string.IsNullOrEmpty(whoisEnhancedRecord.StateProvince) && whoisEnhancedRecord.StateProvince.ToLower().Contains(filter))
            {
                return "StateProvince";
            }
            if (!string.IsNullOrEmpty(whoisEnhancedRecord.City) && whoisEnhancedRecord.City.ToLower().Contains(filter))
            {
                return "City";
            }
            if (!string.IsNullOrEmpty(whoisEnhancedRecord.Organization) && whoisEnhancedRecord.Organization.ToLower().Contains(filter))
            {
                return "Organization";
            }
            return null;
        }
    }
}