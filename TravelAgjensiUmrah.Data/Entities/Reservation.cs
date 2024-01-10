
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgjensiUmrah.Data.Entities
{
    public partial class Reservation
    {
        [Key]
        public int Id { get; set; }
        [StringLength(450)]
        public string? UserId { get; set; }
        public int? PackageId { get; set; }
        public int? NumberOfPeople { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BookingDate { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? TotalPrice { get; set; }
        [StringLength(50)]
        public string? Status { get; set; }
        [StringLength(100)]
        public string? PaymentMethod { get; set; }
        public string? AdditionalRequests { get; set; }

        [ForeignKey("PackageId")]
        [InverseProperty("Reservations")]
        public virtual Package? Package { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Reservations")]
        public virtual AspNetUser? User { get; set; }
    }
}
