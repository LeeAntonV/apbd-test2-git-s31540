namespace Test2.Entities;

public class Reservation
{
    public int ReservationId { get; set; }
    public int GuestId { get; set; }
    public int RoomId { get; set; }
    public DateOnly CheckInDate { get; set; }
    public DateOnly? CheckOutDate { get; set; }
    public string Status { get; set; } = null!;

    public Guest Guest { get; set; } = null!;
    public Room Room { get; set; } = null!;
    public ICollection<ReservationService> ReservationServices { get; set; } =
        new List<ReservationService>();
}
