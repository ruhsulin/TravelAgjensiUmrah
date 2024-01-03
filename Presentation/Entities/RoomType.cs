using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Entities
{
    public partial class RoomType
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("RoomType")]
        [StringLength(50)]
        public string? RoomType1 { get; set; }
        public int? Capacity { get; set; }
    }
}
