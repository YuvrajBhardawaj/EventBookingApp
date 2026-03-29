using EventBookingApp.Domain.Entities;
using EventBookingApp.Domain.Interfaces;
using EventBookingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventBookingApp.Infrastructure.Repositories;

public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    public BookingRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId)
    {
        return await _context.Bookings
            .Include(b => b.Event)   // like TypeORM's relations: ['event']
            .Include(b => b.Seat)
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> IsSeatAlreadyBookedAsync(int seatId, int eventId)
    {
        return await _context.Bookings
            .AnyAsync(b => b.SeatId == seatId
                        && b.EventId == eventId
                        && b.Status != BookingStatus.Cancelled);
    }
}