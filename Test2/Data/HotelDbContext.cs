using Microsoft.EntityFrameworkCore;
using Test2.Entities;

namespace Test2.Data;

public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options)
{
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<ReservationService> ReservationServices => Set<ReservationService>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(x => x.GuestId);
            entity.Property(x => x.FirstName).HasMaxLength(50).IsUnicode(false);
            entity.Property(x => x.LastName).HasMaxLength(100).IsUnicode(false);
            entity.Property(x => x.Email).HasMaxLength(100).IsUnicode(false);
            entity.Property(x => x.Phone).HasMaxLength(9).IsUnicode(false);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(x => x.RoomId);
            entity.Property(x => x.RoomNumber).HasMaxLength(10).IsUnicode(false);
            entity.Property(x => x.Type).HasMaxLength(50).IsUnicode(false);
            entity.Property(x => x.PricePerNight).HasPrecision(10, 2);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(x => x.ReservationId);
            entity.Property(x => x.Status).HasMaxLength(50).IsUnicode(false);
            entity.Property(x => x.CheckInDate).HasColumnType("date");
            entity.Property(x => x.CheckOutDate).HasColumnType("date");

            entity.HasOne(x => x.Guest)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Room)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(x => x.ServiceId);
            entity.Property(x => x.Name).HasMaxLength(100).IsUnicode(false);
            entity.Property(x => x.Description).HasMaxLength(200).IsUnicode(false);
            entity.Property(x => x.Price).HasPrecision(10, 2);
        });

        modelBuilder.Entity<ReservationService>(entity =>
        {
            entity.ToTable("Reservation_Services");
            entity.HasKey(x => new { x.ReservationId, x.ServiceId });
            entity.Property(x => x.ServiceDate).HasColumnType("date");

            entity.HasOne(x => x.Reservation)
                .WithMany(x => x.ReservationServices)
                .HasForeignKey(x => x.ReservationId);

            entity.HasOne(x => x.Service)
                .WithMany(x => x.ReservationServices)
                .HasForeignKey(x => x.ServiceId);
        });
    }
}
