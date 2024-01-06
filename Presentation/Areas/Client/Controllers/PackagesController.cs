using Microsoft.AspNetCore.Mvc;
using TravelAgjensiUmrah.App.Constants;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Entities;

namespace Presentation.Areas.Client.Controllers
{
    [Area(AreasConstants.Client)]

    public class PackagesController : Controller
    {

        private readonly IPackageRepository _packageRepository;

        // Constructor injection
        public PackagesController(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }


        public IActionResult Index()
        {
            var packages = GetPackages();
            return View(packages);
        }

        public List<Package> GetPackages()
        {
            return _packageRepository.GetAll().ToList();
        }

    }
}
