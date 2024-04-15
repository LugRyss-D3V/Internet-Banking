using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.Transaction;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InternetBanking.Core.Application.Helpers;

namespace InternetBanking.Controllers
{
    public class PaymentController : Controller 
    {
        private readonly ITransactionService _transactionService;
        private readonly AuthenticationResponse? _user;

        public PaymentController(ITransactionService transactionService, IHttpContextAccessor httpContextAccessor)
        {
            _transactionService = transactionService;
            _user = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task< IActionResult> Express()
        { 
            PaymentViewModel vm = new PaymentViewModel();
            vm = await _transactionService.GetPaymentViewModel(_user.Id);
             return View(vm);
        }

        public IActionResult CreditCard()
        {
            return View();
        }

        public IActionResult Loan()
        {
            return View();
        }

        public IActionResult Beneficiaries()
        {
            return View();
        }
    }
}
