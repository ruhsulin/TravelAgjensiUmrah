using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Client.Models.ReservationViewModel;
using TravelAgjensiUmrah.App.Constants;
using TravelAgjensiUmrah.App.Interfaces;

namespace Presentation.Areas.Client.Controllers
{
    [Area(AreasConstants.Client)]

    public class ReservationsController : Controller
    {
        private readonly IPackageRepository _packageRepository;

        public ReservationsController(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateReservation(int packageId)
        {
            var package = _packageRepository.GetPackageById(packageId);
            var model = new ReservationViewModel
            {
                PackageId = packageId,
                PackageName = package.PackageName,
                BookingDate = DateTime.Now,
                TotalPrice = package.TicketPrice
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ProcessReservation(ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Convert ViewModel to Entity, handle booking logic
                // Update package availability, save reservation data

                return RedirectToAction("ReservationConfirmation"); // Or another appropriate action
            }

            // If model state is not valid, show the form again with validation messages
            return View("CreateReservation", model);
        }


    }
}
