using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Az203kz.ServicebusPublish.Models;
using Az203kz.ServicebusPublish.Services;
using Az203kz.ServicebusPublish.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Az203kz.ServicebusPublish.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPublisherService _publisherService;

        public HomeController(ILogger<HomeController> logger,IPublisherService publisherService)
        {
            this._publisherService = publisherService;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RestaurantOrderViewModel viewModel)
        {
            await _publisherService.Send(viewModel);

            return RedirectToAction("Success", "Home");
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet]
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
