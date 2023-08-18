using AutoMapper;
using BookShop.Application.BookShop.Commands.CreateBookShop;
using BookShop.Application.BookShop.Commands.EditBookShop;
using BookShop.Application.BookShop.Queries.GetAllBookShops;
using BookShop.Application.BookShop.Queries.GetBookShopByEncodedName;
using BookShop.Application.BookShopService.Commands;
using BookShop.Application.BookShopService.Queries.GetBookShopServices;
using BookShop.MVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BookShop.MVC.Controllers
{
    public class BookShopController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookShopController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var bookShop = await _mediator.Send(new GetAllBookShopsQuery());
            return View(bookShop);
        }


        //czy użytkownik jest zalogowany i sprawdzenie roli użytkownika
        //Sposób 2
        [Authorize(Roles = "Owner")]
        public IActionResult Create()
        {
            //Sposób 1 sprawdzenia roli użytkownika
            //if(!User.IsInRole("Owner"))
            //{
            //   return RedirectToAction("NoAccess", "Home");
            //}
            //Sposób 1
            //if (User.Identity == null || !User.Identity.IsAuthenticated)
            //{
            //metoda zwracająca stronę, metoda toAction zwraca akcje
            //return RedirectToPage("/Account/Login", new { area = "Identity" });
            //}
            //Koniec sposobu 1
            return View();
        }

        [Route("BookShop/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var bookShopDto = await _mediator.Send(new GetBookShopByEncodedNameQuery(encodedName));
            return View(bookShopDto);
        }

        [Route("BookShop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var bookShopDto = await _mediator.Send(new GetBookShopByEncodedNameQuery(encodedName));

            if (!bookShopDto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            EditBookShopCommand command = _mapper.Map<EditBookShopCommand>(bookShopDto);

            return View(command);
        }

        [HttpPost]
        [Route("BookShop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName, EditBookShopCommand editBookShopCommand)
        {
            if (!ModelState.IsValid)
            {
                return View(editBookShopCommand);
            }
            await _mediator.Send(editBookShopCommand);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Create(CreateBookShopCommand createBookShopCommand)
        {
            if (!ModelState.IsValid)
            {
                return View(createBookShopCommand);
            }
            await _mediator.Send(createBookShopCommand);

            this.SetNotification("success", $"Created bookshop: {createBookShopCommand.Name}");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        [Route("BookShop/BookShopService")]
        public async Task<IActionResult> CreateBookShopService(CreateBookShopServiceCommand createBookShopServiceCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(createBookShopServiceCommand);

            return Ok();
        }

        [HttpGet]
        [Route("BookShop/{encodedName}/BookShopService")]
        public async Task<IActionResult> GetBookShopServices(string encodedName)
        {
            var data = await _mediator.Send(new GetBookShopServicesQuery() { EncodedName = encodedName});
            return Ok(data);
        }
    }
}
