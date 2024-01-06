using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    public class ReservationsController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
