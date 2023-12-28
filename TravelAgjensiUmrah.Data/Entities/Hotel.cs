using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgjensiUmrah.Data.Entities
{
    public partial class Hotel
    {
        public Hotel()
        {
            Packages = new HashSet<Package>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(100)]
        public string? Name { get; set; }
        [Column("stars")]
        public int? Stars { get; set; }
        [Column("photo")]
        public string? Photo { get; set; }
        [Column("location")]
        [StringLength(100)]
        public string? Location { get; set; }

        [InverseProperty("Hotel")]
        public virtual ICollection<Package> Packages { get; set; }
    }
}
