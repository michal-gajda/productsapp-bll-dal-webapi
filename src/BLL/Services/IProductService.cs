namespace BLL.Services;

using BLL.Models;
using BLL.Validation;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
    Task<IEnumerable<Product>> GetActiveProductsAsync();
    Task<IEnumerable<string>> GetCategoriesAsync();
    Task<(ValidationResult Validation, Product? Product)> CreateProductAsync(CreateProduct model);
    Task<(ValidationResult Validation, Product? Product)> UpdateProductAsync(int id, UpdateProduct model);
    Task<bool> DeleteProductAsync(int id);
    Task<Product?> ToggleActiveAsync(int id);
}
