namespace DAL.Repositories;

using DAL.Entities;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryAsync(string category);
    Task<IEnumerable<Product>> GetActiveProductsAsync();
    Task<IEnumerable<string>> GetCategoriesAsync();
}
