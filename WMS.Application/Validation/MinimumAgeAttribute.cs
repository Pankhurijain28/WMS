using System.ComponentModel.DataAnnotations;

namespace WMS.Application.Validation;

/// <summary>
/// Validates that a date-of-birth value is at least <paramref name="minimumAge"/> years
/// in the past. Supports both DateOnly and DateTime members.
/// </summary>
public class MinimumAgeAttribute : ValidationAttribute
{
    private readonly int _minimumAge;

    public MinimumAgeAttribute(int minimumAge)
    {
        _minimumAge = minimumAge;
    }

    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        var dob = value switch
        {
            DateOnly d => d,
            DateTime dt => DateOnly.FromDateTime(dt),
            _ => (DateOnly?)null
        };

        if (dob == null)
            return ValidationResult.Success;

        var today = DateOnly.FromDateTime(DateTime.Today);

        var age = today.Year - dob.Value.Year;

        if (dob.Value > today.AddYears(-age))
            age--;

        if (age < _minimumAge)
        {
            return new ValidationResult(
                ErrorMessage
                ?? $"Employee must be at least {_minimumAge} years old.");
        }

        return ValidationResult.Success;
    }
}
