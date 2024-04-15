namespace InternetBanking.Core.Application.Interfaces.Repositories.Core
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task AddAsync(Entity entity);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAllAsync();
        Task<List<Entity>> GetAllWithIncludeAsync();
        Task<Entity> GetByIdAsync(int id);
        Task UpdateAsync(Entity entity, string id);
    }
}
