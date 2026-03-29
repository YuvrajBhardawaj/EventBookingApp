// IBookingRepository.cs  ← Booking-specific queries
using EventBookingApp.Domain.Entities;

namespace EventBookingApp.Domain.Interfaces;

public interface IBookingRepository : IRepository<Booking>
{
    Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId);
    Task<bool> IsSeatAlreadyBookedAsync(int seatId, int eventId);
}