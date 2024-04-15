
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Contexts;
using InternetBanking.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly ApplicationContext _dbContext;

        public TransactionRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetTransactionsToday()
        {
            DateTime today = DateTime.Today;

            return await _dbContext.Transactions
                            .Where(t => t.CreatedAt.Date == today)
                            .CountAsync();
        }

        public async Task<int> GetTransactionsTotal()
        {
            return await _dbContext.Transactions.CountAsync();
        }

        public async Task<int> GetPaymentsToday()
        {
            DateTime today = DateTime.Today;

            return await _dbContext.Transactions
                            .Where(t => t.CreatedAt.Date == today && t.Type == (int)TransactionType.Payment)
                            .CountAsync();
        }
        public async Task<int> GetPaymentsTotal()
        {
            return await _dbContext.Transactions
                            .Where(t => t.Type == (int)TransactionType.Payment)
                            .CountAsync();
        }
    }
}
