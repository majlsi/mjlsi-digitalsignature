
using Data.Infrastructure;
using Data.Repositories;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Services
{

    public class FieldTypeService : IFieldTypeService
    {
        private readonly IFieldTypeRepository FieldTypeRepository;
        private readonly IUnitOfWork unitOfWork;

        public FieldTypeService(IFieldTypeRepository FieldTypeRepository, IUnitOfWork unitOfWork)
        {
            this.FieldTypeRepository = FieldTypeRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IApplicationService Members


        public FieldType GetFieldType(int id)
        {
            var FieldType = FieldTypeRepository.GetById(id);
            return FieldType;
        }

        public void CreateFieldType(FieldType FieldType)
        {
            FieldTypeRepository.Add(FieldType);
        }
        public List<FieldType> GetAll()
        {
            List<FieldType> FieldTypes = FieldTypeRepository.GetAll().ToList();
            return FieldTypes;
        }
        public void UpdateFieldType(FieldType fieldType)
        {
            FieldTypeRepository.Update(fieldType.FieldTypeID,fieldType);
        }

        public void DeleteFieldType(FieldType fieldType)
        {
            FieldTypeRepository.Delete(fieldType);
        }
        public void SaveFieldType()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
