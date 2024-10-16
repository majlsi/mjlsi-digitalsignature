using Helpers;
using Models.DTO;
using RazorEngine;
using RazorEngine.Templating;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;

namespace Notifications
{
    public class MailGunConnector : NotificationConnector
    {
        public override NotificationMessageDTO PreapareNotificationMessage(object messageObj, string templateName, string notificationSubject,
            List<string> receiverEmails, List<string> receiverPhones, string lang, Dictionary<string, string> data = null, List<string> tokens = null)
        {
            //get configuration
            NotificationMessageDTO notificationMessage = new NotificationMessageDTO();
            string emailBody = Engine.Razor.Run(templateName + '-' + lang, messageObj.GetType(), messageObj);
            notificationMessage.Body = emailBody;
            notificationMessage.IsBodyHtml = true;
            notificationMessage.To = receiverEmails;
            notificationMessage.Subject = notificationSubject;
            return notificationMessage;
        }

        public override ConfigurationDTO GetProviderConfiguration()
        {
            ConfigurationDTO config = new ConfigurationDTO
            {
                Username = ConfigurationHelper.MailGunUsername,
                Password = ConfigurationHelper.MailGunPassword,
                Host = ConfigurationHelper.MailGunHost,
                LogoPath = Path.Combine(HostEnvironmentHelper.HostingEnvironment.ContentRootPath, "EmailTemplates", "logo.png"),
                AppStorePath = Path.Combine(HostEnvironmentHelper.HostingEnvironment.ContentRootPath, "EmailTemplates", "app-store.png"),
                GooglePlayPath = Path.Combine(HostEnvironmentHelper.HostingEnvironment.ContentRootPath, "EmailTemplates", "google-play.png"),
                From = ConfigurationHelper.MailGunFrom
            };
            return config;
        }

        public override void ConnectToProvider(NotificationMessageDTO notificationMessage, ConfigurationDTO configuration)
        {
            RestClientOptions client = new RestClientOptions
            {
                BaseUrl = new Uri(configuration.Host),
                Authenticator = new HttpBasicAuthenticator(configuration.Username, configuration.Password)
            };

            RestClient rest = new RestClient(client);

            var request = new RestRequest("", Method.Post);
            request.AddParameter("from", configuration.From);
            request.AddParameter("subject", notificationMessage.Subject);
            request.AddParameter(notificationMessage.IsBodyHtml ? "html" : "text", notificationMessage.Body);

            foreach (var recipient in notificationMessage.To)
            {
                request.AddParameter("to", recipient);
            }

            if (notificationMessage.CC != null && notificationMessage.CC.Count > 0)
            {
                foreach (var ccRecipient in notificationMessage.CC)
                {
                    request.AddParameter("cc", ccRecipient);
                }
            }

            if (notificationMessage.BCC != null && notificationMessage.BCC.Count > 0)
            {
                foreach (var bccRecipient in notificationMessage.BCC)
                {
                    request.AddParameter("bcc", bccRecipient);
                }
            }

            request.AddFile("attachment", configuration.LogoPath);

            var response = rest.Execute(request);
        }
    }
}
