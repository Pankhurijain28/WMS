using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.Leave;

public class CreateLeaveDto : IValidatableObject
{
    [Required]
    public int EmpId { get; set; }

    [Required]
    [RegularExpression("^(Sick|Casual|Earned)$",
        ErrorMessage = "LeaveType must be Sick, Casual or Earned.")]
    public string LeaveType { get; set; }
        = string.Empty;

    [MaxLength(255)]
    public string? Reason { get; set; }

    [Required]
    public DateOnly FromDate { get; set; }

    [Required]
    public DateOnly ToDate { get; set; }

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (ToDate < FromDate)
        {
            yield return new ValidationResult(
                "ToDate cannot be earlier than FromDate.",
                new[] { nameof(ToDate) });
        }
    }
}
