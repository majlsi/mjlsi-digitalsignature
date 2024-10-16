using Data.Infrastructure;
using Data.Repositories;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class NotificationActionService : INotificationActionService
    {
        private readonly INotificationActionRepository _notificationActionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public NotificationActionService(INotificationActionRepository notificationActionRepository, IUnitOfWork unitOfWork)
        {
            _notificationActionRepository = notificationActionRepository;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Get list of NotificationActions DTO form database
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public PagedResult<NotificationActionDTO> GetAll()
        {
            List<string> includes = new List<string>();
            PagedResult<NotificationActionDTO> notificationActions = _notificationActionRepository.GetAllNotificationActions();
            return notificationActions;
        }
    }
}
