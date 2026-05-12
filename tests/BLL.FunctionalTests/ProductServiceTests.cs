namespace BLL.FunctionalTests;

using BLL.FunctionalTests.Builders;
using BLL.Services;

[TestClass]
public sealed class ProductServiceTests : TestBase
{
    [TestMethod]
    public async Task CreateProduct_WithValidData_ReturnsCreatedProduct()
    {
        var productService = this.ServiceProvider.GetRequiredService<IProductService>();

        var model = CreateProductBuilder.CreateProductDefault()
            .WithCategory("Test Category")
            .WithDescription("This is a test product.")
            .WithName("Test Product")
            .WithPrice(9.99m)
            .WithStockQuantity(100)
            .Build();

        var (_, product) = await productService.CreateProductAsync(model);

        product.ShouldNotBeNull();

        product.Category
            .ShouldBe(model.Category)
            ;

        product.Description
            .ShouldBe(model.Description)
            ;

        product.Name
            .ShouldBe(model.Name)
            ;

        product.Price
            .ShouldBe(model.Price)
            ;

        product.StockQuantity
            .ShouldBe(model.StockQuantity)
            ;
    }
}
