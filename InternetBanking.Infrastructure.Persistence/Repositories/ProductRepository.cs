using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Common;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Contexts;
using InternetBanking.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Products>, IProductRepository
    {
        private readonly ApplicationContext _dbContext;

        public ProductRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task UpdateAsync(Products entity, string id)
        {
            var entry = await _dbContext.Products.FindAsync(id);

            _dbContext.Entry(entry).CurrentValues.SetValues(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetProductsTotal()
        {
            return await _dbContext.Products.CountAsync();
        }

        public async Task<List<Products>> GetAllById(string id)
        {
            return await _dbContext.Set<Products>()
                                        .Where(p => p.ProductId == id)
                                        .ToListAsync();
        }

        public async Task<List<T>> GetUserEntities<T>(string userId) where T : Products
        {
            var entities = await _dbContext.Set<Products>()
                .Where(p => p.UserId == userId)
                .OfType<T>()
                .ToListAsync();

            return entities;
        }

        public async Task<Products> GetByIdAsync(string id)
        {
            return await _dbContext.Set<Products>().FindAsync(id);
        }

        public async Task<SavingAccount>? GetMainAccount(string userId)
        {
            var SavingAccounts = await _dbContext.Set<Products>()
                .OfType<SavingAccount>()
                .Where(p => p.UserId == userId && p.SavingAccountType == (int)SavingAccountType.Primary)
                .FirstAsync();

            return SavingAccounts;
        }
    }
}
