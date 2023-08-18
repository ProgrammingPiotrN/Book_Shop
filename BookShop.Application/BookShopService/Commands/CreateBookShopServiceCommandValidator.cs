using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Application.BookShopService.Commands
{
    public class CreateBookShopServiceCommandValidator : AbstractValidator<CreateBookShopServiceCommand>
    {
        public CreateBookShopServiceCommandValidator()
        {
            RuleFor(p => p.Cost).NotEmpty().NotNull();
            RuleFor(p => p.Description).NotEmpty().NotNull();
            RuleFor(p => p.BookShopEncodedName).NotEmpty().NotNull();
        }
    }
}
