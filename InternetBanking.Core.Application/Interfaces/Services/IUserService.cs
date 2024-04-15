
using InternetBanking.Core.Application.Dtos.Account.Request;
using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<int> ActiveUsers();
        Task<ChangeStatusResponse> ChangeStatus(ChangeStatusRequest request);
        Task<int> DeactiveUsers();
        Task<List<UserViewModel>> GetUsersAsync();
        Task<UserEditViewModel> GetUserEditAsync(string id);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(RegisterViewModel vm);
        Task SignOutAsync();
        Task<UserViewModel> GetUserByIdAsync(string id);
        Task<UpdateResponse> EditAsync(UserEditViewModel vm);
    }
}
