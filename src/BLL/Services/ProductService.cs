namespace BLL.Services;

using BLL.Models;
using BLL.Validation;
using DAL.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;
using ProductEntity = DAL.Entities.Product;

internal sealed class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IValidator<CreateProduct> _createValidator;
    private readonly IValidator<UpdateProduct> _updateValidator;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository, IValidator<CreateProduct> createValidator, IValidator<UpdateProduct> updateValidator, ILogger<ProductService> logger)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        _logger.LogInformation("Pobieranie wszystkich produktów");
        var products = await _repository.GetAllAsync();
        var models = products.Select(p => (Product)p);

        return models;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        _logger.LogInformation("Pobieranie produktu o ID: {Id}", id);
        var product = await _repository.GetByIdAsync(id);
        var model = product != null ? (Product)product : null;

        return model;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
    {
        _logger.LogInformation("Pobieranie produktów z kategorii: {Category}", category);
        var products = await _repository.GetByCategoryAsync(category);
        var models = products.Select(p => (Product)p);

        return models;
    }

    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        _logger.LogInformation("Pobieranie aktywnych produktów");
        var products = await _repository.GetActiveProductsAsync();
        var models = products.Select(p => (Product)p);

        return models;
    }

    public async Task<IEnumerable<string>> GetCategoriesAsync()
    {
        return await _repository.GetCategoriesAsync();
    }

    public async Task<(ValidationResult Validation, Product? Product)> CreateProductAsync(CreateProduct model)
    {
        _logger.LogInformation("Tworzenie nowego produktu: {Name}", model.Name);

        var fluentResult = await _createValidator.ValidateAsync(model);
        var validation = ValidationResult.FromFluentValidation(fluentResult);

        if (!validation.IsValid)
        {
            _logger.LogWarning("Walidacja produktu nie powiodła się: {Errors}", string.Join(", ", validation.Errors.Select(e => e.Message)));
            return (validation, null);
        }

        var entity = (ProductEntity)model;
        var created = await _repository.AddAsync(entity);

        _logger.LogInformation("Produkt utworzony z ID: {Id}", created.Id);
        var result = (Product)created;

        return (validation, result);
    }

    public async Task<(ValidationResult Validation, Product? Product)> UpdateProductAsync(int id, UpdateProduct model)
    {
        _logger.LogInformation("Aktualizacja produktu o ID: {Id}", id);

        var fluentResult = await _updateValidator.ValidateAsync(model);
        var validation = ValidationResult.FromFluentValidation(fluentResult);

        if (!validation.IsValid)
        {
            _logger.LogWarning("Walidacja produktu nie powiodła się: {Errors}", string.Join(", ", validation.Errors.Select(e => e.Message)));
            return (validation, null);
        }

        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
        {
            _logger.LogWarning("Produkt o ID {Id} nie został znaleziony", id);

            var notFoundResult = ValidationResult.Failure(new[]
            {
                new ValidationError
                {
                    Field = "Id",
                    Message = $"Produkt o ID {id} nie został znaleziony",
                },
            });

            return (notFoundResult, null);
        }

        entity.Name = model.Name;
        entity.Description = model.Description;
        entity.Price = model.Price;
        entity.StockQuantity = model.StockQuantity;
        entity.Category = model.Category;
        entity.IsActive = model.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(entity);

        _logger.LogInformation("Produkt o ID {Id} zaktualizowany", id);
        var result = (Product)entity;

        return (validation, result);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        _logger.LogInformation("Usuwanie produktu o ID: {Id}", id);

        if (!await _repository.ExistsAsync(id))
        {
            _logger.LogWarning("Produkt o ID {Id} nie istnieje", id);
            return false;
        }

        await _repository.DeleteAsync(id);

        _logger.LogInformation("Produkt o ID {Id} usunięty", id);

        return true;
    }

    public async Task<Product?> ToggleActiveAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
        {
            return null;
        }

        entity.IsActive = !entity.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(entity);

        _logger.LogInformation("Produkt {Id} - zmieniono status aktywności na {IsActive}", id, entity.IsActive);
        var result = (Product)entity;

        return result;
    }
}
