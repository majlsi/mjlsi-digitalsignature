using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Models.DTO;
using Newtonsoft.Json;
using RazorEngine;
using RazorEngine.Templating;

namespace Notifications
{
    public class SMSConnector : NotificationConnector
    {
        /// <summary>
        /// Converts list of phoneNumbers into string containning the phone numbers separated by commas
        /// </summary>
        private string ConcatenateNumbers(List<string> receiverPhones)
		{
            string phoneNumbers = "";
            foreach(string phonenumber in receiverPhones)
			{
                if(!phoneNumbers.Equals(""))
				{
                    phoneNumbers += ",";
				}
                phoneNumbers += phonenumber;
			}
            return phoneNumbers;
		}
        public override NotificationMessageDTO PreapareNotificationMessage(object messageObj, string templateName, string notificationSubject, List<string> receiversmss, List<string> receiverPhones, string lang, Dictionary<string, string> data = null, List<string> tokens = null)
        {
            //get configuration
            NotificationMessageDTO notificationMessage = new NotificationMessageDTO();
            string smsBody = Engine.Razor.Run(templateName + '-' + lang, messageObj.GetType(), messageObj);
            notificationMessage.Body = smsBody;
            notificationMessage.IsBodyHtml = false;
            notificationMessage.To = receiverPhones;
            notificationMessage.Subject = notificationSubject;
            return notificationMessage;
        }

        public override void ConnectToProvider(NotificationMessageDTO notificationMessage, ConfigurationDTO configuration)
        {
            
                string numbers = ConcatenateNumbers(notificationMessage.To);
                // Following is the key value pairs (data) which we need to send to server via API.
                Dictionary<string, string> jsonValues = new Dictionary<string, string>();
                jsonValues.Add("userName", configuration.Username);
                jsonValues.Add("numbers", numbers);
                jsonValues.Add("userSender", configuration.From);
                jsonValues.Add("apiKey", configuration.ApiKey);
                jsonValues.Add("msg", notificationMessage.Body);


                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(jsonValues), UnicodeEncoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                Task.Run(() =>
                {
                    client.PostAsync(configuration.Host, stringContent);
                });

        }

        public override ConfigurationDTO GetProviderConfiguration()
        {
            ConfigurationDTO configuration = new ConfigurationDTO();
            configuration.Username = ConfigurationHelper.SmsUsername;
            configuration.Host = ConfigurationHelper.SmsAddress;
            configuration.From = ConfigurationHelper.SmsUserSender;
            configuration.ApiKey = ConfigurationHelper.SmsApiKey;
            configuration.LogoPath = Path.Combine(HostEnvironmentHelper.HostingEnvironment.ContentRootPath, "EmailTemplates", "logo.png");
            configuration.AppStorePath = Path.Combine(HostEnvironmentHelper.HostingEnvironment.ContentRootPath, "EmailTemplates", "app-store.png");
            configuration.GooglePlayPath = Path.Combine(HostEnvironmentHelper.HostingEnvironment.ContentRootPath, "EmailTemplates", "google-play.png");

            configuration.From = ConfigurationHelper.MailFrom;
            return configuration;
        }
    }
}