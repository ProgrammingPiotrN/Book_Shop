using AutoMapper;
using BookShop.Application.ApplicationUser;
using BookShop.Domain.Interfaces;
using MediatR;

namespace BookShop.Application.BookShop.Commands.CreateBookShop
{
    public class CreateBookShopCommandHandler : IRequestHandler<CreateBookShopCommand>
    {
        private readonly IBookShopRepository _bookShopRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateBookShopCommandHandler(IBookShopRepository bookShopRepository, IMapper mapper, IUserContext userContext) 
        {
            _bookShopRepository = bookShopRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(CreateBookShopCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if(currentUser == null || !currentUser.IsInRole("Owner"))
            {
                return Unit.Value;
            }

            var bookShop = _mapper.Map<Domain.Entities.BookShop>(request);
            bookShop.CodeUrl();

            bookShop.CreatedById = currentUser.Id;

            await _bookShopRepository.Create(bookShop);

            return Unit.Value;
        }
    }
}
