using BookShop.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Application.BookShop.Commands.CreateBookShop
{
    public class CreateBookShopCommandValidator : AbstractValidator<CreateBookShopCommand>
    {
        public CreateBookShopCommandValidator(IBookShopRepository repository)
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MinimumLength(5).WithMessage("Name should have atleast 5 characters")
                .MaximumLength(20).WithMessage("Name should have maximum 20 characters")
                .Custom((value, context) =>
                {
                    var existingBookShop = repository.GetByName(value).Result;
                    if (existingBookShop != null)
                    {
                        context.AddFailure($"{value} is not unique name for book bookshop");
                    }
                });

            RuleFor(a => a.Description)
                .NotEmpty()
                .WithMessage("Please complete the description");

            RuleFor(a => a.NumberPhone)
                .MinimumLength(8)
                .MaximumLength(12);
        }
    }
}
