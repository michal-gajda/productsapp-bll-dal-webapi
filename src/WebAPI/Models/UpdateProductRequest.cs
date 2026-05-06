namespace WebAPI.Models;

using BLL.Models;

public sealed record UpdateProductRequest
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required decimal Price { get; init; }
    public required int StockQuantity { get; init; }
    public required string Category { get; init; }
    public required bool IsActive { get; init; }

    public static explicit operator UpdateProduct(UpdateProductRequest request)
    {
        return new UpdateProduct
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            Category = request.Category,
            IsActive = request.IsActive,
        };
    }
}
