using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgjensiUmrah.Data.Entities
{
    public partial class RoomType
    {
        public RoomType()
        {
            Packages = new HashSet<Package>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("type_name")]
        [StringLength(50)]
        public string? TypeName { get; set; }
        [Column("capacity")]
        public int? Capacity { get; set; }
        [Column("price_modifier", TypeName = "decimal(10, 2)")]
        public decimal? PriceModifier { get; set; }

        [InverseProperty("RoomType")]
        public virtual ICollection<Package> Packages { get; set; }
    }
}
