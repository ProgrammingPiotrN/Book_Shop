using AutoMapper;
using BookShop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Application.BookShop.Queries.GetAllBookShops
{
    public class GetAllBookShopsHandler : IRequestHandler<GetAllBookShopsQuery, IEnumerable<BookShopDto>>
    {
        private readonly IBookShopRepository _bookShopRepository;
        private readonly IMapper _mapper;

        public GetAllBookShopsHandler(IBookShopRepository bookShopRepository, IMapper mapper) 
        {
            _bookShopRepository = bookShopRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BookShopDto>> Handle(GetAllBookShopsQuery request, CancellationToken cancellationToken)
        {
            var bookShops = await _bookShopRepository.GetAll();

            var dtoBook = _mapper.Map<IEnumerable<BookShopDto>>(bookShops);

            return dtoBook;
        }
    }
}
