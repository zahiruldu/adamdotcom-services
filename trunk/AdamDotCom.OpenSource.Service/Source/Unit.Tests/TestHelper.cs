using System.IO;

namespace Unit.Tests
{
    public static class TestHelper
    {
        public static string PageSourceGitHubAdamDotCom_Xml { get { return LoadFile("GitHub-AdamDotCom-Xml.txt"); }}
        
        public static string PageSourceGitHubAdamDotComProjectBadge_Xml { get { return LoadFile("GitHub-AdamDotCom-Project-Badge-Xml.txt"); }}

        public static string PageSourceGoogleCodeAdamKahtavaCom_ProjectWebsite { get { return LoadFile("GoogleCode-Adam.Kahtava.com-ProjectWebsite.txt"); }}

        public static string PageSourceGoogleCodeAdamKahtavaCom_ProfileWebsite { get { return LoadFile("GoogleCode-Adam.Kahtava.com.txt"); }}

        public static string PageSourceGoogleCodeAdamKahtavaCom_ServicesXML { get { return LoadFile("GoogleCode-AdamDotCom-Services.txt"); }}
        
        public static string PageSourceGoogleCodeAdamKahtavaCom_AdamDotComServicesWebsite { get { return LoadFile("GoogleCode-Adam.Kahtava.com-AdamDotCom-Services.txt"); }}
        
        public static string LoadFile(string filename)
        {
            TextReader textReader = File.OpenText(string.Format(@"Data\{0}", filename));
            return textReader.ReadToEnd();            
        }
    }
}