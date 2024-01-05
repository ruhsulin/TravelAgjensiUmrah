using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.ReservationViewModel;
using TravelAgjensiUmrah.App.Interfaces;

namespace Presentation.Areas.Admin.Controllers
{
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


        public IActionResult CreateReservation(int packageId)
        {
            //fetch the package from repository
            var package = _packageRepository.GetPackageById(packageId);

            var model = new ReservationViewModel
            {
                PackageId = packageId,
                NumberOfPeople = 1,
                TotalPrice = package.TicketPrice + package.MealPrice + package.VisaPrice

                // Initialize other properties as needed
            };

            // Populate necessary SelectList, etc.

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ProcessReservation(ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Convert ViewModel to Entity, handle booking logic
                // Update package availability, save reservation data
                model.TotalPrice = model.TotalPrice * model.NumberOfPeople;

                return RedirectToAction("ReservationConfirmation"); // Or another appropriate action
            }

            // If model state is not valid, show the form again with validation messages
            return View("CreateReservation", model);
        }


    }
}
