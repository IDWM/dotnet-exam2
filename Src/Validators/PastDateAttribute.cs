using System.ComponentModel.DataAnnotations;

namespace dotnet_exam2.Src.Validators
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext
        )
        {
            if (value is DateOnly date)
            {
                if (date >= DateOnly.FromDateTime(DateTime.Now))
                {
                    return new ValidationResult("La fecha debe ser anterior a la actual.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
