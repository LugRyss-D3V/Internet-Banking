using InternetBanking.Core.Application.Dtos.Account.Request;
using InternetBanking.Core.Application.Dtos.Account.Response;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<int> ActiveUsers();
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<int> DeactiveUsers();
        Task<AuthenticationResponse> GetUserAsync(string id);
        Task<AuthenticationResponse> GetUserByUserNameAsync(string userName);
        Task<List<AuthenticationResponse>> GetUsersAsync();
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SignOutAsync();
        Task<ChangeStatusResponse> SwitchStatusAsync(ChangeStatusRequest request);
        Task<UpdateResponse> UpdateUserAsync(UpdateRequest request);
    }
}
