namespace Test2.Entities;

public class Service
{
    public int ServiceId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int DurationMinutes { get; set; }

    public ICollection<ReservationService> ReservationServices { get; set; } =
        new List<ReservationService>();
}
