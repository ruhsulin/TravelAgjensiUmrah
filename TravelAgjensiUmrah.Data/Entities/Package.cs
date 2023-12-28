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
        [Column("name")]
        [StringLength(100)]
        public string? Name { get; set; }
        [Column("pax")]
        public int? Pax { get; set; }
        [Column("price", TypeName = "decimal(10, 2)")]
        public decimal? Price { get; set; }
        [Column("details")]
        public string? Details { get; set; }
        [Column("hotel_id")]
        public int? HotelId { get; set; }
        [Column("room_type_id")]
        public int? RoomTypeId { get; set; }
        [Column("activity1_id")]
        public int? Activity1Id { get; set; }
        [Column("activity2_id")]
        public int? Activity2Id { get; set; }
        [Column("activity3_id")]
        public int? Activity3Id { get; set; }

        [ForeignKey("Activity1Id")]
        [InverseProperty("PackageActivity1s")]
        public virtual Activity? Activity1 { get; set; }
        [ForeignKey("Activity2Id")]
        [InverseProperty("PackageActivity2s")]
        public virtual Activity? Activity2 { get; set; }
        [ForeignKey("Activity3Id")]
        [InverseProperty("PackageActivity3s")]
        public virtual Activity? Activity3 { get; set; }
        [ForeignKey("HotelId")]
        [InverseProperty("Packages")]
        public virtual Hotel? Hotel { get; set; }
        [ForeignKey("RoomTypeId")]
        [InverseProperty("Packages")]
        public virtual RoomType? RoomType { get; set; }
    }
}
