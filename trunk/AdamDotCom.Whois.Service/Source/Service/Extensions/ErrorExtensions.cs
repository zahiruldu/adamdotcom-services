using System.Collections.Generic;

namespace AdamDotCom.Whois.Service.Extensions
{
    public static class ErrorExtensions
    {
        public static bool AreEmpty(this List<KeyValuePair<string, string>> errors)
        {
            return errors == null || errors.Count == 0;
        }

        public static List<KeyValuePair<string, string>> AddFailedLookupError(this List<KeyValuePair<string, string>> errors, string uri)
        {
            if (errors == null)
            {
                errors = new List<KeyValuePair<string, string>>();
            }

            errors.Add(new KeyValuePair<string, string>("WhoisClient", string.Format("Lookup for uri {0} failed", uri)));

            return errors;
        }
 
    }
}
