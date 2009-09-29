using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdamDotCom.Common.Service.Utilities;
using AdamDotCom.Whois.Service.WhoisClient;

namespace AdamDotCom.Whois.Service.WhoisClient
{
    public static class WhoisRecordExtensions
    {
        public static WhoisRecord Translate(string query, string rawWhoisResult)
        {
            var whoisRecord = new WhoisRecord
                                  {
                                      DomainName = query,
                                      RegistryData = new RegistryData
                                                         {
                                                             CreatedDate = GetToken(rawWhoisResult, "RegDate"),
                                                             UpdatedDate = GetToken(rawWhoisResult, "Updated"),
                                                             RawText = rawWhoisResult,
                                                             Registrant = new Registrant
                                                                              {
                                                                                  City = GetToken(rawWhoisResult, "City"),
                                                                                  StateProv = GetToken(rawWhoisResult, "StateProv"),
                                                                                  PostalCode = GetToken(rawWhoisResult, "PostalCode"),
                                                                                  Country = GetToken(rawWhoisResult, "Country"),
                                                                                  Name = GetToken(rawWhoisResult, "OrgName")
                                                                              }
                                                         },
                                  };

            var hasAddress = !string.IsNullOrEmpty(GetToken(rawWhoisResult, "Address"));
            if (hasAddress)
            {
                foreach (string address in GetTokenList(rawWhoisResult, "Address"))
                {
                    whoisRecord.RegistryData.Registrant.Address += address;
                }
            }

            var hasAbuseInfo = !string.IsNullOrEmpty(GetToken(rawWhoisResult, "OrgAbuseName"));
            if (hasAbuseInfo)
            {
                whoisRecord.RegistryData.AbuseContact = new Contact
                                                            {
                                                                Name = GetToken(rawWhoisResult, "OrgAbuseName"),
                                                                Email = GetToken(rawWhoisResult, "OrgAbuseEmail"),
                                                                Phone = GetToken(rawWhoisResult, "OrgAbusePhone")
                                                            };
            }

            var hasTechnicalInfo = !string.IsNullOrEmpty(GetToken(rawWhoisResult, "OrgTechName"));
            if (hasTechnicalInfo)
            {
                whoisRecord.RegistryData.TechnicalContact = new Contact
                                                                {
                                                                    Name = GetToken(rawWhoisResult, "OrgTechName"),
                                                                    Email = GetToken(rawWhoisResult, "OrgTechEmail"),
                                                                    Phone = GetToken(rawWhoisResult, "OrgTechPhone")
                                                                };
            }

            return whoisRecord;
        }

        public static string GetToken(string rawResult, string token)
        {
            var token1 = token.Replace(" ", "");
            var regex = new Regex(string.Format(@"{0}:(?<{1}>(([^\n]|[^\r\n])*))", token, token1), RegexOptions.Multiline);
            return RegexUtilities.GetTokenString(regex.Match(rawResult), token1);
        }

        public static List<string> GetTokenList(string rawResult, string token)
        {
            var token1 = token.Replace(" ", "");
            var regex = new Regex(string.Format(@"{0}:(?<{1}>(([^\n]|[^\r\n])*))", token, token1), RegexOptions.Multiline);
            return RegexUtilities.GetTokenStringList(regex.Match(rawResult), token1);
        }
    }
}