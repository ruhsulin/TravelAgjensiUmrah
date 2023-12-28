using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TravelAgjensiUmrah.App.Constants;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    [Authorize(Roles = RoleConstants.Admin)]
    public class HomeController : Controller
    {
        private IOptions<RequestLocalizationOptions> _options;
        private IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor, IOptions<RequestLocalizationOptions> options)
        {
            _httpContextAccessor = httpContextAccessor;
            _options = options;
        }

        // Index
        public IActionResult Index()
        {
            return View();
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
