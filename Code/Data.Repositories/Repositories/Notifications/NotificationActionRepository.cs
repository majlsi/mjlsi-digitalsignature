using System.Collections.Generic;
using System.Linq;
using Data.Infrastructure;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;

namespace Data.Repositories
{
    public class NotificationActionRepository : BaseRepository<NotificationAction>, INotificationActionRepository
    {
        private readonly SecurityHelper _securityHelper;
        public NotificationActionRepository(IDbFactory dbFactory, SecurityHelper securityHelper) : base(dbFactory, securityHelper)
        {
            _securityHelper = securityHelper;
        }

        /// <summary>
        /// Get list of notificationAction DTO from database
        /// </summary>
        /// <param></param>
        /// <returns>List of notificationActions DTO</returns>
        public PagedResult<NotificationActionDTO> GetAllNotificationActions()
        {
            Models.DTO.PagedResult<NotificationActionDTO> NotificationActionDTOList = new PagedResult<NotificationActionDTO>();

            List<string> includes = new List<string>();
            List<NotificationAction> NotificationActionList = GetAll().ToList();
            NotificationActionDTOList.TotalRecords = NotificationActionList.Count();

            NotificationActionDTOList.Results = NotificationActionList.Select(x => new NotificationActionDTO
                                                                      {
                                                                          NotificationActionID = x.NotificationActionID,
                                                                          ActionName = x.ActionName,
                                                                          Title = x.TitleEn,
                                                                          TitleAr = x.TitleAr,
                                                                          Icon = x.Icon
                                                                      }).ToList(); ;
            return NotificationActionDTOList;

        }
        public NotificationAction GetNotificationActionByName(string name)
        {
            NotificationAction action = DbContext.NotificationActions.Where(x => x.ActionName == name).Include(x => x.NotificationSettings)
               .ThenInclude(x => x.NotificationType).FirstOrDefault();
            return action;
        }

    }
}