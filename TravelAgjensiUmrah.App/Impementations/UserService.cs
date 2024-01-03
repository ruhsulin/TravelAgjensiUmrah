
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Entities;
using TravelAgjensiUmrah.Data.Identity;

namespace TravelAgjensiUmrah.App.Impementations
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly Repository _userRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRolesRepository _rolesRepository;
        private HttpContext _httpContext { get { return _contextAccessor.HttpContext; } }

        public UserService(IHttpContextAccessor contextAccessor,
                            UserManager<ApplicationUser> userManager,
                            IUserRepository userRepository,
                            IRolesRepository rolesRepository)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _userRepository = userRepository;
            _rolesRepository = rolesRepository;
            if (_httpContext.User.Identity!.IsAuthenticated)
            {
                var id = userManager.GetUserId(_httpContext.User);
                CurrentUser = userRepository.GetByStringId(id);
                CurrentRole = _rolesRepository.GetByUserId(id);
            }
        }

        private AspNetUser? CurrentUser { get; set; }
        private AspNetRole? CurrentRole { get; set; }
        public string GetUserEmail()
        {
            if (CurrentUser != null)
            {
                return CurrentUser.Email!;
            }
            else
            {
                return "";
            }
        }

        public string GetUserId()
        {
            try
            {


                if (CurrentUser != null)
                {
                    return CurrentUser.Id;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetUserName()
        {
            if (CurrentUser != null)
            {
                return CurrentUser.UserName!;
            }
            else
            {
                return "";
            }
        }

        public string? GetUserPhoneNumber()
        {
            if (CurrentUser != null)
            {
                return CurrentUser.PhoneNumber;
            }
            else
            {
                return "";
            }
        }

        public string GetUserRole()
        {
            if (CurrentRole != null)
            {
                return CurrentRole.Name!;
            }
            else
            {
                return "";
            }
        }

        public string GetFullName()
        {
            if (CurrentUser != null)
            {
                return CurrentUser.Name + " " + CurrentUser.Surname;
            }
            else
            {
                return "";
            }
        }

        public string FormatCookie(string cookie)
        {
            // c=sq-AL|uic=sq-AL
            var list = cookie.Split('=', StringSplitOptions.RemoveEmptyEntries).ToList();

            return list[list.Count - 1];
        }


        public string GetCulture(HttpContext httpContext)
        {
            var cookie = httpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

            if (cookie != null)
            {
                var formatedCookie = FormatCookie(cookie);

                return formatedCookie;
            }
            else
            {
                // Fallback
                return "sq-AL";
            }
        }

        public string GetProfilePicture(bool thumbnail)
        {
            throw new NotImplementedException();
            //if (CurrentUser != null && CurrentUser.PictureId.HasValue)
            //{
            //    var picture = _userRepository.GetUserPicture(CurrentUser.PictureId.Value);
            //    if (picture != null)
            //    {
            //        return picture.Path;  // Assuming Path stores the URL of the picture
            //    }
            //}
            //return "/path/to/default/image.jpg";  // Path to a default image if user doesn't have a picture

        }
    }
}
