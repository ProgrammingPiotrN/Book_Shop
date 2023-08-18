using AutoMapper;
using BookShop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Application.BookShop.Queries.GetBookShopByEncodedName
{
    public class GetBookShopByEncodedNameQueryHandler : IRequestHandler<GetBookShopByEncodedNameQuery, BookShopDto>
    {
        private readonly IBookShopRepository _bookShopRepository;
        private readonly IMapper _mapper;

        public GetBookShopByEncodedNameQueryHandler(IBookShopRepository bookShopRepository, IMapper mapper) 
        {
            _bookShopRepository = bookShopRepository;
            _mapper = mapper;
        }
        public async Task<BookShopDto> Handle(GetBookShopByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var bookShop = await _bookShopRepository.GetByEncodedName(request.EncodedName);

            var dtoBookShop = _mapper.Map<BookShopDto>(bookShop);

            return dtoBookShop;
        }
    }
}
