using Microsoft.AspNetCore.Mvc;
using ServiceRepWithFactoryWithFactory.Models;
using ServiceRepWithFactoryWithFactory.Repositories;
using ServiceRepWithFactoryWithFactory.Services;
using ServiceRepWithFactoryWithFactory.Services.ServiceFactory;
using System.Diagnostics;

namespace ServiceRepWithFactoryWithFactory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IBooksRepository _booksRePository;
        private readonly IConfiguration _configuration;

        //public HomeController(ILogger<HomeController> logger, 
        //    IBooksRepository booksRepository,
        //    IConfiguration configuration)
        public HomeController(ILogger<HomeController> logger,
        IConfiguration configuration)
        {
            _logger = logger;
            //this._booksRePository = booksRepository;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var mode = _configuration["Source"];

            var booksServiceFactory = new BooksServiceFactory();
            var booksService = booksServiceFactory.CreateBooksService(mode);

            var books = booksService.GetBooks();

            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}