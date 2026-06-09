namespace Test2.Entities;

public class ReservationService
{
    public int ReservationId { get; set; }
    public int ServiceId { get; set; }
    public int Quantity { get; set; }
    public DateOnly ServiceDate { get; set; }

    public Reservation Reservation { get; set; } = null!;
    public Service Service { get; set; } = null!;
}
