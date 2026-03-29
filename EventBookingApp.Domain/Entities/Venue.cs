namespace EventBookingApp.Domain.Entities;

public class Venue
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int TotalCapacity { get; set; }

    public ICollection<Event> Events { get; set; } = new List<Event>();

}