
using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account.Request;
using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.User;
using System.Threading;

namespace InternetBanking.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public UserService(IAccountService service, ITransactionService transactionService, IProductService productService ,IMapper mapper)
        {
            _accountService = service;
            _transactionService = transactionService;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<UserViewModel> GetUserByIdAsync(string id)
        {
            var result = await _accountService.GetUserAsync(id);
            var response = _mapper.Map<UserViewModel>(result);

            return response;
        }

        public async Task<UserEditViewModel> GetUserEditAsync(string id)
        {
            var result = await _accountService.GetUserAsync(id);
            var response = _mapper.Map<UserEditViewModel>(result);

            return response;
        }

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            var result = await _accountService.GetUsersAsync();
            var response = _mapper.Map<List<UserViewModel>>(result);

            return response;
        }
        public async Task<RegisterResponse> RegisterAsync(RegisterViewModel vm)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);

            RegisterResponse response = await _accountService.RegisterUserAsync(registerRequest);

            if(response.IsClient)
            {
                SavingAccountViewModel savm = new SavingAccountViewModel()
                {
                    UserId = response.Id,
                    Cuantity = (double)vm.Amount,
                    ProductType = "SavingAccount",
                    SavingAccountType = (int)SavingAccountType.Primary
                };

                _transactionService.CreateProduct(savm);
            }

            return response;
        }
        public async Task<UpdateResponse> EditAsync(UserEditViewModel vm)
        {
            UpdateRequest updateRequest = _mapper.Map<UpdateRequest>(vm);

            UpdateResponse response = await _accountService.UpdateUserAsync(updateRequest);

            if (response.IsClient)
            {
                _transactionService.DepositOnMainAccount(response.Id, (double)vm.Amount);
            }

            return response;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }
        public async Task<ChangeStatusResponse> ChangeStatus(ChangeStatusRequest request)
        {
            ChangeStatusResponse response = new ChangeStatusResponse();

            response = await _accountService.SwitchStatusAsync(request);

            return response;
        }

        public async Task<int> ActiveUsers()
        {
            return await _accountService.ActiveUsers();
        }

        public async Task<int> DeactiveUsers()
        {
            return await _accountService.DeactiveUsers();
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

    }
}
