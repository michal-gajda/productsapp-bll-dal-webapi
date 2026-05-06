namespace WebAPI.Models;

using BLL.Models;

public sealed record CreateProductRequest
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required decimal Price { get; init; }
    public required int StockQuantity { get; init; }
    public required string Category { get; init; }

    public static explicit operator CreateProduct(CreateProductRequest request)
    {
        return new CreateProduct
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            Category = request.Category,
        };
    }
}
