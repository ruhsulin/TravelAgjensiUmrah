using System.ComponentModel.DataAnnotations;

namespace Presentation.Areas.Client.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
