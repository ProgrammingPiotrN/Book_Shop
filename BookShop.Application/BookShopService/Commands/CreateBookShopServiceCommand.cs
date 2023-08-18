using MediatR;

namespace BookShop.Application.BookShopService.Commands
{
    public class CreateBookShopServiceCommand : BookShopServiceDto, IRequest
    {
        public string BookShopEncodedName { get; set; } = default!;
    }
}
