

using Models;
using System.Collections.Generic;

namespace Services
{

    public interface IActionTypeService
    {
        ActionType GetActionType(int id);
        void CreateActionType(ActionType ActionType);
        void UpdateActionType(ActionType actionType);
        List<ActionType> GetAll();

        public void DeleteActionType(ActionType actionType);

        void SaveActionType();
    }
}
