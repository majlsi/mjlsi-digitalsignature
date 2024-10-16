
using Data.Infrastructure;
using Models;
using System.Collections.Generic;
using System.Linq;
namespace Data.Repositories
{

    public class FieldTypeRepository : BaseRepository<FieldType>, IFieldTypeRepository
    {
        public FieldTypeRepository(IDbFactory dbFactory, Helpers.SecurityHelper securityHelper)
            : base(dbFactory, securityHelper) { }
        public bool AllFieldTypesExist(List<int>  fieldTypeIds)
        {
            var results = DbContext.FieldTypes.Where(a => fieldTypeIds.Any(b => b.Equals(a.FieldTypeID)));
                return results.Count() == fieldTypeIds.Count();
       
        }

    }


}
