using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookShop.Application.BookShopService.Commands.Tests
{
    [TestClass()]
    public class CreateBookShopServiceCommandValidatorTests
    {
        [TestMethod()]
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

        [TestMethod()]
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