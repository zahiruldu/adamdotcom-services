using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace AdamDotCom.Common.Service.Infrastructure
{
    public class RestException : Exception
    {
        public RestException()
        {
            throw new HttpException((int) HttpStatusCode.InternalServerError, "Error");
        }

        public RestException(string message)
            : this(
                HttpStatusCode.BadRequest,
                new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("Unknown", message)},
                (int) ErrorCode.InternalError)
        {
        }

        public RestException(KeyValuePair<string, string> error)
            : this(
                HttpStatusCode.BadRequest,
                new List<KeyValuePair<string, string>> { error },
                (int)ErrorCode.InternalError)
        {
        }

        public RestException(HttpStatusCode httpStatusCode, IEnumerable<KeyValuePair<string, string>> errorList,
                             int errorCode)
        {
            var exception = new HttpException((int) httpStatusCode, "RestException", errorCode);

            foreach (var item in errorList)
            {
                exception.Data.Add(item.Key, item.Value);
            }

            throw exception;
        }
    }
}