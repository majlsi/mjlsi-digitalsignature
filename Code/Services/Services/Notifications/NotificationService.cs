using Models;
using Notifications;
using System.Threading.Tasks;
using Data.Repositories;
using Models.Enums;
using Notifications.Helpers;
using System.Collections.Generic;
using Models.DTO;

namespace Services
{
     public class NotificationService: INotificationService
     {
         private readonly INotificationSettingRepository _notificationSettingRepository;
         private readonly IUserRepository _userRepository;
         private readonly INotificationActionRepository _notificationActionRepository;
        public NotificationService(INotificationSettingRepository notificationSettingRepository, IUserRepository userRepository, INotificationActionRepository notificationActionRepository)
        {
            _notificationActionRepository = notificationActionRepository;
            _notificationSettingRepository = notificationSettingRepository;
            _userRepository = userRepository;
        }

        public void SendRegisterationNotification(int userID,string lang)
        {
            User user = _userRepository.GetById(userID);
            List<string> receiverEmails = new List<string>();
            receiverEmails.Add(user.UserEmail);
            List<string> receiverPhones = new List<string>();
            SendNotification(user, NotificationSettingEnum.UserRegistration.ToString(), receiverEmails, receiverPhones,lang);
        }

        public void SendNotification(object NotifcationModel,string ActionName,List<string> receiverEmails, List<string> receiverPhones,string lang)
        {
            NotificationAction notificationAction = _notificationActionRepository.GetNotificationActionByName(ActionName);
            foreach (NotificationSetting setting in notificationAction.NotificationSettings)
            {
                NotificationConnector notificationConnector = ReflectionHelper.LoadNotificationConnector($"{setting.NotificationType.TypeName}Connector");
                Task.Run(() => notificationConnector.SendNotification(NotifcationModel, setting.TemplateName,setting.Subject, receiverEmails,receiverPhones, lang));
            }
        }

		public void SendVerificationCode(int userID, VerificationCodeGenerationDTO verificationCodeGenerationDTO,DocumentSignatureCode documentSignatureCode,string lang)
		{
            User user = _userRepository.GetById(userID);
            List<string> receiverEmails = new List<string>();
            receiverEmails.Add(user.UserEmail);
            List<string> receiverPhones = new List<string>();
            receiverPhones.Add(user.UserPhoneNumber);
            if(verificationCodeGenerationDTO.SendEmail && verificationCodeGenerationDTO.SendSMS)
			{
                SendNotification(documentSignatureCode, NotificationSettingEnum.SendVerificationCodeAll.ToString(), receiverEmails, receiverPhones, lang);
            }else if(verificationCodeGenerationDTO.SendEmail)
            {
                SendNotification(documentSignatureCode, NotificationSettingEnum.SendVerificationCodeEmail.ToString(), receiverEmails, receiverPhones, lang);
            }else if (verificationCodeGenerationDTO.SendSMS)
			{
                SendNotification(documentSignatureCode, NotificationSettingEnum.SendVerificationCodeSMS.ToString(), receiverEmails, receiverPhones, lang);
            }
        }
	}
}

