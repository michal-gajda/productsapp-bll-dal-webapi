namespace BLL.Validation;

public sealed record ValidationError
{
    public required string Field { get; init; }
    public required string Message { get; init; }
}
