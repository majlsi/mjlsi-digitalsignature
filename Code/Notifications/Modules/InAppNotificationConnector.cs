using System;
using System.Collections.Generic;
using Models;
using Models.DTO;
using Notifications.Helpers;

namespace Notifications
{
    public class InAppNotificationConnector : NotificationConnector
    {
        public override NotificationMessageDTO PreapareNotificationMessage(object messageObj, string templateName, string notificationSubject, List<string> receiverEmails, List<string> receiverPhones, string lang, Dictionary<string, string> data = null, List<string> tokens = null)
        {
            NotificationMessageDTO notificationMessage = new NotificationMessageDTO();

            return notificationMessage;
        }

        public override void ConnectToProvider(NotificationMessageDTO notificationMessage, ConfigurationDTO configuration)
        {

        }

        public override ConfigurationDTO GetProviderConfiguration()
        {
            return new ConfigurationDTO();
        }
    }
}