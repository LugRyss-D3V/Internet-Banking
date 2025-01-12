﻿
namespace InternetBanking.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public string? Id { get; set; } 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? PersonalId { get; set; }
        public List<string>? Roles { get; set; }
        public bool Status { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
