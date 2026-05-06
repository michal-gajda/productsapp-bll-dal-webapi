namespace BLL.Models;

public sealed record UpdateProduct
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required decimal Price { get; init; }
    public required int StockQuantity { get; init; }
    public required string Category { get; init; }
    public required bool IsActive { get; init; }
}