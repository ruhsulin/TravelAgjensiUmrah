using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgjensiUmrah.Data.Entities
{
    [Table("UserPicture")]
    public partial class UserPicture
    {
        public UserPicture()
        {
            AspNetUsers = new HashSet<AspNetUser>();
            Packages = new HashSet<Package>();
        }

        [Key]
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? Extension { get; set; }
        public string? Path { get; set; }

        [InverseProperty("Picture")]
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        [InverseProperty("Picture")]
        public virtual ICollection<Package> Packages { get; set; }
    }
}
