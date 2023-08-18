using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Application.BookShop.Commands.EditBookShop
{
    public class EditBookShopCommandValidator : AbstractValidator<EditBookShopCommand>
    {
        public EditBookShopCommandValidator() 
        {
            RuleFor(a => a.Description)
                .NotEmpty()
                .WithMessage("Please complete the description");

            RuleFor(a => a.NumberPhone)
                .MinimumLength(8)
                .MaximumLength(12);
        }
    }
}
