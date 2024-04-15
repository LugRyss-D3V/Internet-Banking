
namespace InternetBanking.Core.Application.Dtos.Account.Request
{
    public class ResetPasswordRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
