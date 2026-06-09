namespace Test2.DTOs;

public record RoomHistoryDto(
    int RoomId,
    string RoomNumber,
    string Type,
    decimal PricePerNight,
    int Floor,
    IReadOnlyCollection<ReservationHistoryDto> Reservations);

public record ReservationHistoryDto(
    int ReservationId,
    DateOnly CheckInDate,
    DateOnly? CheckOutDate,
    string Status,
    GuestDto Guest,
    IReadOnlyCollection<ReservationServiceDto> Services);

public record GuestDto(
    int GuestId,
    string FirstName,
    string LastName,
    string Email,
    string Phone);

public record ReservationServiceDto(
    int ServiceId,
    string Name,
    string Description,
    decimal Price,
    int DurationMinutes,
    int Quantity,
    DateOnly ServiceDate);
