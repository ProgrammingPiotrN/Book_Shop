using MediatR;

namespace BookShop.Application.BookShopService.Queries.GetBookShopServices
{
    public class GetBookShopServicesQuery : IRequest<IEnumerable<BookShopServiceDto>>
    {
        public string EncodedName { get; set; } = default!;
    }
}
