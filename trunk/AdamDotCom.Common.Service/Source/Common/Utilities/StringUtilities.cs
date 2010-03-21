using System;
using System.Collections.Generic;

namespace AdamDotCom.Common.Service.Utilities
{
    public static class StringUtilities
    {
        public static string Scrub(string dirtyString)
        {
            if (!string.IsNullOrEmpty(dirtyString))
            {
                return dirtyString.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("     ", "").Replace(",,", ",").Trim();
            }
            return dirtyString;
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
