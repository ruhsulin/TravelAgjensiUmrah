using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Interfaces
{

    public interface IRolesRepository : IRepository<AspNetRole>
    {
        AspNetRole? GetByUserId(string userId);
        AspNetRole? GetByStringId(string? id);
    }
}

