using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgjensiUmrah.Data.Entities
{
    public partial class Package
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("packageName")]
        [StringLength(255)]
        public string? PackageName { get; set; }
        [Column("pax")]
        public int? Pax { get; set; }
        [Column("hotelInMecca")]
        public int? HotelInMecca { get; set; }
        [Column("hotelInMedina")]
        public int? HotelInMedina { get; set; }
        [Column("roomType")]
        [StringLength(255)]
        public string? RoomType { get; set; }
        [Column("daysInMecca")]
        public int? DaysInMecca { get; set; }
        [Column("daysInMedina")]
        public int? DaysInMedina { get; set; }
        [Column("startDay", TypeName = "datetime")]
        public DateTime? StartDay { get; set; }
        [Column("returnDay", TypeName = "datetime")]
        public DateTime? ReturnDay { get; set; }
        [Column("startTime")]
        [StringLength(10)]
        public string? StartTime { get; set; }
        [Column("mealIncluded")]
        public bool MealIncluded { get; set; }
        [Column("mealPrice", TypeName = "decimal(10, 2)")]
        public decimal? MealPrice { get; set; }
        [Column("ticketIncluded")]
        public bool TicketIncluded { get; set; }
        [Column("ticketPrice", TypeName = "decimal(10, 2)")]
        public decimal? TicketPrice { get; set; }
        [Column("visaIncluded")]
        public bool VisaIncluded { get; set; }
        [Column("visaPrice", TypeName = "decimal(10, 2)")]
        public decimal? VisaPrice { get; set; }
        [Column("ihramIncluded")]
        public bool IhramIncluded { get; set; }
        [Column("ihramPrice", TypeName = "decimal(10, 2)")]
        public decimal? IhramPrice { get; set; }
        [Column("zemzemIncluded")]
        public bool ZemzemIncluded { get; set; }
        [Column("zemzemPrice", TypeName = "decimal(10, 2)")]
        public decimal? ZemzemPrice { get; set; }
        [Column("guideGuy")]
        [StringLength(150)]
        public string? GuideGuy { get; set; }
        [Column("transportToAirportIncluded")]
        public bool TransportToAirportIncluded { get; set; }
        [Column("transportToAirportPrice", TypeName = "decimal(10, 2)")]
        public decimal? TransportToAirportPrice { get; set; }
        [Column("transportInArabiaPrice", TypeName = "decimal(10, 2)")]
        public decimal? TransportInArabiaPrice { get; set; }
        [Column("packagePrice", TypeName = "decimal(10, 2)")]
        public decimal? PackagePrice { get; set; }
        public string? Description { get; set; }
        [Column("service", TypeName = "decimal(10, 2)")]
        public decimal? Service { get; set; }

        [ForeignKey("HotelInMecca")]
        [InverseProperty("PackageHotelInMeccaNavigations")]
        public virtual Hotel? HotelInMeccaNavigation { get; set; }
        [ForeignKey("HotelInMedina")]
        [InverseProperty("PackageHotelInMedinaNavigations")]
        public virtual Hotel? HotelInMedinaNavigation { get; set; }
    }
}
