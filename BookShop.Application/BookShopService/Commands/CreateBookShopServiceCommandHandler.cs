using BookShop.Application.ApplicationUser;
using BookShop.Domain.Interfaces;
using MediatR;

namespace BookShop.Application.BookShopService.Commands
{
    public class CreateBookShopServiceCommandHandler : IRequestHandler<CreateBookShopServiceCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IBookShopRepository _bookShopRepository;
        private readonly IBookShopServiceRepository _bookShopServiceRepository;

        public CreateBookShopServiceCommandHandler(IUserContext userContext, IBookShopRepository bookShopRepository,
            IBookShopServiceRepository bookShopServiceRepository)
        {
            _userContext = userContext;
            _bookShopRepository = bookShopRepository;
            _bookShopServiceRepository = bookShopServiceRepository;
        }

        public async Task<Unit> Handle(CreateBookShopServiceCommand request, CancellationToken cancellationToken)
        {
            var bookShop = await _bookShopRepository.GetByEncodedName(request.BookShopEncodedName!);

            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && (bookShop.CreatedById == user.Id || user.IsInRole("Moderator"));

            if (!isEditable)
            {
                return Unit.Value;
            }

            var bookShopService = new Domain.Entities.BookShopService()
            {
                Cost = request.Cost,
                Description = request.Description,
                BookShopId = bookShop.Id,
            };

            await _bookShopServiceRepository.Create(bookShopService);

            return Unit.Value;
        }
    }
}
