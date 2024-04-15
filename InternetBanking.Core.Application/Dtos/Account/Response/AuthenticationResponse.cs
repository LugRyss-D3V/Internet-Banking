namespace InternetBanking.Core.Application.Dtos.Account.Response
{
    public class AuthenticationResponse : BaseResponse
    {
        public string? Id { get; set; } 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? PersonalId { get; set; }
        public List<string>? Roles { get; set; }
        public bool Status { get; set; }
    }
}
