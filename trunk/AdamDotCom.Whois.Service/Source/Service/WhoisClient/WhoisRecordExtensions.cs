using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdamDotCom.Whois.Service.Utilities;
using AdamDotCom.Whois.Service.WhoisClient;

namespace AdamDotCom.Whois.Service.WhoisClient
{
    public static class WhoisRecordExtensions
    {
        public static WhoisRecord Translate(string query, string rawWhoisResult)
        {
            var queryEnding = query.Split('.')[query.Split('.').Length-1];
            int result;
            var isNumeric = int.TryParse(queryEnding, out result);
            WhoisRecord record;
            if(isNumeric)
            {
                record = BuildWhoisRecordFromResult(query, rawWhoisResult);
            }
            else
            {
                record = BuildWhoisRecordFromDomainResult(query, rawWhoisResult);
            }
            return record;
        }

        private static WhoisRecord BuildWhoisRecordFromDomainResult(string query, string rawWhoisResult)
        {
            var registrantRegex = new Regex(@"Registrant:(\r\n|\n)(?<Name>(([^\n]|[^\r\n])*))(\r\n|\n)(?<Address>(([^\n]|[^\r\n])*))(\r\n|\n)(?<City>(([^,])*)),(?<ProvState>((\w)*.{0,4}))(?<PostalCode>(([^\n]|[^\r\n])*))(\r\n|\n)(?<Country>(([^\n]|[^\r\n])*))", RegexOptions.Multiline);

            var registrant = new Registrant
                                 {
                                     Address = RegexUtilities.GetTokenString(registrantRegex.Match(rawWhoisResult), "Address"),
                                     City = RegexUtilities.GetTokenString(registrantRegex.Match(rawWhoisResult), "City"),
                                     StateProv = RegexUtilities.GetTokenString(registrantRegex.Match(rawWhoisResult), "ProvState"),
                                     PostalCode = RegexUtilities.GetTokenString(registrantRegex.Match(rawWhoisResult), "PostalCode"),
                                     Country = RegexUtilities.GetTokenString(registrantRegex.Match(rawWhoisResult), "Country"),
                                     Name = RegexUtilities.GetTokenString(registrantRegex.Match(rawWhoisResult), "Name")
                                 };
            
            var registryData = new RegistryData() { Registrant = registrant, RawText = rawWhoisResult };

            var adminContactRegex = new Regex(@"Administrative Contact:(\r\n|\n)(?<Name>(([^\n]|[^\r\n])*))(\r\n|\n)(?<Address>(([^\n]|[^\r\n])*))(\r\n|\n)(?<City>(([^,])*)),(?<ProvState>((\w)*.{0,4}))(?<PostalCode>(([^\n]|[^\r\n])*))(\r\n|\n)(?<Country>(([^\n]|[^\r\n])*))(\r\n|\n)(?<Phone>(([^\n]|[^\r\n])*))(\r\n|\n)", RegexOptions.Multiline);

            var hasAdminContact = !string.IsNullOrEmpty(RegexUtilities.GetTokenString(adminContactRegex.Match(rawWhoisResult), "Name"));
            if (hasAdminContact)
            {
                var nameEmail = RegexUtilities.GetTokenString(adminContactRegex.Match(rawWhoisResult), "Name").Split(' ');
                var email = nameEmail[nameEmail.Length - 1];
                var name = string.Empty;
                for (int i = 0; i < nameEmail.Length - 1; i++)
                {
                    name += nameEmail[i] + " ";
                }
                registryData.AdministrativeContact = new Contact
                                                         {
                                                             Name = name,
                                                             Email = email,
                                                             Phone = RegexUtilities.GetTokenString(adminContactRegex.Match(rawWhoisResult), "Phone")
                                                         };
            }

            var techContactRegex = new Regex(@"Technical Contact:(\r\n|\n)(?<Name>(([^\n]|[^\r\n])*))(\r\n|\n)(?<Address>(([^\n]|[^\r\n])*))(\r\n|\n)(?<City>(([^,])*)),(?<ProvState>((\w)*.{0,4}))(?<PostalCode>(([^\n]|[^\r\n])*))(\r\n|\n)(?<Country>(([^\n]|[^\r\n])*))(\r\n|\n)(?<Phone>(([^\n]|[^\r\n])*))(\r\n|\n)", RegexOptions.Multiline);

            var hasTechContact = !string.IsNullOrEmpty(RegexUtilities.GetTokenString(techContactRegex.Match(rawWhoisResult), "Name"));
            if (hasTechContact)
            {
                var nameEmail = RegexUtilities.GetTokenString(techContactRegex.Match(rawWhoisResult), "Name").Split(' ');
                var email = nameEmail[nameEmail.Length - 1];
                var name = string.Empty;
                for (int i = 0; i < nameEmail.Length - 1; i++)
                {
                    name += nameEmail[i] + " ";
                }
                var phoneFax = RegexUtilities.GetTokenString(techContactRegex.Match(rawWhoisResult), "Phone").Split(' ');
                registryData.TechnicalContact = new Contact
                                                    {
                                                        Name = name,
                                                        Email = email,
                                                        Phone = phoneFax[0]
                                                    };
            }

            var createdRegex = new Regex(@"Record last updated on(?<RegDate>(([^\n]|[^\r\n])*)).", RegexOptions.Multiline);
            registryData.UpdatedDate = RegexUtilities.GetTokenString(createdRegex.Match(rawWhoisResult), "RegDate");

            var udpatedRegex = new Regex(@"Record created on(?<Updated>(([^\n]|[^\r\n])*)).", RegexOptions.Multiline);
            registryData.CreatedDate = RegexUtilities.GetTokenString(udpatedRegex.Match(rawWhoisResult), "Updated");

            return new WhoisRecord { DomainName = query, RegistryData = registryData };
        }

        private static WhoisRecord BuildWhoisRecordFromResult(string query, string rawWhoisResult)
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