using FluentValidation;
using FluentValidation.Results;

internal static class _GenericValidator<TValidator, TData>
    where TValidator : AbstractValidator<TData>, new()
{
    public static Dictionary<string, string[]>? TryValidation(TData data)
    {
        TValidator validator = new();
        ValidationResult results = validator.Validate(data);

        if (!results.IsValid)
        {
            return results.Errors.ToDictionary(
                (value) => value.PropertyName,
                (value) => new string[] { value.ErrorMessage }
            );
        }

        return null;
    }
}
