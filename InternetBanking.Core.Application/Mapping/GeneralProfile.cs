
using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account.Request;
using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.Transaction;
using InternetBanking.Core.Application.ViewModels.User;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            #region Users "Identity"
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, RegisterViewModel>()
                .ForMember(x => x.TypeUser, opt => opt.MapFrom(src => (Roles)src.TypeUser))
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AuthenticationResponse, UserViewModel>()
                .ReverseMap();

            CreateMap<AuthenticationResponse, UserEditViewModel>()
                .ReverseMap();

            CreateMap<UserEditViewModel, UpdateRequest>()
                .ReverseMap();
            #endregion

            #region Products
            CreateMap<SavingAccountViewModel, SavingAccount>()
                .ReverseMap();

            CreateMap<CreditCardViewModel, CreditCard>()
                .ReverseMap();

            CreateMap<LoanViewModel, Loan>()
                .ReverseMap();
            #endregion

            #region Transactions
            CreateMap<TransactionSaveViewModel, Transaction>()
                .ReverseMap();
            #endregion

        }
    }
}
