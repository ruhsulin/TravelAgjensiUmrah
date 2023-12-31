using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgjensiUmrah.Data.Entities
{
    public partial class Activity
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string? ActivityName { get; set; }
        [StringLength(255)]
        public string? ActDescription { get; set; }
        [StringLength(100)]
        public string? ActLocation { get; set; }
    }
}
