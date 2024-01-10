using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Client.Models.PackageViewModel;
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
            ViewBag.ShowNavbar = true;

            var packages = GetPackages();
            return View(packages);
        }

        public List<PackageViewModel> GetPackages()
        {
            var packages = _packageRepository.GetAll().ToList();
            var packageViewModels = packages.Select(p => MapPackageToViewModel(p)).ToList();
            return packageViewModels;
        }


        private PackageViewModel MapPackageToViewModel(Package package)
        {
            return new PackageViewModel
            {
                PackageName = package.PackageName,
                Pax = package.Pax,
                HotelInMeccaName = package.HotelInMeccaNavigation?.HotelName,
                HotelInMedinaName = package.HotelInMedinaNavigation?.HotelName,
                GuideGuyName = package.GuideGuy,
                PackagePrice = package.TicketPrice + package.MealPrice + package.VisaPrice + package.IhramPrice + package.ZemzemPrice + package.TransportInArabiaPrice + package.Service,
                StartDay = package.StartDay,
                TotalDays = package.DaysInMecca + package.DaysInMedina

                // Add other fields as needed
            };
        }

    }
}
