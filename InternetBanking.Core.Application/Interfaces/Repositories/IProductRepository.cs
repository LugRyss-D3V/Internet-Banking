using InternetBanking.Core.Application.Interfaces.Repositories.Core;
using InternetBanking.Core.Domain.Common;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Products>
    {
        Task<List<Products>> GetAllById(string id);
        Task<Products> GetByIdAsync(string id);
        Task<SavingAccount>? GetMainAccount(string userId);
        Task<int> GetProductsTotal();
        Task<List<T>> GetUserEntities<T>(string userId) where T : Products;
    }
}
