using System;
using System.Collections.Generic;
using Helpers;
using Loggers;
using Models.DTO;

namespace Notifications
{
    public abstract class NotificationConnector
    {
        public abstract ConfigurationDTO GetProviderConfiguration();
        public abstract NotificationMessageDTO PreapareNotificationMessage(object messageObj, string templateName, string notificationSubject, List<string> receiverEmails, List<string> receiverPhones, string lang, Dictionary<string, string> data = null, List<string> tokens = null);
        public abstract void ConnectToProvider(NotificationMessageDTO notificationMessage, ConfigurationDTO configuration);
        public virtual void SendNotification(object messageObj, string templateName, string notificationSubject, List<string> receiverEmails, List<string> receiverPhones, string lang, Dictionary<string, string> data = null, List<string> tokens = null)
        {
            try
            {
                ConfigurationDTO configuration = GetProviderConfiguration();
                NotificationMessageDTO notificationMessage = PreapareNotificationMessage(messageObj, templateName, notificationSubject, receiverEmails, receiverPhones, lang, data, tokens);
                ConnectToProvider(notificationMessage, configuration);
            }
            catch (Exception ex)
            {
                ILogger logger = LoggerFactory.CreateLogger();
                logger.Error(ex);
                throw;
            }
        }


    }
}