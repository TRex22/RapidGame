using System.Collections.Specialized;
using System.Configuration;

namespace RapidGame.Helpers
{
    public static class GlobalConfig
    {
        public static readonly NameValueCollection GlobalSettings = ConfigurationManager.AppSettings;
    }
}
