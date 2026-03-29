using EventBookingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventBookingApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // These are like your TypeORM entity registrations
    public DbSet<User> Users => Set<User>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Venue> Venues => Set<Venue>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(u => u.Email).IsUnique(); // like @Unique() in TypeORM
            entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Role).HasDefaultValue("User");
        });

        // Venue
        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Name).IsRequired().HasMaxLength(150);
            entity.Property(v => v.Location).IsRequired().HasMaxLength(200);
        });

        // Event
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.TicketPrice).HasColumnType("decimal(10,2)");
            entity.Property(e => e.Status).HasConversion<string>(); // store enum as string in DB

            // Relationship: Event -> Venue (Many-to-One)
            entity.HasOne(e => e.Venue)
                  .WithMany(v => v.Events)
                  .HasForeignKey(e => e.VenueId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Seat
        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.SeatNumber).IsRequired().HasMaxLength(10);
            entity.Property(s => s.Type).HasConversion<string>();
            entity.Property(s => s.IsBooked).HasDefaultValue(false);

            // Seat → Event (not Venue anymore)
            entity.HasOne(s => s.Event)
                  .WithMany(e => e.Seats)
                  .HasForeignKey(s => s.EventId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Booking
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.AmountPaid).HasColumnType("decimal(10,2)");
            entity.Property(b => b.Status).HasConversion<string>();

            // Relationship: Booking -> User
            entity.HasOne(b => b.User)
                  .WithMany(u => u.Bookings)
                  .HasForeignKey(b => b.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Booking -> Event
            entity.HasOne(b => b.Event)
                  .WithMany(e => e.Bookings)
                  .HasForeignKey(b => b.EventId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Booking -> Seat
            entity.HasOne(b => b.Seat)
                  .WithMany(s => s.Bookings)
                  .HasForeignKey(b => b.SeatId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Unique constraint: one seat per event (no double booking)
            entity.HasIndex(b => new { b.SeatId, b.EventId }).IsUnique();
        });
    }
}