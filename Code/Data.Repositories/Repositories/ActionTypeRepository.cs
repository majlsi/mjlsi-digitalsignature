
using Data.Infrastructure;
using Models;

namespace Data.Repositories
{

    public class ActionTypeRepository : BaseRepository<ActionType>, IActionTypeRepository
    {
        public ActionTypeRepository(IDbFactory dbFactory, Helpers.SecurityHelper securityHelper)
            : base(dbFactory, securityHelper) { }


    }


}
