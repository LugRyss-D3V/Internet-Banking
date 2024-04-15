using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories.Core;
using InternetBanking.Core.Application.Interfaces.Services.Core;

namespace InternetBanking.Core.Application.Services.Core
{
    public class GenericService<ViewModel, SaveViewModel, Entity>
        : IGenericService<ViewModel, SaveViewModel, Entity>
                where ViewModel : class
                where SaveViewModel : class
                where Entity : class
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task Add(SaveViewModel vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            await _repository.AddAsync(entity);
        }

        public virtual async Task Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<List<ViewModel>> Get()
        {
            var entities = await _repository.GetAllAsync();
            List<ViewModel> result = _mapper.Map<List<ViewModel>>(entities);

            return result;
        }

        public virtual async Task<List<ViewModel>> GetWithAll()
        {
            var entities = await _repository.GetAllWithIncludeAsync();
            List<ViewModel> result = _mapper.Map<List<ViewModel>>(entities);

            return result;
        }
    }
}
