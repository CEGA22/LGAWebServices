using System.Configuration;

namespace EZWebServices.Helpers
{
    public class ConnectionHelper
    {
        public static string LGAConnection()
        {
            return ConfigurationManager.ConnectionStrings["LGAConnectdb"].ToString();
        }


        public static string BaseConnection()
        {
            return ConfigurationManager.ConnectionStrings["BaseConnection"].ToString();
        }

        public static string BaseXConnection()
        {
            return ConfigurationManager.ConnectionStrings["BaseXConnection"].ToString();
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

        public static string AEOSDBConnection()
        {
            return ConfigurationManager.ConnectionStrings["AEOSDBConnection"].ToString();
        }


    }
}