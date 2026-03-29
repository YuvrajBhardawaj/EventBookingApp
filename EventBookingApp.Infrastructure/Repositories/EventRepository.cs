using EventBookingApp.Domain.Entities;
using EventBookingApp.Domain.Interfaces;
using EventBookingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventBookingApp.Infrastructure.Repositories;

public class EventRepository : BaseRepository<Event>, IEventRepository
{
    public EventRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Event>> GetUpcomingEventsAsync()
    {
        return await _context.Events
            .Include(e => e.Venue)
            .Include(e => e.Seats) // ← include seats
            .Where(e => e.Status == EventStatus.Upcoming && e.EventDate > DateTime.UtcNow)
            .OrderBy(e => e.EventDate)
            .ToListAsync();
    }

    public override async Task<Event?> GetByIdAsync(int id)
    {
        return await _context.Events
            .Include(e => e.Venue)
            .Include(e => e.Seats) // ← include seats
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}