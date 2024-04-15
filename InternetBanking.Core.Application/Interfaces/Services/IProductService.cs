using InternetBanking.Core.Application.Dtos.Product.Request;
using InternetBanking.Core.Application.Dtos.Product.Response;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Domain.Common;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task Add(ProductSaveViewModel vm);
        Task<DebitResponse> Debit(DebitRequest request);
        Task Delete(string id);
        Task<DepositResponse> Deposit(DepositRequest request);
        Task<DepositResponse> DepositOnMainAccount(string userId, double Amount);
        Task<DepositResponse> DepositOnMainAccount(SavingAccount entity, double Amount);
        Task<List<ProductViewModel>> GetAll();
        Task<List<ProductViewModel>> GetAllById(string id);
        Task<List<TViewModel>> GetAllByUserAndMap<T, TViewModel>(string userId)
            where T : Products
            where TViewModel : ProductSaveViewModel;
        Task<ProductViewModel> GetById(string id);
        Task<SavingAccount> GetMainAccount(string userId);
        Task<int> GetProductsCount();
    }
}
