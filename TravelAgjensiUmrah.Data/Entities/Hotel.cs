using System.ComponentModel.DataAnnotations;

namespace TravelAgjensiUmrah.Data.Entities
{
    public partial class Hotel
    {
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
    }
}
