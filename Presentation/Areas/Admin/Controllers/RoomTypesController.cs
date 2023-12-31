using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.RoomTypeViewModel;
using Presentation.FileHelper;
using TravelAgjensiUmrah.App.Constants;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Entities;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    public class RoomTypesController : Controller
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IFileHelper _fileHelper;


        public RoomTypesController(IRoomTypeRepository roomTypeRepository, IFileHelper fileHelper)
        {
            _roomTypeRepository = roomTypeRepository;
            _fileHelper = fileHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddRoomType()
        {
            return View(new RoomTypeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditRoomType(RoomTypeViewModel roomTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                RoomType roomtype;
                if (roomTypeViewModel.Id == 0)
                {
                    // Create new hotel
                    roomtype = new RoomType();
                }
                else
                {
                    // Update existing hotel
                    roomtype = _roomTypeRepository.GetRoomTypeById(roomTypeViewModel.Id);
                    if (roomtype == null)
                    {
                        return NotFound();
                    }
                }

                // Map properties
                roomtype.Id = roomTypeViewModel.Id;
                roomtype.RoomType1 = roomTypeViewModel.RoomType1;
                roomtype.Capacity = roomTypeViewModel.Capacity;


                if (roomTypeViewModel.Id == 0)
                {
                    _roomTypeRepository.Insert(roomtype);
                }
                else
                {
                    _roomTypeRepository.Update(roomtype);
                }
                _roomTypeRepository.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(roomTypeViewModel.Id == 0 ? "AddRoomType" : "EditRoomType", roomTypeViewModel);
        }

        [HttpGet]
        public IActionResult GetRoomTypesJson()
        {
            try
            {
                var roomtype = _roomTypeRepository.GetAll();
                var result = roomtype.Select(x => new
                {
                    id = x.Id,
                    roomtype1 = x.RoomType1,
                    capacity = x.Capacity

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
            RoomType roomtype = _roomTypeRepository.GetRoomTypeById(id);
            if (roomtype != null)
            {
                var model = new RoomTypeViewModel
                {
                    Id = roomtype.Id,
                    RoomType1 = roomtype.RoomType1,
                    Capacity = roomtype.Capacity
                };

                return View("AddRoomType", model);
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoomTypeAsync(int id)
        {
            try
            {
                var roomtype = _roomTypeRepository.GetById(id);
                if (roomtype != null)
                {
                    _roomTypeRepository.Delete(roomtype);
                    _roomTypeRepository.SaveChanges();
                    return Ok(true);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}
