using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Areas.Admin.Models.PackageViewModel;
using Presentation.FileHelper;
using TravelAgjensiUmrah.App.Constants;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Entities;


namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    public class PackagesController : Controller
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IFileHelper _fileHelper;

        //Constructor
        public PackagesController(IFileHelper fileHelper, IPackageRepository packageRepository, IHotelRepository hotelRepository)
        {
            _fileHelper = fileHelper;
            _packageRepository = packageRepository;
            _hotelRepository = hotelRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddPackage()
        {
            var mekeHotels = _hotelRepository.GetHotelsByLocation("Meke")
       .Select(h => new SelectListItem { Value = h.Id.ToString(), Text = h.HotelName })
       .ToList();

            var medinaHotels = _hotelRepository.GetHotelsByLocation("Medina")
                .Select(h => new SelectListItem { Value = h.Id.ToString(), Text = h.HotelName })
                .ToList();

            var model = new PackageViewModel
            {
                MekeHotels = new SelectList(mekeHotels, "Value", "Text"),
                MedinaHotels = new SelectList(medinaHotels, "Value", "Text"),
                MinDate = DateTime.Today
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddOrEditPackage(PackageViewModel packageViewModel)
        {
            if (ModelState.IsValid)
            {
                Package package = packageViewModel.Id == 0 ? new Package() : _packageRepository.GetPackageById(packageViewModel.Id);
                if (package == null && packageViewModel.Id != 0)
                {
                    return NotFound();
                }

                // Map properties from PackageViewModel to Package
                package.PackageName = packageViewModel.PackageName;
                package.Pax = packageViewModel.Pax;
                package.HotelInMecca = packageViewModel.HotelInMeka;
                package.HotelInMedina = packageViewModel.HotelInMedina;
                package.RoomType = packageViewModel.RoomType;
                package.DaysInMecca = packageViewModel.DaysInMecca;
                package.DaysInMedina = packageViewModel.DaysInMedina;
                package.StartDay = packageViewModel.StartDay;
                package.ReturnDay = packageViewModel.ReturnDay;
                package.StartTime = packageViewModel.StartTime;
                package.MealIncluded = packageViewModel.FoodIncluded;
                package.MealPrice = packageViewModel.FoodPrice ?? 0;
                package.TicketIncluded = packageViewModel.TicketIncluded;
                package.TicketPrice = packageViewModel.TicketPrice ?? 0;
                package.VisaIncluded = packageViewModel.VisaIncluded;
                package.VisaPrice = packageViewModel.VisaPrice ?? 0;
                package.IhramIncluded = packageViewModel.IhramIncluded;
                package.IhramPrice = packageViewModel.IhramPrice ?? 0;
                package.ZemzemIncluded = packageViewModel.ZemZemIncluded;
                package.ZemzemPrice = packageViewModel.ZemzemPrice ?? 0;
                package.GuideGuy = packageViewModel.GuideGuyName;
                package.TransportToAirportIncluded = packageViewModel.TransportationToAirportIncluded;
                package.TransportToAirportPrice = packageViewModel.TransportationToAirportPrice ?? 0;
                package.TransportInArabiaPrice = packageViewModel.TransportationInArabiaPrice ?? 0;
                package.PackagePrice = packageViewModel.PackagePrice;
                package.Description = packageViewModel.Description;
                package.Service = packageViewModel.Service ?? 0;

                // Save or Update Package
                if (packageViewModel.Id == 0)
                {
                    _packageRepository.Insert(package);
                }
                else
                {
                    _packageRepository.Update(package);
                }
                _packageRepository.SaveChanges();

                return RedirectToAction("Index");
            }

            // Handle the case when the model state is not valid
            return View(packageViewModel.Id == 0 ? "AddPackage" : "EditPackage", packageViewModel);
        }
        [HttpGet]
        public IActionResult GetPackagesJson()
        {
            try
            {
                var packages = _packageRepository.GetAll();
                var result = packages.Select(x => new
                {
                    id = x.Id,
                    name = x.PackageName,
                    pax = x.Pax,
                    ticket = x.TicketPrice,
                    visa = x.VisaPrice,
                    hotelMeka = x.HotelInMeccaNavigation?.HotelName,
                    hotelMedina = x.HotelInMedinaNavigation?.HotelName

                });

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Package package = _packageRepository.GetPackageById(id);
            if (package != null)
            {
                var model = new PackageViewModel
                {
                    Id = package.Id,
                    PackageName = package.PackageName,
                    Pax = package.Pax,
                    // Insurance = package.InsurancePrice,
                    HotelInMeka = package.HotelInMecca,
                    HotelInMedina = package.HotelInMedina,
                    RoomType = package.RoomType,
                    DaysInMecca = package.DaysInMecca,
                    DaysInMedina = package.DaysInMedina,
                    StartDay = package.StartDay,
                    ReturnDay = package.ReturnDay,
                    StartTime = package.StartTime,
                    FoodIncluded = package.MealIncluded,
                    FoodPrice = package.MealPrice,
                    TicketIncluded = package.TicketIncluded,
                    TicketPrice = package.TicketPrice,
                    VisaIncluded = package.VisaIncluded,
                    VisaPrice = package.VisaPrice,
                    IhramIncluded = package.IhramIncluded,
                    IhramPrice = package.IhramPrice,
                    ZemZemIncluded = package.ZemzemIncluded,
                    ZemzemPrice = package.ZemzemPrice,
                    GuideGuyName = package.GuideGuy,
                    TransportationToAirportIncluded = package.TransportToAirportIncluded,
                    TransportationToAirportPrice = package.TransportToAirportPrice ?? 0,
                    TransportationInArabiaPrice = package.TransportInArabiaPrice,
                    PackagePrice = package.PackagePrice,
                    Description = package.Description,
                    Service = package.Service ?? 0
                };
                return View("AddPackage", model);
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePackageAsync(int id)
        {
            try
            {
                var package = _packageRepository.GetById(id);
                if (package != null)
                {
                    _packageRepository.Delete(package);
                    _packageRepository.SaveChanges();
                    return Ok(true);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }

        public IActionResult BookPackage(int id)
        {
            // You can pass additional data if needed, like package details
            return RedirectToAction("CreateReservation", "Reservations", new { area = "Client", packageId = id });
        }


    }
}