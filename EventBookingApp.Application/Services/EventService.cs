// EventBookingApp.Application/Services/EventService.cs
using EventBookingApp.Application.DTOs.Event;
using EventBookingApp.Application.Interfaces;
using EventBookingApp.Domain.Entities;
using EventBookingApp.Domain.Interfaces;

namespace EventBookingApp.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IRepository<Venue> _venueRepository;

    public EventService(IEventRepository eventRepository, IRepository<Venue> venueRepository)
    {
        _eventRepository = eventRepository;
        _venueRepository = venueRepository;
    }

    public async Task<IEnumerable<EventDto>> GetUpcomingEventsAsync()
    {
        var events = await _eventRepository.GetUpcomingEventsAsync();
        return events.Select(MapToDto);
    }

    public async Task<EventDto?> GetEventByIdAsync(int id)
    {
        var ev = await _eventRepository.GetByIdAsync(id);
        return ev == null ? null : MapToDto(ev);
    }

    public async Task<EventDto> CreateEventAsync(CreateEventRequest request)
    {
        var venue = await _venueRepository.GetByIdAsync(request.VenueId)
            ?? throw new Exception("Venue not found.");

        var ev = new Event
        {
            Title = request.Title,
            Description = request.Description,
            EventDate = request.EventDate,
            TicketPrice = request.TicketPrice,
            VenueId = request.VenueId
        };

        await _eventRepository.AddAsync(ev);
        ev.Venue = venue; // attach for mapping
        return MapToDto(ev);
    }

    private static EventDto MapToDto(Event ev) => new()
    {
        Id = ev.Id,
        Title = ev.Title,
        Description = ev.Description,
        EventDate = ev.EventDate,
        TicketPrice = ev.TicketPrice,
        Status = ev.Status.ToString(),
        VenueName = ev.Venue?.Name ?? "",
        VenueLocation = ev.Venue?.Location ?? "",
        TotalSeats = ev.Seats.Count,
        AvailableSeats = ev.Seats.Count(s => !s.IsBooked),
        BookedSeats = ev.Seats.Count(s => s.IsBooked),
        Seats = ev.Seats.Select(s => new SeatDto
        {
            Id = s.Id,
            SeatNumber = s.SeatNumber,
            Row = s.Row,
            Type = s.Type.ToString(),
            IsBooked = s.IsBooked
        }).ToList()
    };
}