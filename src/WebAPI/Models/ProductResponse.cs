namespace WebAPI.Models;

using BLL.Models;

public sealed record ProductResponse
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
    public required string StockStatus { get; init; }

    public static explicit operator ProductResponse(Product model)
    {
        return new ProductResponse
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            StockQuantity = model.StockQuantity,
            Category = model.Category,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            IsActive = model.IsActive,
            StockStatus = model.StockStatus,
        };
    }
}
