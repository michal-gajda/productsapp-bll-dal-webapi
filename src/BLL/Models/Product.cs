namespace BLL.Models;

using ProductEntity = DAL.Entities.Product;

public sealed record Product
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required decimal Price { get; init; }
    public required int StockQuantity { get; init; }
    public required string Category { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public required bool IsActive { get; init; }

    public string StockStatus => StockQuantity switch
    {
        0 => "Brak",
        <= 10 => "Niski stan",
        <= 30 => "Dostępny",
        _ => "Pełny magazyn",
    };

    public static explicit operator Product(ProductEntity entity)
    {
        return new Product
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            StockQuantity = entity.StockQuantity,
            Category = entity.Category,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            IsActive = entity.IsActive,
        };
    }
}