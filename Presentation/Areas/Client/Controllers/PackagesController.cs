using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Client.Models;
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

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentViewModel model)
        {
            // Implement your payment processing and booking logic here
            // ...

            return RedirectToAction("BookingConfirmation");
        }

        public IActionResult BookPackage(int id)
        {
            // You can pass additional data if needed, like package details
            return RedirectToAction("CreateReservation", "Reservations", new { packageId = id });
        }


    }
}
