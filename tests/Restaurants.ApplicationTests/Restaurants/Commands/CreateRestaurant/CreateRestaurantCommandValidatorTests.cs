﻿using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNoHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand
            {
                Name = "Test",
                Category = "Italian",
                ContactEmail = "test@test.com",
                PostalCode = "12-345"
            };

            var validator = new CreateRestaurantCommandValidator();

            //act 
           var result=  validator.TestValidate(command);

            //assert

            result.ShouldNotHaveAnyValidationErrors();
        }


        [Fact()]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand
            {
                Name = "T",
                Category = "Invalid",
                ContactEmail = "test",
                PostalCode = "12345"
              
            };

            var validator = new CreateRestaurantCommandValidator();

            //act 
            var result = validator.TestValidate(command);

            //assert

            result.ShouldHaveValidationErrorFor(c=>c.Name);
            result.ShouldHaveValidationErrorFor(c=>c.Category);
            result.ShouldHaveValidationErrorFor(c=>c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c=>c.PostalCode);
        }

        [Theory()]
        [InlineData("Italian")]
        [InlineData("Mexican")]
        [InlineData("Japanese")]
        [InlineData("American")]
        [InlineData("Indian")]

        public void Validator_ForValidCategory_ShouldNotHaveValidationError(string category)
        {
            //arrange
            var command = new CreateRestaurantCommand
            {
                Category = category
            };

            var validator = new CreateRestaurantCommandValidator();

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldNotHaveValidationErrorFor(c => c.Category);
        }

        [Theory()]
        [InlineData("10220")]
        [InlineData("102-20")]
        [InlineData("10 220")]
        [InlineData("10-2 20")]

        public void Validator_ForInvalidPostalCode_ShouldHaveValidationError(string postalCode)
        {
            //arrange
            var command = new CreateRestaurantCommand
            {
                PostalCode = postalCode
            };

            var validator = new CreateRestaurantCommandValidator();

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }

    }
}