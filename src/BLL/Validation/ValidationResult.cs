namespace BLL.Validation;

using FluentValidation.Results;

public sealed record ValidationResult
{
    public required bool IsValid { get; init; }
    public required IReadOnlyList<ValidationError> Errors { get; init; }

    public static ValidationResult Success()
    {
        return new ValidationResult
        {
            IsValid = true,
            Errors = Array.Empty<ValidationError>(),
        };
    }

    public static ValidationResult Failure(IEnumerable<ValidationError> errors)
    {
        return new ValidationResult
        {
            IsValid = false,
            Errors = errors.ToList(),
        };
    }

    public static ValidationResult FromFluentValidation(FluentValidation.Results.ValidationResult fluentResult)
    {
        if (fluentResult.IsValid)
        {
            return Success();
        }

        var errors = fluentResult.Errors.Select(e => new ValidationError
        {
            Field = e.PropertyName,
            Message = e.ErrorMessage,
        });

        return Failure(errors);
    }
}
