using System.IO;

namespace Unit.Tests
{
    public static class TestHelper
    {
        public static string PageSourceGitHubAdamDotCom_Xml
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource-GitHub-AdamDotCom-Xml.txt");
                return textReader.ReadToEnd();
            }
        }

        public static string PageSourceGoogleCodeAdamKahtavaCom_ProjectWebsite
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource-GoogleCode-Adam.Kahtava.com-ProjectWebsite.txt");
                return textReader.ReadToEnd();
            }
        }

        public static string PageSourceGoogleCodeAdamKahtavaCom_ProfileWebsite
        {
            get
            {
                TextReader textReader = File.OpenText("PageSource-GoogleCode-Adam.Kahtava.com.txt");
                return textReader.ReadToEnd();
            }
        }
    }
}