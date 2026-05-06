namespace BLL;

using BLL.Models;
using BLL.Services;
using BLL.Validation;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
    {
        var validationConfig = new ValidationConfig();
        configuration.GetSection("Validation").Bind(validationConfig);
        services.AddSingleton(validationConfig);

        services.AddScoped<IValidator<CreateProduct>>(sp =>
        {
            var config = sp.GetRequiredService<ValidationConfig>();
            return new CreateProductValidator(config);
        });

        services.AddScoped<IValidator<UpdateProduct>>(sp =>
        {
            var config = sp.GetRequiredService<ValidationConfig>();
            return new UpdateProductValidator(config);
        });

        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
