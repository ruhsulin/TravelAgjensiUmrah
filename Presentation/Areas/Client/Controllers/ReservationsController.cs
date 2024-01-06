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

        // Konstruktori
        public ReservationsController(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        // Index
        public IActionResult Index()
        {
            return View();
        }


        // Create Reservation
        [HttpGet]
        public IActionResult CreateReservation(int packageId)
        {
            var package = _packageRepository.GetPackageById(packageId);
            var model = new ReservationViewModel
            {
                PackageId = packageId,
                NumberOfPeople = 1,
                PackageName = package.PackageName,
                BookingDate = DateTime.Now,
                UserName = User.Identity!.Name,
                TotalPrice = package.TicketPrice + package.MealPrice + package.VisaPrice + package.IhramPrice + package.ZemzemPrice + package.TransportInArabiaPrice + package.Service
            };
            return View(model);
        }

        //Process Payment
        [HttpPost]
        public async Task<IActionResult> ProcessReservation(ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.TotalPrice = model.TotalPrice * model.NumberOfPeople;

                return RedirectToAction("ReservationConfirmation"); // Or another appropriate action
            }

            // If model state is not valid, show the form again with validation messages
            return View("CreateReservation", model);
        }


    }
}
