using System.Configuration;

namespace EZWebServices.Helpers
{
    public class ConnectionHelper
    {
        public static string BaseConnection()
        {
            return ConfigurationManager.ConnectionStrings["BaseConnection"].ToString();
        }

        public static string MyDirectoryConnection()
        {
            return ConfigurationManager.ConnectionStrings["MyDirectoryConnection"].ToString();
        }

        public static string ServicesConnection()
        {
            return ConfigurationManager.ConnectionStrings["ServicesConnection"].ToString();
        }

        public static string HRDBConnection()
        {
            return ConfigurationManager.ConnectionStrings["HRDBConnection"].ToString();
        }
    }
}