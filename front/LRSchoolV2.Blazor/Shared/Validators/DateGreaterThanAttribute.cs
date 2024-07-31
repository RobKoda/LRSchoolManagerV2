using System.ComponentModel.DataAnnotations;

namespace LRSchoolV2.Blazor.Shared.Validators;

[AttributeUsage(AttributeTargets.Property)]
public class DateGreaterThanAttribute : ValidationAttribute
{
    private string TargetPropertyName { get; }
    
    public DateGreaterThanAttribute(string inTargetPropertyName)
        => TargetPropertyName = inTargetPropertyName;

    protected override ValidationResult? IsValid(object? inValue, ValidationContext inValidationContext)
    {
        var targetValue = inValidationContext.ObjectInstance
            .GetType()
            .GetProperty(TargetPropertyName)
            ?.GetValue(inValidationContext.ObjectInstance, null);

        if (!((DateTime?)inValue < (DateTime?)targetValue)) return ValidationResult.Success;
        
        var propertyName = inValidationContext.MemberName ?? string.Empty;
        return new ValidationResult(ErrorMessage, new[] { propertyName });

    }
}