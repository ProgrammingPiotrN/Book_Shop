using Xunit;
using BookShop.Application.BookShopService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace BookShop.Application.BookShopService.Commands.Tests
{
    
    public class CreateBookShopServiceCommandValidatorTests
    {
        [Fact()]
        public void CreateBookShopServiceCommandValidator_ValidationError_WithValidateCommandTest()
        {
            var validator = new CreateBookShopServiceCommandValidator();
            var command = new CreateBookShopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Description",
                BookShopEncodedName = "workshop1"
            };
            var result = validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void CreateBookShopServiceCommandValidator_ValidationErrors_WithValidateCommandTest()
        {
            var validator = new CreateBookShopServiceCommandValidator();
            var command = new CreateBookShopServiceCommand()
            {
                Cost = "",
                Description = "",
                BookShopEncodedName = null
            };
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Cost);
            result.ShouldHaveValidationErrorFor(c => c.Description);
            result.ShouldHaveValidationErrorFor(c => c.BookShopEncodedName);
        }
    }
}