using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Application.BookShop.Queries.GetAllBookShops
{
    public class GetAllBookShopsQuery : IRequest<IEnumerable<BookShopDto>>
    {

    }
}
