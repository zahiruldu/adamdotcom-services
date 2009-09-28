using System.Runtime.Serialization;

namespace AdamDotCom.Whois.Service
{
    [DataContract]
    public class WhoisRecord
    {
        public string DomainName { get; set; }

        public RegistryData RegistryData { get; set; }

//
//        [DataMember]
//        public string Organization { get; set; }
//
//        [DataMember]
//        public string Country { get; set; }
//
//        public string CountryCode2 { get; set; }
//
//        [DataMember]
//        public string Region { get; set; }
//
//        [DataMember]
//        public string City { get; set; }
    }

    public class RegistryData
    {
        public Contact AbuseContact { get; set; }
        public string CreatedDate { get; set; }

        public string UpdatedDate { get; set; }

        public Registrant Registrant { get; set; }

        public Contact AdministrativeContact { get; set; }

        public Contact BillingContact { get; set; }

        public Contact TechnicalContact { get; set; }

        public Contact ZoneContat { get; set; }

        public string RawText { get; set; }
    }

    public class Registrant
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateProv { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}