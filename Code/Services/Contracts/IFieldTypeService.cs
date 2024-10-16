

using Models;
using System.Collections.Generic;

namespace Services
{

    public interface IFieldTypeService
    {
        FieldType GetFieldType(int id);
        void CreateFieldType(FieldType FieldType);
        void UpdateFieldType(FieldType fieldType);
        List<FieldType> GetAll();
        public void DeleteFieldType(FieldType fieldType);

        void SaveFieldType();
    }
}
