using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgjensiUmrah.App.Constants;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    [Authorize(Roles = RoleConstants.Admin)]
    public class HomeController : Controller
    {
        // Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
