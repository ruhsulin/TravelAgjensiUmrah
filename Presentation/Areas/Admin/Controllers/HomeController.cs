using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Presentation.Areas.Admin.Models.DashboardViewModels;
using TravelAgjensiUmrah.App.Constants;
using TravelAgjensiUmrah.App.Interfaces;


namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    [Authorize(Roles = RoleConstants.Admin)]
    public class HomeController : Controller
    {
        private IOptions<RequestLocalizationOptions> _options;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IPackageRepository _packageRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHotelRepository _hotelRepository;


        public HomeController(IHttpContextAccessor httpContextAccessor, IOptions<RequestLocalizationOptions> options, IPackageRepository packageRepository, IReservationRepository reservationRepository, IUserRepository userRepository, IHotelRepository hotelRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _options = options;
            _packageRepository = packageRepository;
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _hotelRepository = hotelRepository;
        }

        // Index
        public IActionResult Index()
        {
            try
            {
                var model = new DashboardViewModel();
                model.NoPackages = _packageRepository.GetAll().Count();
                model.NoHotels = _hotelRepository.GetAll().Count();
                model.NoUsers = _userRepository.GetAll().Count();
                model.NoReservations = _reservationRepository.GetAll().Count();

                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IActionResult SetLanguage(string culture)
        {
            try
            {
                var cultureItems = _options.Value.SupportedUICultures!.Select(x => x.Name).ToArray();
                if (cultureItems.Any(x => x.Equals(culture)))
                {
                    Response.Cookies.Append(
                        CookieRequestCultureProvider.DefaultCookieName,
                        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), HttpOnly = true, Secure = _httpContextAccessor.HttpContext!.Request.IsHttps }
                    );
                }

                // Get the referring URL
                string returnUrl = Request.Headers["Referer"].ToString();

                // If the referring URL is empty or null, redirect to a default page
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }

                return Redirect(returnUrl); // Redirect back to the previous URL
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
