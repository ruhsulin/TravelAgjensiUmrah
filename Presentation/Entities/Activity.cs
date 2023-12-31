using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Entities
{
    public partial class Activity
    {
        [Key]
        [Column("ActivityID")]
        public int ActivityId { get; set; }
        [StringLength(100)]
        public string? ActivityName { get; set; }
        [StringLength(255)]
        public string? Description { get; set; }
        [StringLength(100)]
        public string? Location { get; set; }
    }
}
