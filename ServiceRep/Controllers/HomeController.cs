﻿using Microsoft.AspNetCore.Mvc;
using ServiceRepWithFactory.Models;
using ServiceRepWithFactory.Repositories;
using ServiceRepWithFactory.Services;
using System.Diagnostics;

namespace ServiceRepWithFactory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBooksRepository _booksRePository;

        public HomeController(ILogger<HomeController> logger, IBooksRepository booksRepository)
        {
            _logger = logger;
            _booksRePository = booksRepository;
        }

        public IActionResult Index()
        {
            var booksService = new BooksService(_booksRePository);
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