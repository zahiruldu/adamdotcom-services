using System;
using System.Collections.Generic;
using System.Net;
using AdamDotCom.Resume.Service.Extensions;
using AdamDotCom.Resume.Service.Utilities;

namespace AdamDotCom.Resume.Service
{
    public class ResumeService : IResume
    {
        public Resume ResumeXml(string firstnameLastname)
        {
            return Resume(firstnameLastname);
        }

        public Resume ResumeJson(string firstnameLastname)
        {
            return Resume(firstnameLastname);
        }

        private static Resume Resume(string firstnameLastname)
        {
            AssertValidInput(firstnameLastname, "firstname-lastname");

            firstnameLastname = Scrub(firstnameLastname);

            if (ServiceCache.IsInCache(firstnameLastname))
            {
                return (Resume)ServiceCache.GetFromCache(firstnameLastname);
            }

            var resumeSniffer = new LinkedInResumeSniffer(firstnameLastname);

            var resume = resumeSniffer.GetResume();

            HandleErrors(resumeSniffer.Errors);

            return resume.AddToCache(firstnameLastname);
        }

        private static string Scrub(string username)
        {
            return username.Replace("%20", " ").Replace("-", " ");
        }

        private static void AssertValidInput(string inputValue, string inputName)
        {
            inputName = (string.IsNullOrEmpty(inputName) ? "Unknown" : inputName);

            if (string.IsNullOrEmpty(inputValue) || inputValue.Equals("null", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new RestException(HttpStatusCode.BadRequest,
                                        new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(inputName, string.Format("{0} is not a valid value.", inputValue)) },
                                        (int)ErrorCode.InternalError);
            }
        }

        private static void HandleErrors(List<KeyValuePair<string, string>> errors)
        {

            if (errors != null && errors.Count != 0)
            {
                throw new RestException(HttpStatusCode.BadRequest, errors, (int)ErrorCode.InternalError);
            }
        }
    }
}