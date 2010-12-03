using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace AdamDotCom.Whois.Service.Extensions
{
    public static class WhoisRecordExtensions
    {
        public static WhoisRecord Translate(this WhoisRecord whoisRecord, string query, XmlDocument searchResult, XmlDocument orgResult, XmlDocument pocsSearchResult, List<XmlDocument> pocResults)
        {
            XmlElement netRef = searchResult["net"];
            XmlElement orgRef = orgResult["org"];

            whoisRecord = new WhoisRecord
                              {
                                  DomainName = query,
                                  RegistryData = new RegistryData
                                                     {
                                                         CreatedDate = netRef.InnerText("registrationDate"),
                                                         UpdatedDate = netRef.InnerText("updateDate"),
                                                         RawText = null,
                                                         Registrant = new Registrant
                                                                          {
                                                                              City = orgRef.InnerText("city"),
                                                                              StateProv = orgRef.InnerText("iso3166-2"),
                                                                              PostalCode =
                                                                                  orgRef.InnerText("postalCode"),
                                                                              Country =
                                                                                  orgRef["iso3166-1"].InnerText("code2"),
                                                                              Name = orgRef.InnerText("name"),
                                                                              Address =
                                                                                  orgRef.InnerText("streetAddress")
                                                                          }
                                                     },
                              };

            whoisRecord.TranslateContacts(pocsSearchResult, pocResults);

            return whoisRecord;
        }

        private static void TranslateContacts(this WhoisRecord whoisRecord, XmlDocument pocsSearchResult, List<XmlDocument> pocResults)
        {
            XmlElement pocsRef = pocsSearchResult["pocs"];

            var contactTable = new List<KeyValuePair<string, string>>();
            foreach (XmlElement child in pocsRef.ChildNodes)
            {
                contactTable.Add(new KeyValuePair<string, string>(child.Attributes["description"].Value,
                                                                  child.Attributes["handle"].Value));
            }

            foreach (XmlDocument document in pocResults)
            {
                XmlElement pocRef = document["poc"];
                KeyValuePair<string, string> contactType =
                    contactTable.Where(c => c.Value == pocRef.InnerText("handle")).FirstOrDefault();
                Contact contact = new Contact().Translate(pocRef);

                switch (contactType.Key)
                {
                    case "Abuse":
                        whoisRecord.RegistryData.AbuseContact = contact;
                        break;
                    case "Admin":
                        whoisRecord.RegistryData.AdministrativeContact = contact;
                        break;
                    case "Billing":
                        whoisRecord.RegistryData.BillingContact = contact;
                        break;
                    case "Tech":
                        whoisRecord.RegistryData.TechnicalContact = contact;
                        break;
                    case "Zone":
                        whoisRecord.RegistryData.ZoneContact = contact;
                        break;
                }

                contactTable.Remove(contactType);
            }
        }

        public static Contact Translate(this Contact contact, XmlElement element)
        {
            return new Contact
                       {
                           Email = element["emails"].InnerText("email"),
                           Name = element.InnerText("lastName"),
                           Phone = element["phones"]["phone"].InnerText("number")
                       };
        }

        private static string InnerText(this XmlElement element, string key)
        {
            if (element != null && element[key] != null)
            {
                if (element[key].ChildNodes.Count > 1)
                {
                    string returnValue = string.Empty;
                    string delimiter = ", ";
                    foreach (XmlElement child in element[key].ChildNodes)
                    {
                        returnValue += child.InnerText + delimiter;
                    }
                    return returnValue.Remove(returnValue.Length - delimiter.Length, delimiter.Length);
                }
                return element[key].InnerText;
            }
            return string.Empty;
        }
    }
}