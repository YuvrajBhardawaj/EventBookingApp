namespace EventBookingApp.Domain.Entities;

public class Seat
{
    public int Id { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public string Row { get; set; } = string.Empty;
    public SeatType Type { get; set; } = SeatType.General;
    public bool IsBooked { get; set; } = false; // ← track availability

    // Seat now belongs to an EVENT, not a venue
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

public enum SeatType
{
    General,
    VIP,
    Premium
}