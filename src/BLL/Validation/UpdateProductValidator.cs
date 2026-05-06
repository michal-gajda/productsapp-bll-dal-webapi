namespace BLL.Validation;

using BLL.Models;
using FluentValidation;

internal sealed class UpdateProductValidator : AbstractValidator<UpdateProduct>
{
    public UpdateProductValidator(ValidationConfig config)
    {
        var rules = config.Product;

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nazwa produktu jest wymagana")
            .Length(rules.NameMinLength, rules.NameMaxLength)
            .WithMessage($"Nazwa musi mieć od {rules.NameMinLength} do {rules.NameMaxLength} znaków");

        RuleFor(x => x.Description)
            .MaximumLength(rules.DescriptionMaxLength)
            .WithMessage($"Opis nie może przekraczać {rules.DescriptionMaxLength} znaków")
            .When(x => x.Description != null);

        RuleFor(x => x.Price)
            .InclusiveBetween(rules.PriceMin, rules.PriceMax)
            .WithMessage($"Cena musi być między {rules.PriceMin} a {rules.PriceMax}");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Ilość nie może być ujemna");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Kategoria jest wymagana")
            .Length(rules.CategoryMinLength, rules.CategoryMaxLength)
            .WithMessage($"Kategoria musi mieć od {rules.CategoryMinLength} do {rules.CategoryMaxLength} znaków");
    }
}
