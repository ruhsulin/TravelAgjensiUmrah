using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgjensiUmrah.Data.Entities
{
    public partial class Activity
    {
        public Activity()
        {
            PackageActivity1s = new HashSet<Package>();
            PackageActivity2s = new HashSet<Package>();
            PackageActivity3s = new HashSet<Package>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(100)]
        public string? Name { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("photo")]
        public string? Photo { get; set; }

        [InverseProperty("Activity1")]
        public virtual ICollection<Package> PackageActivity1s { get; set; }
        [InverseProperty("Activity2")]
        public virtual ICollection<Package> PackageActivity2s { get; set; }
        [InverseProperty("Activity3")]
        public virtual ICollection<Package> PackageActivity3s { get; set; }
    }
}
