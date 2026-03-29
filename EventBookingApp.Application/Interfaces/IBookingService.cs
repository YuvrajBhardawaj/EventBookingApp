using EventBookingApp.Application.DTOs.Booking;

namespace EventBookingApp.Application.Interfaces;

public interface IBookingService
{
    Task<BookingDto> CreateBookingAsync(int userId, CreateBookingRequest request);
    Task<IEnumerable<BookingDto>> GetUserBookingsAsync(int userId);
    Task<bool> CancelBookingAsync(int bookingId, int userId);
}