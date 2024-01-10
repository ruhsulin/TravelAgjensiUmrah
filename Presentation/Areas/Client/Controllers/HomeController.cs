using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Client.Models;
using System.Diagnostics;
using TravelAgjensiUmrah.App.Constants;

namespace Presentation.Areas.Client
{
    [Area(AreasConstants.Client)]
    [Authorize(Roles = RoleConstants.Client)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ShowTopbar = true;
            ViewBag.ShowNavbar = true;
            ViewBag.ShowHero = true;
            ViewBag.ShowAboutUsPage = true;
            ViewBag.ShowReadUsButton = true;
            return View();
        }

        public IActionResult AboutUs()
        {
            ViewBag.ShowNavbar = true;
            ViewBag.ShowAboutUsPage = true;
            return View();
        }

        public IActionResult ContactUs()
        {
            ViewBag.ShowNavbar = true;
            return View();
        }

        public IActionResult Faq()
        {
            ViewBag.ShowNavbar = true;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
