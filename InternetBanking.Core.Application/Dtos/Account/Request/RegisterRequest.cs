
using InternetBanking.Core.Application.Enums;

namespace InternetBanking.Core.Application.Dtos.Account.Request
{
    public class RegisterRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PersonalId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public Roles TypeUser { get; set; }
    }
}
