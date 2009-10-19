using System.Collections.Generic;
using System.Runtime.Serialization;
using AdamDotCom.Whois.Service.Extensions;
using AdamDotCom.Whois.Service.WhoisClient;

namespace AdamDotCom.Whois.Service
{
    [DataContract]
    public class WhoisEnhancedRecord
    {
        public WhoisEnhancedRecord(){}

        public WhoisEnhancedRecord(WhoisRecord whoisRecord, string filters, string referrer)
        {
            var registrant = whoisRecord.RegistryData.Registrant;
            City = registrant.City;
            StateProvince = registrant.StateProv;
            Organization = registrant.Name;
            CountryCode2 = registrant.Country;
            try
            {
                Country = new CountryNameLookup.CountryNameLookup().GetCountryName(CountryCode2);
            }
            catch
            {
                Country = CountryCode2;
            }
            this.ProcessFilters(filters, referrer);
            this.ProcessFriendly();
            this.ProcessReferrer(referrer);
        }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string StateProvince { get; set; }
        
        [DataMember]
        public string Organization { get; set; }

        public string CountryCode2 { get; set; }

        [DataMember]
        public bool IsFriendly { get; set; }

        [DataMember]
        public bool IsFilterMatch { get; set; }

        [DataMember]
        public bool IsReferrerMatch { get; set; }

        [DataMember]
        public List<string> FilterMatches { get; set; }

        [DataMember]
        public List<string> FriendlyMatches { get; set; }

        [DataMember]
        public List<string> ReferrerMatches { get; set; }

        [DataMember]
        public string Country { get; set; }
    }
}