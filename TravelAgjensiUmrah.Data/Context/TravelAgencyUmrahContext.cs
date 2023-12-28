using Microsoft.EntityFrameworkCore;
using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.Data.Context
{
    public partial class TravelAgencyUmrahContext : DbContext
    {
        public TravelAgencyUmrahContext()
        {
        }

        public TravelAgencyUmrahContext(DbContextOptions<TravelAgencyUmrahContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Hotel> Hotels { get; set; } = null!;
        public virtual DbSet<Package> Packages { get; set; } = null!;
        public virtual DbSet<RoomType> RoomTypes { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<UserPicture> UserPictures { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.HasOne(d => d.Picture)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.PictureId)
                    .HasConstraintName("FK_AspNetUsers_UserPicture");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Activity1)
                    .WithMany(p => p.PackageActivity1s)
                    .HasForeignKey(d => d.Activity1Id)
                    .HasConstraintName("FK__Packages__activi__6477ECF3");

                entity.HasOne(d => d.Activity2)
                    .WithMany(p => p.PackageActivity2s)
                    .HasForeignKey(d => d.Activity2Id)
                    .HasConstraintName("FK__Packages__activi__656C112C");

                entity.HasOne(d => d.Activity3)
                    .WithMany(p => p.PackageActivity3s)
                    .HasForeignKey(d => d.Activity3Id)
                    .HasConstraintName("FK__Packages__activi__66603565");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Packages)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK__Packages__hotel___628FA481");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.Packages)
                    .HasForeignKey(d => d.RoomTypeId)
                    .HasConstraintName("FK__Packages__room_t__6383C8BA");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
