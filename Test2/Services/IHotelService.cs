using Test2.DTOs;

namespace Test2.Services;

public interface IHotelService
{
    Task<RoomHistoryDto?> GetRoomHistoryAsync(int roomId, CancellationToken cancellationToken);
    Task<CreateGuestResult> CreateGuestWithReservationAsync(
        CreateGuestRequest request,
        CancellationToken cancellationToken);
}

public record CreateGuestResult(
    bool Succeeded,
    string? Error,
    CreateGuestResponse? Value)
{
    public static CreateGuestResult RoomNotFound() =>
        new(false, "Room with the specified ID does not exist.", null);

    public static CreateGuestResult Success(int guestId, int reservationId) =>
        new(true, null, new CreateGuestResponse(guestId, reservationId));
}
