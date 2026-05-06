namespace BLL.FunctionalTests;

using BLL.Models;
using BLL.Services;

[TestClass]
public sealed class ProductServiceTests : TestBase
{
    [TestMethod]
    public async Task CreateProduct_WithValidData_ReturnsCreatedProduct()
    {
        var productService = this.ServiceProvider.GetRequiredService<IProductService>();

        var model = new CreateProduct
        {
            Name = "Test Product",
            Description = "This is a test product.",
            Price = 9.99m,
            Category = "Test Category",
            StockQuantity = 100,
        };

        var (_, product) = await productService.CreateProductAsync(model);

        product.ShouldNotBeNull();

        product.Name
            .ShouldBe(model.Name)
            ;

        product.Description
            .ShouldBe(model.Description)
            ;

        product.Price
            .ShouldBe(model.Price)
            ;

        product.Category
            .ShouldBe(model.Category)
            ;

        product.StockQuantity
            .ShouldBe(model.StockQuantity)
            ;
    }
}
