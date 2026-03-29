using EventBookingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventBookingApp.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Venues.AnyAsync()) return;

        // ── Venues (IPL Stadiums) ─────────────────────────
        var venues = new List<Venue>
        {
            new Venue
            {
                Name = "Wankhede Stadium",
                Location = "Mumbai, Maharashtra",
                TotalCapacity = 33000
            },
            new Venue
            {
                Name = "M. Chinnaswamy Stadium",
                Location = "Bengaluru, Karnataka",
                TotalCapacity = 40000
            },
            new Venue
            {
                Name = "Eden Gardens",
                Location = "Kolkata, West Bengal",
                TotalCapacity = 66000
            },
            new Venue
            {
                Name = "Narendra Modi Stadium",
                Location = "Ahmedabad, Gujarat",
                TotalCapacity = 132000
            }
        };

        await context.Venues.AddRangeAsync(venues);
        await context.SaveChangesAsync();

        // ── Seats per Venue ───────────────────────────────
        var allSeats = new List<Seat>();
        foreach (var venue in venues)
        {
            // VIP rows A-B (10 seats each)
            foreach (var row in new[] { "A", "B" })
                for (int i = 1; i <= 10; i++)
                    allSeats.Add(new Seat
                    {
                        SeatNumber = $"{row}{i}",
                        Row = row,
                        Type = SeatType.VIP,
                        VenueId = venue.Id
                    });

            // Premium rows C-E (10 seats each)
            foreach (var row in new[] { "C", "D", "E" })
                for (int i = 1; i <= 10; i++)
                    allSeats.Add(new Seat
                    {
                        SeatNumber = $"{row}{i}",
                        Row = row,
                        Type = SeatType.Premium,
                        VenueId = venue.Id
                    });

            // General rows F-J (10 seats each)
            foreach (var row in new[] { "F", "G", "H", "I", "J" })
                for (int i = 1; i <= 10; i++)
                    allSeats.Add(new Seat
                    {
                        SeatNumber = $"{row}{i}",
                        Row = row,
                        Type = SeatType.General,
                        VenueId = venue.Id
                    });
        }

        await context.Seats.AddRangeAsync(allSeats);
        await context.SaveChangesAsync();

        // ── IPL 2025 Matches (Apr - May) ──────────────────
        var wankhede = venues[0];
        var chinnaswamy = venues[1];
        var eden = venues[2];
        var modiStadium = venues[3];

        var matches = new List<Event>
        {
            // April matches
            new Event
            {
                Title = "MI vs CSK",
                Description = "Mumbai Indians vs Chennai Super Kings — The El Clasico of IPL!",
                EventDate = new DateTime(2025, 4, 2, 19, 30, 0, DateTimeKind.Utc),
                TicketPrice = 3500,
                Status = EventStatus.Upcoming,
                VenueId = wankhede.Id
            },
            new Event
            {
                Title = "RCB vs KKR",
                Description = "Royal Challengers Bengaluru vs Kolkata Knight Riders.",
                EventDate = new DateTime(2025, 4, 5, 19, 30, 0, DateTimeKind.Utc),
                TicketPrice = 2500,
                Status = EventStatus.Upcoming,
                VenueId = chinnaswamy.Id
            },
            new Event
            {
                Title = "KKR vs MI",
                Description = "Kolkata Knight Riders vs Mumbai Indians at Eden Gardens.",
                EventDate = new DateTime(2025, 4, 10, 19, 30, 0, DateTimeKind.Utc),
                TicketPrice = 2000,
                Status = EventStatus.Upcoming,
                VenueId = eden.Id
            },
            new Event
            {
                Title = "GT vs RCB",
                Description = "Gujarat Titans vs Royal Challengers Bengaluru at home.",
                EventDate = new DateTime(2025, 4, 15, 19, 30, 0, DateTimeKind.Utc),
                TicketPrice = 1800,
                Status = EventStatus.Upcoming,
                VenueId = modiStadium.Id
            },
            new Event
            {
                Title = "CSK vs RCB",
                Description = "Chennai Super Kings vs Royal Challengers Bengaluru.",
                EventDate = new DateTime(2025, 4, 20, 19, 30, 0, DateTimeKind.Utc),
                TicketPrice = 3000,
                Status = EventStatus.Upcoming,
                VenueId = wankhede.Id
            },
            new Event
            {
                Title = "MI vs KKR",
                Description = "Mumbai Indians vs Kolkata Knight Riders.",
                EventDate = new DateTime(2025, 4, 25, 19, 30, 0, DateTimeKind.Utc),
                TicketPrice = 2800,
                Status = EventStatus.Upcoming,
                VenueId = wankhede.Id
            },

            // May matches
            new Event
            {
                Title = "RCB vs GT",
                Description = "Royal Challengers Bengaluru vs Gujarat Titans.",
                EventDate = new DateTime(2025, 5, 1, 19, 30, 0, DateTimeKind.Utc),
                TicketPrice = 2200,
                Status = EventStatus.Upcoming,
                VenueId = chinnaswamy.Id
            },
            new Event
            {
                Title = "KKR vs CSK",
                Description = "Kolkata Knight Riders vs Chennai Super Kings.",
                EventDate = new DateTime(2025, 5, 6, 19, 30, 0, DateTimeKind.Utc),
                TicketPrice = 2500,
                Status = EventStatus.Upcoming,
                VenueId = eden.Id
            },
            new Event
            {
                Title = "GT vs MI",
                Description = "Gujarat Titans vs Mumbai Indians.",
                EventDate = new DateTime(2025, 5, 11, 19, 30, 0, DateTimeKind.Utc),
                TicketPrice = 1500,
                Status = EventStatus.Upcoming,
                VenueId = modiStadium.Id
            },
            new Event
            {
                Title = "IPL 2025 Final",
                Description = "The Grand Finale — Top 2 teams battle for the IPL Trophy!",
                EventDate = new DateTime(2025, 5, 25, 19, 30, 0, DateTimeKind.Utc),
                TicketPrice = 8000,
                Status = EventStatus.Upcoming,
                VenueId = modiStadium.Id
            }
        };

        await context.Events.AddRangeAsync(matches);
        await context.SaveChangesAsync();

        // ── Admin User ────────────────────────────────────
        if (!await context.Users.AnyAsync())
        {
            var admin = new User
            {
                FullName = "Admin",
                Email = "admin@iplbooking.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = "Admin"
            };
            await context.Users.AddAsync(admin);
            await context.SaveChangesAsync();
        }

        Console.WriteLine("✅ IPL 2025 seed data inserted successfully!");
    }
}