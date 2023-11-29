using System.ComponentModel.DataAnnotations;

namespace Presentation.Areas.Client.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
