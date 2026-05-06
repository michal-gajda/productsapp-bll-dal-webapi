namespace BLL.Models;

using ProductEntity = DAL.Entities.Product;

public sealed record CreateProduct
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required decimal Price { get; init; }
    public required int StockQuantity { get; init; }
    public required string Category { get; init; }

    public static explicit operator ProductEntity(CreateProduct model)
    {
        return new ProductEntity
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            StockQuantity = model.StockQuantity,
            Category = model.Category,
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
        };
    }
}