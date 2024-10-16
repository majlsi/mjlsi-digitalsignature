
using Data.Infrastructure;
using Data.Repositories;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Services
{

    public class ActionTypeService : IActionTypeService
    {
        private readonly IActionTypeRepository ActionTypeRepository;
        private readonly IUnitOfWork unitOfWork;

        public ActionTypeService(IActionTypeRepository ActionTypeRepository, IUnitOfWork unitOfWork)
        {
            this.ActionTypeRepository = ActionTypeRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IActionTypeService Members


        public ActionType GetActionType(int id)
        {
            var ActionType = ActionTypeRepository.GetById(id);
            return ActionType;
        }

        public void CreateActionType(ActionType ActionType)
        {
            ActionTypeRepository.Add(ActionType);
        }
        public List<ActionType> GetAll()
        {
            List<ActionType> ActionTypes = ActionTypeRepository.GetAll().ToList();
            return ActionTypes;
        }
        public void UpdateActionType(ActionType actionType)
        {
            ActionTypeRepository.Update(actionType.ActionTypeID, actionType);
        }

        public void DeleteActionType(ActionType actionType)
        {
            ActionTypeRepository.Delete(actionType);
        }
        public void SaveActionType()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
