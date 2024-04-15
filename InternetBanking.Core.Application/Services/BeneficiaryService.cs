
using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Services.Core;
using InternetBanking.Core.Application.ViewModels.Beneficiary;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class BeneficiaryService : GenericService<BeneficiaryViewModel, BeneficiarySaveViewModel, Beneficiary>, IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _repository;
        private readonly IMapper _mapper;

        public BeneficiaryService(IBeneficiaryRepository repository, IMapper mapper) :base (repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
