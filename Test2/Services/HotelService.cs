using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.DTOs;
using Test2.Entities;

namespace Test2.Services;

public class HotelService(HotelDbContext context) : IHotelService
{
    public async Task<RoomHistoryDto?> GetRoomHistoryAsync(
        int roomId,
        CancellationToken cancellationToken)
    {
        return await context.Rooms
            .AsNoTracking()
            .Where(room => room.RoomId == roomId)
            .Select(room => new RoomHistoryDto(
                room.RoomId,
                room.RoomNumber,
                room.Type,
                room.PricePerNight,
                room.Floor,
                room.Reservations
                    .OrderByDescending(reservation => reservation.CheckInDate)
                    .Select(reservation => new ReservationHistoryDto(
                        reservation.ReservationId,
                        reservation.CheckInDate,
                        reservation.CheckOutDate,
                        reservation.Status,
                        new GuestDto(
                            reservation.Guest.GuestId,
                            reservation.Guest.FirstName,
                            reservation.Guest.LastName,
                            reservation.Guest.Email,
                            reservation.Guest.Phone),
                        reservation.ReservationServices
                            .OrderBy(item => item.ServiceDate)
                            .Select(item => new ReservationServiceDto(
                                item.ServiceId,
                                item.Service.Name,
                                item.Service.Description,
                                item.Service.Price,
                                item.Service.DurationMinutes,
                                item.Quantity,
                                item.ServiceDate))
                            .ToList()))
                    .ToList()))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<CreateGuestResult> CreateGuestWithReservationAsync(
        CreateGuestRequest request,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        if (!await context.Rooms.AnyAsync(
                room => room.RoomId == request.RoomId,
                cancellationToken))
        {
            await transaction.RollbackAsync(cancellationToken);
            return CreateGuestResult.RoomNotFound();
        }

        var guest = new Guest
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone
        };

        var reservation = new Reservation
        {
            Guest = guest,
            RoomId = request.RoomId,
            CheckInDate = request.CheckInDate,
            CheckOutDate = request.CheckOutDate,
            Status = request.Status
        };

        context.Reservations.Add(reservation);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return CreateGuestResult.Success(guest.GuestId, reservation.ReservationId);
    }
}
