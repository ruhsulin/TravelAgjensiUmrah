using ravelAgjensiUmrah.App.Impementations;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Context;
using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Impementations
{
    public class RolesRepository : Repository<AspNetRole>, IRolesRepository
    {
        protected readonly TravelAgencyUmrahContext _travelAgencyUmrahContext;

        public RolesRepository(TravelAgencyUmrahContext travelAgencyUmrahContext) : base(travelAgencyUmrahContext)
        {
            _travelAgencyUmrahContext = travelAgencyUmrahContext;
        }

        public AspNetRole? GetByUserId(string userId)
        {
            return _travelAgencyUmrahContext.AspNetUsers.FirstOrDefault(x => x.Id == userId)?.Roles.FirstOrDefault();
        }

        public AspNetRole? GetByStringId(string id)
        {
            return _travelAgencyUmrahContext.AspNetRoles.FirstOrDefault(x => x.Id == id);
        }
    }
}
