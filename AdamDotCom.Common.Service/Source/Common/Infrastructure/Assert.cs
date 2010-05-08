using System;
using System.Collections.Generic;

namespace AdamDotCom.Common.Service.Infrastructure
{
    public static class Assert
    {
        public static void ValidInput(string value, string fieldName)
        { 
            fieldName = (string.IsNullOrEmpty(fieldName) ? "Unknown" : fieldName);

            if (string.IsNullOrEmpty(value) || 
                value.EqualsCaseInsensitive("Null") ||
                value.EqualsCaseInsensitive("Unknown") ||
                value.EqualsCaseInsensitive("None") || 
                value.EqualsCaseInsensitive("NaN") ||
                value.EqualsCaseInsensitive("undefined") ||
                value.EqualsCaseInsensitive("String.Empty"))
            {
                throw new RestException(new KeyValuePair<string, string>(fieldName, string.Format("{0} is not a valid value for input {1}.", value, fieldName)));
            }
        }

        private static bool EqualsCaseInsensitive(this string theString, string value)
        {
            return theString.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}