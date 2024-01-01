namespace Presentation.Areas.Admin.Models.PackageViewModel
{
    public class PackageViewModel
    {
        public int Id { get; set; }
        public string? PackageName { get; set; }
        public int Pax { get; set; }
        public decimal TicketPrice { get; set; }
        public decimal VisaPrice { get; set; }
        public bool Insurance { get; set; }
        public int HotelInMeka { get; set; }
        public int HotelInMedina { get; set; }
        public decimal FoodPrice { get; set; }
        public bool TransportationToAirport { get; set; }
        public decimal TransportationToAirportPrice { get; set; }
        public decimal TransportationInArabiaPrice { get; set; }
        public decimal IhramPrice { get; set; }
        public decimal ZemzemPrice { get; set; }
        public string RoomType { get; set; }
    }
}
