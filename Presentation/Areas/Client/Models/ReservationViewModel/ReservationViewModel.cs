using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Areas.Client.Models.ReservationViewModel
{

    public class ReservationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User")]
        public string? UserId { get; set; }
        public SelectList? Users { get; set; } // For dropdown list of users

        [Display(Name = "Package")]
        public int? PackageId { get; set; }
        public SelectList? Packages { get; set; } // For dropdown list of packages

        [Required]
        [Display(Name = "Number of People")]
        public int? NumberOfPeople { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Booking Date")]
        public DateTime? BookingDate { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Total Price")]
        public decimal? TotalPrice { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Status")]
        public string? Status { get; set; } // e.g., 'Pending', 'Confirmed', 'Cancelled'

        [StringLength(100)]
        [Display(Name = "Payment Method")]
        public string? PaymentMethod { get; set; } // e.g., 'Credit Card', 'PayPal'

        [Display(Name = "Additional Requests")]
        public string? AdditionalRequests { get; set; }

        // Optional: You can include properties for displaying related data
        public string? UserName { get; set; } // To display user's name
        public string? PackageName { get; set; } // To display package's name
    }
}
