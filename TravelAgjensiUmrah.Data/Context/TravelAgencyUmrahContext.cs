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
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<RoomType> RoomTypes { get; set; } = null!;
        public virtual DbSet<UserPicture> UserPictures { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasOne(d => d.HotelInMeccaNavigation)
                    .WithMany(p => p.PackageHotelInMeccaNavigations)
                    .HasForeignKey(d => d.HotelInMecca)
                    .HasConstraintName("FK__Packages__hotelI__2B0A656D");

                entity.HasOne(d => d.HotelInMedinaNavigation)
                    .WithMany(p => p.PackageHotelInMedinaNavigations)
                    .HasForeignKey(d => d.HotelInMedina)
                    .HasConstraintName("FK__Packages__hotelI__2BFE89A6");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasOne(d => d.Package)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("FK__Reservati__Packa__3D2915A8");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Reservati__UserI__3C34F16F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
