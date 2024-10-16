using Microsoft.Extensions.Configuration;
using System;

namespace Helpers
{
    public static class ConfigurationHelper
    {
        private static IConfiguration _configuration;
        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static int MailPort => Convert.ToInt32(GetConfigValue("Smtp:Port"));
        public static string MailHost => GetConfigValue("Smtp:Host");
        public static string MailUsername => GetConfigValue("Smtp:Username");
        public static string MailPassword => GetConfigValue("Smtp:Password");
        public static string MailFrom => GetConfigValue("Smtp:From");

        public static string MailGunHost => GetConfigValue("MailGun:Host");
        public static string MailGunUsername => GetConfigValue("MailGun:Username");
        public static string MailGunPassword => GetConfigValue("MailGun:Password");
        public static string MailGunFrom => GetConfigValue("MailGun:From");

        public static string SmsAddress => GetConfigValue("Sms:address");
        public static string SmsUsername => GetConfigValue("Sms:userName");
        public static string SmsUserSender => GetConfigValue("Sms:userSender");
        public static string SmsApiKey => GetConfigValue("Sms:apiKey");

        public static string SplitAPIURL => GetConfigValue("SplitAPIURL");
        public static string SplitActionName => GetConfigValue("SplitActionName");
        public static string NewUserDefaultPassword => GetConfigValue("NewUserDefaultPassword");
        public static int DocumentSignatureCodeLength => Convert.ToInt32(GetConfigValue("DocumentSignatureCode:Length"));

        public static int CodeExpirationDurationInMins => Convert.ToInt32(GetConfigValue("DocumentSignatureCode:CodeExpirationDurationInMins"));

        public static bool EnableAmazoneS3 => Convert.ToBoolean(GetConfigValue("EnableAmazoneS3"));
        public static string AmazoneBucket => GetConfigValue("AmazoneS3:BucketName");
        public static string AmazoneURL => GetConfigValue("AmazoneS3:URL");
        public static string AmazoneAccessKey => GetConfigValue("AmazoneS3:AccessKey");
        public static string AmazoneSecretAccessKey => GetConfigValue("AmazoneS3:SecretAccessKey");
        public static bool EnableNotifications => Convert.ToBoolean(GetConfigValue("EnableNotifications"));
        public static string SerilogDBConnection => GetConfigValue("ConnectionStrings:SerilogDBConnection");
        public static string LoggingType => GetConfigValue("LoggingType");
        public static string LogTableName => GetConfigValue("LogTableName");
        public static bool ApplicationReturnURL => Convert.ToBoolean(GetConfigValue("ApplicationReturnURL"));
        public static bool IsThiqah => Convert.ToBoolean(GetConfigValue("IsThiqah"));
        public static string GetConfigValue(string key)
        {
            string value = string.Empty;
            value = _configuration[key];
            return value;
        }
    }
}
