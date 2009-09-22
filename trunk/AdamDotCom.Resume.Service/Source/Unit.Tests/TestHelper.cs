using System.IO;

namespace AdamDotCom.Resume.Service.Unit.Tests
{
    public static class TestHelper
    {
        public static string PageSource
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource-Kahtava-Sept-17-2009.txt");
                return textReader.ReadToEnd();
            }
        }
        public static string PageSource2
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource-Gluzman-Sept-22-2009.txt");
                return textReader.ReadToEnd();
            }            
        }
        public static string PageSource3
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource-Tulloch-Sept-22-2009.txt");
                return textReader.ReadToEnd();
            }
        }
    }
}