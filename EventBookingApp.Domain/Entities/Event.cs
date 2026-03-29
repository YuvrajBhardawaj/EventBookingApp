namespace EventBookingApp.Domain.Entities;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public decimal TicketPrice { get; set; }
    public EventStatus Status { get; set; } = EventStatus.Upcoming;

    public int VenueId { get; set; }
    public Venue Venue { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Seat> Seats { get; set; } = new List<Seat>(); // ← added
}

public enum EventStatus
{
    Upcoming,
    Ongoing,
    Completed,
    Cancelled
}