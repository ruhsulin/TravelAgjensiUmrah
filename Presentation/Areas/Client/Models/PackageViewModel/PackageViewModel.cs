using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Areas.Client.Models.PackageViewModel
{
    public class PackageViewModel
    {
        public int Id { get; set; }
        public string? PackageName { get; set; }
        public int? Pax { get; set; }
        public int? HotelInMeka { get; set; }
        public int? HotelInMedina { get; set; }
        public string? HotelInMeccaName { get; set; }
        public string? HotelInMedinaName { get; set; }
        public SelectList? MekeHotels { get; set; }
        public SelectList? MedinaHotels { get; set; }
        public int? SelectedMekeHotelId { get; set; }
        public int? SelectedMedinaHotelId { get; set; }
        public string? RoomType { get; set; }
        public int? DaysInMedina { get; set; }
        public int? DaysInMecca { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? ReturnDay { get; set; }
        public string? StartTime { get; set; }
        public bool FoodIncluded { get; set; }
        public decimal? FoodPrice { get; set; }

        public bool TicketIncluded { get; set; }
        public decimal? TicketPrice { get; set; }
        public bool VisaIncluded { get; set; }
        public decimal? VisaPrice { get; set; }
        public bool Insurance { get; set; }
        public bool IhramIncluded { get; set; }
        public decimal? IhramPrice { get; set; }
        public bool ZemZemIncluded { get; set; }
        public decimal? ZemzemPrice { get; set; }
        public string? GuideGuyName { get; set; }
        public bool TransportationToAirportIncluded { get; set; }
        public decimal? TransportationToAirportPrice { get; set; }
        public decimal? TransportationInArabiaPrice { get; set; }
        public string? Description { get; set; }
        public decimal? Service { get; set; }
        public decimal? PackagePrice { get; set; }
        public string? PicturePath { get; set; }
        public int? TotalDays { get; set; }


    }
}
