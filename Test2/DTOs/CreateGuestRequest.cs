using System.ComponentModel.DataAnnotations;

namespace Test2.DTOs;

public class CreateGuestRequest : IValidatableObject
{
    [Required, MaxLength(50)]
    public string FirstName { get; init; } = null!;

    [Required, MaxLength(100)]
    public string LastName { get; init; } = null!;

    [Required, EmailAddress, MaxLength(100)]
    public string Email { get; init; } = null!;

    [Required, RegularExpression(@"^\d{9}$")]
    public string Phone { get; init; } = null!;

    [Range(1, int.MaxValue)]
    public int RoomId { get; init; }

    public DateOnly CheckInDate { get; init; }
    public DateOnly? CheckOutDate { get; init; }

    [Required, MaxLength(50)]
    public string Status { get; init; } = null!;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CheckInDate == default)
            yield return new ValidationResult(
                "Check-in date is required.",
                [nameof(CheckInDate)]);

        if (CheckOutDate.HasValue && CheckOutDate.Value < CheckInDate)
            yield return new ValidationResult(
                "Check-out date cannot be earlier than check-in date.",
                [nameof(CheckOutDate)]);
    }
}

public record CreateGuestResponse(int GuestId, int ReservationId);
