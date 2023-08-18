using BookShop.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookShop.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult NoAccess()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        var model = new List<Person>
        {
            new Person
            {
                FirstName = "Piotr",
                LastName = "Nawrocki"
            },
             new Person
            {
                FirstName = "Adam",
                LastName = "Nowak"
            },
        };
        return View(model);
    }

    public IActionResult About()
    {
        var about = new About()
        {
            Title = "Book shop application",
            Description = "Book shop application",
            Tags = new List<string>() { "Book", "Books", "Liblary", "Shop" }
        };
        return View(about);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
