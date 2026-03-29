namespace EventBookingApp.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public DateTime BookedAt { get; set; } = DateTime.UtcNow;
    public BookingStatus Status { get; set; } = BookingStatus.Confirmed;
    public decimal AmountPaid { get; set; }

    // Foreign keys
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    public int SeatId { get; set; }
    public Seat Seat { get; set; } = null!;
}

public enum BookingStatus
{
    Confirmed,
    Cancelled,
    Pending
}