using AutoMapper;
using BookShop.Domain.Interfaces;
using MediatR;

namespace BookShop.Application.BookShopService.Queries.GetBookShopServices
{
    public class GetBookShopServicesQueryHandler : IRequestHandler<GetBookShopServicesQuery, IEnumerable<BookShopServiceDto>>
    {

        private readonly IBookShopServiceRepository _bookShopServiceRepository;
        private readonly IMapper _mapper;

        public GetBookShopServicesQueryHandler(IBookShopServiceRepository bookShopServiceRepository, IMapper mapper)
        {
            _bookShopServiceRepository = bookShopServiceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookShopServiceDto>> Handle(GetBookShopServicesQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookShopServiceRepository.GetAllByEncodedName(request.EncodedName);

            var dtos = _mapper.Map<IEnumerable<BookShopServiceDto>>(result);

            return dtos;
        }
    }
}
