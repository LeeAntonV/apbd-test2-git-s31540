using Microsoft.AspNetCore.Mvc;
using Test2.DTOs;
using Test2.Services;

namespace Test2.Controllers;

[ApiController]
[Route("api/guests")]
public class GuestsController(IHotelService hotelService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateGuest(
        CreateGuestRequest request,
        CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        if (request.CheckInDate < today)
        {
            return BadRequest(new
            {
                message = $"Check-in date cannot be earlier than {today:yyyy-MM-dd}."
            });
        }

        var result = await hotelService.CreateGuestWithReservationAsync(
            request,
            cancellationToken);

        if (!result.Succeeded)
            return NotFound(new { message = result.Error });

        return StatusCode(StatusCodes.Status201Created, result.Value);
    }
}
