namespace BLL.Validation;

public sealed class ValidationConfig
{
    public ProductValidationRules Product { get; set; } = new();
}

public sealed class ProductValidationRules
{
    public int NameMinLength { get; set; } = 2;
    public int NameMaxLength { get; set; } = 200;
    public int DescriptionMaxLength { get; set; } = 1000;
    public decimal PriceMin { get; set; } = 0.01m;
    public decimal PriceMax { get; set; } = 999999.99m;
    public int CategoryMinLength { get; set; } = 2;
    public int CategoryMaxLength { get; set; } = 100;
}
