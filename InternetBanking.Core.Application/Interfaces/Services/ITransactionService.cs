
using InternetBanking.Core.Application.Interfaces.Services.Core;
using InternetBanking.Core.Application.ViewModels.Home;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.Transaction;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ITransactionService : IGenericService<TransactionViewModel, TransactionSaveViewModel, Transaction>
    {
        Task CreateProduct(ProductSaveViewModel vm);
        Task DepositOnMainAccount(string userId, double monto);
        Task<HomeViewModel> GetDashboard();
        Task<PaymentViewModel> GetPaymentViewModel(string id);
    }
}
