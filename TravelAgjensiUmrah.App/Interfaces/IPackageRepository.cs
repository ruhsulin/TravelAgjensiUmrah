using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Interfaces
{
    public interface IPackageRepository : IRepository<Package>
    {
        Package GetPackageById(int id);
        List<Package> GetAllPackages();
        void Insert(Package package);
        void Delete(Package package);
    }
}
