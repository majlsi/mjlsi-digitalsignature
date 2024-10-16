using Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface INotificationActionService
    {
        PagedResult<NotificationActionDTO> GetAll();
    }
}
