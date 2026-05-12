namespace BLL.FunctionalTests.Builders;

using BLL.Models;

public sealed class CreateProductBuilder
{
    private string name = "Test Product";
    private string? description = default;
    private decimal price;
    private int stockQuantity;
    private string category = "Default Category";

    private CreateProductBuilder() { }

    public static CreateProductBuilder CreateProductDefault()
    {
        return new CreateProductBuilder();
    }

    public CreateProductBuilder WithCategory(string category)
    {
        this.category = category;
        return this;
    }

    public CreateProductBuilder WithDescription(string? description)
    {
        this.description = description;
        return this;
    }

    public CreateProductBuilder WithName(string name)
    {
        this.name = name;
        return this;
    }

    public CreateProductBuilder WithPrice(decimal price)
    {
        this.price = price;
        return this;
    }

    public CreateProductBuilder WithStockQuantity(int stockQuantity)
    {
        this.stockQuantity = stockQuantity;
        return this;
    }

    public CreateProduct Build()
    {
        return new CreateProduct
        {
            Name = this.name,
            Description = this.description,
            Price = this.price,
            StockQuantity = this.stockQuantity,
            Category = this.category,
        };
    }
}
