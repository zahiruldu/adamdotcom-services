using System;
using System.Collections.Generic;

namespace AdamDotCom.Common.Service.Utilities
{
    public static class StringUtilities
    {
        public static string Scrub(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("     ", "").Replace(",,", ",").Replace("%20", " ").Replace("-", " ");    
            }
            return value;
        }

        public static List<string> Scrub(List<string> list)
        {
            if (list == null)
            {
                return list;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                {
                    list.RemoveAt(i);
                }
                else
                {
                    list[i] = Scrub(list[i]);
                }
            }
            return list;
        }
    
        public static bool Has(this string value, string query)
        {
            return value.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) != -1;
        }
    }
}
