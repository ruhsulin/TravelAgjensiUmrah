using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Areas.Admin.Models.UserViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; } = null;
        public string? Name { get; set; } = null;
        public string? Surname { get; set; } = null;
        public string? Email { get; set; } = null;
        public bool EmailConfirmed { get; set; }
        public string? Password { get; set; } = null;
        public string? ConfirmPassword { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        public bool PhoneNumberConfirmed { get; set; }
        public string? RoleId { get; set; } = null;
        public IFormFile? Picture { get; set; }
        public SelectList? Roles { get; set; }



    }
}
