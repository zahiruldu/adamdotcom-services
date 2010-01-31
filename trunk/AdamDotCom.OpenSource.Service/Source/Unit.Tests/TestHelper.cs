using System.IO;

namespace Unit.Tests
{
    public static class TestHelper
    {
        public static string PageSourceGitHubAdamDotCom_Xml
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource-AdamDotCom-GitHub-Xml.txt");
                return textReader.ReadToEnd();
            }
        }

        public static string PageSourceGoogleCodeAdamDotCom_Website
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource-AdamDotCom-Website.txt");
                return textReader.ReadToEnd();
            }
        }

        public static string PageSourceAdamKahtavaCom
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource-Adam.Kahtava.com.txt");
                return textReader.ReadToEnd();
            }
        }
    }
}