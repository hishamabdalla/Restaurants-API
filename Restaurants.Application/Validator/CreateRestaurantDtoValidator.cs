
using FluentValidation;
using Restaurants.Application.DTOs.RestaurantDtos;

namespace Restaurants.Application.Validator;

public class CreateRestaurantDtoValidator:AbstractValidator<CreateRestaurantDto>
{
    private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];

    public CreateRestaurantDtoValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 100);

        RuleFor(x => x.Category)
            .Must(category => validCategories.Contains(category))
            .WithMessage("Invalid category. Please choose from the valid categories");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");
        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid postal code (XX-XXX).");
    }
}
