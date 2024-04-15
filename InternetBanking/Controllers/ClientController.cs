using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.Products.Home;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    [ServiceFilter(typeof(AppAuthorize))]
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly ITransactionService _transactionService;

        public ClientController(IUserService userService, IProductService productService, ITransactionService transactionService)
        {
            _userService = userService;
            _productService = productService;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index(string id)
        {
            ProductsViewModel vm = new ProductsViewModel();
            vm.User = await _userService.GetUsersByIdAsync(id);

            vm.SavingAccounts = await _productService.GetAllByUserAndMap<SavingAccount, SavingAccountViewModel>(id);
            vm.CreditCards = await _productService.GetAllByUserAndMap<CreditCard, CreditCardViewModel>(id);
            vm.Loans = await _productService.GetAllByUserAndMap<Loan, LoanViewModel>(id);

            return View(vm);
        }
        public async Task<IActionResult> Create(ProductsViewModel home)
        {
            try
            {
                if (home.SavingAccount != null)
                {
                    home.SavingAccount.ProductType = "SavingAccount";
                    home.SavingAccount.SavingAccountType = (int)SavingAccountType.Secondary;

                    await _transactionService.CreateProduct(home.SavingAccount);

                    return RedirectToAction("Index", "Products", new { id = home.SavingAccount.UserId });

                }

                if (home.CreditCard != null)
                {
                    home.CreditCard.ProductType = "CreditCard";

                    await _transactionService.CreateProduct(home.CreditCard);

                    return RedirectToAction("Index", "Products", new { id = home.CreditCard.UserId });
                }
                if (home.Loan != null)
                {
                    home.Loan.ProductType = "Loan";

                    await _transactionService.CreateProduct(home.Loan);

                    return RedirectToAction("Index", "Products", new { id = home.Loan.UserId });
                }
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index", "Products");

        }

    }
}
