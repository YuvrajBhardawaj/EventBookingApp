using EventBookingApp.Application.DTOs.Event;

namespace EventBookingApp.Application.Interfaces;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetUpcomingEventsAsync();
    Task<EventDto?> GetEventByIdAsync(int id);
    Task<EventDto> CreateEventAsync(CreateEventRequest request);
}