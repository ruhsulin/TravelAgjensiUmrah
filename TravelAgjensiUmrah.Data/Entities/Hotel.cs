using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgjensiUmrah.Data.Entities
{
    public partial class Hotel
    {
        public Hotel()
        {
            PackageHotelInMeccaNavigations = new HashSet<Package>();
            PackageHotelInMedinaNavigations = new HashSet<Package>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string? HotelName { get; set; }
        [StringLength(100)]
        public string? Location { get; set; }
        public int Stars { get; set; }
        [StringLength(255)]
        public string? Description { get; set; }
        public string? Photo { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal RoomFor2 { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal RoomFor3 { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal RoomFor4 { get; set; }

        [InverseProperty("HotelInMeccaNavigation")]
        public virtual ICollection<Package> PackageHotelInMeccaNavigations { get; set; }
        [InverseProperty("HotelInMedinaNavigation")]
        public virtual ICollection<Package> PackageHotelInMedinaNavigations { get; set; }
    }
}
