namespace EventBookingApp.Application.DTOs.Booking;

public class BookingDto
{
    public int Id { get; set; }
    public string EventTitle { get; set; } = string.Empty;
    public string SeatNumber { get; set; } = string.Empty;
    public string SeatType { get; set; } = string.Empty;
    public decimal AmountPaid { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime BookedAt { get; set; }
}