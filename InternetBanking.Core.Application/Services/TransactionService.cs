
using AutoMapper;
using InternetBanking.Core.Application.Dtos.Product.Response;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Services.Core;
using InternetBanking.Core.Application.ViewModels.Home;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.Transaction;
using InternetBanking.Core.Domain.Common;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class TransactionService : GenericService<TransactionViewModel, TransactionSaveViewModel, Transaction>, ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository repository, IProductService productService, IMapper mapper): base(repository, mapper) 
        {
            _repository = repository;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<HomeViewModel> GetDashboard()
        {
            HomeViewModel home = new HomeViewModel();

            home.TransactionsTotal = await _repository.GetTransactionsTotal();
            home.TransactionsToday = await _repository.GetTransactionsToday();
            home.PaymentsToday = await _repository.GetPaymentsToday();
            home.PaymentsTotal = await _repository.GetPaymentsTotal();
            home.AssignedProducts = await _productService.GetProductsCount();

            return home;
        }

        public async Task<PaymentViewModel> GetPaymentViewModel(string id)
        {
            PaymentViewModel vm = new PaymentViewModel();

            vm.SavingAccounts = await _productService.GetAllByUserAndMap<SavingAccount, SavingAccountViewModel>(id);

            return vm;
        }

        public async Task CreateProduct(ProductSaveViewModel vm)
        {
            string productId = ProductIdHelper.GenerateId();

            if (vm is SavingAccountViewModel savingAccountVM)
            {
                savingAccountVM.ProductId = productId;

                await _productService.Add(savingAccountVM);

                await this.Add(new TransactionSaveViewModel()
                {
                    FromId = null,
                    ToId = productId,
                    Type = (int)TransactionType.Transaction,
                    Cuantity = (double)savingAccountVM.Cuantity,
                    FromType = null,
                    ToType = (int)ProductType.SavingAccount
                });
            }
            else if (vm is LoanViewModel loanVM)
            {
                loanVM.ProductId = productId;

                await _productService.Add(loanVM);
                await _productService.DepositOnMainAccount(loanVM.UserId, loanVM.Cuantity);

                await this.Add(new TransactionSaveViewModel()
                {
                    FromId = productId,
                    ToId = null,
                    Type = (int)TransactionType.Transaction,
                    Cuantity = (double)loanVM.Cuantity,
                    FromType = (int)ProductType.Loan,
                    ToType = (int)ProductType.SavingAccount
                });
            }
            else if (vm is CreditCardViewModel creditCardVM)
            {
                creditCardVM.ProductId = productId;

                await _productService.Add(creditCardVM);
            }
        }

        public async Task DepositOnMainAccount(string userId, double monto)
        {
            SavingAccount account = await _productService.GetMainAccount(userId);

            DepositResponse response = await _productService.DepositOnMainAccount(account, monto);

            await this.Add(new TransactionSaveViewModel()
            {
                FromId = null,
                ToId = response.ProductId,
                Type = (int)TransactionType.Transaction,
                Cuantity = (double)monto,
                FromType = null,
                ToType = (int)ProductType.SavingAccount
            });
        }
    }
}
