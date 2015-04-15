using System.Web.Configuration;

namespace HFDIncidents.Web
{
    public static class AppSettings
    {
        public static string GoogleAnalyticsKey { get { return GetSetting("GoogleAnalyticsKey"); } }
        public static string GoogleMapsKey { get { return GetSetting("GoogleMapsKey"); } }
        public static string IncidentsServiceUrl { get { return GetSetting("IncidentsServiceUrl"); } }

        private static string GetSetting(string name)
        {
            return WebConfigurationManager.AppSettings[name];
        }
    }
}