// IEventRepository.cs
using EventBookingApp.Domain.Entities;

namespace EventBookingApp.Domain.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    Task<IEnumerable<Event>> GetUpcomingEventsAsync();
}