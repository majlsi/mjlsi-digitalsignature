using Data.Infrastructure;
using Models;
using Models.DTO;

namespace Data.Repositories
{
    public interface INotificationActionRepository : IRepository<NotificationAction>
    {
        NotificationAction GetNotificationActionByName(string name);
        PagedResult<NotificationActionDTO> GetAllNotificationActions();
    }
}