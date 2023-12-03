using ravelAgjensiUmrah.App.Impementations;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Context;
using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Impementations
{
    public class UserRepository : Repository<AspNetUser>, IUserRepository
    {
        protected readonly TravelAgencyUmrahContext _travelAgencyUmrahContext;
        public UserRepository(TravelAgencyUmrahContext travelAgencyUmrahContext) : base(travelAgencyUmrahContext)
        {
            _travelAgencyUmrahContext = travelAgencyUmrahContext;
        }

        public AspNetUser GetByStringId(string id)
        {
            return _travelAgencyUmrahContext.AspNetUsers.FirstOrDefault(x => x.Id == id)!;
        }
    }
}
