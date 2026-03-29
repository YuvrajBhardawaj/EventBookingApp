namespace EventBookingApp.Application.DTOs.Event;

public class EventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public decimal TicketPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public string VenueName { get; set; } = string.Empty;
    public string VenueLocation { get; set; } = string.Empty;

    // ← Seat availability
    public int TotalSeats { get; set; }
    public int AvailableSeats { get; set; }
    public int BookedSeats { get; set; }
    public List<SeatDto> Seats { get; set; } = new();
}

public class SeatDto
{
    public int Id { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public string Row { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsBooked { get; set; }
}