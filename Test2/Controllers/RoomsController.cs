using Microsoft.AspNetCore.Mvc;
using Test2.Services;

namespace Test2.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomsController(IHotelService hotelService) : ControllerBase
{
    [HttpGet("{id:int}/guests")]
    public async Task<IActionResult> GetGuests(
        int id,
        CancellationToken cancellationToken)
    {
        var roomHistory =
            await hotelService.GetRoomHistoryAsync(id, cancellationToken);

        return roomHistory is null
            ? NotFound(new { message = $"Room with ID {id} does not exist." })
            : Ok(roomHistory);
    }
}
