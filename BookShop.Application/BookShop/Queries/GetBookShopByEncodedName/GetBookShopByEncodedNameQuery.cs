using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Application.BookShop.Queries.GetBookShopByEncodedName
{
    public class GetBookShopByEncodedNameQuery : IRequest<BookShopDto>
    {
        public string EncodedName { get; set; }

        public GetBookShopByEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
