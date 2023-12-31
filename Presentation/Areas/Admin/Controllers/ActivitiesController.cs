using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.ActivityViewModels;
using Presentation.FileHelper;
using TravelAgjensiUmrah.App.Constants;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Entities;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    public class ActivitiesController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IFileHelper _fileHelper;

        public ActivitiesController(IActivityRepository activityRepository, IFileHelper fileHelper)
        {
            _activityRepository = activityRepository;
            _fileHelper = fileHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetActivitiesJson()
        {
            try
            {
                var activities = _activityRepository.GetAll();
                var result = activities.Select(x => new
                {
                    id = x.Id,
                    name = x.ActivityName,
                    location = x.ActLocation,
                    description = x.ActDescription

                });

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult AddActivity()
        {
            return View(new ActivityViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> AddOrEditActivity(ActivityViewModel activityViewModel)
        {
            if (ModelState.IsValid)
            {
                Activity activity;
                if (activityViewModel.Id == 0)
                {
                    // Create new hotel
                    activity = new Activity();
                }
                else
                {
                    // Update existing hotel
                    activity = _activityRepository.GetActivityById(activityViewModel.Id);
                    if (activity == null)
                    {
                        return NotFound();
                    }
                }

                // Map properties
                activity.ActivityName = activityViewModel.ActivityName;
                activity.ActLocation = activityViewModel.ActivityLocation;
                activity.ActDescription = activityViewModel.ActivityDescription;

                if (activityViewModel.Id == 0)
                {
                    _activityRepository.Insert(activity);
                }
                else
                {
                    _activityRepository.Update(activity);
                }
                _activityRepository.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(activityViewModel.Id == 0 ? "AddHotel" : "EditHotel", activityViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Activity activity = _activityRepository.GetActivityById(id); // Assuming GetById method exists in your repository
            if (activity != null)
            {
                var model = new ActivityViewModel
                {
                    Id = activity.Id,
                    ActivityName = activity.ActivityName,
                    ActivityLocation = activity.ActLocation,
                    ActivityDescription = activity.ActDescription
                    // Handle HotelPicture if needed. Typically, for editing, you might show the existing picture and provide an option to change it.
                };

                // If you have additional data to load (like select lists), load them here

                return View("AddActivity", model); // Reuse the AddHotel view if it's set up to handle both add and edit scenarios
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteActivityAsync(int id)
        {
            try
            {
                var hotel = _activityRepository.GetById(id); // Synchronous call
                if (hotel != null)
                {
                    _activityRepository.Delete(hotel);
                    _activityRepository.SaveChanges(); // Synchronous call
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
