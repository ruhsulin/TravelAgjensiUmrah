using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgjensiUmrah.App.Constants;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.Admin)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
