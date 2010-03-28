using System.IO;

namespace AdamDotCom.Resume.Service.Unit.Tests
{
    public static class TestHelper
    {
        public static string PageSource1
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource1.txt");
                return textReader.ReadToEnd();
            }
        }
        public static string PageSource2
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource2.txt");
                return textReader.ReadToEnd();
            }            
        }
        public static string PageSource3
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource3.txt");
                return textReader.ReadToEnd();
            }
        }
        public static string PageSource4
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource4.txt");
                return textReader.ReadToEnd();
            }
        }
        public static string PageSource5
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource5.txt");
                return textReader.ReadToEnd();
            }
        }
        public static string PageSource6
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource6.txt");
                return textReader.ReadToEnd();
            }
        }
    }
}