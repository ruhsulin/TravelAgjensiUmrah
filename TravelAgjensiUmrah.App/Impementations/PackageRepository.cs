using ravelAgjensiUmrah.App.Impementations;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Context;
using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Impementations
{
    public class PackageRepository : Repository<Package>, IPackageRepository
    {
        protected readonly TravelAgencyUmrahContext _travelAgencyUmrahContext;
        public PackageRepository(TravelAgencyUmrahContext travelAgencyUmrahContext) : base(travelAgencyUmrahContext)
        {
            _travelAgencyUmrahContext = travelAgencyUmrahContext;
        }

        public Package GetPackageById(int id)
        {
            return _travelAgencyUmrahContext.Packages.FirstOrDefault(x => x.Id == id);
        }

        public List<Package> GetAllPackages()
        {
            return _travelAgencyUmrahContext.Packages.ToList();
        }

        public void Insert(Package package)
        {
            _travelAgencyUmrahContext.Packages.Add(package);
        }

        public void Update(Package package)
        {
            _travelAgencyUmrahContext.Packages.Update(package);
        }

        public void Delete(Package package)
        {
            _travelAgencyUmrahContext.Packages.Remove(package);
        }
    }
}
