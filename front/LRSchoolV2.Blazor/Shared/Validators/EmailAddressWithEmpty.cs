using System.ComponentModel.DataAnnotations;

namespace LRSchoolV2.Blazor.Shared.Validators;

public class EmailAddressWithEmptyAttribute : ValidationAttribute
{
    private readonly EmailAddressAttribute _validator = new();

    public override bool IsValid(object? inValue) => 
        string.IsNullOrEmpty(inValue?.ToString()) || _validator.IsValid(inValue.ToString());
}