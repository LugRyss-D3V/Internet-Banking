
using InternetBanking.Core.Application.Interfaces.Repositories.Core;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<int> GetPaymentsToday();
        Task<int> GetPaymentsTotal();
        Task<int> GetTransactionsToday();
        Task<int> GetTransactionsTotal();
    }
}
