using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Home;
using InternetBanking.Middlewares;
using InternetBanking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InternetBanking.Controllers
{
    [ServiceFilter(typeof(AppAuthorize))]
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;
        
        public HomeController(ILogger<HomeController> logger, IUserService userService, ITransactionService transactionService)
        {
            _logger = logger;
            _userService = userService;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel vm = new HomeViewModel();

            vm = await _transactionService.GetDashboard();

            vm.ActiveClients = await _userService.ActiveUsers();
            vm.DeactiveClients = await _userService.DeactiveUsers();

            return View(vm);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}