using System;
using System.Xml;

namespace TravelLine.Food.Core.Configs
{
    public class ConfigService
    {
        
        public static string ImagesStore = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
        public static string WebImagesStoreSmall = "/Images/small/";
        public static string WebImagesStoreBig = "/Images/big/";
        public static string CookieUserID = "id_user";
        public const int MaxCalendarDaysCount = 31;
        public const int StdCalendarDaysCount = 31;       
        public static readonly int[] TravellineLegals = { 14, 15, 16 };
        public static readonly int StudentsID = 12;

        private static readonly DateTime _quotaChangeDate = new DateTime( 2020, 3, 1 );
        private static readonly string _xmlConfigName = "Global.xml";

        public static int GetUserDayPriceQuota( DateTime date )
        {
            if ( date >= _quotaChangeDate )
            {
                return 300;
            }

            return 250;
        }

        public static void Save()
        {
            throw new NotImplementedException();
        }

        // ------------------------------ Email constants -------------------------------

        private static string _smtpHost = String.Empty;
        public static string SMPT_HOST
        {
            get
            {
                _smtpHost = GetXMLConfigValue( "smtp", "host" );
                return _smtpHost;
            }
        }

        private static string _smtpPort = String.Empty;
        public static string SMPT_PORT
        {
            get
            {
                _smtpPort = GetXMLConfigValue( "smtp", "port" );
                return _smtpPort;
            }
        }

        private static string _smtpLogin = String.Empty;
        public static string SMTP_LOGIN
        {
            get
            {
                _smtpLogin = GetXMLConfigValue( "smtp", "login" );
                return _smtpLogin;
            }
        }

        private static string _smtpPassword = String.Empty;
        public static string SMTP_PASSWORD
        {
            get
            {
                _smtpPassword = GetXMLConfigValue( "smtp", "password" );
                return _smtpPassword;
            }
        }

        private static string _smtpFrom = String.Empty;
        public static string SMTP_FROM
        {
            get
            {
                _smtpFrom = GetXMLConfigValue( "smtp", "from" );
                return _smtpFrom;
            }
        }

        private static string _smtpEncoding = String.Empty;
        public static string SMTP_ENCODING
        {
            get
            {
                _smtpEncoding = GetXMLConfigValue( "smtp", "encoding" );
                return _smtpEncoding;
            }
        }

        private static bool _smtpEnableSsl = false;
        public static bool SMTP_ENABLE_SSL
        {
            get
            {
                if ( GetXMLConfigValue( "smtp", "enableSsl" ).Contains( "true" ) )
                {
                    _smtpEnableSsl = true;
                }
                return _smtpEnableSsl;
            }
        }

        private static string GetXMLConfigValue( string nodeName, string paramName )
        {
            string paramValue = String.Empty;

            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load( AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\" + _xmlConfigName );
            }
            catch ( System.Exception )
            {
                return paramValue;
            }

            XmlNode node = xmlDoc.DocumentElement.FirstChild;
            while ( node != null )
            {
                if ( node.Name == nodeName )
                {
                    break;
                }
                node = node.NextSibling;
            }

            if ( node != null )
            {
                node = node.FirstChild;
            }
            while ( node != null )
            {
                if ( node.Name == paramName )
                {
                    paramValue = node.InnerText;
                    break;
                }
                node = node.NextSibling;
            }

            return paramValue;
        }

        // ------------------------------------------------------------------------------
    }
}
