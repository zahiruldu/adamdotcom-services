using System.IO;

namespace AdamDotCom.Whois.Service.Unit.Tests
{
    public static class TestHelper
    {
        public static string WhoisRawResult1
        {
            get
            {
                TextReader textReader = File.OpenText("WhoisRawResult1.txt");
                return textReader.ReadToEnd();
            }
        }
        public static string WhoisRawResult2
        {
            get
            {
                TextReader textReader = File.OpenText("WhoisRawResult2.txt");
                return textReader.ReadToEnd();
            }
        }
    }
}