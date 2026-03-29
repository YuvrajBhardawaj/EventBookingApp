namespace EventBookingApp.Application.DTOs.Event;

public class CreateEventRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public decimal TicketPrice { get; set; }
    public int VenueId { get; set; }
}