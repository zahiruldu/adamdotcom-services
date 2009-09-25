using System.IO;
using System.Net;

namespace AdamDotCom.Whois.Service
{
    public class LocationService
    {
        private string ipToCountryFileName = "ip-to-country.csv";
        private Stream ipToCountryFile;

        public LocationService(IPAddress ipAddress)
        {
            ipToCountryFile = GetType().Assembly.GetManifestResourceStream(string.Format("{0}.{1}", GetType().Assembly, ipToCountryFileName));
           

        }

        public Location GetLocation()
        {
            return null;
        }
    }

    public class Location
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
    }
}
