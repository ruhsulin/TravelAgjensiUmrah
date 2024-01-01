using Microsoft.AspNetCore.Mvc;
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
        private readonly IFileHelper _fileHelper;

        //Constructor
        public PackagesController(IFileHelper fileHelper, IPackageRepository packageRepository)
        {
            _fileHelper = fileHelper;
            _packageRepository = packageRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddPackage()
        {
            return View(new PackageViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditPackage(PackageViewModel packageViewModel)
        {
            if (ModelState.IsValid)
            {
                Package package;
                if (packageViewModel.Id == 0)
                {
                    // Create new hotel
                    package = new Package();
                }
                else
                {
                    // Update existing hotel
                    package = _packageRepository.GetPackageById(packageViewModel.Id);
                    if (package == null)
                    {
                        return NotFound();
                    }
                }

                // Map properties
                package.PackageName = packageViewModel.PackageName;
                package.Pax = packageViewModel.Pax;
                package.TicketPrice = packageViewModel.TicketPrice;
                package.VisaPrice = packageViewModel.VisaPrice;
                package.InsurancePrice = packageViewModel.Insurance;
                package.HotelInMecca = packageViewModel.HotelInMeka;
                package.HotelInMedina = packageViewModel.HotelInMedina;
                package.FoodPrice = packageViewModel.FoodPrice;
                package.TransportToAirport = packageViewModel.TransportationToAirport;
                package.TransportationToAirportPrice = packageViewModel.TransportationToAirportPrice;
                package.TransportationInArabiaPrice = packageViewModel.TransportationInArabiaPrice;
                package.IhramPrice = packageViewModel.IhramPrice;
                package.ZemzemPrice = packageViewModel.ZemzemPrice;
                package.RoomType = packageViewModel.RoomType;


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
                    insurance = x.InsurancePrice,
                    hotelMeke = x.HotelInMecca,
                    hotelMedine = x.HotelInMedina,
                    food = x.FoodPrice,
                    transToAirport = x.TransportToAirport,
                    transAirportPrice = x.TransportationToAirportPrice,
                    transArabia = x.TransportationInArabiaPrice,
                    ihram = x.IhramPrice,
                    zemzem = x.ZemzemPrice,
                    roomType = x.RoomType
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
                    TicketPrice = package.TicketPrice,

                };
                return View("AddPackage", model);
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHotelAsync(int id)
        {
            try
            {
                var hotel = _hotelRepository.GetById(id);
                if (hotel != null)
                {
                    _hotelRepository.Delete(hotel);
                    _hotelRepository.SaveChanges();
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

    }
}
