using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Beneficiary;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    public class BeneficiariesController : Controller
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public BeneficiariesController(IBeneficiaryService beneficiaryService, IProductService productService, IUserService userService)
        {
            _beneficiaryService = beneficiaryService;
            _productService = productService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _beneficiaryService .Delete(id);
            return RedirectToAction("Index");
        }

    }
}
