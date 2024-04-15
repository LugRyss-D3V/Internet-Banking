namespace InternetBanking.Core.Application.Interfaces.Services.Core
{
    public interface IGenericService<ViewModel, SaveViewModel, Entity>
                                     where ViewModel : class
                                     where SaveViewModel : class
                                     where Entity : class
    {
        Task Add(SaveViewModel vm);
        Task Delete(int id);
        Task<List<ViewModel>> Get();
        Task<List<ViewModel>> GetWithAll();
    }
}
