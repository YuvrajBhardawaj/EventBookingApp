using EventBookingApp.Application.DTOs.Booking;
using EventBookingApp.Application.Interfaces;
using EventBookingApp.Domain.Entities;
using EventBookingApp.Domain.Interfaces;

namespace EventBookingApp.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRepository<Event> _eventRepository;
    private readonly IRepository<Seat> _seatRepository;

    public BookingService(
        IBookingRepository bookingRepository,
        IRepository<Event> eventRepository,
        IRepository<Seat> seatRepository)
    {
        _bookingRepository = bookingRepository;
        _eventRepository = eventRepository;
        _seatRepository = seatRepository;
    }

    public async Task<BookingDto> CreateBookingAsync(int userId, CreateBookingRequest request)
    {
        var alreadyBooked = await _bookingRepository
            .IsSeatAlreadyBookedAsync(request.SeatId, request.EventId);

        if (alreadyBooked)
            throw new Exception("This seat is already booked for the event.");

        var eventItem = await _eventRepository.GetByIdAsync(request.EventId)
            ?? throw new Exception("Event not found.");

        var seat = await _seatRepository.GetByIdAsync(request.SeatId)
            ?? throw new Exception("Seat not found.");

        // ← Verify seat belongs to this event
        if (seat.EventId != request.EventId)
            throw new Exception("This seat does not belong to the selected event.");

        // ← Mark seat as booked
        seat.IsBooked = true;
        await _seatRepository.UpdateAsync(seat);

        var booking = new Booking
        {
            UserId = userId,
            EventId = request.EventId,
            SeatId = request.SeatId,
            AmountPaid = eventItem.TicketPrice,
            Status = BookingStatus.Confirmed
        };

        await _bookingRepository.AddAsync(booking);

        return new BookingDto
        {
            Id = booking.Id,
            EventTitle = eventItem.Title,
            SeatNumber = seat.SeatNumber,
            SeatType = seat.Type.ToString(),
            AmountPaid = booking.AmountPaid,
            Status = booking.Status.ToString(),
            BookedAt = booking.BookedAt
        };
    }
    public async Task<IEnumerable<BookingDto>> GetUserBookingsAsync(int userId)
    {
        var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId);

        return bookings.Select(b => new BookingDto
        {
            Id = b.Id,
            EventTitle = b.Event.Title,
            SeatNumber = b.Seat.SeatNumber,
            SeatType = b.Seat.Type.ToString(),
            AmountPaid = b.AmountPaid,
            Status = b.Status.ToString(),
            BookedAt = b.BookedAt
        });
    }

    public async Task<bool> CancelBookingAsync(int bookingId, int userId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);

        if (booking == null || booking.UserId != userId)
            return false;

        // ← Free the seat back
        var seat = await _seatRepository.GetByIdAsync(booking.SeatId);
        if (seat != null)
        {
            seat.IsBooked = false;
            await _seatRepository.UpdateAsync(seat);
        }

        booking.Status = BookingStatus.Cancelled;
        await _bookingRepository.UpdateAsync(booking);
        return true;
    }
}