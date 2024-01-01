using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgjensiUmrah.Data.Entities
{
    public partial class Package
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string? PackageName { get; set; }
        public int Pax { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TicketPrice { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal VisaPrice { get; set; }
        public bool InsurancePrice { get; set; }
        public int HotelInMecca { get; set; }
        public int HotelInMedina { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal FoodPrice { get; set; }
        public bool TransportToAirport { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TransportationInArabiaPrice { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TransportationToAirportPrice { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal IhramPrice { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ZemzemPrice { get; set; }
        [StringLength(50)]
        public string RoomType { get; set; }

        [ForeignKey("HotelInMecca")]
        [InverseProperty("PackageHotelInMeccaNavigations")]
        public virtual Hotel? HotelInMeccaNavigation { get; set; }
        [ForeignKey("HotelInMedina")]
        [InverseProperty("PackageHotelInMedinaNavigations")]
        public virtual Hotel? HotelInMedinaNavigation { get; set; }
    }
}
