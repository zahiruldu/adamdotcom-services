using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdamDotCom.Resume.Service.Utilities
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
            if(list == null)
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
    }

    public static class RegexUtilities
    {
        public static List<string> GetTokenStringList(Match match, string token)
        {
            if (match.Success)
            {
                var matchList = new List<string>();

                matchList.Add(GetTokenString(match, token));

                do
                {
                    match = match.NextMatch();
                    matchList.Add(GetTokenString(match, token));
                } while (match.Success);

                return matchList;
            }
            return null;
        }

        public static string GetTokenString(Match match, string token)
        {
            if (match.Success)
            {
                try
                {
                    string returnValue = string.Empty;
                    foreach(var item in match.Groups[token].Captures)
                    {
                        returnValue += item;
                    }
                    return returnValue;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
    }
}