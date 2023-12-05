using Microsoft.AspNetCore.Mvc;
using TravelAgjensiUmrah.App.Interfaces;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()

        {
            // Call GetUsersJson action to retrieve users' JSON data
            IActionResult usersJsonResult = GetUsersJson();

            // Access the JSON data from the IActionResult
            JsonResult jsonResult = usersJsonResult as JsonResult;
            var usersData = jsonResult?.Value; // Contains the users' JSON data

            // Process 'usersData' as needed in your Index2 action

            return View();
        }

        // Marrim te gjithe users
        [HttpGet]
        public IActionResult GetUsersJson()
        {
            // GetAll() - metod qe e kemi brenda IUserRepository
            var users = _userRepository.GetAll();

            var result = users.Select(x => new
            {

                name = x.Name,
                surname = x.Surname,
                email = x.Email,
                emailConfirmed = x.EmailConfirmed
            });

            return new JsonResult(result);

        }
    }
}
