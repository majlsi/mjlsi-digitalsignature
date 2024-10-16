

using Data.Infrastructure;
using Models;
using System.Collections.Generic;

namespace Data.Repositories
{

    public interface IFieldTypeRepository : IRepository<FieldType>
    {
        bool AllFieldTypesExist(List<int> fieldTypeIds);
    }



}
