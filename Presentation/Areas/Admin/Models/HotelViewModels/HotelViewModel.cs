namespace Presentation.Areas.Admin.Models.HotelViewModels
{
    public class HotelViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Stars { get; set; }
        public IFormFile? HotelPicture { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public decimal RoomFor2 { get; set; }
        public decimal RoomFor3 { get; set; }
        public decimal RoomFor4 { get; set; }
    }
}
