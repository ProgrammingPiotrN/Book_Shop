using BookShop.Application.ApplicationUser;
using BookShop.Domain.Interfaces;
using MediatR;

namespace BookShop.Application.BookShop.Commands.EditBookShop
{
    public class EditBookShopCommandHandler : IRequestHandler<EditBookShopCommand>
    {
        private readonly IBookShopRepository _bookShopRepository;
        private readonly IUserContext _userContext;

        public EditBookShopCommandHandler(IBookShopRepository bookShopRepository, IUserContext userContext) 
        {
            _bookShopRepository = bookShopRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(EditBookShopCommand request, CancellationToken cancellationToken)
        {
            var bookShop = await _bookShopRepository.GetByEncodedName(request.EncodedName!);

            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && (bookShop.CreatedById == user.Id || user.IsInRole("Moderator"));

            if (!isEditable)
            {
                return Unit.Value;
            }

            bookShop.Description = request.Description;
            bookShop.About = request.About;

            bookShop.ContactDetails.City = request.City;
            bookShop.ContactDetails.NumberPhone = request.NumberPhone;
            bookShop.ContactDetails.PostalCode = request.PostalCode;
            bookShop.ContactDetails.Street = request.Street;

            await _bookShopRepository.Books();

            return Unit.Value;
        }
    }
}
