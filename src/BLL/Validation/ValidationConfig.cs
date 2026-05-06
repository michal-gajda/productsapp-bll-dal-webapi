namespace BLL.Validation;

public sealed class ValidationConfig
{
    public ProductValidationRules Product { get; set; } = new();
}
