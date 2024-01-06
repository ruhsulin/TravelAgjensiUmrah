using Microsoft.AspNetCore.Mvc;
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
    }
}
