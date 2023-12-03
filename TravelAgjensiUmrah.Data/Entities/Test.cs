using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgjensiUmrah.Data.Entities
{
    [Table("Test")]
    public partial class Test
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("testName")]
        [StringLength(150)]
        public string? TestName { get; set; }
    }
}
