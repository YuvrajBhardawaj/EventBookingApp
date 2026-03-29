namespace EventBookingApp.Application.DTOs.Booking;

public class CreateBookingRequest
{
    public int EventId { get; set; }
    public int SeatId { get; set; }
}