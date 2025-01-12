﻿
namespace InternetBanking.Core.Application.Dtos.Account.Request
{
    public class UpdateRequest
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PersonalId { get; set; }
        public string? UserName { get; set; }
        public List<string>? Roles { get; set; }

    }
}
