using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.HotelViewModels;
using Presentation.FileHelper;
using TravelAgjensiUmrah.App.Constants;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Entities;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    public class HotelsController : Controller
    {

        private readonly IHotelRepository _hotelRepository;
        private readonly IFileHelper _fileHelper;


        public HotelsController(IHotelRepository hotelRepository, IFileHelper fileHelper)
        {
            _hotelRepository = hotelRepository;
            _fileHelper = fileHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddHotel()
        {
            return View(new HotelViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditHotel(HotelViewModel hotelViewModel)
        {
            if (ModelState.IsValid)
            {
                Hotel hotel;
                if (hotelViewModel.Id == 0)
                {
                    // Create new hotel
                    hotel = new Hotel();
                }
                else
                {
                    // Update existing hotel
                    hotel = _hotelRepository.GetHotelById(hotelViewModel.Id);
                    if (hotel == null)
                    {
                        return NotFound();
                    }
                }

                // Map properties
                hotel.HotelName = hotelViewModel.Name;
                hotel.Stars = hotelViewModel.Stars;
                hotel.Location = hotelViewModel.Location;
                hotel.Description = hotelViewModel.Description;

                if (hotelViewModel.Id == 0)
                {
                    _hotelRepository.Insert(hotel);
                }
                else
                {
                    _hotelRepository.Update(hotel);
                }
                _hotelRepository.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(hotelViewModel.Id == 0 ? "AddHotel" : "EditHotel", hotelViewModel);
        }

        [HttpGet]
        public IActionResult GetHotelsJson()
        {
            try
            {
                var hotels = _hotelRepository.GetAll();
                var result = hotels.Select(x => new
                {
                    id = x.Id,
                    name = x.HotelName,
                    stars = x.Stars,
                    location = x.Location,
                    description = x.Description

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
            Hotel hotel = _hotelRepository.GetHotelById(id); // Assuming GetById method exists in your repository
            if (hotel != null)
            {
                var model = new HotelViewModel
                {
                    Id = hotel.Id,
                    Name = hotel.HotelName,
                    Stars = hotel.Stars,
                    Location = hotel.Location,
                    Description = hotel.Description
                    // Handle HotelPicture if needed. Typically, for editing, you might show the existing picture and provide an option to change it.
                };

                // If you have additional data to load (like select lists), load them here

                return View("AddHotel", model); // Reuse the AddHotel view if it's set up to handle both add and edit scenarios
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHotelAsync(int id)
        {
            try
            {
                var hotel = _hotelRepository.GetById(id); // Synchronous call
                if (hotel != null)
                {
                    _hotelRepository.Delete(hotel);
                    _hotelRepository.SaveChanges(); // Synchronous call
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
